using System;
using System.Transactions;
using System.Windows.Forms;
using log4net;
using Mega.Common;
using Mega.Common.Enum;
using Mega.Common.Helpers;
using Mega.POS.Helper;
using System.Linq;
using Mega.POS.Properties;
using Mega.POS.Report;
using Microsoft.Reporting.WinForms;

namespace Mega.POS.Movement
{
    public partial class MovementAdd : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MovementAdd));

        AdminDataContext dc = ApplicationHelper.GetPosDataContext();
        private string authorizedBy;
        private MoneyMovement currentMovement;
        private MovementType? movementTypeToDo;

        public MovementAdd()
        {
            InitializeComponent();
        }

        public MovementAdd(MovementType movementTypeToDo)
            : this()
        {
            this.movementTypeToDo = movementTypeToDo;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MovementAdd_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

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
                    LoadDataCombo();

                    if (movementTypeToDo != null)
                    {
                        TypeComboBox.SelectedValue = (int)movementTypeToDo;
                        TypeComboBox.Enabled = false;
                        OperationGroupBox.Text = "La Opearción seleccionada es requerida";

                        AmountTextBox.Focus();
                        ActiveControl = AmountTextBox;
                    }
                }
                else
                {
                    Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error en MovementAdd_Load", ex);
                DialogHelper.ShowError(this, "Ha ocurrido un error inesperado durante el proceso de carga del formulario", ex);
            }
        }

        private bool VerifyAuthorization()
        {
            if (movementTypeToDo == MovementType.CashierOpen || ApplicationHelper.IsCurrentUserInRole(Constant.SupervisorOrMore))
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

        private void LoadDataCombo()
        {
            CurrencyComboBox.ValueMember = "Id";
            CurrencyComboBox.DisplayMember = "Name";
            CurrencyComboBox.DataSource = DataHelper.GetUDCCurrencies(dc).Select(c => new { c.Id, Name = c.Code + " " + c.Name });
            CurrencyComboBox.SelectedValue = DataHelper.GetDefaultCurrencyShop(dc, Settings.Default.CurrentShop);

            TypeComboBox.ValueMember = "Id";
            TypeComboBox.DisplayMember = "Name";
            TypeComboBox.DataSource = DataHelper.GetUDCMovementType(dc).Where(t => t.Optional2 == "1");
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsAllowedMovement())
                {
                    decimal amount;
                    if (string.IsNullOrWhiteSpace(AmountTextBox.Text) ||
                        !decimal.TryParse(AmountTextBox.Text, out amount))
                    {
                        DialogHelper.ShowError(this, "El campo [Importe] es requerido y debe ser de tipo numerico");
                    }
                    else
                    {
                        var movType = DataHelper.GetUDCItemRow(dc, Convert.ToInt32(TypeComboBox.SelectedValue));

                        if (Convert.ToInt32(movType.Optional1) == -1) //Es una operacion de extraccion
                        {
                            decimal cash = DataHelper.GetCurrentCash(dc, Convert.ToInt32(CurrencyComboBox.SelectedValue), new Guid(Settings.Default.CurrentCashier));

                            if (amount > cash)
                            {
                                DialogHelper.ShowError(this,
                                                       string.Format(
                                                           "El efectivo en Caja para la Moneda de la Extracción es de {0} y usted está intentando extraer {1} que excede dicho monto",
                                                           cash.ToString("N"), amount.ToString("N")));
                                return;
                            }
                        }

                        if (DialogHelper.ShowWarningQuestion(this, "¿Está seguro que desea agregar este Movimiento?") ==
                            DialogResult.Yes)
                        {
                            Cursor = Cursors.WaitCursor;
                            using (TransactionScope trans = new TransactionScope())
                            {
                                int sign = DataHelper.GetOperationSign(dc, Convert.ToInt32(TypeComboBox.SelectedValue));
                                decimal changeRate = DataHelper.GetChangeRate(dc,
                                                                              Convert.ToInt32(
                                                                                  CurrencyComboBox.SelectedValue),
                                                                              Settings.Default.CurrentShop);

                                currentMovement = new MoneyMovement
                                                      {
                                                          Id = Guid.NewGuid(),

                                                          Consecutive = GetNextConsecutive(),
                                                          IdType = Convert.ToInt32(TypeComboBox.SelectedValue),
                                                          IdCashier = new Guid(Settings.Default.CurrentCashier),
                                                          IdOperationCurrency =
                                                              Convert.ToInt32(CurrencyComboBox.SelectedValue),
                                                          ChangeRate = changeRate,
                                                          OperationAmount = amount * sign,
                                                          Amount = Math.Round(amount * changeRate * sign),
                                                          IsCanceling = false,
                                                          AddedDate = DateTime.Now,
                                                          IsManual = true,
                                                          //IdOperation = null,
                                                          Notes = NoteTextBox.Text,
                                                          ModifiedBy = ApplicationHelper.GetCurrentUser(),
                                                          SupervisedBy = authorizedBy
                                                      };

                                dc.MoneyMovements.InsertOnSubmit(currentMovement);
                                dc.SubmitChanges();

                                trans.Complete();
                            }

                            try
                            {
                                Logger.InfoFormat("Movimiento de Caja Insertado. No. {0}, Tipo. {1}", currentMovement.Consecutive, currentMovement.TypeName);

                                PrintMovement();
                            }
                            catch (Exception ex)
                            {
                                DialogHelper.ShowError(this, "Un error inesperado ocurrio durante la Impresión del Comprobante de Movimiento de Caja");
                                Logger.Error("Error imprimiendo Comprobante de Movimiento de Caja con numero: " + currentMovement.Consecutive, ex);
                            }

                            Cursor = Cursors.Default;

                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //DataHelper.RollbackTransaction(dc);
                Cursor = Cursors.Default;
                //dc = ApplicationHelper.GetFreePosDataContext();

                DialogHelper.ShowError(this, DataHelper.GetDefaultAddExceptionMessage(ex));

                Logger.Error("Error insertardo Movimiento de Caja", ex);
            }
        }

        private bool IsAllowedMovement()
        {
            bool cashierIsClosed = DataHelper.CashierIsClosed(dc, Settings.Default.CurrentCashier);
            int currentMovementType = Convert.ToInt32(TypeComboBox.SelectedValue);

            if (cashierIsClosed && currentMovementType != (int)MovementType.CashierOpen)
            {
                DialogHelper.ShowWarningInfo(this, "La caja está Cerrada. Primero debe efectuar la apertura de la misma.");
                return false;
            }

            if (!cashierIsClosed && currentMovementType == (int)MovementType.CashierOpen)
            {
                DialogHelper.ShowWarningInfo(this, "La caja ya se encuentra Abierta. Puede efectuar cualquier otro tipo de Movimiento, menos Apertura.");
                return false;
            }

            try
            {
                ApplicationHelper.VerifyEnviromentConditions(this, dc);
            }
            catch (Exception ex)
            {
                const string msg = "No se puede agregar el Movimiento de Caja porque no ha pasado uno de los chequeos requeridos.";

                DialogHelper.ShowError(this, msg, ex);
                Logger.Error(msg, ex);

                return false;
            }
            
            return true;
        }

        private void PrintMovement()
        {
            var saleReport = new LocalReport
                                         {
                                             ReportEmbeddedResource = "Mega.POS.Report.MoneyMovement.rdlc"
                                         };

            var ds1 = new ReportDataSource("MoneyMovement")
                                       {
                                           Value =
                                               ApplicationHelper.GetPosDataContext().MoneyMovements.Where(
                                                   m => m.Id == currentMovement.Id)
                                       };

            saleReport.DataSources.Add(ds1);

            var cashier = ApplicationHelper.GetCurrentCashier();

            var rp = new ReportPrintDocument(saleReport, true, cashier.TicketPageSize, cashier.TicketPageMargin);

            rp.Print();
        }

        private string GetNextConsecutive()
        {
            //var localDC = ApplicationHelper.GetFreePosDataContext();

            return DataHelper.GetNextSequence(dc, SequenceId.MoneyMovementConsecutive).ToString();
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            try
            {
                PrintMovement();
                PrintButton.Enabled = true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error en CashierClose.PrintCloseCashier_Click", ex);
                DialogHelper.ShowError(this, "Ha ocurrido un error durante el intento de Impresión del Cierre.", ex);
            }
        }

        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MovementAdd_Activated(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-MX");
        
        }
    }
}
