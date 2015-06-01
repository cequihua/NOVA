using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using log4net;
using Mega.Common;
using Mega.Common.Enum;
using Mega.Common.Helpers;
using Mega.POS.Helper;

namespace Mega.POS.Operation
{
    public partial class GeneralPay : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Pay));
     
        private Guid operationId;
        private decimal amountToPay;
        private Dim dimToPay;
        private decimal byPay;
        private AdminDataContext dc;
        private int operationCurrencyId;
        private MovementType cashType;
        MovementType cardType;
        private MovementType currencyType;

        public GeneralPay(AdminDataContext dc, Guid operationId, decimal amountToPay, Dim dimToPay, int operationCurrencyId,
            MovementType cashType, MovementType cardType, MovementType currencyType)
        {
            this.dc = dc;
            this.operationId = operationId;
            this.amountToPay = amountToPay;
            this.dimToPay = dimToPay;
            this.operationCurrencyId = operationCurrencyId;
            this.cashType = cashType;
            this.cardType = cardType;
            this.currencyType = currencyType;

            InitializeComponent();
        }

        private void Pay_Load(object sender, EventArgs e)
        {
            KeyPreview = true; 

            try
            {
                ApplicationHelper.ConfigureGridView(payDataGridView);
                LoadCurrencyCombo();
                LoadOperacionData();
                LoadPayGridView();

                CashReceivedTextBox.Focus();
                ActiveControl = CashReceivedTextBox;

            }
            catch (Exception ex)
            {
                Logger.Error("Error al cargar el formulario de Pago de Ventas", ex);
                DialogHelper.ShowError(this, "Error inesperado al cargar el formulario de Pago de Ventas", ex);
            }
        }

        
        private void LoadCurrencyCombo()
        {
            CurrencyComboBox.ValueMember = "Id";
            CurrencyComboBox.DisplayMember = "Name";
            CurrencyComboBox.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY;
            var curr = DataHelper.GetUDCCurrenciesByComboPay(dc, operationCurrencyId);
            CurrencyComboBox.DataSource = curr;
            
            if (curr.Count > 2)
            {
                CurrencyComboBox.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY;
            }
        }

        private void LoadOperacionData()
        {
            DimNameLabel.Text = string.Format("{0} - {1}", dimToPay.Id, dimToPay.FullName);

            DimInfoLabel2.Text = string.Format("Crédito actual: {0}, Fecha: {1}",
                                              dimToPay.CreditAmount.ToString("N"),
                                              dimToPay.CreditAmountDate.ToString("d"));

            AmountOperationLabel.Text = amountToPay.ToString("N");
            CurrencyOperationLabel.Text = DataHelper.GetUDCItemRow(dc, operationCurrencyId).Name;

            UpdateCurrencyPayValues();
        }

        private void LoadPayGridView()
        {
            payDataGridView.DataSource = dc.Operation_Pays.Where(p => p.IdOperation == operationId);

            RefreshAmounts();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            dc.Operation_Pays.DeleteAllOnSubmit(dc.Operation_Pays.Where(p => p.IdOperation == operationId));
            dc.SubmitChanges();

            Close();
        }

        private void AddCashButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (byPay == 0)
                {
                    DialogHelper.ShowWarningInfo(this, "Ya ha alcanzado el importe total de la Operación");
                }
                else
                {
                    decimal toPay = decimal.Parse(CashReceivedTextBox.Text);

                    dc.Operation_Pays.InsertOnSubmit(new Operation_Pay
                    {
                        Id = Guid.NewGuid(),
                        IdOperation = operationId,
                        IdType = (int)cashType,
                        IdOperationCurrency = operationCurrencyId,
                        ChangeRate = (decimal)1.0000,
                        OperationAmount = Math.Min(toPay, byPay),
                        Amount = Math.Min(toPay, byPay)
                    });

                    dc.SubmitChanges();

                    LoadPayGridView();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error al agregar Venta en Efectivo", ex);
                DialogHelper.ShowError(this, "Error inesperado procesando la Venta en Efectivo", ex);
            }
        }

        private void RefreshAmounts()
        {
            decimal amountPayed = dc.Operation_Pays.Where(p => p.IdOperation == operationId).Count() > 0 ?  dc.Operation_Pays.Where(p => p.IdOperation == operationId).Sum(p => p.Amount) : 0;
            byPay = Math.Round(amountToPay - amountPayed, 2);

            decimal maxDiffChangeRate = ApplicationHelper.GetCurrentShop().Company.MaxDiffAmountInChangeRate;

            bool allowConfirmPay = Math.Abs(byPay) <= maxDiffChangeRate; 

            AddCashButton.Enabled = !allowConfirmPay;
            AddCardButton.Enabled = !allowConfirmPay;
            AddCurrencyPayButton.Enabled = !allowConfirmPay;

            PayAndConfirmButton.Enabled = allowConfirmPay;

            CashReceivedTextBox.Text = "0";
            CardAmountTextBox.Text = byPay.ToString("N");

            ByPayLabel.Text = string.Format("{0}  {1}", byPay.ToString("N"), allowConfirmPay ? "!!AJUSTE POR REDONDEO!!" : string.Empty) ;

            UpdateCurrencyPayValues();
        }

        private void PayAndConfirmButton_Click(object sender, EventArgs e)
        {
            if (byPay != 0)
            {
                dc.Operation_Pays.InsertOnSubmit(new Operation_Pay
                {
                    Id = Guid.NewGuid(),
                    IdOperation = operationId,
                    IdType = (int)MovementType.AdjustByRound,
                    IdOperationCurrency = operationCurrencyId,
                    ChangeRate = (decimal)1.0000,
                    OperationAmount = byPay,
                    Amount = byPay
                });

                dc.SubmitChanges();
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void CashReceivedTextBox_Validating(object sender, CancelEventArgs e)
        {
            decimal toPay = 0;
            if (CashReceivedTextBox.Text.Trim() == string.Empty || !decimal.TryParse(CashReceivedTextBox.Text, out toPay))
            {
                DialogHelper.ShowError(this, "El valor [Recibido] es requerido y debe ser numérico");
                e.Cancel = true;
            }

            CashChangeTextBox.Text = (byPay > toPay ? 0 : toPay - byPay).ToString("N");
        }

        private void CurrencyReceivedTextBox_Validating(object sender, CancelEventArgs e)
        {
            decimal received = 0;
            if (CurrencyReceivedTextBox.Text.Trim() == string.Empty || !decimal.TryParse(CurrencyReceivedTextBox.Text, out received))
            {
                DialogHelper.ShowError(this, "El valor [Recibido] es requerido y debe ser numérico");
                e.Cancel = true;
            }

            decimal changerate = Convert.ToDecimal(CurrencyChangeRateTextBox.Text);
            decimal receivedConverted = Math.Round(received*changerate, 2);
            decimal change = byPay > receivedConverted ? 0: receivedConverted - byPay;

            CurrencyConvertionReceivedTextBox.Text = receivedConverted.ToString("N");

            CurrencyChangeTextBox.Text = DataHelper.IsActiveRoundByFive(dc, operationCurrencyId)
                                             ? ToolHelper.RoundByFiveByDefault(change).ToString("N")
                                             : change.ToString("N");
        }

        private void DeleteEntryButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (payDataGridView.SelectedRows.Count == 0)
                {
                    DialogHelper.ShowInformation(this, "Debe exitir una fila seleccionada");
                }
                else
                {
                    dynamic item = payDataGridView.SelectedRows[0].DataBoundItem;

                    dc.Operation_Pays.DeleteOnSubmit(item);
                    dc.SubmitChanges();

                    LoadPayGridView();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error cargando el Formulario OperationEdit o refrescando los datos en Listado", ex);
                DialogHelper.ShowError(this, "Error inesperado.", ex);
            }
        }

        private void CashReceivedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                AddCashButton.Focus();
            }
        }

        private void AddCardButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (byPay == 0)
                {
                    DialogHelper.ShowWarningInfo(this, "Ya ha alcanzado el importe total de la Operación");
                    return;
                }

                decimal toPay = decimal.Parse(CardAmountTextBox.Text);

                if (toPay > byPay)
                {
                    DialogHelper.ShowWarningInfo(this, "No puede pagar mas de lo pendiente.");
                    return;
                }

                if (CardNumberTextBox.Text.Length < 19)
                {
                    DialogHelper.ShowWarningInfo(this, "Su Número de Tarjeta no es correcto. Debe tener 16 dígitos.");
                    return;
                }

                if (CardValidityTextBox.Text.Length < 5 )
                {
                    DialogHelper.ShowWarningInfo(this, "Los datos de Validez de la Tarjeta no son correctos. Debe ser MM/AA.");
                    return;
                }
                
                int month = Convert.ToInt32(CardValidityTextBox.Text.Substring(0, 2));
                int year = Convert.ToInt32(CardValidityTextBox.Text.Substring(3, 2));
                int currentMonth = Convert.ToInt32(DateTime.Today.Month);
                int currentYear = Convert.ToInt32(DateTime.Today.Year - 2000);

                if (month < 1 || month > 12 || (year * 100 + month) < (currentYear * 100 + currentMonth))
                {
                    DialogHelper.ShowWarningInfo(this, "Los datos de Validez de la Tarjeta no son correctos. Debe ser MM/AA y además no pueden ser menor al mes/año actual.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(CardAuthorizationTextBox.Text))
                {
                    DialogHelper.ShowWarningInfo(this, "El Número de Autorización es Requerido.");
                    return;
                }

                dc.Operation_Pays.InsertOnSubmit(new Operation_Pay
                {
                    Id = Guid.NewGuid(),
                    IdOperation = operationId,
                    IdType = (int)cardType,
                    IdOperationCurrency = operationCurrencyId,
                    ChangeRate = (decimal)1.0000,
                    OperationAmount = toPay,
                    Amount = toPay,
                    CardNumber = CardNumberHiddenTextBox.Text,
                    CardAuthNumber = CardAuthorizationTextBox.Text,
                    CardValidity = CardValidityTextBox.Text,
                    IsCreditCard = CreditcardRadioButton.Checked
                });

                dc.SubmitChanges();

                CardNumberTextBox.Text = string.Empty;
                CardNumberHiddenTextBox.Text = string.Empty;
                CardValidityTextBox.Text = string.Empty;
                CardAuthorizationTextBox.Text = string.Empty;

                LoadPayGridView();
            }
            catch (Exception ex)
            {
                Logger.Error("Error al cargar el formulario de Pago de Ventas", ex);
                DialogHelper.ShowError(this, "Error inesperado al Ejecutar el pago por Tarjeta", ex);
            }
        }

        private void CardAmountTextBox_Validating(object sender, CancelEventArgs e)
        {
            decimal toPay;
            if (CardAmountTextBox.Text.Trim() == string.Empty || !decimal.TryParse(CardAmountTextBox.Text, out toPay))
            {
                DialogHelper.ShowError(this, "El valor [A Pagar] es requerido y debe ser numérico");
                e.Cancel = true;
            }
        }
        
        private void CardNumberTextBox_Validating(object sender, CancelEventArgs e)
        {
            CardNumberHiddenTextBox.Text = "**** **** **** " + CardNumberTextBox.Text.Substring(15);
            CardNumberHiddenTextBox.Location = CardNumberTextBox.Location;
            CardNumberTextBox.Visible = false;
            CardNumberHiddenTextBox.Visible = true;
            Refresh();
        }

        private void CardNumberHiddenTextBox_Enter(object sender, EventArgs e)
        {
            CardNumberHiddenTextBox.Visible = false;
            CardNumberTextBox.Visible = true;
            CardNumberTextBox.Focus();
        }

        private void Pay_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12: 
                    PayAndConfirmButton.PerformClick();
                    break;
                case Keys.F5:
                    payTabControl.SelectTab(tabPageCash);
                    break;
                case Keys.F6:
                    payTabControl.SelectTab(tabPageCard);
                    break;
                case Keys.F8:
                    payTabControl.SelectTab(tabPageDivisa);
                    break;
            }
        }

        private void CurrencyReceivedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int) Keys.Enter)
            {
                AddCurrencyPayButton.Focus();
            }
        }

        private void CurrencyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateCurrencyPayValues();
            }
            catch (Exception ex)
            {
                Logger.Error("Error en Pay.CurrencyComboBox_SelectedIndexChanged", ex);
                DialogHelper.ShowError(this, "Error inesperado realizando las operaciones internas necesarias. verifique los Tipos de Cambio en las UDC. ", ex);
            }
        }

        private void UpdateCurrencyPayValues()
        {
            int currency = Convert.ToInt32(CurrencyComboBox.SelectedValue); 

            if (currency == Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY)
            {
                CurrencyChangeRateTextBox.Text = "0";
                CurrencyToPayLabel.Text = "A Pagar:";
                CurrencyToPayTextBox.Text = "0";
                CurrencyReceivedLabel.Text = "Recibido: ";
                CurrencyReceivedTextBox.Text = "0";
                CurrencyConvertionReceivedTextBox.Text = "0";
                //CurrencyChangeTextBox.Text = "0";
            }
            else
            {
                string code = CurrencyComboBox.Text.Substring(0, 3); 
                decimal changeRate = DataHelper.GetChangeRate(dc, currency, ApplicationHelper.GetCurrentShop().IdCurrency);

                CurrencyChangeRateTextBox.Text = changeRate.ToString();
                CurrencyToPayLabel.Text = string.Format("A Pagar {0}:", code);
                decimal currencyToPay = byPay/changeRate;
                CurrencyToPayTextBox.Text = Math.Round(currencyToPay, 2, MidpointRounding.AwayFromZero).ToString("N");
                CurrencyReceivedLabel.Text = string.Format("Recibido {0}:", code);
                CurrencyReceivedTextBox.Text = "0";
                CurrencyConvertionReceivedTextBox.Text = "0";
                //CurrencyChangeTextBox.Text = "0";

                CurrencyReceivedTextBox.Focus();
                ActiveControl = CurrencyReceivedTextBox; 
            }
        }

        private void AddCurrencyPayButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (byPay == 0)
                {
                    DialogHelper.ShowWarningInfo(this, "Ya ha alcanzado el importe total de la Operación");
                }
                else
                {
                    decimal changeRate = Convert.ToDecimal(CurrencyChangeRateTextBox.Text);
                    decimal toPayOperation = decimal.Parse(CurrencyToPayTextBox.Text);
                    
                    decimal amountOperationReceived = decimal.Parse(CurrencyReceivedTextBox.Text);
                    //decimal amountReceived = decimal.Parse(CurrencyConvertionReceivedTextBox.Text);

                    decimal realOperationPaying = Math.Min(toPayOperation,  amountOperationReceived);

                    decimal cash = DataHelper.GetCurrentCash(dc, Convert.ToInt32(operationCurrencyId), new Guid(Properties.Settings.Default.CurrentCashier));
                    decimal change = Convert.ToDecimal(CurrencyChangeTextBox.Text);

                    if (Convert.ToDecimal(CurrencyChangeTextBox.Text) > cash)
                    {
                        DialogHelper.ShowError(this,
                                               string.Format(
                                                   "El efectivo en Caja para la Devolución es de {0} y usted debe devolver {1} por lo que no es posible efectuar el cobro en esta Divisa.",
                                                   cash.ToString("N"), change.ToString("N")));
                        return;
                    }

                    dc.Operation_Pays.InsertOnSubmit(new Operation_Pay
                    {
                        Id = Guid.NewGuid(),
                        IdOperation = operationId,
                        IdType = (int)currencyType,
                        IdOperationCurrency = Convert.ToInt32(CurrencyComboBox.SelectedValue),
                        ChangeRate = changeRate,
                        OperationAmount = realOperationPaying,
                        Amount = realOperationPaying * changeRate, 

                        ReceivedAmountOperation = amountOperationReceived,
                        ChangeAmount = change
                    });

                    dc.SubmitChanges();

                    LoadPayGridView();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error al agregar Venta en Efectivo", ex);
                DialogHelper.ShowError(this, "Error inesperado procesando la Venta en Efectivo", ex);
            }
        }

        private void CardAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                AddCardButton.Focus();
            }
        }

        private void CardNumberTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (CardNumberTextBox.Text.Trim().Length == 0)
            {
                CardNumberTextBox.SelectAll();
            }
        }

        private void CardValidityTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (CardValidityTextBox.Text.Trim().Length == 1)
            {
                CardValidityTextBox.SelectAll();
            }
        }

        private void payTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (payTabControl.SelectedTab == tabPageCash)
            {
                //CashReceivedTextBox.Focus();
                ActiveControl = CashReceivedTextBox;
                CashReceivedTextBox.SelectAll();
            }
            else if (payTabControl.SelectedTab == tabPageCard)
            {
                if (CardNumberTextBox.Visible)
                {
                    ActiveControl = CardNumberTextBox;
                    CardNumberTextBox_MouseClick(null, null);
                }
                else
                {
                    ActiveControl = CardNumberHiddenTextBox;
                }
            }
            
            if (payTabControl.SelectedTab == tabPageDivisa)
            {
                //CashReceivedTextBox.Focus();
                ActiveControl = CurrencyReceivedTextBox;
                CurrencyReceivedTextBox.SelectAll();
            }
            
        }

        private void GeneralPay_Activated(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-MX");
        
        }
    }
}
