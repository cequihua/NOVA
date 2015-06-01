using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using log4net;
using Mega.Common;
using Mega.Common.Enum;
using Mega.Common.Helpers;
using Mega.Common.Model;
using Mega.POS.Client;
using Mega.POS.Helper;
using Mega.POS.Properties;
using Mega.POS.Report;
using Microsoft.Reporting.WinForms;

namespace Mega.POS.Operation
{
    public partial class CreditCollect : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CreditCollect));
        protected AdminDataContext dc = ApplicationHelper.GetPosDataContext();
        protected Dim currentDim;
        private string authorizedBy;
        private DimCreditCollect currentCreditCollect;
        public IList<CreditSaleToCollect> Sales { get; set; }

        public CreditCollect()
        {
            InitializeComponent();
        }

        private void CreditCollect_Load(object sender, EventArgs e)
        {
            try
            {
                if (ApplicationHelper.IsOpenSimilarForm(this))
                {
                    DialogHelper.ShowWarningInfo(this, "Ya existe otro formulario Similar abierto");
                    Close();
                    return;
                }

                if (VerifyAuthorization())
                {
                    PrintButton.Visible = false;
                }
                else
                {
                    Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error cargando CreditCollect_Load");
                DialogHelper.ShowError(this, "Error Inesperado preparando los datos del formulario", ex);
            }
        }

        private bool VerifyAuthorization()
        {
            if (ApplicationHelper.IsCurrentUserInRole(Constant.SupervisorOrMore))
            {
                authorizedBy = ApplicationHelper.GetCurrentUser();
                return true;
            }

            var f = new AuthorizationRequired(Constant.SupervisorOrMore,
                                                                "Un usuario con Rol de [Supervisor] o [Gerente] necesita autorizar esta Operación.");

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                authorizedBy = f.AuthorizedUser;
                return true;
            }

            return false;
        }

        private bool IsValidDim()
        {
            int code;
            if (string.IsNullOrWhiteSpace(DimTextBox.Text) || !int.TryParse(DimTextBox.Text, out code))
            {
                DialogHelper.ShowError(this, "El campo [Dim] es requerido y debe ser un valor Numérico Entero");

                return false;
            }

            currentDim = GetDimItem();

            if (currentDim == null)
            {
                DialogHelper.ShowError(this, "El DIM tecleado debe existir en la Base de datos y no ha sido encontrado.");
                return false;
            }

            if (currentDim.Disabled)
            {
                DialogHelper.ShowWarningInfo(this, "Este DIM está desactivado. Consulte con la Empresa.");
                return false;
            }

            if (ApplicationHelper.GetCurrentShop().IdCountry != currentDim.IdCountry)
            {
                DialogHelper.ShowWarningInfo(this, "El DIM no corresponde con el país de la Tienda.");
                return false;
            }

            return true;
        }

        private void PopulateData()
        {
            if (IsValidDim())
            {
                Sales = dc.Operations.Where(
                          op => op.Operation_Pays.Any(opay => opay.IdType == (int)MovementType.SaleCredit
                                                        && op.IdStatus == (int)OperationStatus.Confirmed
                                                        && op.IdDim == currentDim.Id)
                          &&
                          (op.Operation_Pays.Where(opay => opay.IdType == (int)MovementType.SaleCredit
                                                        && op.IdStatus == (int)OperationStatus.Confirmed
                                                        && op.IdDim == currentDim.Id).Sum(
                              e => e.OperationAmount) >
                           dc.Dim_CreditSaleCollecteds.Where(dcc => dcc.OfficialConsecutive == op.OfficialConsecutive && dcc.IdShop == op.IdShop).
                               Sum(dcc1 => dcc1.OperationAmount)
                           || !dc.Dim_CreditSaleCollecteds.Any(dcc => dcc.OfficialConsecutive == op.OfficialConsecutive && dcc.IdShop == op.IdShop))
                    ).Select(op => new CreditSaleToCollect
                                       {
                                           IdOperation = op.Id,
                                           OfficialConsecutive = op.OfficialConsecutive,
                                           CreditAmount =
                                       op.Operation_Pays.Where(opay1 => opay1.IdType == (int)MovementType.SaleCredit).
                                       Sum(e => e.OperationAmount),
                                           Billed =
                                       (decimal?)
                                       dc.Dim_CreditSaleCollecteds.Where(dcc => dcc.OfficialConsecutive == op.OfficialConsecutive && dcc.IdShop == op.IdShop).
                                           Sum(dcc1 => dcc1.OperationAmount) ?? 0,
                                           ToBill =
                                       op.Operation_Pays.Where(opay1 => opay1.IdType == (int)MovementType.SaleCredit
                                           && op.IdStatus == (int)OperationStatus.Confirmed
                                           && op.IdDim == currentDim.Id).
                                           Sum(e => e.OperationAmount) - ((decimal?)dc.Dim_CreditSaleCollecteds.Where(
                                               dcc => dcc.OfficialConsecutive == op.OfficialConsecutive && dcc.IdShop == op.IdShop).Sum(
                                                   dcc1 => dcc1.OperationAmount) ?? 0),
                                           ModifiedDate = op.ModifiedDate
                                       }).Distinct().OrderBy(op => op.ModifiedDate).ToList();

                ConfirmCollectButton.Enabled = Sales.Count() > 0;

                DIMLabel.Text = currentDim != null ? currentDim.FullName : "";

                TotalCreditlabel.Text = Sales.Count() > 0 ? Sales.Sum(c => c.ToBill).ToString() : "0";

                CreditsdataGridView.DataSource = Sales;
            }

            SetReadOnlyForm(false);
        }

        private void FindDimButton_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new FindClient(DimTextBox.Text, true);

                Cursor = Cursors.WaitCursor;

                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    DimTextBox.Text = f.SelectedDIM;

                    var dimSize =
                        Convert.ToInt32(DataHelper.GetUDCItemRow(dc, Constant.CFG_DIM_MAX_SIZE_UDCITEM_KEY).Optional1);

                    if (DimTextBox.Text.Trim().Length != dimSize)
                    {
                        PopulateData();
                    }
                }

                Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;

                Logger.Error("Error FindDimButton_Click from CreditCollect");
                DialogHelper.ShowError(this, "Error inesparado abriendo el formulario de busqueda de Dim", ex);
            }

        }

        private Dim GetDimItem()
        {
            int dimId;
            if (int.TryParse(DimTextBox.Text.Trim(), out dimId))
            {
                return dc.Dims.Where(d => d.Id == dimId).SingleOrDefault();
            }

            return null;
        }

        private void DimTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (int)Keys.Enter)
                {
                    currentDim = GetDimItem();

                    if (currentDim == null)
                    {
                        FindDimButton.PerformClick();
                    }
                    else
                    {
                        PrintButton.Enabled = false;
                        PopulateData();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error en CreditCollect.DIMTextBox_KeyPress ");
                DialogHelper.ShowError(this, "Error realizando procesos internos", ex);
            }
        }

        private void DimTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var dimSize = Convert.ToInt32(DataHelper.GetUDCItemRow(dc, Constant.CFG_DIM_MAX_SIZE_UDCITEM_KEY).Optional1);

                if (DimTextBox.Text.Trim().Length == dimSize)
                {
                    currentDim = GetDimItem();

                    if (currentDim != null)
                    {
                        PrintButton.Enabled = false;
                        PopulateData();
                    }
                    else
                    {
                        FindDimButton.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error en DimTextBox_TextChanged");
                DialogHelper.ShowError(this, "Error inesperado validando la extensión de Número de DIM. revise la configuración de sus UDC.", ex);
            }
        }

        private List<CreditSaleToCollect> GetSelectedSales()
        {
            return CreditsdataGridView.Rows.Cast<DataGridViewRow>().Where(row => row.Cells[0].Value != null && bool.Parse(row.Cells[0].Value.ToString())).Select(row => Sales.Where(s => s.IdOperation == new Guid(row.Cells[1].Value.ToString())).Single()).ToList();
        }

        private void GenerateMoneyMovements(bool isCanceling)
        {
            foreach (var pay in dc.Operation_Pays.Where(op => op.IdOperation == currentCreditCollect.Id))
            {
                int sign = isCanceling ? pay.TypeSign * -1 : pay.TypeSign;

                var mm = new MoneyMovement
                {
                    Id = Guid.NewGuid(),
                    IdType = pay.IdType,
                    IdCashier = pay.DimCreditCollect.IdCashier,
                    IdOperationCurrency = pay.IdOperationCurrency,
                    ChangeRate = pay.ChangeRate,
                    OperationAmount =
                        pay.IdType != (int)MovementType.SaleCurrency
                            ? pay.OperationAmount * sign
                            : (pay.ReceivedAmountOperation ?? 0) * sign,
                    Amount =
                        pay.IdType != (int)MovementType.SaleCurrency
                            ? pay.Amount * sign
                            : Math.Round((pay.ReceivedAmountOperation ?? 0) * pay.ChangeRate * sign, 2),
                    IsCanceling = isCanceling,
                    AddedDate = pay.DimCreditCollect.ModifiedDate,
                    IsManual = false,
                    IdOperation = pay.IdOperation,
                    ModifiedBy = pay.DimCreditCollect.ModifiedBy
                };

                dc.MoneyMovements.InsertOnSubmit(mm);

                if (pay.IdType == (int)MovementType.SaleCurrency && pay.OperationAmount != pay.ReceivedAmountOperation)
                {
                    //Debemos agregar la devolucion en moneda principal que se le realizo al cliente pues no se le da 
                    //cambio en Divisas
                    sign *= -1;

                    var mmChange = new MoneyMovement
                    {
                        Id = Guid.NewGuid(),
                        IdType = (int)MovementType.SaleChange,
                        IdCashier = pay.DimCreditCollect.IdCashier,
                        IdOperationCurrency = pay.IdOperationCurrency,
                        ChangeRate = 1,
                        OperationAmount = (pay.ChangeAmount ?? 0) * sign,
                        Amount = (pay.ChangeAmount ?? 0) * sign,
                        IsCanceling = isCanceling,
                        AddedDate = currentCreditCollect.ModifiedDate,
                        IsManual = false,
                        IdOperation = pay.IdOperation,
                        ModifiedBy = pay.DimCreditCollect.ModifiedBy
                    };

                    dc.MoneyMovements.InsertOnSubmit(mmChange);
                }
            }
        }

        private void MySubreportEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            if (e.ReportPath == "CreditCollectSalesSubReport")
            {
                var ds2 = new ReportDataSource("Dim_CreditSaleCollecteds")
                {
                    Value = dc.Dim_CreditSaleCollecteds.Where(
                        o => o.IdDimCreditCollect == currentCreditCollect.Id)
                };
                e.DataSources.Add(ds2);
            }

            if (e.ReportPath == "OperationPay")
            {
                var ds3 = new ReportDataSource("OperationPay")
                {
                    Value =
                        dc.Operation_Pays.Where(
                        o => o.IdOperation == currentCreditCollect.Id)
                };
                e.DataSources.Add(ds3);
            }
        }

        private void PrintTicket(bool rePrint)
        {
            var report = new LocalReport
            {
                ReportEmbeddedResource = "Mega.POS.Report.CreditCollectTicket.rdlc"
            };

            report.SubreportProcessing += MySubreportEventHandler;

            var ds1 = new ReportDataSource("CreditCollect") { Value = new List<DimCreditCollect> { currentCreditCollect } };

            report.DataSources.Add(ds1);

            var additionalInfo = string.Empty;

            if (rePrint)
            {
                additionalInfo += " (Reimpresión)";
            }

            report.SetParameters(new ReportParameter("RePrintingReport", additionalInfo));
            var cashier = ApplicationHelper.GetCurrentCashier();

            var rp = new ReportPrintDocument(report, true, cashier.TicketPageSize, cashier.TicketPageMargin);

            for (int i = 0; i < currentCreditCollect.Cashier.TicketCountToPrint; i++)
            {
                rp.Print();
            }
        }

        private void SetReadOnlyForm(bool value)
        {
            CreditsdataGridView.ReadOnly = value;
            PrintButton.Visible = value;
            PrintButton.Enabled = value;
            ConfirmCollectButton.Enabled = !value;
        }

        private void ConfirmCollectButton_Click(object sender, EventArgs e)
        {
            try
            {
                ApplicationHelper.VerifyEnviromentConditions(this, dc);
            }
            catch (Exception ex)
            {
                const string msg = "No se puede realizar el Cobro del Crédito porque no ha pasado uno de los chequeos requeridos.";

                DialogHelper.ShowError(this, msg, ex);
                Logger.Error(msg, ex);

                return;
            }

            try
            {
                var selectedSales = GetSelectedSales();

                if (selectedSales.Count == 0)
                {
                    DialogHelper.ShowError(this, "Debe seleccionar al menos una Venta a Crédito.");
                }
                else
                {

                    var total = selectedSales.Sum(s => s.ToBill);

                    currentCreditCollect = new DimCreditCollect
                                               {
                                                   Id = Guid.NewGuid(),
                                                   IdCashier = new Guid(Settings.Default.CurrentCashier),
                                                   IdDim = currentDim.Id,
                                                   Consecutive = GetNextConsecutive(),
                                                   IdOperationCurrency =
                                                       ApplicationHelper.GetCurrencyByCurrentShop(),
                                                   ChangeRate = 1,
                                                   OperationAmount = total,
                                                   Amount = total,
                                                   Notes = NotesTextBox.Text,
                                                   IdStatus = (int)OperationStatus.NotConfirmed,
                                                   SupervisedBy = authorizedBy
                                               };

                    DataHelper.FillAuditoryValuesDesktop(currentCreditCollect);

                    dc.DimCreditCollects.InsertOnSubmit(currentCreditCollect);

                    GenerateCreditCollections(currentCreditCollect);

                    dc.SubmitChanges();

                    if (DoCollect(currentCreditCollect))
                    {
                        Refresh();

                        Cursor = Cursors.WaitCursor;

                        using (TransactionScope trans = new TransactionScope())
                        {
                            Logger.InfoFormat("Iniciando la Confirmación del Cobro: Consecutive {0}",
                                  currentCreditCollect.Consecutive);

                            GenerateMoneyMovements(false);

                            UpdateCurrentDimCreditAmount(total);

                            currentCreditCollect.UDCItem = DataHelper.GetUDCItemRow(dc, (int)OperationStatus.Confirmed);

                            dc.SubmitChanges();

                            trans.Complete();
                        }

                        try
                        {
                            PrintTicket(false);
                        }
                        catch (Exception ex)
                        {
                            DialogHelper.ShowError(this, "La operación se ha efectuado pero ha ocurrido error al imprimir el Comprobante.", ex);
                            Logger.Error("La operación se ha efectuado pero ha ocurrido error al imprimir el Comprobante.", ex);
                        }

                        SetReadOnlyForm(true);

                        Cursor = Cursors.Default;
                    }
                    else
                    {
                        dc.DimCreditCollects.DeleteOnSubmit(currentCreditCollect);
                        dc.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                //DataHelper.RollbackTransaction(dc);
                Cursor = Cursors.Default;

                if (currentCreditCollect != null)
                {
                    dc.DimCreditCollects.DeleteOnSubmit(currentCreditCollect);
                    dc.SubmitChanges();
                }

                ApplicationHelper.ResetPosDataContext();

                DialogHelper.ShowError(this, "Un Error inesperado ha sido detectado al intentar realizar el cobro. El formulario puede que deba cerrarse para evitar inconsistencia de datos.", ex);

                Logger.Error(
                    string.Format("Error Confirmando el Cobro de Credito. Consecutivo {0}",
                                  currentCreditCollect.Consecutive), ex);

                Close();
            }
        }

        private bool DoCollect(DimCreditCollect dimCreditCollect)
        {
            return
                new GeneralPay(dc, dimCreditCollect.Id, dimCreditCollect.OperationAmount, currentDim,
                               dimCreditCollect.IdOperationCurrency, MovementType.CreditCollectCash,
                               MovementType.CreditCollectCard, MovementType.CreditCollectCurrency).ShowDialog(this) ==
                DialogResult.OK;
        }


        private string GetNextConsecutive()
        {
            //var localDC = ApplicationHelper.GetFreePosDataContext();

            return DataHelper.GetNextSequence(dc, SequenceId.InventoryMovementConsecutive).ToString();
        }

        private void UpdateCurrentDimCreditAmount(decimal total)
        {
            currentDim.CreditAmount = currentDim.CreditAmount - total;

            dc.SubmitChanges();
        }

        private void GenerateCreditCollections(DimCreditCollect dimCreditCollect)
        {
            foreach (var sale in GetSelectedSales())
            {
                var operation = dc.Operations.Where(op => op.Id == sale.IdOperation).Single();

                var dimCreditSaleCollected = new Dim_CreditSaleCollected
                                                 {
                                                     Id = Guid.NewGuid(),
                                                     IdShop = operation.IdShop,
                                                     IdDim = currentDim.Id,
                                                     IdOperation = operation.Id,
                                                     IdDimCreditCollect = dimCreditCollect.Id,
                                                     OfficialConsecutive = operation.OfficialConsecutive,
                                                     IdCurrency = dimCreditCollect.IdOperationCurrency,
                                                     ChangeRate = dimCreditCollect.ChangeRate,
                                                     OperationAmount = sale.ToBill,
                                                     Amount = sale.ToBill,
                                                 };

                DataHelper.FillAuditoryValuesDesktop(dimCreditSaleCollected);

                dc.Dim_CreditSaleCollecteds.InsertOnSubmit(dimCreditSaleCollected);
            }
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            PrintTicket(true);
        }

        private void CreditCollect_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F11:
                    PrintButton.PerformClick();
                    break;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentCreditCollect != null && currentCreditCollect.IdStatus == (int)OperationStatus.NotConfirmed)
                {
                    foreach (var dimCreditSaleCollected in currentCreditCollect.Dim_CreditSaleCollecteds)
                    {
                        dc.Dim_CreditSaleCollecteds.DeleteOnSubmit(dimCreditSaleCollected);
                    }

                    dc.DimCreditCollects.DeleteOnSubmit(currentCreditCollect);
                    dc.SubmitChanges();
                }

                Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Error Cerrando formulario de Cobro de Créditos", ex);
                DialogHelper.ShowError(this, DataHelper.GetDefaultAddExceptionMessage(ex));
            }
        }
    }
}
