using System;
using System.Linq;
using System.Net.Mail;
using System.Transactions;
using System.Windows.Forms;
using log4net;
using Mega.Common;
using Mega.Common.Enum;
using Mega.Common.Helpers;
using Mega.POS.Helper;
using Mega.POS.Report;
using Microsoft.Reporting.WinForms;

namespace Mega.POS.Movement
{
    public partial class CashierClose : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CashierClose));

        private bool isCashierVerification;
        private AdminDataContext dc = ApplicationHelper.GetPosDataContext();
        private Common.CashierClose currentCashierClose;
        private int attempts;
        private string supervisedBy;
        Operation.InvoicesOperations oInvOperations = null;

        public CashierClose(bool isCashierVerification)
        {
            this.isCashierVerification = isCashierVerification;
            InitializeComponent();
        }

        private void CashierClose_Load(object sender, EventArgs e)
        {
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                if (ApplicationHelper.IsOpenSimilarForm(this))
                {
                    DialogHelper.ShowWarningInfo(this, "Ya existe otro formulario Similar abierto");
                    Close();
                    return;
                }

                MakeCloseButton.Text = isCashierVerification ? "Efectuar el Arqueo" : "Efectuar el Cierre";
                VerifyAuthorization();

                PrintCloseCashier.Visible = false;

                Logger.Info("Iniciando el proceso para " + MakeCloseButton.Text);

                try
                {
                    ApplicationHelper.VerifyEnviromentConditions(this, dc);
                }
                catch (Exception ex)
                {
                    const string msg = "No se puede realizar la Operación porque no ha pasado uno de los chequeos requeridos.";

                    DialogHelper.ShowError(this, msg, ex);
                    Logger.Error(msg, ex);

                    return;
                }

                using (TransactionScope trans = new TransactionScope())
                {
                    DataHelper.DeleteUnClosedCashierClose(dc, new Guid(Properties.Settings.Default.CurrentCashier));
                    DeleteNotConfirmedSale();

                    var cashier = ApplicationHelper.GetCurrentCashier();
                    CashierNameLabel.Text = cashier.Name;

                    Common.CashierClose lastClose = DataHelper.GetLastCashierClose(dc, new Guid(Properties.Settings.Default.CurrentCashier));

                    currentCashierClose = new Common.CashierClose
                                              {
                                                  Id = Guid.NewGuid(),
                                                  IsCashierVerification = isCashierVerification,
                                                  Consecutive = GetNextConsecutive(),
                                                  IdCashier = cashier.Id,
                                                  AddedDate = DateTime.Now,
                                                  InitialDate = lastClose == null ? null : lastClose.FinalDate,
                                                  FinalDate = DateTime.Now,
                                                  IsOK = false,
                                                  ModifiedBy = ApplicationHelper.GetCurrentUser(),
                                                  SupervisedBy = supervisedBy,
                                                  IsClosed = false
                                              };

                    LastCloseLabel.Text = lastClose != null ? lastClose.AddedDate.ToString("d") : "Ninguno";

                    var mm = DataHelper.GetUnClosedMoneyMovement(dc, lastClose, new Guid(Properties.Settings.Default.CurrentCashier));

                    if (mm.Count() == 0)
                    {
                        DialogHelper.ShowWarningInfo(this, "No existen movimientos para realizar un cierre de Cajas. Si esto no es correcto realice una Auditoria al Sistema");
                        return;
                    }

                    var movementsByType =
                        from m in mm
                        where m.IdType != (int)MovementType.AdjustByRound
                        group m by new { m.IdType, m.IsCanceling, m.IdOperationCurrency }
                            into g
                            select
                                new
                                    {
                                        g.Key.IdType,
                                        g.Key.IsCanceling,
                                        g.Key.IdOperationCurrency,
                                        Count = g.Count(),
                                        OperationAmount = g.Sum(m => m.OperationAmount),
                                        Amount = g.Sum(m => m.Amount),
                                        LastChangeRate = g.First().ChangeRate
                                    };

                    foreach (var r in movementsByType.OrderBy(m => m.IdType).ThenBy(m => m.IdOperationCurrency))
                    {
                        var detail = new CashierCloseDetail
                        {
                            Id = Guid.NewGuid(),
                            IdCashierClose = currentCashierClose.Id,
                            IdMovementType = r.IdType,
                            IdOperationCurrency = r.IdOperationCurrency,
                            IsCanceling = r.IsCanceling,
                            OperationAmount = r.OperationAmount,
                            Amount = r.Amount,
                            LastChangeRate = r.LastChangeRate,
                            CashierOperationAmount = 0,
                            Count = r.Count
                        };

                        currentCashierClose.CashierCloseDetails.Insert(0, detail);
                    }

                    var mmAsList =
                        mm.ToList().Where(m => m.IdType != (int)MovementType.AdjustByRound).
                            Select(
                                m =>
                                new
                                    {
                                        m.Id,
                                        m.Consecutive,
                                        m.IdType,
                                        m.IdCashier,
                                        m.IdOperationCurrency,
                                        m.ChangeRate,
                                        m.OperationAmount,
                                        m.Amount,
                                        m.IsCanceling,
                                        m.AddedDate,
                                        m.IdOperation,
                                        m.IsManual,
                                        m.Notes,
                                        m.ModifiedBy,
                                        m.SupervisedBy,
                                        IdTypeNew = GetNewType(m.IdType, m.IdOperationCurrency, m.Cashier.Shop.IdCurrency)
                                    });

                    var cashMovementsByCurrency =
                        mmAsList.GroupBy(m => new { m.IdTypeNew, m.IdOperationCurrency }).
                            Select(g => new
                                            {
                                                IdType = g.Key.IdTypeNew,
                                                g.Key.IdOperationCurrency,
                                                OperationAmount = g.Sum(m => m.OperationAmount),
                                                Amount = g.Sum(m => m.Amount),
                                                LastChangeRate = g.First().ChangeRate
                                            }).OrderBy(m => m.IdType).ThenBy(m => m.IdOperationCurrency);

                    foreach (var r in cashMovementsByCurrency)
                    {
                        var detail = new CashierCloseMoney
                        {
                            Id = Guid.NewGuid(),
                            IdCashierClose = currentCashierClose.Id,
                            IdType = r.IdType,
                            IdOperationCurrency = r.IdOperationCurrency,
                            OperationAmount = r.OperationAmount,
                            Amount = r.Amount,
                            LastChangeRate = r.LastChangeRate,
                            CashierOperationAmount = 0
                        };

                        currentCashierClose.CashierCloseMoneys.Insert(0, detail);
                    }

                    dc.CashierCloses.InsertOnSubmit(currentCashierClose);
                    dc.SubmitChanges();

                    CloseByMoneyDataGridView.AutoGenerateColumns = false;
                    CloseByMoneyDataGridView.DataSource = currentCashierClose.CashierCloseMoneys.ToList();

                    MakeCloseButton.Enabled = true;

                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                //DataHelper.RollbackTransaction(dc);

                Logger.Error("Error Realizando el cierre o arqueo ");
                DialogHelper.ShowError(this, "Error Inesperado preparando los datos del formulario", ex);
            }
        }

        private int GetNewType(int idType, int idCurrency, int idCurrencyShop)
        {
            switch (idType)
            {
                case (int)MovementType.SaleChange:
                case (int)MovementType.Extract:
                case (int)MovementType.Deposit:
                case (int)MovementType.CreditCollectCash:
                case (int)MovementType.CreditCollectCurrency:
                case (int)MovementType.CashierOpen:
                    return idCurrency == idCurrencyShop
                        ? (int)MovementType.SaleCash
                        : (int)MovementType.SaleCurrency;
                case (int)MovementType.CreditCollectCard:
                    return (int)MovementType.SaleCard;
                default:
                    return idType;
            }
        }

        private void DeleteNotConfirmedSale()
        {
            dc.Operations.DeleteAllOnSubmit(
                dc.Operations.Where(
                    o =>
                    o.IdStatus == (int)OperationStatus.NotConfirmed &&
                    (o.IdType == (int)OperationType.Sale ||
                    o.IdType == (int)OperationType.Consignation ||
                    o.IdType == (int)OperationType.ConsignationReturn)));

            dc.SubmitChanges();
        }

        private void VerifyAuthorization()
        {
            if (ApplicationHelper.IsCurrentUserInRole(Constant.SupervisorOrMore))
            {
                supervisedBy = ApplicationHelper.GetCurrentUser();
            }
            else
            {
                AuthorizationRequired f = new AuthorizationRequired(Constant.SupervisorOrMore,
                                                                    "Un usuario con Rol de [Supervisor] o [Gerente] necesita autorizar esta Operación.");

                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    supervisedBy = f.AuthorizedUser;
                }
                else
                {
                    Close();
                    return;
                }
            }
        }

        private int GetNextConsecutive()
        {
            //var localDC = ApplicationHelper.GetFreePosDataContext();

            return
                DataHelper.GetNextSequence(dc,
                                           isCashierVerification
                                               ? SequenceId.CashierVerificationConsecutive
                                               : SequenceId.CashierCloseConsecutive);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            ApplicationHelper.ResetPosDataContext();
            Close();
        }

        private void MakeCloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    dc.SubmitChanges();

                    var diff =
                        currentCashierClose.CashierCloseMoneys.Where(c => c.OperationAmount != c.CashierOperationAmount);

                    if (diff.Count() > 0)
                    {
                        if (isCashierVerification)
                        {
                            if (DialogHelper.ShowWarningQuestion(this,
                                                                 "Existen diferencias en los Importes declarados. ¿Desea proceder con el arqueo y habilitar su impresión?") !=
                                DialogResult.Yes)
                            {
                                trans.Complete();
                                return;
                            }
                        }
                        else
                        {
                            attempts++;

                            if (attempts < 3)
                            {
                                DialogHelper.ShowWarningInfo(this,
                                                             "Existen diferencias entre los Importes declarados y los calculados por el sistema. Por favor revíselos e intente nuevamente." +
                                                             string.Format("{0}Intento {1} de 3", Environment.NewLine,
                                                                           attempts));
                                trans.Complete();
                                return;
                            }

                            if (DialogHelper.ShowWarningQuestion(this,
                                                                 "Continuan las diferencias en los valores. ¿Desea efectuar el Cierre bajo estas condiciones? Un Email será enviado notificando a un supervisor de la Compañía") !=
                                DialogResult.Yes)
                            {
                                trans.Complete();
                                return;
                            }
                        }

                        CloseCashier(false);
                    }
                    else
                    {
                        CloseCashier(true);
                    }

                    trans.Complete();
                }

                PrintCloseCashier.Visible = true;
                PrintCloseCashier.Text = isCashierVerification ? "Imprimir el Arqueo" : "Imprimir el Cierre";

                DialogHelper.ShowInformation(this, "La operación ha sido efectuada y guardada. Puede verificar las Cantidades e Imprimir el Comprobante");

                #region ADTECH - GENERA ARCHIVO TXT FACTURA GENERAL Y LO ENVIA A XSA

                bool errorFiscalData = false;
                bool failConection = false;
                bool statusDoc = false;
                string errorDescription = "";

                if (this.oInvOperations != null)
                {
                    if (this.oInvOperations.CreateFileTXTInvoiceGeneral(currentCashierClose.Id))
                    {
                        this.oInvOperations.SendToXSA(ref errorFiscalData, ref failConection, ref errorDescription, ref statusDoc, currentCashierClose.Id.ToString().ToUpper(), 2);
                        if (errorFiscalData)
                        {
                            DialogHelper.ShowWarningInfo(this, "No fue posible enviar la factura " + this.oInvOperations.SerieFolio + " ya que contiene errores de datos fiscales \n" + errorDescription);
                            Logger.Error(errorDescription);
                        }
                        else if (failConection)
                        {
                            DialogHelper.ShowError(this, "Ocurrio un error al enviar la factura " + this.oInvOperations.SerieFolio + " a XSA por problemas de conexion, \n se realizara durante el transcurso del dia, \n ó al restablecerse el servicio");
                            Logger.Error(errorDescription);
                        }
                        else if (statusDoc)
                        {
                            DialogHelper.ShowInformation(this, "La factura general " + this.oInvOperations.SerieFolio + " se generó con éxito por favor valídelo en XSA");
                            Logger.Info("La factura general se realizo con exito");
                        }
                        else
                        {
                            DialogHelper.ShowWarningInfo(this, "La factura " + oInvOperations.SerieFolio + " se envió a XSA sin embargo ocurrieron errores al generarse por favor verifique el error en XSA \n" + errorDescription);
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                //DataHelper.RollbackTransaction(dc);
                Cursor = Cursors.Default;
                ApplicationHelper.ResetPosDataContext();

                DialogHelper.ShowError(this, "Error inesperado durante las operaciones requeridas para el cierre. El formulario debe cerrarse para evitar inconsistencia de datos", ex);

                Logger.Error("Error haciendo el Cierre. MakeCloseButton_Click", ex);
                Close();
            }
        }

        private void CloseCashier(bool isOk)
        {
            CloseByMoneyDataGridView.Columns["OperationAmountByMoney"].Visible = true;
            //CloseByMoneyDataGridView.Columns["AmountByMoney"].Visible = true;
            //CloseByMoneyDataGridView.Columns["LastChangeRateByMoney"].Visible = true;

            MakeCloseButton.Enabled = false;

            Refresh();

            currentCashierClose.IsOK = isOk;
            currentCashierClose.IsClosed = true;

            Cursor = Cursors.WaitCursor;

            if (!isCashierVerification)
            {
                var newClose = new MoneyMovement
                {
                    Id = Guid.NewGuid(),
                    IdOperationCurrency = currentCashierClose.Cashier.Shop.IdCurrency,
                    IdType = (int)MovementType.CashierClose,
                    IdCashier = currentCashierClose.IdCashier,
                    ModifiedBy = currentCashierClose.ModifiedBy,
                    SupervisedBy = currentCashierClose.SupervisedBy,
                    IdOperation = currentCashierClose.Id,
                    Amount = 0,
                    ChangeRate = 1,
                    IsManual = false,
                    IsCanceling = false,
                    AddedDate = DateTime.Now
                };

                dc.MoneyMovements.InsertOnSubmit(newClose);

                #region ADTECH - GENERAR LA FACTURA GENERAL

                oInvOperations = new Operation.InvoicesOperations(Guid.Empty, currentCashierClose.Cashier.IdShop, 2);
                oInvOperations.IdInvoice = 0;
                oInvOperations.ShopName = currentCashierClose.ShopName;

                // GENERAMOS UN NUEVO FOLIO
                //Shop oShop = dc.Shops.Single(o => o.Id == currentCashierClose.Cashier.IdShop);
                //oShop.String1 = (Convert.ToInt32(oShop.String1) + 1).ToString();

                // ACTUALIZAR EL FOLIO Y EL IdCasherClose
                oInvOperations.UpdateFolioAndIdCasherInvoiceGeneral(currentCashierClose.Id);
                
                #endregion
            }

            if (!isOk && !isCashierVerification)
            {
                try
                {
                    const string templatePath = "MailTemplate/cashier-close-notification-mail.xslt";
                    const string imagePath = "MailTemplate/Images/";

                    AlternateView htmlBody = MailTemplateHelper.RetrieveCashierCloseNotOkNotificationBody(
                        templatePath, imagePath, currentCashierClose);

                    string adminMail = Properties.Settings.Default.MailAdminAddress;

                    MailTemplateHelper.SendMessage(
                        string.Format("Cierre de Caja con descuadre. {0}, CNV: {1}, No. {2}",
                                      currentCashierClose.Cashier.Shop.CountryName,
                                      currentCashierClose.Cashier.Shop.Name, currentCashierClose.Consecutive), htmlBody,
                        currentCashierClose.Cashier.Shop.Company.Email2, string.Empty, adminMail);

                    currentCashierClose.NotifySent = true;
                }
                catch (Exception ex)
                {
                    currentCashierClose.NotifySent = false;
                    Logger.Error("Un error ha ocurrido durante el envio del Correo de Notificación: ", ex);
                    DialogHelper.ShowWarningInfo(this, "Un error ha ocurrido durante el envio del Correo de Notificación: " + ex.Message);
                }
            }

            dc.SubmitChanges();

            Cursor = Cursors.Default;
        }

        private void PrintCloseCashier_Click(object sender, EventArgs e)
        {
            try
            {
                LocalReport saleReport = new LocalReport
                                             {
                                                 ReportEmbeddedResource = "Mega.POS.Report.CashierClose.rdlc"
                                             };
                saleReport.SubreportProcessing += MySubreportEventHandler;

                ReportDataSource ds1 = new ReportDataSource("CashierClose")
                                           {
                                               Value =
                                                   ApplicationHelper.GetPosDataContext().CashierCloses.Where(
                                                       o => o.Id == currentCashierClose.Id)
                                           };
                saleReport.DataSources.Add(ds1);

                saleReport.SetParameters(new ReportParameter("ReportTitle", isCashierVerification ? "Arqueo de Caja" : "Cierre de Caja"));

                var cashier = ApplicationHelper.GetCurrentCashier();

                ReportPrintDocument rp = new ReportPrintDocument(saleReport, true, cashier.TicketPageSize, cashier.TicketPageMargin);
                rp.Print();
            }
            catch (Exception ex)
            {
                Logger.Error("Error en CashierClose.PrintCloseCashier_Click", ex);
                DialogHelper.ShowError(this, "Ha ocurrido un error durante el intento de Impresión del Cierre.", ex);
            }
        }

        void MySubreportEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            if (e.ReportPath == "CashierCloseMoneySubReport")
            {
                ReportDataSource ds2 = new ReportDataSource("CashierCloseMoney")
                                           {
                                               Value =
                                                   ApplicationHelper.GetPosDataContext().CashierCloseMoneys.Where(
                                                       o => o.CashierClose.Id == currentCashierClose.Id).OrderBy(o => o.IdType).ThenBy(o => o.IdOperationCurrency)
                                           };
                e.DataSources.Add(ds2);
            }
            else if (e.ReportPath == "CashierCloseDetailSubReport")
            {
                ReportDataSource ds3 = new ReportDataSource("CashierCloseDetail")
                                           {
                                               Value =
                                                   ApplicationHelper.GetPosDataContext().CashierCloseDetails.Where(
                                                       o => o.CashierClose.Id == currentCashierClose.Id).OrderBy(o => o.IdMovementType).ThenBy(o => o.IdOperationCurrency)
                                           };
                e.DataSources.Add(ds3);
            }

        }

        private void CloseByMoneyDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (CloseByMoneyDataGridView.Columns[e.ColumnIndex].DataPropertyName == "CashierOperationAmountByMoney")
            {
                e.Cancel = true;
                DialogHelper.ShowError(this, "El valor de la columna [Su Importe] no es correcto");
            }
            else
            {
                DialogHelper.ShowError(this, "Un error ha sido detectado en los datos que ha editado. por lo que deben ser revisados antes de continuar.");
            }
        }

        private void CashierClose_Activated(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-MX");
        
        }

    }
}
