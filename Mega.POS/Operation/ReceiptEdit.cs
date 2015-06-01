using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Net.Mail;
using System.Transactions;
using System.Windows.Forms;
using log4net;
using Mega.Common;
using Mega.Common.Enum;
using Mega.Common.Helpers;
using Mega.POS.Helper;
using Microsoft.Reporting.WinForms;

namespace Mega.POS.Operation
{
    public partial class ReceiptEdit : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ReceiptEdit));

        private Common.Operation currentItem;
        private AdminDataContext dc = ApplicationHelper.GetPosDataContext();

        public Guid Id { get; set; }

        public ReceiptEdit(Guid id)
        {
            Id = id;

            InitializeComponent();
        }

        private void ReceiptEdit_Load(object sender, EventArgs e)
        {
            if (ApplicationHelper.IsOpenSimilarForm(this))
            {
                DialogHelper.ShowWarningInfo(this, "Ya existe otro formulario Similar abierto");
                Close();
                return;
            }

            try
            {
                currentItem = dc.Operations.Where(o => o.Id == Id).Single();

                LoadValuesToResumeControls();

                dataGridView1.DataSource =
                    dc.OperationDetails.Where(d => d.IdOperation == Id).
                        OrderBy(d => d.IdProduct);

                ApplicationHelper.ConfigureGridView(dataGridView1);
                dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, "Ha ocurrido un error inesperado durante el proceso de carga del formulario", ex);
                Logger.Error("Error en ReceiptEdit_Load", ex); 
                Close();
            }
        }

        private void LoadValuesToResumeControls()
        {
            ConsecutiveLabel.Text = currentItem.Consecutive;
            ShopLabel.Text = currentItem.Shop.Name;
            PedimentTextBox.Text = currentItem.Pediment;
            CurrencyLabel.Text = currentItem.OperationCurrencyCode;
            OperationAmountLabel.Text = currentItem.OperationAmount.ToString("N");
            AddedDateLabel.Text = currentItem.AddedDate.ToString("d");
            StatusLabel.Text = currentItem.StatusName;
            ModifiedDateLabel.Text = currentItem.ModifiedDate.ToString("d");

            switch (currentItem.IdStatus)
            {
                case (int)OperationStatus.NotConfirmed:
                    ConfirmOperationButton.Visible = true;
                    CancelOperationButton.Visible = false;
                    PedimentTextBox.Enabled = true;
                    countDataGridViewTextBoxColumn.ReadOnly = false;
                    LotColumn.ReadOnly = false;
                    break;
                case (int)OperationStatus.Confirmed:
                    ConfirmOperationButton.Visible = false;
                    CancelOperationButton.Location = ConfirmOperationButton.Location;
                    //No quieren cancelaciones
                    CancelOperationButton.Visible = false;
                    PedimentTextBox.Enabled = false;
                    countDataGridViewTextBoxColumn.ReadOnly = true;
                    LotColumn.ReadOnly = true;
                    break;
                default:    //OperationStatus.Canceled
                    ConfirmOperationButton.Visible = false;
                    CancelOperationButton.Visible = false;
                    PedimentTextBox.Enabled = false;
                    countDataGridViewTextBoxColumn.ReadOnly = true;
                    LotColumn.ReadOnly = true;
                    break;
            }
        }

        private void UpdateResumeOperationValues()
        {
            currentItem.ChangeRate = GetCurrentRate(currentItem);
            var opAmount = currentItem.OperationDetails.Sum(d => d.OperationAmount);

            if (DataHelper.IsActiveRoundByFive(dc, currentItem.IdOperationCurrency))
            {
                currentItem.OperationAmount = ToolHelper.RoundByFiveByExcess(opAmount);
                currentItem.Amount = ToolHelper.RoundByFiveByExcess(opAmount * currentItem.ChangeRate);
            }
            else
            {
                currentItem.OperationAmount = Math.Round(opAmount, 2);
                currentItem.Amount = Math.Round(opAmount * currentItem.ChangeRate, 2);
            }

            currentItem.Pediment = PedimentTextBox.Text;

            DataHelper.FillAuditoryValuesDesktop(currentItem);

            OperationAmountLabel.Text = currentItem.OperationAmount.ToString("N");
        }

        private decimal GetCurrentRate(Common.Operation operation)
        {
            if (operation.Shop.IdCurrency == operation.IdOperationCurrency)
            {
                return 1;
            }

            return DataHelper.GetChangeRate(dc, operation.IdOperationCurrency, operation.Shop.IdCurrency);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ConfirmOperationButton_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.InfoFormat("Iniciando la Confirmación del Recibo: Consecutive {0}",
                                  currentItem.Consecutive);

                ApplicationHelper.VerifyEnviromentConditions(this, dc);
            }
            catch (Exception ex)
            {
                const string msg = "No se puede realizar el Recibo porque no ha pasado uno de los chequeos requeridos.";

                DialogHelper.ShowError(this, msg, ex);
                Logger.Error(msg, ex);

                return;
            }

            try
            {
                ApplicationHelper.VerifyOperationModifiedDateState(currentItem);

                if (string.IsNullOrWhiteSpace(PedimentTextBox.Text))
                {
                    DialogHelper.ShowError(this, "El campo [Pedimento] es Requerido");
                    return;
                }

                var result = DialogHelper.ShowWarningQuestion(this,
                                                              "¿Está seguro que desea Confirmar y reflejar en el Inventario esta operación? Después de Confirmada no podrá modificar ninguno de sus valores, solo podrá ser Cancelada");

                if (result == DialogResult.No) return;

                Cursor = Cursors.WaitCursor;

                //using (DataHelper.BeginTransaction(dc))
                using (TransactionScope trans = new TransactionScope())
                {
                    UpdateResumeOperationValues();

                    currentItem.OfficialConsecutive = GetNextOfficialConsecutive();
                    currentItem.Status = DataHelper.GetUDCItemRow(dc, (int)OperationStatus.Confirmed);
                    DataHelper.FillAuditoryValuesDesktop(currentItem);

                    dc.SubmitChanges();

                    GenerateKardexAndInventoryEntries(false);

                    trans.Complete();
                    //DataHelper.CommitTransaction(dc);
                }

                try
                {
                    LoadValuesToResumeControls();

                    CheckDifferencesToNotify();
                }
                catch (Exception ex)
                {
                    DialogHelper.ShowWarningInfo(this, "Un error ha ocurrido durante el envio del Correo de Notificación: " + ex.Message);
                    Logger.Error("Un error ha ocurrido durante el envio del Correo de Notificación: ", ex);
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                //DataHelper.RollbackTransaction(dc);
                Cursor = Cursors.Default;
                ApplicationHelper.ResetPosDataContext();

                DialogHelper.ShowError(this, "Un Error delicado ha sido detectado durante la confirmación de la Operación. Para que la aplicación pueda recuperarse este formulario debe cerrarse. Si el error persiste cierra la aplicación y ejecútela nuevamente por favor.", ex);

                Logger.Error(string.Format("Error confirmando el Recibo de Mercancia. Consecutive {0}", currentItem.Consecutive), ex);
                
                Close();
            }
        }

        private void CheckDifferencesToNotify()
        {

            if (currentItem.OperationDetails.Where(op => op.Count != op.DocCount).Count() > 0)
            {
                const string templatePath = "MailTemplate/receipt-notification-mail.xslt";
                const string imagePath = "MailTemplate/Images/";

                var rv = new ReportViewer("Mega.POS.Report.ReceiptDetails.rdlc");

                rv.LocalReport.DataSources.Add(new ReportDataSource("Operation") { Value = new List<Common.Operation> { currentItem } });

                rv.LocalReport.SubreportProcessing += FillSubreportEventHandler;

                var attachments = new List<Attachment> { new Attachment(rv.RenderedReport, "Detalles del Recibo.pdf") };

                var htmlBody = MailTemplateHelper.RetrieveReceiptNotOkNotificationBody(templatePath, imagePath, currentItem);

                var adminMail = Properties.Settings.Default.MailAdminAddress;

                var to = string.Empty;

                if (!string.IsNullOrEmpty(currentItem.Shop.Email2))
                {
                    to = currentItem.Shop.Email2;
                }

                if (!string.IsNullOrEmpty(currentItem.Shop.Company.Email2))
                {
                    to = string.IsNullOrEmpty(to) ? currentItem.Shop.Company.Email2 : string.Format("{0},{1}", to, currentItem.Shop.Company.Email2);
                }

                MailTemplateHelper.SendMessage(
                    string.Format("Recibo con diferencias. {0}, No. {1}",
                                  currentItem.Shop.Name, currentItem.Consecutive), htmlBody,
                    to, string.Empty, adminMail, attachments);
            }

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "Count")
            {
                e.Cancel = true;
                DialogHelper.ShowError(this, "El valor de la columna [Cantidad] no es correcto");
            }
            else
            {
                DialogHelper.ShowError(this, "Un error ha sido detectado en los datos que ha editado. por lo que deben ser revisados antes de continuar.");
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "Count")
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["operationAmountColumn"].Value =
                        Math.Round(
                            Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) *
                            Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["operationPriceColumn"].Value),
                            2);

                        UpdateResumeOperationValues();
                    }
                    else if (dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "Lot" &&
                        string.IsNullOrWhiteSpace(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)))
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Constant.CFG_NOT_LOT_CODE;
                    }
                    
                    dc.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error actualizando Importe", ex);
                DialogHelper.ShowError(this, "El sistema ha detectado un Error actualizando el Importe de la fila. Por favor verifique manualmente el valor.");
            }
        }

        private void CancelOperationButton_Click(object sender, EventArgs e)
        {
            try
            {
                ApplicationHelper.VerifyOperationModifiedDateState(currentItem);

                var result = DialogHelper.ShowWarningQuestion(this,
                                                              "¿Está seguro que desea Cancelar esta operación? Después de Cancelada las cantidades reflejadas previamente en el Inventario y Kardex serán revertidas.");

                if (result == DialogResult.No) return;
                {
                    using (TransactionScope trans = new TransactionScope())
                    {
                        currentItem.OfficialConsecutive = GetNextCanceledConsecutive();
                        currentItem.Status = DataHelper.GetUDCItemRow(dc, (int) OperationStatus.Canceled);
                        DataHelper.FillAuditoryValuesDesktop(currentItem);

                        dc.SubmitChanges();

                        GenerateKardexAndInventoryEntries(true);

                        LoadValuesToResumeControls();

                        trans.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                //DataHelper.RollbackTransaction(dc);

                Logger.Error(string.Format("Error Cancelando el Recibo de Mercancia. OfficialConsecutive {0}", currentItem.OfficialConsecutive), ex);

                DialogHelper.ShowError(this, "Un Error delicado ha sido detectado durante la Cancelación de la Operación. Para que la aplicación pueda recuperarse este formulario debe cerrarse. Si el error persiste cierra la aplicación y ejecútela nuevamente por favor.", ex);
                
                ApplicationHelper.ResetPosDataContext();
                Close();
            }
        }

        private string GetNextCanceledConsecutive()
        {
            //var localDC = ApplicationHelper.GetFreePosDataContext();

            return DataHelper.GetNextSequence(dc, SequenceId.ReceiptCanceledConsecutive).ToString();
        }

        private void GenerateKardexAndInventoryEntries(bool isCanceling)
        {
            int sign = DataHelper.GetOperationSign(dc, currentItem.IdType);

            if (isCanceling)
            {
                sign *= -1;
            }

            dc.Refresh(RefreshMode.OverwriteCurrentValues, dc.Inventories);

            foreach (var op in currentItem.OperationDetails)
            {
                Inventory inv;

                try
                {
                    inv = dc.Inventories.Where(
                        i => i.IdProduct == op.IdProduct && i.IdLocation == op.IdLocation && i.Lot == op.Lot).
                        SingleOrDefault();
                }
                catch (Exception ex)
                {
                    string msg = string.Format(
                            "Existen filas de Inventario Duplicadas para el Producto: {0}, Ubicación: {1}, Lote: {2}",
                            op.IdProduct, op.IdLocation, op.Lot);

                    Logger.Error(msg);
                    throw new Exception(msg, ex);
                }

                if (inv == null)
                {
                    inv = new Inventory
                              {
                                  Id = Guid.NewGuid(),
                                  IdShop = op.Location.IdShop,
                                  IdLocation = op.IdLocation,
                                  IdProduct = op.IdProduct,
                                  Lot = op.Lot,
                                  LotAddedDate = DateTime.Now
                              };

                    inv.Count = inv.Count + (sign * op.Count);
                    inv.ModifiedDate = DateTime.Now;

                    dc.Inventories.InsertOnSubmit(inv);
                }
                else
                {
                    inv.Count = inv.Count + (sign * op.Count);
                    inv.ModifiedDate = DateTime.Now;
                }

                if (inv.Count < 0 && !op.Location.AllowNegativeSale)
                {
                    DialogHelper.ShowError(this, string.Format("La Confirmacion no se puede realizar porque generaria Inventarios negativos sobre el Producto: {0}, Almacen {1}, Cantidad a Vender: {2}, Cantidad final en Inventario: {3}", op.Product.Id, op.Location.Name, op.Count, inv.Count));

                    throw new Exception("No se pueden generar existencias negativas en el Inventario");
                }

                Kardex k = new Kardex
                {
                    Id = Guid.NewGuid(),
                    IdShop = Properties.Settings.Default.CurrentShop,
                    IdOperation = currentItem.Id,
                    IdOperationType = currentItem.IdType,
                    IdLocation = op.IdLocation,
                    IdOperationCurrency = currentItem.IdOperationCurrency,
                    OperationDate = DateTime.Now,
                    ChangeRate = currentItem.ChangeRate,
                    IdProduct = op.IdProduct,
                    IdProductType = (int)ProductType.Simple,
                    IdProductComposite = op.IdProduct,
                    IdUM = op.IdUM,
                    Lot = op.Lot,
                    //IdPriceType = null,
                    OperationPrice = op.OperationPrice,
                    OperationCount = op.Count * sign,
                    InventoryCount = inv.Count,
                    ByCancelation = isCanceling,
                };

                dc.Kardexes.InsertOnSubmit(k);

                dc.SubmitChanges();
            }
        }

        private string GetNextOfficialConsecutive()
        {
            //var localDC = ApplicationHelper.GetFreePosDataContext();

            return DataHelper.GetNextSequence(dc, SequenceId.ReceiptOfficialConsecutive).ToString();
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            try
            {
                var rv = new ReportViewer("Mega.POS.Report.ReceiptDetails.rdlc");
                rv.LocalReport.DataSources.Add(new ReportDataSource("Operation") { Value = new List<Common.Operation> { currentItem } });

                rv.LocalReport.SubreportProcessing += FillSubreportEventHandler;

                rv.Show();
            }
            catch (Exception ex)
            {
                Logger.Error("Error en ReceiptEdit.PrintButton_Click", ex);
                DialogHelper.ShowError(this, "Error inesperado durante la impresión del Reporte.", ex);
            }
        }

        void FillSubreportEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            if (e.ReportPath == "ReceiptDetailsSubReport")
            {
                e.DataSources.Add(new ReportDataSource("OperationDetails") { Value = currentItem.OperationDetails.OrderBy(o => o.IdProduct) });
            }
        }

        private void PedimentTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                currentItem.Pediment = PedimentTextBox.Text;
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, "Ocurrio el siguiente error al Guardar el Pedimento: " + ex.Message);
            }
            
        }

        private void ReceiptEdit_Activated(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-MX");
        
        }
    }
}
