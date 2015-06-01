using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using log4net;
using Mega.Common;
using Mega.Common.Enum;
using Mega.Common.Helpers;
using Mega.POS.Client;
using Mega.POS.Helper;
using Mega.POS.Movement;
using Mega.POS.Properties;
using Mega.POS.Report;
using Microsoft.Reporting.WinForms;

namespace Mega.POS.Operation
{
    public partial class SaleEdit : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SaleEdit));

        protected AdminDataContext dc = ApplicationHelper.GetPosDataContext();
        protected Common.Operation currentItem;
        protected Product currentProduct;
        protected IEnumerable<Product_Price> prices;
        protected Product_Price currentPrice;
        protected Dim currentDim;
        private string authorizedBy;
        protected int maxExpiredDays;
        private InvoicesOperations oInvOperations;
        private bool isDeniedProduct = false;
        protected Guid id;

        protected OperationType saleType;

        protected OperationType SaleType
        {
            get { return saleType; }
            set
            {
                saleType = value;

                ConfirmOperationButton.Visible = (saleType == OperationType.Consignation ||
                                                  saleType == OperationType.ConsignationReturn);

                DoPayButton.Visible = !ConfirmOperationButton.Visible;
            }
        }

        protected bool IsSaleType
        {
            get { return SaleType == OperationType.Sale; }
        }

        public SaleEdit(OperationType saleType, Guid id)
        {
            InitializeComponent();

            this.id = id;
            SaleType = saleType;
        }

        public SaleEdit(OperationType saleType)
            : this(saleType, Guid.Empty)
        {
        }

        private void SaleEdit_Load(object sender, EventArgs e)
        {
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-MX");
         
                if (ApplicationHelper.IsOpenSimilarForm(this))
                {
                    DialogHelper.ShowWarningInfo(this, "Ya existe otro formulario Similar abierto");
                    Close();
                    return;
                }

                Text = string.Format("Punto de Venta [{0}]", ApplicationHelper.GetCurrentShop().Name);

                maxExpiredDays = Convert.ToInt32(DataHelper.GetUDCItemRow(dc, Constant.MAX_DAYS_RETURN_CONSIGNATION_UDC_KEY).Optional1);
                AlertLabel.Text = string.Format(AlertLabel.Text, maxExpiredDays);

                #region ADTECH - Activar datos de envio en base a la configuracion
                if (Settings.Default.DistribuitionData)
                    btnDataDistr.Visible = true;
                #endregion

                if (DataHelper.CashierIsClosed(dc, Settings.Default.CurrentCashier))
                {
                    DialogHelper.ShowWarningInfo(this, "Es necesario realizar una Apertura de Caja antes de comenzar a Vender");
                    Close();

                    var f = new MovementAdd(MovementType.CashierOpen) { TopMost = true };
                    f.Show();

                    return;
                }

                KeyPreview = true;

                ApplicationHelper.ConfigureGridView(dataGridView1);
                AddProductButton.Size = new Size(0, 0);

                SaleTypeRadioButton.Checked = SaleType == OperationType.Sale;
                ConsignationTypeRadioButton.Checked = SaleType == OperationType.Consignation;
                ConsignationReturnTypeRadioButton.Checked = SaleType == OperationType.ConsignationReturn;

                LoadCurrentSale();
            }
            catch (ObjectDisposedException ex)
            {
                Logger.Error("ObjectDisposedException en SaleEdit_Load", ex);
            }
            catch (Exception ex)
            {
                Logger.Error("Error en SaleEdit_Load", ex);
                DialogHelper.ShowError(this,
                                       "Ha ocurrido un error inesperado durante el proceso de carga del formulario", ex);
                Close();
            }
        }

        private void LoadCurrentSale()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            if (currentItem == null && id == Guid.Empty)
            {
                RequestNewSale();
            }
            else
            {
                if (currentItem == null)
                {
                    currentItem = dc.Operations.Where(o => o.Id == id).Single();
                }

                if (currentDim == null)
                {
                    currentDim = currentItem.Dim;
                }

                LoadAddProductControls();
                LoadResumeControls();
                LoadGridDetails();

                HiddenPanel.Visible = false;
                SaleTypeRadioButton.Enabled = false;
                ConsignationTypeRadioButton.Enabled = false;
                ConsignationReturnTypeRadioButton.Enabled = false;

                IdProductTextBox.Focus();
                ActiveControl = IdProductTextBox;
            }
        }

        private void LoadAddProductControls()
        {
            LocationComboBox.ValueMember = "Id";
            LocationComboBox.DisplayMember = "Name";
            var locations = currentItem.Shop.Locations.Where(l => !l.Disabled && l.IsSalePoint);

            if (locations.Count() == 0)
            {
                DialogHelper.ShowError(this,
                                       "No existen Ubicaciones permitidas para realizar Ventas. Debe agregar al menos una a la Tienda");
                AddProductButton.Enabled = false;
            }
            else
            {
                LocationComboBox.DataSource = locations.ToList();
            }
        }

        private void LoadResumeControls()
        {


            ConsecutiveLabel.Text = currentItem.Consecutive;
            DIMLabel.Text = currentItem.IdDim + " " + currentItem.DimName;
            IVADimLabel.Text = (currentItem.IVAAppliedToManagement ?? 0).ToString("N") + " %";
            StatusLabel.Text = currentItem.StatusName;
            ConsignationAmountLabel.Text = currentItem.Dim.ConsignableAmount.ToString("N");
            ReferenceValueLabel.Text = currentItem.Reference;

            SaleType = (OperationType)Enum.Parse(typeof(OperationType), currentItem.IdType.ToString());
            SaleTypeRadioButton.Checked = SaleType == OperationType.Sale;
            ConsignationTypeRadioButton.Checked = SaleType == OperationType.Consignation;
            ConsignationReturnTypeRadioButton.Checked = SaleType == OperationType.ConsignationReturn;
            ConsignationLabel.Visible = SaleType != OperationType.Sale;
            ConsignationAmountLabel.Visible = SaleType != OperationType.Sale;
            ReferenceLabel.Visible = SaleType != OperationType.Sale && !string.IsNullOrWhiteSpace(currentItem.Reference);
            ReferenceValueLabel.Visible = SaleType != OperationType.Sale && !string.IsNullOrWhiteSpace(currentItem.Reference);

            ConvertToSaleButton.Visible = SaleType == OperationType.ConsignationReturn &&
                                          currentItem.IdStatus == (int)OperationStatus.Confirmed;

            CreateConsignationReturnButton.Visible = SaleType == OperationType.Consignation &&
                                                     currentItem.IdStatus == (int)OperationStatus.Confirmed &&
                                                     string.IsNullOrWhiteSpace(currentItem.Reference);
            // && SqlMethods.DateDiffDay(currentItem.ModifiedDate, DateTime.Today) 
            // <= maxExpiredDays;

            AlertLabel.Visible = SaleType == OperationType.Consignation &&
                                 currentItem.IdStatus == (int)OperationStatus.Confirmed &&
                                 string.IsNullOrWhiteSpace(currentItem.Reference) &&
                                 SqlMethods.DateDiffDay(currentItem.ModifiedDate, DateTime.Today) > maxExpiredDays;

            if (SaleType == OperationType.Consignation)
            {
                ReferenceLabel.Text = "Fue Retornada en Ticket:";
            }
            else if (SaleType == OperationType.ConsignationReturn)
            {
                ReferenceLabel.Text = "Fue Consignada en Ticket:";
            }

            SetTotalValuesToControls();

            switch (currentItem.IdStatus)
            {
                case (int)OperationStatus.NotConfirmed:
                    //ConfirmOperationButton.Visible = SaleType != OperationType.ConsignationReturn;
                    CancelOperationButton.Visible = false;
                    AddProductButton.Enabled = true;
                    DeleteProductButton.Enabled = true;
                    //DoPayButton.Enabled = true;
                    IdProductTextBox.Enabled = true;
                    UMComboBox.Enabled = true;
                    LocationComboBox.Enabled = true;
                    CountTextBox.Enabled = true;
                    LotComboBox.Enabled = true;
                    PrintButton.Enabled = false;
                    DeleteSaleButton.Visible = true;
                    break;
                case (int)OperationStatus.Confirmed:
                    ConfirmOperationButton.Visible = false;
                    CancelOperationButton.Location = ConfirmOperationButton.Location;
                    CancelOperationButton.Visible = true;
                    AddProductButton.Enabled = false;
                    DeleteProductButton.Enabled = false;
                    IdProductTextBox.Enabled = false;
                    UMComboBox.Enabled = false;
                    LocationComboBox.Enabled = false;
                    CountTextBox.Enabled = false;
                    LotComboBox.Enabled = false;
                    DoPayButton.Enabled = false;
                    PrintButton.Enabled = true;
                    DeleteSaleButton.Visible = false;
                    break;
                default: //OperationStatus.Canceled
                    ConfirmOperationButton.Visible = false;
                    CancelOperationButton.Visible = false;
                    AddProductButton.Enabled = false;
                    DeleteProductButton.Enabled = false;
                    IdProductTextBox.Enabled = false;
                    UMComboBox.Enabled = false;
                    LocationComboBox.Enabled = false;
                    CountTextBox.Enabled = false;
                    LotComboBox.Enabled = false;
                    DoPayButton.Enabled = false;
                    PrintButton.Enabled = true;
                    DeleteSaleButton.Visible = false;
                    break;
            }
        }

        private void SaveChanges()
        {
            CalculateResumeOperationValuesAndRefreshControls();

            dc.SubmitChanges();
        }

        private void CalculateResumeOperationValuesAndRefreshControls()
        {
            currentItem.ChangeRate = GetCurrentRate(currentItem);
            currentItem.DiscountPercentApply = currentItem.Dim.DiscountPercent;
            currentItem.Cashier = ApplicationHelper.GetCurrentCashier();

            decimal totalPoints = IsSaleType
                                      ? currentItem.OperationDetails.Sum(
                                          d =>
                                          d.Product.ApplyDiscount
                                              ? d.Points - (d.Points * (currentItem.DiscountPercentApply ?? 0) / 100)
                                              : d.Points)
                                      : 0;
            decimal subTotalOpAmount = currentItem.OperationDetails.Sum(d => d.OperationAmount);

            decimal totalManagement = currentItem.OperationDetails.Sum(d => d.Management);
            decimal managementIVA = totalManagement * (currentItem.IVAAppliedToManagement ?? 0) / 100;

            decimal discount =
                currentItem.OperationDetails.Where(d => d.Product.ApplyDiscount).Sum(d => d.OperationAmount) *
                (currentItem.DiscountPercentApply ?? 0) / 100;

            decimal productIVA =
                currentItem.OperationDetails.Sum(
                    d =>
                    d.Product.ApplyDiscount
                        ? (d.OperationAmount - (d.OperationAmount * currentItem.Dim.DiscountPercent / 100)) *
                          (d.ProductIVAApplied ?? 0) / 100
                        : d.OperationAmount * (d.ProductIVAApplied ?? 0) / 100);

            decimal totalOpAmount = subTotalOpAmount + totalManagement - discount + managementIVA + productIVA;

            currentItem.TotalPointOperation = totalPoints;
            currentItem.SubTotalOperationAmount = subTotalOpAmount;
            currentItem.TotalManagementOperation = totalManagement;
            currentItem.TotalIVAOperation = managementIVA + productIVA;
            currentItem.TotalOperationDiscount = Math.Round(discount, 2);

            if (DataHelper.IsActiveRoundByFive(dc, currentItem.IdOperationCurrency))
            {
                currentItem.OperationAmount = ToolHelper.RoundByFiveByExcess(totalOpAmount);
                currentItem.Amount = ToolHelper.RoundByFiveByExcess(totalOpAmount * currentItem.ChangeRate);
            }
            else
            {
                currentItem.OperationAmount = Math.Round(totalOpAmount, 2);
                currentItem.Amount = Math.Round(totalOpAmount * currentItem.ChangeRate, 2);
            }

            DataHelper.FillAuditoryValuesDesktop(currentItem);

            SetTotalValuesToControls();

            dc.SubmitChanges();
        }

        private void SetTotalValuesToControls()
        {
            TotalPointsLabel.Text = (currentItem.TotalPointOperation ?? 0).ToString("N");
            SubTotalLabel.Text = (currentItem.SubTotalOperationAmount ?? 0).ToString("N");
            TotalManagementLabel.Text = (currentItem.TotalManagementOperation ?? 0).ToString("N");
            TotalDiscountLabel.Text = (currentItem.TotalOperationDiscount ?? 0).ToString("N");
            TotalIVALabel.Text = (currentItem.TotalIVAOperation ?? 0).ToString("N");
            TotalOperationAmountLabel.Text = currentItem.OperationAmount.ToString("N");
            DoPayButton.Enabled = currentItem.IdStatus == (int)OperationStatus.NotConfirmed &&
                                  currentItem.OperationAmount != 0;
            ConfirmOperationButton.Enabled = currentItem.IdStatus == (int)OperationStatus.NotConfirmed &&
                                  currentItem.OperationAmount != 0;
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
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Error Cerrarndo formulario de Ventas", ex);
                DialogHelper.ShowError(this, DataHelper.GetDefaultAddExceptionMessage(ex));
            }
        }

        private void ConfirmOperationButton_Click(object sender, EventArgs e)
        {
            var result = DialogHelper.ShowWarningQuestion(this,
                                                          "¿Está seguro que desea Confirmar y reflejar en el Inventario esta operación? Después de Confirmada no podrá modificar ninguno de sus valores, solo podrá ser Cancelada");

            if (result == DialogResult.No) return;

            try
            {
                Logger.Info(
                    string.Format("Comenzando la Confirmación de la Consignación o Retorno. Consecutivo {0} Tipo {1}",
                                  currentItem.Consecutive, currentItem.TypeName));

                ApplicationHelper.VerifyEnviromentConditions(this, dc);
            }
            catch (Exception ex)
            {
                const string msg = "No se puede Confirmar la Operación porque no ha pasado uno de los chequeos requeridos.";

                DialogHelper.ShowError(this, msg, ex);
                Logger.Error(msg, ex);

                return;
            }

            try
            {
                ApplicationHelper.VerifyOperationModifiedDateState(currentItem);

                using (TransactionScope trans = new TransactionScope())
                {
                    ConfirmSale();

                    trans.Complete();
                }

                try
                {
                    LoadResumeControls();
                }
                catch (Exception)
                {
                    DialogHelper.ShowWarningInfo(this, "La acción concluyó correctamente pero los datos no se pudieron refrescar. Le sugerimos cerrar el formulario y volver a abrirlo");
                }
            }
            catch (Exception ex)
            {
                //DataHelper.RollbackTransaction(dc);
                Cursor = Cursors.Default;

                const string msg =
                    @"Un Excepción ha ocurrido durante la confirmación de la Operación. 
Para que la aplicación pueda recuperarse este formulario quizás deba cerrarse. 
Si es producto de una Venta Negativa elimine los renglones problematicos e insertelos nuevamente con valores correctos, observando la información de ayuda  
que le ofrece el sistema. Si el error persiste cierre la aplicación y ejecútela nuevamente por favor.";

                DialogHelper.ShowError(this, msg, ex);

                Logger.Error(
                    string.Format("Error Confirmando la Operación. Consecutivo {0} OfficialConsecutive {1} Tipo {2}",
                                  currentItem.Consecutive, currentItem.OfficialConsecutive, currentItem.TypeName), ex);

                ApplicationHelper.ResetPosDataContext();
                Close();
            }
        }

        private void ConfirmSale()
        {
            SaveChanges();

            dc.Refresh(RefreshMode.OverwriteCurrentValues, currentItem.Dim);

            if (SaleType == OperationType.Consignation &&
                currentItem.Dim.MaxConsignableAmount <
                (currentItem.Dim.ConsignableAmount + currentItem.SubTotalOperationAmount))
            {
                DialogHelper.ShowError(this,
                                       string.Format(
                                           "El Valor máximo en Consignación para este Cliente se sobrepasaría por lo que no es posible Confirmar la Operación. Máximo permitido: {0}, Actual: {1}, En la Operación: {2}",
                                           currentItem.Dim.MaxConsignableAmount,
                                           currentItem.Dim.ConsignableAmount,
                                           currentItem.SubTotalOperationAmount));

                return;
            }

            Cursor = Cursors.WaitCursor;

            currentItem.OfficialConsecutive = GetNextOfficialConsecutive((int)SaleType);
            currentItem.Status = DataHelper.GetUDCItemRow(dc, (int)OperationStatus.Confirmed);
            DataHelper.FillAuditoryValuesDesktop(currentItem);

            dc.SubmitChanges();

            GenerateKardexAndInventoryEntries(false);

            if (saleType == OperationType.Sale)
            {
                GenerateMoneyMovements(false);
            }

            try
            {
                PrintTicket(false);
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, "Un error inesperado ocurrio durante la Impresión del Ticket");
                Logger.Error("Error imprimiendo Ticket con numero: " + currentItem.OfficialConsecutive, ex);
            }
            
            Cursor = Cursors.Default;

            DimTextBox.Focus();
        }

        private void PrintTicket(bool rePrint)
        {
            //dc.Log = Console.Out;

            LocalReport saleReport = null;
            if (Settings.Default.AmericaCenterCountry.Trim() != string.Empty)
            {
                saleReport = new LocalReport
                {
                    ReportEmbeddedResource = "Mega.POS.Report.SaleTicketCA.rdlc"
                };
            }
            else
            {
                saleReport = new LocalReport
                {
                    ReportEmbeddedResource = "Mega.POS.Report.SaleTicket.rdlc"
                };
            }

            saleReport.SubreportProcessing += MySubreportEventHandler;

            var ds1 = new ReportDataSource("Sale")
                          {
                              Value =
                                  dc.Operations.Where(o => o.Id == currentItem.Id).
                                  Select(o => o)
                          };

            saleReport.DataSources.Add(ds1);

            string additionalInfo = string.Empty;

            if (currentItem.IdStatus == (int)OperationStatus.Canceled)
            {
                additionalInfo = "(Cancelación)";
            }

            if (rePrint)
            {
                additionalInfo += " (Reimpresión)";
            }

            saleReport.SetParameters(new ReportParameter("RePrintingReport", additionalInfo));
            saleReport.SetParameters(new ReportParameter("InvoiceName",
                    DataHelper.GetUDCItemRow(dc, Constant.CFG_INVOICE_NAME_IN_TICKET_UDCITEM_KEY).Optional1));


            var cashier = ApplicationHelper.GetCurrentCashier();

            var rp = new ReportPrintDocument(saleReport, true, cashier.TicketPageSize,
                                                             cashier.TicketPageMargin);

            for (int i = 0; i < currentItem.Cashier.TicketCountToPrint; i++)
            {
                rp.Print();
            }

            dc.Log = null;
        }

        private void MySubreportEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            if (e.ReportPath == "SaleDetailSubReport")
            {
                var ds2 = new ReportDataSource("SaleDetail")
                              {
                                  Value =
                                      dc.OperationDetails.Where(
                                          o => o.Operation.Id == currentItem.Id).OrderByDescending(o => o.AddedDate)
                              };
                e.DataSources.Add(ds2);
            }

            if (currentItem.IdType == (int)OperationType.Sale)
            {
                if (e.ReportPath == "OperationPay")
                {
                    var ds3 = new ReportDataSource("OperationPay")
                                  {
                                      Value =
                                          dc.Operation_Pays.Where(
                                              o => o.Operation.Id == currentItem.Id).Select(o => o)
                                  };
                    e.DataSources.Add(ds3);
                }
                else if (e.ReportPath == "SalePayNotes")
                {
                    var ds4 = new ReportDataSource("SalePayNotes")
                                               {
                                                   Value =
                                                       dc.Operation_Pays.Where(
                                                           o => o.Operation.Id == currentItem.Id).
                                                       Select(o => o.UDCItem1).GroupBy(u => u.Id).Select(g => g.First())
                                               };

                    e.DataSources.Add(ds4);
                }
            }
        }

        private void CancelOperationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentItem.ModifiedDate.Date != DateTime.Today)
                {
                    DialogHelper.ShowWarningInfo(this, "No puede Cancelar una Operación de otro día.");
                    return;
                }

                if (SaleType == OperationType.Consignation && !string.IsNullOrWhiteSpace(currentItem.Reference))
                {
                    DialogHelper.ShowWarningInfo(this, "No puede Cancelar una Consignación que ha sido Retornada. Primero debe cancelar el Retorno Referenciado.");
                    return;
                }

                if (ApplicationHelper.IsCurrentUserInRole(Constant.SupervisorOrMore))
                {
                    authorizedBy = ApplicationHelper.GetCurrentUser();

                    var result = DialogHelper.ShowWarningQuestion(this,
                                                                  "¿Está seguro que desea Cancelar esta operación? Después de Cancelada las cantidades reflejadas previamente en el Inventario y Kardex serán revertidas.");
                    if (result == DialogResult.No) return;
                }
                else
                {
                    var f = new AuthorizationRequired(Constant.SupervisorOrMore,
                                                                        "Un usuario con Rol de [Supervisor] o [Gerente] necesita autorizar esta Operación.");

                    if (f.ShowDialog(this) == DialogResult.OK)
                    {
                        authorizedBy = f.AuthorizedUser;
                    }
                    else
                    {
                        return;
                    }
                }

                try
                {
                    Logger.Info(string.Format("Comenzando la Cancelacion del Ticket. Consecutivo {0} OfficialConsecutivo {1} Tipo {2}",
                                  currentItem.Consecutive, currentItem.OfficialConsecutive, currentItem.TypeName));

                    ApplicationHelper.VerifyEnviromentConditions(this, dc);
                }
                catch (Exception ex)
                {
                    const string msg = "No se puede realizar la acción porque no ha pasado uno de los chequeos requeridos.";

                    DialogHelper.ShowError(this, msg, ex);
                    Logger.Error(msg, ex);

                    return;
                }

                Cursor = Cursors.WaitCursor;
                ApplicationHelper.VerifyOperationModifiedDateState(currentItem);

                using (TransactionScope trans = new TransactionScope())
                {
                    currentItem.CancelConsecutive = GetNextCanceledConsecutive((int)SaleType);
                    currentItem.Status = DataHelper.GetUDCItemRow(dc, (int)OperationStatus.Canceled);
                    DataHelper.FillAuditoryValuesDesktop(currentItem);
                    currentItem.User = dc.Users.Where(u => u.Id == authorizedBy).Single();

                    dc.SubmitChanges();
                    dc.Refresh(RefreshMode.OverwriteCurrentValues, currentItem.Dim);

                    GenerateKardexAndInventoryEntries(true);

                    if (currentItem.IdType == (int)OperationType.Sale)
                    {
                        GenerateMoneyMovements(true);
                    }

                    //Si es Retorno de Consignacion buscar la Operacion con OfficalNumber = Reference y asignarle Null a dicha Referencia.
                    if (SaleType == OperationType.ConsignationReturn)
                    {
                        var consignationAssigned =
                            dc.Operations.Where(o => o.IdType == (int)OperationType.Consignation &&
                                                     o.IdStatus == (int)OperationStatus.Confirmed &&
                                                     o.OfficialConsecutive == currentItem.Reference).Single();

                        consignationAssigned.Reference = null;
                        dc.SubmitChanges();
                    }

                    trans.Complete();
                }

                try
                {
                    LoadResumeControls();
                }
                catch (Exception)
                {
                    DialogHelper.ShowWarningInfo(this, "La acción concluyó correctamente pero los datos no se pudieron refrescar. Le sugerimos cerrar el formulario y volver a abrirlo");
                }

                Cursor = Cursors.Default;

                //DialogHelper.ShowInformation(this, "Cancelación exitosa. Las cantidades han sido reflejadas en el Inventario y el Kardex.");

                try
                {
                    PrintTicket(false);
                }
                catch (Exception ex)
                {
                    DialogHelper.ShowError(this, "Un error inesperado ocurrio durante la Impresión del Ticket");
                    Logger.Error("Error imprimiendo Ticket con numero: " + currentItem.OfficialConsecutive, ex);
                }
            }
            catch (Exception ex)
            {
                //DataHelper.RollbackTransaction(dc);
                Cursor = Cursors.Default;

                DialogHelper.ShowError(this,
                                       "Un Error ha sido detectado durante la Cancelación de la Operación. Para que la aplicación pueda recuperarse este formulario quizás deba cerrarse. Si el error persiste cierra la aplicación y ejecútela nuevamente por favor.");

                Logger.Error(string.Format("Error Cancelando la Operación. Consecutivo {0} OfficialConsecutive {1} Tipo {2}",
                                  currentItem.Consecutive, currentItem.OfficialConsecutive, currentItem.TypeName), ex);

                ApplicationHelper.ResetPosDataContext();
                Close();
            }
        }

        private void GenerateMoneyMovements(bool isCanceling)
        {
            foreach (var pay in currentItem.Operation_Pays)
            {
                int sign = isCanceling ? pay.TypeSign * -1 : pay.TypeSign;

                var mm = new MoneyMovement
                             {
                                 Id = Guid.NewGuid(),
                                 IdType = pay.IdType,
                                 IdCashier = new Guid(Settings.Default.CurrentCashier),
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
                                 AddedDate = currentItem.ModifiedDate,
                                 IsManual = false,
                                 IdOperation = currentItem.Id,
                                 ModifiedBy = currentItem.ModifiedBy
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
                                           IdCashier = currentItem.IdCashier ?? Guid.Empty,
                                           IdOperationCurrency = currentItem.IdOperationCurrency,
                                           ChangeRate = 1,
                                           OperationAmount = (pay.ChangeAmount ?? 0) * sign,
                                           Amount = (pay.ChangeAmount ?? 0) * sign,
                                           IsCanceling = isCanceling,
                                           AddedDate = currentItem.ModifiedDate,
                                           IsManual = false,
                                           IdOperation = currentItem.Id,
                                           ModifiedBy = currentItem.ModifiedBy
                                       };

                    dc.MoneyMovements.InsertOnSubmit(mmChange);
                }

                dc.SubmitChanges();
            }
        }

        private SequenceId GetSequenceIDForCanceledConsecutive(int operationType)
        {
            switch (operationType)
            {
                case (int)OperationType.Sale:
                    return SequenceId.SaleCanceledConsecutive;
                case (int)OperationType.Consignation:
                    return SequenceId.ConsignationCanceledConsecutive;
                default: //(int)OperationType.ConsignationReturn:
                    return SequenceId.ConsignationReturnCanceledConsecutive;
            }
        }

        private string GetNextCanceledConsecutive(int operationType)
        {
            //var localDC = ApplicationHelper.GetFreePosDataContext();

            return DataHelper.GetNextSequence(dc, GetSequenceIDForCanceledConsecutive(operationType)).ToString();
        }

        private void GenerateKardexAndInventoryEntries(bool isCanceling)
        {
            int sign = DataHelper.GetOperationSign(dc, currentItem.IdType);
                if (isCanceling){
                    sign *= -1;
                }
            dc.Refresh(RefreshMode.OverwriteCurrentValues, dc.Inventories);
//            string ids = "";
                foreach (var op in currentItem.OperationDetails){
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
                        if (op.IdProductType != (int)ProductType.Composite)
                        {
                            if (inv == null)
                            {
                                inv = new Inventory
                                          {
                                              Id = Guid.NewGuid(),
                                              IdShop = op.Location.IdShop,
                                              IdLocation = op.IdLocation,
                                              IdProduct = op.IdProduct,
                                              Lot = op.Lot,
                                              LotAddedDate = DateTime.Now,
                                              Count = sign * op.Count,
                                              ModifiedDate = DateTime.Now
                                          };
                                dc.Inventories.InsertOnSubmit(inv);
                            }
                            else
                            {
                                inv.Count = inv.Count + (sign * op.Count);
                                inv.ModifiedDate = DateTime.Now;
                            }
                            if (inv.Count < 0 && !op.Location.AllowNegativeSale)
                            {
                                string msg = string.Format(
                                    "La Confirmacion no se puede realizar porque generaria Inventarios negativos sobre el Producto: {0}, Almacen {1}, Cantidad a Vender: {2}, Cantidad final en Inventario: {3}",
                                    op.Product.Id, op.Location.Name, op.Count, inv.Count);
                                DialogHelper.ShowError(this, msg);
                                throw new Exception(msg);
                            }
                        }
                    var k = new Kardex
                                   {
                                       Id = Guid.NewGuid(),
                                       IdShop = Settings.Default.CurrentShop,
                                       IdOperation = currentItem.Id,
                                       IdOperationType = currentItem.IdType,
                                       IdLocation = op.IdLocation,
                                       IdOperationCurrency = currentItem.IdOperationCurrency,
                                       OperationDate = DateTime.Now,
                                       ChangeRate = currentItem.ChangeRate,
                                       IdProduct = op.IdProduct,
                                       IdProductType = op.IdProductType,
                                       IdProductComposite = op.IdProductParent,
                                       IdUM = op.IdUM,
                                       Lot = op.Lot,
                                       IdPriceType = op.IdPriceType,
                                       OperationPrice = op.OperationPrice,
                                       OperationCount = op.Count * sign,
                                       InventoryCount = op.IdProductType != (int)ProductType.Composite ? inv.Count : 0,
                                       ByCancelation = isCanceling,
                                   };
                    dc.Kardexes.InsertOnSubmit(k);
                        if (!IsSaleType)
                        {
                            Inventory invDim = null;
                                if (op.IdProductType != (int)ProductType.Composite)
                                {
                                    try
                                    {
/*                                        if ( ids.Equals("") ){
                                            invDim = dc.Inventories.Where(
                                                i => i.IdProduct == op.IdProduct && i.IdDim == op.Operation.IdDim && i.Lot == op.Lot && i.Count == op.Count)
                                                .FirstOrDefault();
//                                            .SingleOrDefault();
                                        }else{
*/ 
                                            var qry = dc.Inventories.Where(i => i.IdProduct == op.IdProduct && i.IdDim == op.Operation.IdDim && i.Lot == op.Lot && i.Count == op.Count).AsQueryable();
                                            invDim = null;
                                            foreach (Inventory i in qry)
                                            {
                                                if (i.Count == op.Count)
                                                {
                                                    invDim = i;
                                                    break;
                                                }
                                            }
/*                                            String[] ColeccionIds = ids.Split(',');
                                            foreach (String CadId in ColeccionIds)
                                            {
                                                qry = qry.Where(i => i.Id.ToString() != CadId );
                                            }
*/
                                            
//                                            invDim = qry.FirstOrDefault();
/*                                            invDim = dc.Inventories.Where(
                                                i => i.IdProduct == op.IdProduct && i.IdDim == op.Operation.IdDim && i.Lot == op.Lot && i.Count == op.Count && !i.Id.ToString().Contains(ids))
                                                .FirstOrDefault(); 
*/ 
//                                        }
//                                        if ( invDim != null )
//                                            ids += ids.Equals("") ? invDim.Id.ToString() : "," + invDim.Id.ToString();     
                                    }
                                    catch (Exception ex)
                                    {
                                        string msg =
                                            string.Format(
                                                "Existen filas de Inventario Duplicadas para el Producto: {0}, Ubicación DIM: {1}, Lote: {2}",
                                                op.IdProduct, op.Operation.IdDim, op.Lot);
                                        Logger.Error(msg);
                                        throw new Exception(msg, ex);
                                    }
                                    if (invDim == null)
                                    {
                                        invDim = new Inventory
                                                     {
                                                         Id = Guid.NewGuid(),
                                                         IdShop = op.Location.IdShop,
                                                         IdDim = op.Operation.IdDim,
                                                         IdProduct = op.IdProduct,
                                                         Lot = op.Lot,
                                                         LotAddedDate = DateTime.Now,
                                                         Count = sign * -1 * op.Count,
                                                         ModifiedDate = DateTime.Now
                                                     };
                                        dc.Inventories.InsertOnSubmit(invDim);
                                    }
                                    else
                                    {
                                        invDim.Count += (sign * -1 * op.Count);
                                        invDim.ModifiedDate = DateTime.Now;
                                    }
                                    if (invDim.Count < 0 && !op.Location.AllowNegativeSale)
                                    {
                                        string msg = string.Format(
                                            "La Operación no se puede realizar porque generaria Inventarios negativos sobre el Producto: {0}, Dim {1}, Cantidad en Operación: {2}, Cantidad final en Dim: {3}",
                                            op.Product.Id, op.Operation.DimName, op.Count, invDim.Count);
                                        DialogHelper.ShowError(this, msg);
                                        throw new Exception(msg);
                                    }
                                }
                            var k1 = new Kardex
                                            {
                                                Id = Guid.NewGuid(),
                                                IdShop = Settings.Default.CurrentShop,
                                                IdOperation = currentItem.Id,
                                                IdOperationType = currentItem.IdType,
                                                IdDim = currentItem.IdDim,
                                                IdOperationCurrency = currentItem.IdOperationCurrency,
                                                OperationDate = DateTime.Now,
                                                ChangeRate = currentItem.ChangeRate,
                                                IdProduct = op.IdProduct,
                                                IdProductType = op.IdProductType,
                                                IdProductComposite = op.IdProductParent,
                                                IdUM = op.IdUM,
                                                Lot = op.Lot,
                                                IdPriceType = op.IdPriceType,
                                                OperationPrice = op.OperationPrice,
                                                OperationCount = op.Count * sign * -1,
                                                InventoryCount = op.IdProductType != (int)ProductType.Composite ? invDim.Count : 0,
                                                ByCancelation = isCanceling,
                                            };
                            dc.Kardexes.InsertOnSubmit(k1);
                        }
                }
                if (IsSaleType)
                {
                    var dim = currentItem.Dim;
                    #region CaRLoS
                    // Ya no se va a actualizar el PointAmount y PointAmountDate de la tabla de DIM
                    dim.PointAmount -= (currentItem.TotalPointOperation ?? 0) * sign;
                    dim.PointAmountDate = DateTime.Now;
                    #endregion
                    dim.CreditAmount -=
                        currentItem.Operation_Pays.Where(p => p.IdType == (int)MovementType.SaleCredit).Sum(p => p.Amount) *
                        sign;
                    dim.CreditAmountDate = DateTime.Now;
                }
                else
                {
                    var dim = currentItem.Dim;
                    dim.ConsignableAmount -= currentItem.OperationAmount * sign;
                    dim.ConsignableAmountDate = DateTime.Now;
                }
            dc.SubmitChanges();
        }

        private SequenceId GetSequenceIDForOfficialConsecutive(int operationType)
        {
            switch (operationType)
            {
                case (int)OperationType.Sale:
                    return SequenceId.SaleOfficialConsecutive;
                case (int)OperationType.Consignation:
                    return SequenceId.ConsignationOfficialConsecutive;
                default: //(int)OperationType.ConsignationReturn:
                    return SequenceId.ConsignationReturnOfficialConsecutive;
            }
        }

        private string GetNextOfficialConsecutive(int operationType)
        {
            //var localDC = ApplicationHelper.GetFreePosDataContext();

            return DataHelper.GetNextSequence(dc, GetSequenceIDForOfficialConsecutive(operationType)).ToString();
        }

        private void AddProductButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool BandraSustitutos = true;
                ApplicationHelper.VerifyOperationModifiedDateState(currentItem);

                if (IsValidData())
                {
                    var rowDetail = CreatePrincipalProductDetailItem();

                    if (currentProduct.IdType == (int)ProductType.Composite)
                    {
                        var childs = CreateChildProducts(rowDetail.Id);
                        
                        foreach (var child in childs)
                        {
                           

                            var inv = dc.Inventories.Where(i => i.IdLocation != null &&
                                                            i.IdLocation.ToString() ==
                                                            Convert.ToString(LocationComboBox.SelectedValue) &&
                                                            i.IdProduct ==  child.IdProduct &&
                                                            i.Lot == LotComboBox.SelectedValue.ToString()).
                            SingleOrDefault();


                               //  if (inv == null || count > inv.Count)
                            if (inv == null || child.Count  > inv.Count)
                            {
                              
                                BandraSustitutos = false;
                            }
                           
                        }


                        if (BandraSustitutos == true)
                        {
                            foreach (var child in childs)
                            {
                                currentItem.OperationDetails.Insert(0, child);
                            }

                            //Muy importante para el orden de impresion y presentacion
                            rowDetail.AddedDate = DateTime.Now;
                            currentItem.OperationDetails.Insert(0, rowDetail);
                        }
                        else
                        { 
                          DialogHelper.ShowWarningInfo(this,
                                                             "La cantidad que  integra la promoción sobrepasará la existencia actual para la Ubicación y Lote seleccionado");
                          #region ADTECH - Insertar a tabla DeniedProducts

                          DeniedProducts dprod = new DeniedProducts();
                          dprod.Id = Guid.NewGuid();
                          dprod.IdDim = currentDim.Id;
                          dprod.IdProduct = currentProduct.Id;
                          dprod.IdShop = currentItem.IdShop;
                          dprod.AddedDate = DateTime.Now;
                          dprod.Count = Convert.ToDouble(CountTextBox.Text);

                          dc.DeniedProducts.InsertOnSubmit(dprod);
                          //dc.Insert_DeniedProducts(Guid.NewGuid(), currentProduct.Id, DateTime.Now, currentItem.IdShop, currentDim.Id, null, Convert.ToDouble(CountTextBox.Text), null);
                          //dc.SubmitChanges();
                          #endregion
                        
                        }

                     
                    }
                    else
                    {
                        #region CaRLoS
                        // -Codigo original
                         currentItem.OperationDetails.Insert(0, rowDetail);
                        //---------------------------------------------------
/*                        int indexExiste = -1;
                        for (int i = 0; i < currentItem.OperationDetails.Count; i++)
                            if (String.Compare(currentItem.OperationDetails[i].IdProduct, rowDetail.IdProduct) == 0 )
                            {
                                indexExiste = i;
                                break;
                            }
                        if (indexExiste == -1)
                        {
                            currentItem.OperationDetails.Insert(0, rowDetail);
                        }
                        else
                        {
                            double nuevaCantidad = currentItem.OperationDetails[indexExiste].Count + rowDetail.Count;
                            currentItem.OperationDetails[indexExiste].Count = nuevaCantidad;
                            currentItem.OperationDetails[indexExiste].OperationAmount = Math.Round(currentPrice.Price * (decimal)nuevaCantidad, 2);
                            currentItem.OperationDetails[indexExiste].Points = Math.Round(currentPrice.Points * (decimal)nuevaCantidad, 2);
                            currentItem.OperationDetails[indexExiste].Management = Math.Round(currentPrice.Management * (decimal)nuevaCantidad, 2);
                        }
*/ 
                        #endregion
                    }

                    dc.SubmitChanges();
                    RefreshDataAndSave();
                    ClearEntryControls();
                    IdProductTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error agregando producto a la venta", ex);
                DialogHelper.ShowError(this,
                                       "Un Error ha ocurrido al agregar el producto a la Venta. Si el error persiste cierre el formulario y ábralo nuevamente.",
                                       ex);
            }
        }

        private OperationDetail CreatePrincipalProductDetailItem()
        {
            double count = Convert.ToDouble(CountTextBox.Text);

            string ivaGroupCode = DataHelper.GetUDCItemRow(dc, currentDim.IdIVAGroup == Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY
                                     ? currentItem.Shop.IdIVAGroup
                                     : currentDim.IdIVAGroup).Code;
            var ivaValueItem = DataHelper.GetIVAValueItem(dc, currentProduct.IVAType.Code, ivaGroupCode);

            Guid guidDetail = Guid.NewGuid();
            var det = new OperationDetail
                          {
                              Id = guidDetail,
                              IdOperation = currentItem.Id,
                              IdLocation =
                                  new Guid(Convert.ToString(LocationComboBox.SelectedValue)),
                              IdProductType = currentProduct.IdType,
                              IdProductParent = currentProduct.Id,
                              IdProduct = currentProduct.Id,
                              ParentRow = guidDetail,
                              IdUM = currentPrice.IdUM,
                              Count = count,
                              Lot =
                                  currentProduct.IdType == (int)ProductType.Simple
                                      ? LotComboBox.SelectedValue.ToString()
                                      : string.Empty,
                              IdPriceType = currentPrice.IdPriceType,
                              OperationPrice = currentPrice.Price,
                              OperationAmount = Math.Round(currentPrice.Price * (decimal)count, 2),
                              ProductIVACodeApplied = ivaValueItem.Code,
                              ProductIVAApplied = Convert.ToDecimal(ivaValueItem.Optional3),
                              Points = Math.Round(currentPrice.Points * (decimal)count, 2),
                              Management = Math.Round(currentPrice.Management * (decimal)count, 2),
                              Pediment = dc.OperationDetails.Where(
                                  o =>
                                  o.IdProduct == currentProduct.Id &&
                                  o.Lot == LotComboBox.SelectedValue.ToString() &&
                                  o.Operation.IdType == (int)OperationType.Receipt).Select(
                                      o => o.Operation.Pediment).FirstOrDefault(),
                              AddedDate = DateTime.Now
                          };
            #region ADTECH - Insertar a tabla DeniedProducts

            if (isDeniedProduct)
            {
                DeniedProducts dprod = new DeniedProducts();
                dprod.Id = Guid.NewGuid();
                dprod.IdDim = currentDim.Id;
                dprod.IdProduct = currentProduct.Id;
                dprod.IdShop = currentItem.IdShop;
                dprod.AddedDate = DateTime.Now;
                dprod.Count = det.Count;

                dc.DeniedProducts.InsertOnSubmit(dprod);
                isDeniedProduct = false;
            }

            #endregion

            return det;
        }

        private IEnumerable<OperationDetail> CreateChildProducts(Guid parentRow)
        {
            IList<OperationDetail> list = new List<OperationDetail>();

            var childs = currentProduct.ProductCompositions;

            foreach (var child in childs)
            {
                double count = Convert.ToDouble(CountTextBox.Text) * child.Count;
                string lot = GetFirstLot(child.IdProductSimple);

                if (lot == string.Empty)
                {
                    DialogHelper.ShowWarningInfo(this,
                                                 string.Format(
                                                     "El producto con Id: {0}, parte integrante de este Producto Compuesto, no tiene existencias en Invnetario",
                                                     child.IdProductSimple));
                    lot = Constant.CFG_NOT_LOT_CODE;
                    #region ADTECH - Insertar a tabla DeniedProducts

                    DeniedProducts dprod = new DeniedProducts();
                    dprod.Id = Guid.NewGuid();
                    dprod.IdDim = currentDim.Id;
                    dprod.IdProduct = child.IdProductSimple;
                    dprod.IdShop = currentItem.IdShop;
                    dprod.AddedDate = DateTime.Now;
                    dprod.Count = count;
                    dprod.IdProductParent = currentProduct.Id;

                    dc.DeniedProducts.InsertOnSubmit(dprod);
                    //dc.Insert_DeniedProducts(Guid.NewGuid(),child.IdProductSimple, DateTime.Now, currentItem.IdShop, currentDim.Id, null, det.Count, currentProduct.Id);
                    //dc.SubmitChanges();
                    #endregion
                }

                var det = new OperationDetail
                                          {
                                              Id = Guid.NewGuid(),
                                              IdOperation = currentItem.Id,
                                              IdLocation =
                                                  new Guid(Convert.ToString(LocationComboBox.SelectedValue)),
                                              IdProduct = child.IdProductSimple,
                                              IdProductType = (int)ProductType.CompositeChild,
                                              IdProductParent = currentProduct.Id,
                                              ParentRow = parentRow,
                                              IdUM = child.IdUM,
                                              Count = count,
                                              Lot = lot,
                                              IdPriceType = currentPrice.IdPriceType,
                                              OperationPrice = child.Price,
                                              OperationAmount = Math.Round((decimal)count * child.Price, 2),
                                              Points = 0,
                                              Management = 0,
                                              Pediment =
                                                  dc.OperationDetails.Where(
                                                      o =>
                                                      o.IdProduct == child.IdProductSimple &&
                                                      o.Lot == lot &&
                                                      o.Operation.IdType == (int)OperationType.Receipt).Select(
                                                          o => o.Operation.Pediment).FirstOrDefault(),
                                              AddedDate = DateTime.Now
                                          };

                list.Add(det);
            }

            return list;
        }

        private string GetFirstLot(string idProductSimple)
        {
            var lots = GetLots(idProductSimple);

            if (lots == null || lots.Count == 0)
            {
                return string.Empty;
            }

            dynamic lot = lots[0];
            return lot.Id;
        }

        private bool IsValidData()
        {
            if (string.IsNullOrWhiteSpace(IdProductTextBox.Text))
            {
                DialogHelper.ShowError(this, "Debe entrar el No. de Artículo/Producto que desea vender.");
                return false;
            }

            double count;
            if (string.IsNullOrWhiteSpace(CountTextBox.Text) || !double.TryParse(CountTextBox.Text, out count))
            {
                DialogHelper.ShowError(this, "La cantidad que desea vender debe tener valor y ser de tipo numérico.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(LotComboBox.SelectedValue.ToString()))
            {
                DialogHelper.ShowError(this, "El Lote es requerido para realizar la Venta.");
                return false;
            }

            return true;
        }

        private void ClearEntryControls()
        {
            IdProductTextBox.Text = string.Empty;
            ProductDescription.Text = string.Empty;
            UMComboBox.SelectedIndex = -1;
            UMComboBox.DataSource = null;

            LotComboBox.SelectedIndex = -1;
            LotComboBox.DataSource = null;

            CountTextBox.Text = "1";
            PriceLabel.Text = "0";
            PointLabel.Text = "0";
            ManagementLabel.Text = "0";
        }

        private void RefreshDataAndSave()
        {
            LoadGridDetails();
            CalculateResumeOperationValuesAndRefreshControls();
        }

        private void DeleteProductButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    DialogHelper.ShowInformation(this, "Debe exitir una fila seleccionada");
                }
                else
                {
                    ApplicationHelper.VerifyOperationModifiedDateState(currentItem);

                    var item = (OperationDetail)dataGridView1.SelectedRows[0].DataBoundItem;

                    dc.OperationDetails.DeleteAllOnSubmit(dc.OperationDetails.Where(d => d.IdOperation == item.IdOperation && d.ParentRow == item.ParentRow));
                    dc.SubmitChanges();

                    RefreshDataAndSave();
                    IdProductTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error agregando eliminando el producto de la Operación", ex);
                DialogHelper.ShowError(this,
                                       "Un Error ha ocurrido eliminando el producto de la Operación. Si el error persiste cierre el formulario y ábralo nuevamente.",
                                       ex);
            }
        }

        private void LoadGridDetails()
        {
            var details = currentItem.OperationDetails.OrderByDescending(o => o.AddedDate).ToList();
            dataGridView1.DataSource = details;
        }

        private void IdProductTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (IdProductTextBox.Text.Trim() != string.Empty)
                {
                    string pid = IdProductTextBox.Text.Trim();

                    currentProduct =
                        dc.Products.Where(p => !p.Disabled && (p.Id == pid || p.BarCode == pid)).SingleOrDefault();

                    if (currentProduct == null)
                    {
                        DialogHelper.ShowError(this,
                                               "Este producto no existe en el Cátalogo actual o no está habilitado para la venta. Si desea salir de este campo sin agregar déjelo en blanco.");
                        e.Cancel = true;
                    }
                    else
                    {
                        var sb = new StringBuilder();
                        sb.Append("[" + currentProduct.Id + "]").Append(" [" + currentProduct.BarCode + "]").Append(
                            " " + currentProduct.Name);

                        ProductDescription.Text = sb.ToString();

                        prices = GetPrices(currentProduct.Product_Prices);

                        if (prices.Count() == 0)
                        {
                            IdProductTextBox.Text = "";
                            DialogHelper.ShowError(this,
                                                   "Este producto no tiene Lista de Precios vigente en el Cátalogo actual. Debe existir al menos un precio Activo y Vigente en Fecha para la Moneda de Operación. Si desea salir de este campo sin agregar déjelo en blanco.");
                            e.Cancel = true;
                        }
                        else
                        {
                            UMComboBox.ValueMember = "Id";
                            UMComboBox.DisplayMember = "Name";
                            UMComboBox.DataSource =
                                prices.Select(p => new { Id = p.IdUM, Name = p.UDCItem.Code }).ToList();

                            if (prices.Select(p => p.IdUM).Contains(currentProduct.IdUM))
                            {
                                UMComboBox.SelectedValue = currentProduct.IdUM;
                            }

                            FillProductPricesControls();
                            FillLots();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error validando entrada de prodcuto en ventas", ex);
                DialogHelper.ShowError(this,
                                       "Un Error ha sido detectado validando el Producto. Si el error persiste cierra la aplicación y ejecútela nuevamente por favor.");
            }
        }

        private IEnumerable<Product_Price> GetPrices(IEnumerable<Product_Price> productPrices)
        {
            //Al quitarse el campo Pack de la lista de precios, operaciones detalle y kardex se elimino 
            //la posibilidad que un producto tenga varias unidades de medidas directamente. 
            //Si se deseara tener varias unidades de medidas habria que crear otro codigo Simple o bien
            //un codigo Compuesto que lleve el inventario por el simple. 

            //Solo en caso que se vuelva a poner algun dia deberia eliminarse el filtro de IdUM en el 
            //codigo siguiente y volver a poner el agrupado final


            var pprices = productPrices.Where(p => !p.Disabled && p.IdUM == currentProduct.IdUM &&
                                                  p.IdCurrency == currentItem.IdOperationCurrency &&
                                                  p.IdClientType == currentItem.Dim.IdType &&
                                                  (p.IdCompany == null || p.IdCompany == currentItem.Shop.IdCompany) &&
                                                  (p.IdShop == null || p.IdShop == currentItem.IdShop) &&
                                                  (p.IdPriceType == (int)ListPriceType.Normal && (
                                                  DateTime.Today >= p.InitialDate && DateTime.Today <=  p.FinalDate )));
                                                   //(SqlMethods.DateDiffDay(p.InitialDate, DateTime.Today) >= 0) &&
                                                   //SqlMethods.DateDiffDay(DateTime.Today, p.FinalDate) >= 0));

            //Para el caso de multiples UM
            //pprices = pprices.OrderByDescending(p => p.IdShop).ThenByDescending(p => p.IdCompany)
            //    .ThenByDescending(p => p.UDCItem2.Optional1);
            //pprices = pprices.GroupBy(p => p.IdUM).Select(g => g.First());

            pprices = pprices.OrderByDescending(p => p.IdShop).ThenByDescending(p => p.IdCompany)
                .ThenByDescending(p => p.UDCItem2.Optional1).Take(1);

            return pprices;

            //TODO: En la herramienta de entrada de datos, si hubiera, verificar que la lista de 
            //precios solo contenga la UM del producto. 
        }

        private void FillProductPricesControls()
        {
            if (UMComboBox.SelectedIndex != -1)
            {
                currentPrice = prices.Where(p => p.IdUM == Convert.ToInt32(UMComboBox.SelectedValue)).Single();

                PriceLabel.Text = currentPrice.Price.ToString("N");
                PointLabel.Text = currentPrice.Points.ToString("N");
                ManagementLabel.Text = currentPrice.Management.ToString("N");
            }
        }

        private void FillLots()
        {
            if (IdProductTextBox.Text != string.Empty && currentProduct != null)
            {
                LotComboBox.ValueMember = "Id";
                LotComboBox.DisplayMember = "Name";

                if (currentProduct.IdType == (int)ProductType.Simple)
                {
                    var lots = GetLots(currentProduct.Id);

                    LotComboBox.DataSource = lots.Count != 0
                                                 ? lots
                                                 : new ArrayList { new { Id = Constant.CFG_NOT_LOT_CODE, Name = "0: Libre de Lote" } };

                    if (lots.Count == 0)
                    {
                        DialogHelper.ShowWarningInfo(this,
                                                     "No hay existencias del Producto para la Ubicación/Dim seleccionado");
                        isDeniedProduct = true;
                    }
                }
                else
                {
                    LotComboBox.DataSource = new ArrayList { new { Id = Constant.CFG_NOT_LOT_CODE, Name = "0: Lotes de Hijos" } };
                }
            }
        }

        private IList GetLots(string idProduct)
        {
            IList invLots;

            if (SaleType != OperationType.ConsignationReturn)
            {
                dc.Refresh(RefreshMode.OverwriteCurrentValues, dc.Inventories);

                invLots = dc.Inventories.Where(i =>
                                               i.IdLocation.ToString() == Convert.ToString(LocationComboBox.SelectedValue) &&
                                               i.IdProduct == idProduct &&
                                               i.Count > 0).OrderBy(i => i.LotAddedDate).
                    Select(i =>
                           new
                           {
                               Id = i.Lot,
                               Name =
                           string.Format("{0}: {1} {2}", i.Lot, i.Count, currentProduct.UMCode)
                           }).ToList();
            }
            else
            {
                invLots = dc.OperationDetails.Where(o => o.Operation.IdType == (int)OperationType.Consignation &&
                        o.Operation.IdStatus == (int)OperationStatus.Confirmed &&
                        o.OfficialConsecutive == currentItem.Reference &&
                        o.IdProduct == idProduct).OrderBy(o => o.AddedDate).
                Select(o =>
                           new
                           {
                               Id = o.Lot,
                               Name =
                           string.Format("{0}: {1} {2}", o.Lot, o.Count, o.Product.UMCode)
                           }).ToList();
            }

            return invLots;
        }

        private void LocationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (SaleType != OperationType.ConsignationReturn)
                {
                    FillLots();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error LocationComboBox_SelectedIndexChanged", ex);
                DialogHelper.ShowError(this, "Un Error inesperado ha sido detectado realizando operaciones internas.",
                                       ex);
            }
        }

        private void UMComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillProductPricesControls();
                //FillLots();
            }
            catch (Exception ex)
            {
                Logger.Error("Error UMComboBox_SelectedIndexChanged", ex);
                DialogHelper.ShowError(this, "Un Error inesperado ha sido detectado realizando operaciones internas.",
                                       ex);
            }
        }

        private void CountTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (CountTextBox.Text.Trim() != string.Empty && IdProductTextBox.Text.Trim() != string.Empty)
                {
                    double count;

                    if (double.TryParse(CountTextBox.Text, out count))
                    {
                        if (SaleType != OperationType.ConsignationReturn)
                        {

                            //Poner codigo para saber su es un articulo con Kit

                            if (currentProduct.IdType == (int)ProductType.Composite)
                            {

                            }

                            else
                            {


                                var inv = dc.Inventories.Where(i => i.IdLocation != null &&
                                                                i.IdLocation.ToString() ==
                                                                Convert.ToString(LocationComboBox.SelectedValue) &&
                                                                i.IdProduct == currentProduct.Id &&
                                                                i.Lot == LotComboBox.SelectedValue.ToString()).
                                SingleOrDefault();

                                if (inv == null || count > inv.Count)
                                {
                                    DialogHelper.ShowWarningInfo(this,
                                                                 "La cantidad que desea Vender sobrepasará la existencia actual para la Ubicación y Lote seleccionado");
                                }
                            }
                        }
                        else
                        {
                            var inv = dc.Inventories.Where(i => i.IdDim != null &&
                                                            i.IdDim == currentDim.Id &&
                                                            i.IdProduct == currentProduct.Id &&
                                                            i.Lot == LotComboBox.SelectedValue.ToString()).
                            SingleOrDefault();

                            if (inv == null || count > inv.Count)
                            {
                                DialogHelper.ShowWarningInfo(this,
                                                             "La cantidad que desea Regresar sobrepasara la existencia actual para el DIM y Lote seleccionado");
                            }
                        }
                    }
                    else
                    {
                        DialogHelper.ShowError(this, "El tipo de dato de cantidad debe ser numérico");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error CountTextBox_Validating", ex);
                DialogHelper.ShowError(this, "Un Error inesperado ha sido detectado realizando operaciones internas.",
                                       ex);
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            DialogHelper.ShowError(this,
                                   "Ha ocurrido un error en el manejo interno del componente Grid. Repórte de continuar.",
                                   e.Exception);
        }

        private void InsertNewSale()
        {
            if (SaleType == OperationType.ConsignationReturn)
            {
                DialogHelper.ShowWarningInfo(this, "No se puede crear Retornos de Consignación de forma directa. Para ello debe ir al Ticket de Consiganión que desea Retornar.");
                return;
            }

            if (IsValidDim())
            {
                Shop currentShop = ApplicationHelper.GetCurrentShop();
                int idIVAGroup = currentDim.IdIVAGroup == Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY
                                     ? currentShop.IdIVAGroup
                                     : currentDim.IdIVAGroup;

                var ivaValueItem = DataHelper.GetIVAValueItem(dc,
                                                DataHelper.GetUDCItemRow(dc, currentShop.IdIVATypeByManagement).Code,
                                                DataHelper.GetUDCItemRow(dc, idIVAGroup).Code);

                currentItem = new Common.Operation
                                  {
                                      Id = Guid.NewGuid(),
                                      IdShop = Settings.Default.CurrentShop,
                                      IdType = (int)SaleType,
                                      IdDim = currentDim.Id,
                                      IdCashier = new Guid(Settings.Default.CurrentCashier),
                                      Consecutive = GetNextConsecutive((int)SaleType),
                                      IdOperationCurrency = ApplicationHelper.GetCurrencyByCurrentShop(),
                                      ChangeRate = 1,
                                      OperationAmount = 0,
                                      Amount = 0,
                                      IdStatus = (int)OperationStatus.NotConfirmed,
                                      AddedDate = DateTime.Now,
                                      IVACodeAppliedToManagement = ivaValueItem.Code,
                                      IVAAppliedToManagement = Convert.ToDecimal(ivaValueItem.Optional3)
                                  };

                DataHelper.FillAuditoryValuesDesktop(currentItem);

                dc.Operations.InsertOnSubmit(currentItem);
                dc.SubmitChanges();

                LoadCurrentSale();
            }
        }

        private void NewSaleButton_Click(object sender, EventArgs e)
        {
            try
            {
                RequestNewSale();
            }
            catch (Exception ex)
            {
                Logger.Error("Error NewSaleButton_Click", ex);
                DialogHelper.ShowError(this, "Un Error inesperado ha sido detectado creando la nueva venta.", ex);
            }
        }

        private SequenceId GetSequenceIDForConsecutive(int operationType)
        {
            switch (operationType)
            {
                case (int)OperationType.Sale:
                    return SequenceId.SaleConsecutive;
                case (int)OperationType.Consignation:
                    return SequenceId.ConsignationConsecutive;
                default: //(int)OperationType.ConsignationReturn:
                    return SequenceId.ConsignationReturnConsecutive;
            }
        }

        private string GetNextConsecutive(int operationType)
        {
            //var localDC = ApplicationHelper.GetFreePosDataContext();

            return DataHelper.GetNextSequence(dc, GetSequenceIDForConsecutive(operationType)).ToString();
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
                DialogHelper.ShowWarningInfo(this, "Este DIM está desactivado. No puede venderle.");
                return false;
            }

            if (currentDim.SaleRetention)
            {
                DialogHelper.ShowWarningInfo(this, "Está explicitamente prohibida la Venta para este DIM.");
                return false;
            }

            if (!currentDim.Consignable && SaleType == OperationType.Consignation)
            {
                DialogHelper.ShowWarningInfo(this,
                                             "Está explicitamente prohibida la Consignación de Mercancia a este DIM.");
                return false;
            }

            if (ApplicationHelper.GetCurrentShop().IdCountry != currentDim.IdCountry)
            {
                DialogHelper.ShowWarningInfo(this, "El Número de DIM no se corresponde con el país actual.");
                return false;
            }

            return true;
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

        private void IdProductTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                AddProductButton.PerformClick();
                //CountTextBox.Focus();
            }
        }

        private void CountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                //AddProductButton.PerformClick();
                IdProductTextBox.Focus();
                ActiveControl = IdProductTextBox;
            }
        }

        private void DoPayButton_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Info(string.Format("Comenzando El Pago y Confirmación del Ticket. Consecutivo {0}",
                                  currentItem.Consecutive));

                ApplicationHelper.VerifyEnviromentConditions(this, dc);
            }
            catch (Exception ex)
            {
                const string msg = "No se puede Pagar la Operación porque no ha pasado uno de los chequeos requeridos.";

                DialogHelper.ShowError(this, msg, ex);
                Logger.Error(msg, ex);

                return;
            }

            try
            {
                ApplicationHelper.VerifyOperationModifiedDateState(currentItem);

                if (new Pay(dc, currentItem).ShowDialog(this) == DialogResult.OK)
                {
                    Refresh();

                    using (TransactionScope trans = new TransactionScope())
                    {
                        ConfirmSale();

                        trans.Complete();
                    }
                    #region ADTECH - ASIGNACION DE DATOS DE DISTRIBUCION PARA EL ENVIO DE PRODUCTOS.
                    /*
                    * Cesar Equihua
                    * 24/02/2015
                    */
                    if (Settings.Default.DistribuitionData)
                    {
                        if (DialogHelper.ShowConfirmationQuestion(this, "¿ Desea registrar datos de envio ?") == DialogResult.Yes)
                        {
                            FormDistributionData formDistributionData = new FormDistributionData();
                            formDistributionData.IdOperation = currentItem.Id;
                            formDistributionData.ShowDialog(this);
                        }
                    }

                    
                    #endregion
                    
                    #region ADTECH - PROCESO DE FACTURACION INDIVIDUAL CREA LA FACTURA, CREA ARCHIVO TXT, ENVIA A XSA
                    /*
                    * Cesar Equihua
                    * 25/03/2014
                    * typeInvoice 1: Factura individual se hace cuando SI deseo facturar
                    * typeInvoice 2: Factura general se hace cuando NO deseo facturar y no se envia a XSA
                    */
                    bool errorFiscalData = false;
                    bool failConection = false;
                    bool statusDoc = false;
                    string errorFiscalDataDescription = "";

                    if (DialogHelper.ShowConfirmationQuestion(this, "¿ Desea facturar ?") == DialogResult.Yes)
                    {
                    denuevo:
                        if (new FormFiscalData(currentItem.Dim.Id).ShowDialog(this) == DialogResult.OK)
                        {
                            if (new FormInvoicePreView(currentItem.Id).ShowDialog(this) == DialogResult.OK)
                            {
                                //Cursor = Cursors.WaitCursor;
                                // CREA LA FACTURA DE TIPO INDIVIDUAL
                                oInvOperations = new InvoicesOperations(currentItem.Id, currentItem.IdShop, 1);
                                oInvOperations.GenerateInvoiceAndFileTxt();
                                if (oInvOperations.msgError != "")
                                {
                                    DialogHelper.ShowError(this, "Ocurrio un error al generar la factura en la base de datos: \n" + oInvOperations.msgError);
                                }
                                else
                                {
                                    /*ENVIAR FACTURA A XSA*/
                                    oInvOperations.SendToXSA(ref errorFiscalData, ref failConection, ref errorFiscalDataDescription, ref statusDoc);

                                    if (errorFiscalData)
                                    {
                                        DialogHelper.ShowWarningInfo(this, errorFiscalDataDescription + "\n Por favor verifique su informacion e intente nuevamente");
                                        goto denuevo;
                                    }
                                    else if (failConection)
                                    {
                                        DialogHelper.ShowError(this, "Ocurrio un error al enviar la factura " + oInvOperations.SerieFolio + " a XSA por problemas de conexion, \n se realizara durante el transcurso del dia, \n ó al restablecerse el servicio");
                                    }
                                    else if (statusDoc)
                                    {
                                        DialogHelper.ShowInformation(this, "La factura " + oInvOperations.SerieFolio + " se generó con éxito por favor valídelo en XSA");
                                    }
                                    else
                                    {
                                        DialogHelper.ShowWarningInfo(this, "La factura " + oInvOperations.SerieFolio + " se envió a XSA sin embargo ocurrieron errores al generarse por favor verifique el error en XSA \n" + errorFiscalDataDescription);
                                    }
                                }  
                            }
                            else
                            {
                                goto denuevo;
                            }
                        }
                        else
                        {
                            // CREAR FACTURA DE TIPO GENERAL
                            oInvOperations = new InvoicesOperations(currentItem.Id, currentItem.IdShop, 2);
                            oInvOperations.GenerateInvoiceAndFileTxt();
                            //if (oInvOperations.msgError != "")
                            //    DialogHelper.ShowError(this, "Ocurrio un error al generar la factura en la base de datos: \n" + oInvOperations.msgError);

                        }
                    }
                    else
                    {
                        if (DialogHelper.ShowConfirmationQuestionWarning(this, "¿ Esta usted seguro que no desea facturar ?") == DialogResult.No)
                        {
                        denuevo2:
                            if (new FormFiscalData(currentItem.Dim.Id).ShowDialog(this) == DialogResult.OK)
                            {
                                if (new FormInvoicePreView(currentItem.Id).ShowDialog(this) == DialogResult.OK)
                                {
                                    //Cursor = Cursors.WaitCursor;
                                    // CREA LA FACTURA DE TIPO INDIVIDUAL
                                    oInvOperations = new InvoicesOperations(currentItem.Id, currentItem.IdShop, 1);
                                    oInvOperations.GenerateInvoiceAndFileTxt();
                                    if (oInvOperations.msgError != "")
                                    {
                                        DialogHelper.ShowError(this, "Ocurrio un error al generar la factura en la base de datos: \n" + oInvOperations.msgError);
                                    }
                                    else
                                    {
                                        /*ENVIAR FACTURA A XSA*/
                                        oInvOperations.SendToXSA(ref errorFiscalData, ref failConection, ref errorFiscalDataDescription, ref statusDoc);

                                        if (errorFiscalData)
                                        {
                                            DialogHelper.ShowWarningInfo(this, errorFiscalDataDescription + "\n Por favor verifique su informacion e intente nuevamente");
                                            goto denuevo2;
                                        }
                                        else if (failConection)
                                        {
                                            DialogHelper.ShowError(this, "Ocurrio un error al enviar la factura " + oInvOperations.SerieFolio + " a XSA por problemas de conexion, \n se realizara durante el transcurso del dia, \n ó al restablecerse el servicio");
                                        }
                                        else if (statusDoc)
                                        {
                                            DialogHelper.ShowInformation(this, "La factura " + oInvOperations.SerieFolio + " se generó con éxito por favor valídelo en XSAo");
                                        }
                                        else
                                        {
                                            DialogHelper.ShowWarningInfo(this, "La factura " + oInvOperations.SerieFolio + " se envió a XSA sin embargo ocurrieron errores al generarse por favor verifique el error en XSA \n" + errorFiscalDataDescription);
                                        }
                                    }
                                }
                                else
                                {
                                    goto denuevo2;
                                }
                            }
                            else
                            {
                                // CREAR FACTURA DE TIPO GENERAL
                                oInvOperations = new InvoicesOperations(currentItem.Id, currentItem.IdShop, 2);
                                oInvOperations.GenerateInvoiceAndFileTxt();
                                //if (oInvOperations.msgError != "")
                                //    DialogHelper.ShowError(this, "Ocurrio un error al generar la factura en la base de datos: \n" + oInvOperations.msgError);

                            }
                        }
                        else
                        {
                            // CREAR FACTURA DE TIPO GENERAL
                            oInvOperations = new InvoicesOperations(currentItem.Id, currentItem.IdShop, 2);
                            oInvOperations.GenerateInvoiceAndFileTxt();
                            //if (oInvOperations.msgError != "")
                            //    DialogHelper.ShowError(this, "Ocurrio un error al generar la factura: \n" + oInvOperations.msgError);

                        }
                    }

                    #endregion
                }

                try
                {
                    LoadResumeControls();
                }
                catch (Exception)
                {
                    DialogHelper.ShowWarningInfo(this, "La acción concluyó correctamente pero los datos no se pudieron refrescar. Le sugerimos cerrar el formulario y volver a abrirlo");
                }

                try
                {
                    VerifyCashLimit();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error verificando el Límite de Caja, posterior a la Venta.", ex);
                    DialogHelper.ShowWarningInfo(this, "La acción concluyó correctamente pero no se pudo verificar el límite de Efectivo en Caja por la ocurrencia de un error.");
                }
            }
            catch (Exception ex)
            {
                //DataHelper.RollbackTransaction(dc);
                Cursor = Cursors.Default;

                const string msg =
                    @"Un Excepción ha ocurrido durante la confirmación de la Operación. 
Para que la aplicación se recupere puede que este formulario deba cerrarse. 
Si es producto de una Venta Negativa elimine los renglones problematicos e insertelos nuevamente con valores correctos. 
Si el error persiste cierre la aplicación y ejecútela nuevamente por favor.";

                DialogHelper.ShowError(this, msg, ex);

                Logger.Error(string.Format("Error Pagando y Confirmando la Operación. Consecutivo {0} OfficialConsecutive {1} Tipo {2}",
                                  currentItem.Consecutive, currentItem.OfficialConsecutive, currentItem.TypeName), ex);

                ApplicationHelper.ResetPosDataContext();
                Close();
            }
        }

        private void VerifyCashLimit()
        {
            decimal totalAmountCash = DataHelper.GetCurrentCashFromAllCurrency(dc,
                                                new Guid(Settings.Default.CurrentCashier));

            decimal maxCashAllowed = currentItem.Shop.Company.MaxCashInCashier;

            if (totalAmountCash > maxCashAllowed)
            {
                string msg = string.Format(
                    "Alerta!!!{0}El efectivo total en caja (considerando todas las monedas) {1} excede el límite permitido de {2}. {0}Debe realizar una extracción. {0}",
                    Environment.NewLine, totalAmountCash, maxCashAllowed);

                DialogHelper.ShowWarningInfo(this, msg + "Adicionalmente un email será enviado a un resposable (puede demorar unos segundos).");
                Refresh();

                var cashier = ApplicationHelper.GetCurrentCashier();

                Cursor = Cursors.WaitCursor;

                try
                {
                    MailTemplateHelper.SendPlainTextMessage(string.Format("Alerta MegaPOS, Efectivo en Exceso en caja: {0}", cashier.Name),
                        msg, cashier.Shop.Email, string.Empty, string.Empty);

                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Default;
                    DialogHelper.ShowError(this, "Ha ocurrido un error Enviado el mensaje al responsable. Es su responsabilidad verificar y avisar.");
                    Logger.Error("Ha ocurrido un error Enviado el mensaje de Exceso de Efectivo", ex);
                }
            }

        }

        private void IdProductTextBox_TextChanged(object sender, EventArgs e)
        {
            if (IdProductTextBox.Text.Length == Convert.ToInt32(DataHelper.GetUDCItemRow(dc, Constant.CFG_BARCODE_MAX_SIZE_UDCITEM_KEY).Optional1))
            {
                AddProductButton.PerformClick();
                //CountTextBox.Focus();
            }
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                PrintTicket(true);

                //To Print besides of Report Viewer on a Form
                //new Test(currentItem.Id.ToString()).Show(this);

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;

                DialogHelper.ShowError(this, "Un Error inesperado ha sido detectado al intentar imprimer el Ticket.", ex);
                Logger.Error("Error PrintButton_Click", ex);
            }
        }

        private void DeleteSaleButton_Click(object sender, EventArgs e)
        {
            try
            {
                ApplicationHelper.VerifyOperationModifiedDateState(currentItem);

                if (currentItem.IdStatus != (int)OperationStatus.NotConfirmed)
                {
                    DialogHelper.ShowWarningInfo(this, "No puede eliminar Ventas Confirmadas o Canceladas");
                }
                else if (DialogHelper.ShowWarningQuestion(this, "¿Está seguro que desea eliminar la Venta actual?") ==
                         DialogResult.Yes)
                {
                    Logger.InfoFormat("Eliminando el Ticket con No. Consecutivo: " + currentItem.Consecutive);

                    dc.Operations.DeleteOnSubmit(currentItem);
                    dc.SubmitChanges();

                    currentItem = null;
                    RequestNewSale();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error DeleteSaleButton_Click", ex);
                DialogHelper.ShowError(this, "Un Error inesperado ha sido detectado al intentar eliminar Ticket con No. Consecutivo: " + currentItem.Consecutive, ex);
            }
        }

        private void RequestNewSale()
        {
            HiddenPanel.Top = SaleInfoGroupBox.Top;
            HiddenPanel.Left = 0;
            HiddenPanel.Width = Width;
            HiddenPanel.Height = Height;
            HiddenPanel.Visible = true;
            HiddenPanel.TabIndex = 1000;
            HiddenPanel.Update();

            DimTextBox.Text = string.Empty;
            DeleteSaleButton.Visible = false;

            SaleTypeRadioButton.Enabled = true;
            ConsignationTypeRadioButton.Enabled = true;
            ConsignationReturnTypeRadioButton.Enabled = true;

            DimTextBox.Focus();
            ActiveControl = DimTextBox;
        }

        private void DIMTextBox_KeyPress(object sender, KeyPressEventArgs e)
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
                        InsertNewSale();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error en SaleEdit.DIMTextBox_KeyPress ");
                DialogHelper.ShowError(this, "Error realizando procesos internos", ex);
            }
        }

        private void FindDimButton_Click(object sender, EventArgs e)
        {
            try
            {
                //De momento se logra por la longitud del DIM. Si se eliminara eso, pues se procederia con esta variante. 
                FindClient f = new FindClient(DimTextBox.Text, true);

                Cursor = Cursors.WaitCursor;

                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    DimTextBox.Text = f.SelectedDIM;

                    int dimSize =
                        Convert.ToInt32(DataHelper.GetUDCItemRow(dc, Constant.CFG_DIM_MAX_SIZE_UDCITEM_KEY).Optional1);

                    if (DimTextBox.Text.Trim().Length != dimSize)
                    {
                        InsertNewSale();
                    }
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Logger.Error("Error FindDimButton_Click from SaleEdit");
                Cursor = Cursors.Default;
                DialogHelper.ShowError(this, "Error inesparado abriendo el formulario de busqueda de Dim", ex);
            }
        }

        private void DimTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int dimSize =
                    Convert.ToInt32(DataHelper.GetUDCItemRow(dc, Constant.CFG_DIM_MAX_SIZE_UDCITEM_KEY).Optional1);

                if (DimTextBox.Text.Trim().Length == dimSize)
                {
                    currentDim = GetDimItem();

                    if (currentDim != null)
                    {
                        InsertNewSale();
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
                DialogHelper.ShowError(this,
                                       "Error inesperado validando la extensión de Número de DIM. revise la configuración de sus UDC.",
                                       ex);
            }
        }

        private void SaleEdit_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    DoPayButton.PerformClick();
                    break;
                case Keys.F11:
                    PrintButton.PerformClick();
                    break;

                case Keys.F9:
                    NewSaleButton.PerformClick();
                    break;
            }
        }

        private void OperationConsecutiveTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                FindSaleButton.PerformClick();
            }
        }

        private void FindSaleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(OperationConsecutiveTextBox.Text))
                {
                    var sale =
                        dc.Operations.Where(
                            o =>
                            (o.IdType == (int)SaleType) && o.IdStatus != (int)OperationStatus.NotConfirmed && o.OfficialConsecutive == OperationConsecutiveTextBox.Text).
                            SingleOrDefault();

                    if (sale != null)
                    {
                        currentItem = sale;
                        LoadCurrentSale();
                        OperationConsecutiveTextBox.Text = string.Empty;
                    }
                    else
                    {
                        DialogHelper.ShowWarningInfo(this, "No se encontró la Orden buscada. Verifíque el número.");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error en FindSaleButton_Click");
                DialogHelper.ShowError(this, "Error inesperado Buscando el No. de Order de la Venta.", ex);
            }
        }

        private void GotoPreviousOrderButton_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime findDate = currentItem == null ? DateTime.Now : currentItem.AddedDate;

                //Solo encontrara No Confirmadas de su propia Caja, por seguridad en O. concurrentes.
                var sale =
                    dc.Operations.Where(
                        o =>
                        o.IdType == (int)SaleType &&
                        (o.IdStatus != (int)OperationStatus.NotConfirmed ||
                         o.IdCashier == ApplicationHelper.GetCurrentCashierIdAsGuid()) &&
                        o.AddedDate < findDate)
                        .OrderByDescending(o => o.AddedDate).Take(1).SingleOrDefault();

                if (sale != null)
                {
                    currentItem = sale;
                    LoadCurrentSale();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error en GotoPreviousOrderButton_Click");
                DialogHelper.ShowError(this, "Error inesperado Buscando la Venta Anterior.", ex);
            }
        }

        private void GotoNextOrderButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentItem == null)
                {
                    GotoPreviousOrderButton_Click(null, null);
                }
                else
                {
                    DateTime findDate = currentItem.AddedDate;

                    var sale =
                        dc.Operations.Where(
                            o =>
                            o.IdType == (int)SaleType &&
                            (o.IdStatus != (int)OperationStatus.NotConfirmed ||
                             o.IdCashier == ApplicationHelper.GetCurrentCashierIdAsGuid()) &&
                            o.AddedDate > findDate).OrderBy(
                                o => o.AddedDate).Take(1).SingleOrDefault();

                    if (sale != null)
                    {
                        currentItem = sale;
                        LoadCurrentSale();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error en GotoNextOrderButton_Click");
                DialogHelper.ShowError(this, "Error inesperado Buscando la Venta Siguiente.", ex);
            }
        }

        private void SaleTypeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SaleTypeRadioButton.Checked)
            {
                SaleType = OperationType.Sale;
            }
        }

        private void ConsignableSaleTypeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ConsignationTypeRadioButton.Checked)
            {
                SaleType = OperationType.Consignation;
            }
        }

        private void CountTextBox_Enter(object sender, EventArgs e)
        {
            CountTextBox.SelectAll();
        }

        private void CountTextBox_Click(object sender, EventArgs e)
        {
            CountTextBox.SelectAll();
        }

        private void ConsignableSaleReturnTypeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ConsignationReturnTypeRadioButton.Checked)
            {
                SaleType = OperationType.ConsignationReturn;
            }
        }

        private void CreateConsignationReturnButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogHelper.ShowWarningQuestion(this, "¿Está seguro que desea Retornar esta Consignación?. Verifique bien que las cantidades de Productos y Lotes que le entregan coinciden con los de esta Operación!!") == DialogResult.No)
                {
                    return;
                }

                try
                {
                    Logger.InfoFormat("Iniciando el Retorno de Consigancion. Consecutive {0} OfficialConsecutive {1}",
                                  currentItem.Consecutive, currentItem.OfficialConsecutive);

                    ApplicationHelper.VerifyEnviromentConditions(this, dc);
                }
                catch (Exception ex)
                {
                    const string msg = "No se puede realizar la acción porque no ha pasado uno de los chequeos requeridos.";

                    DialogHelper.ShowError(this, msg, ex);
                    Logger.Error(msg, ex);

                    return;
                }

                Cursor = Cursors.WaitCursor;

                ApplicationHelper.VerifyOperationModifiedDateState(currentItem);

                using (TransactionScope trans = new TransactionScope())
                {
                    var newConsignationReturn = new Common.Operation
                                                    {
                                                        Id = Guid.NewGuid(),
                                                        IdShop = currentItem.IdShop,
                                                        IdType = (int)OperationType.ConsignationReturn,
                                                        IdDim = currentItem.IdDim,
                                                        Reference = currentItem.OfficialConsecutive,
                                                        IdCashier = new Guid(Settings.Default.CurrentCashier),
                                                        Consecutive =
                                                            GetNextConsecutive((int)OperationType.ConsignationReturn),
                                                        IdOperationCurrency = ApplicationHelper.GetCurrencyByCurrentShop(),
                                                        ChangeRate = 1,
                                                        OperationAmount = currentItem.OperationAmount,
                                                        Amount = currentItem.Amount,
                                                        IdStatus = (int)OperationStatus.NotConfirmed,
                                                        AddedDate = DateTime.Now,
                                                        IVACodeAppliedToManagement = currentItem.IVACodeAppliedToManagement,
                                                        IVAAppliedToManagement = currentItem.IVAAppliedToManagement
                                                    };

                    DataHelper.FillAuditoryValuesDesktop(newConsignationReturn);

                    foreach (var p in currentItem.OperationDetails)
                    {
                        newConsignationReturn.OperationDetails.Insert(0, CloneOperationDetail(p, newConsignationReturn.Id));
                    }

                    dc.Operations.InsertOnSubmit(newConsignationReturn);
                    dc.SubmitChanges();

                    SaleType = OperationType.ConsignationReturn;
                    var oldOperation = currentItem;

                    currentItem = newConsignationReturn;
                    LoadCurrentSale();
                    Refresh();

                    ConfirmSale();
                    oldOperation.Reference = currentItem.OfficialConsecutive;

                    dc.SubmitChanges();

                    trans.Complete();
                }

                try
                {
                    LoadResumeControls();
                }
                catch (Exception)
                {
                    DialogHelper.ShowWarningInfo(this, "La acción concluyó correctamente pero los datos no se pudieron refrescar. Le sugerimos cerrar el formulario y volver a abrirlo");
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                //DataHelper.RollbackTransaction(dc);
                Cursor = Cursors.Default;

                DialogHelper.ShowError(this, "Error inesperado intentando crear el Retorno de Consignación. El formulario quizás deba Cerrarse para evitar inconsistencia de datos", ex);

                Logger.Error(string.Format("Error Creando el Retorno de Consignacion. Consecutivo {0} OfficialConsecutive {1}",
                                  currentItem.Consecutive, currentItem.OfficialConsecutive), ex);

                ApplicationHelper.ResetPosDataContext();
                Close();
            }
        }

        private OperationDetail CloneOperationDetail(OperationDetail p, Guid operationId)
        {
            return new OperationDetail
                       {
                           Id = Guid.NewGuid(),
                           IdOperation = operationId,
                           IdLocation = p.IdLocation,
                           IdProductType = p.IdProductType,
                           IdProductParent = p.IdProductParent,
                           IdProduct = p.IdProduct,
                           ParentRow = p.ParentRow,
                           IdUM = p.IdUM,
                           Count = p.Count,
                           Lot = p.Lot,
                           IdPriceType = p.IdPriceType,
                           OperationPrice = p.OperationPrice,
                           OperationAmount = p.OperationAmount,
                           ProductIVACodeApplied = p.ProductIVACodeApplied,
                           ProductIVAApplied = p.ProductIVAApplied,
                           Points = p.Points,
                           Management = p.Management,
                           Pediment = p.Pediment,
                           AddedDate = DateTime.Now
                       };
        }

        private void ConvertConsignationIntoSaleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogHelper.ShowWarningQuestion(this, "¿Está seguro que desea Crear una Venta a partir de este Retorno de Consignación?. Se creará con cantidades de Productos y Lotes similares y sin Confirmar!!") == DialogResult.No)
                {
                    return;
                }

                try
                {
                    Logger.InfoFormat("Iniciando la Conversion a Venta de la Consignación: Consecutive {0} OfficialConsecutive {1}",
                                  currentItem.Consecutive, currentItem.OfficialConsecutive);

                    ApplicationHelper.VerifyEnviromentConditions(this, dc);
                }
                catch (Exception ex)
                {
                    const string msg = "No se puede realizar la acción porque no ha pasado uno de los chequeos requeridos.";

                    DialogHelper.ShowError(this, msg, ex);
                    Logger.Error(msg, ex);

                    return;
                }

                Cursor = Cursors.WaitCursor;
                ApplicationHelper.VerifyOperationModifiedDateState(currentItem);

                using (TransactionScope trans = new TransactionScope())
                {
                    var newSale = new Common.Operation
                                      {
                                          Id = Guid.NewGuid(),
                                          IdShop = currentItem.IdShop,
                                          IdType = (int)OperationType.Sale,
                                          IdDim = currentItem.IdDim,
                                          Reference = currentItem.OfficialConsecutive,
                                          IdCashier = new Guid(Settings.Default.CurrentCashier),
                                          Consecutive =
                                              GetNextConsecutive((int)OperationType.Sale),
                                          IdOperationCurrency = ApplicationHelper.GetCurrencyByCurrentShop(),
                                          ChangeRate = 1,
                                          OperationAmount = currentItem.OperationAmount,
                                          Amount = currentItem.Amount,
                                          IdStatus = (int)OperationStatus.NotConfirmed,
                                          AddedDate = DateTime.Now,
                                          IVACodeAppliedToManagement = currentItem.IVACodeAppliedToManagement,
                                          IVAAppliedToManagement = currentItem.IVAAppliedToManagement
                                      };

                    DataHelper.FillAuditoryValuesDesktop(newSale);

                    foreach (var p in currentItem.OperationDetails)
                    {
                        newSale.OperationDetails.Insert(0, CloneOperationDetail(p, newSale.Id));
                    }

                    dc.Operations.InsertOnSubmit(newSale);
                    dc.SubmitChanges();

                    SaleType = OperationType.Sale;

                    currentItem = newSale;
                    LoadCurrentSale();
                    Refresh();

                    dc.SubmitChanges();

                    trans.Complete();
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                //DataHelper.RollbackTransaction(dc);
                Cursor = Cursors.Default;

                DialogHelper.ShowError(this, "Error inesperado intentando crear una Venta a partir de este Retorno de Consignación. El formulario quizás deba ser cerrado para evitar inconsistencia de Datos.", ex);

                Logger.Error(string.Format("Error intentando crear una Venta a partir de un Retorno de Consignación. Consecutivo {0} OfficialConsecutive {1}",
                                  currentItem.Consecutive, currentItem.OfficialConsecutive), ex);

                ApplicationHelper.ResetPosDataContext();
                Close();
            }
        }

        private void CountTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SaleEdit_Activated(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-MX");
         
        }

        private void btnDataDistr_Click(object sender, EventArgs e)
        {
            if (currentItem == null)
                return;
            FormDistributionData frmDis = new FormDistributionData();
            frmDis.IdOperation = currentItem.Id;
            frmDis.ShowDialog(this);
        }
    }
}