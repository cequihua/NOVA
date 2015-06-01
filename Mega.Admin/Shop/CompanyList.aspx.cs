using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using log4net;
using Mega.Admin.Code;
using Mega.Admin.Code.Helpers;
using Mega.Common;
using Mega.Common.Helpers;

namespace Mega.Admin.Shop
{
    public partial class CompanyList : CommonPage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CompanyList));

        readonly AdminDataContext Dc = PortalHelper.GetNewAdminDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeControls();
        }

        private int? ID
        {
            get { return CompaniesGridView.SelectedDataKey != null ? (int?)CompaniesGridView.SelectedDataKey.Value : null; }
        }

        protected void LinqDataSource1_OnContextCreating(object sender, LinqDataSourceContextEventArgs e)
        {
            e.ObjectInstance = PortalHelper.GetNewAdminDataContext();
        }

        protected void CompaniesGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeValues();
            InitializeControls();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    if (ID != null)
                    {
                        UpdateCompany();
                    }
                    else
                    {
                        CreateCompany();
                    }

                    CompaniesGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error salvando la Compañía", ex);

                PortalHelper.ShowMessage(this, DataHelper.GetDefaultAddExceptionMessage(ex));
            }
        }

        private void UpdateCompany()
        {
            var company = Dc.Companies.Where(c => c.Id == ID).Single();

            company.Name = NameTextBox.Text;
            company.LegalCertificate = LegalCertificateTextBox.Text;
            company.FullAddress = FullAddressTextBox.Text;
            company.City = CityTextBox.Text;
            company.CardConfirmationRequeried = CardConfirmationRequeriedCheckBox.Checked;
            company.UsePediment = UsePedimentCheckBox.Checked;
            company.Phone1 = Phone1TextBox.Text;
            company.Phone2 = Phone2TextBox.Text;
            company.Fax = FaxTextBox.Text;
            company.Email = EmailTextBox.Text;
            company.Email2 = Email2TextBox.Text;
            company.Email3 = Email3TextBox.Text;
            company.IdCurrency = int.Parse(CurrencyDropDownList.SelectedValue);
            company.MaxDiffAmountInChangeRate = decimal.Parse(MaxDiffAmountInChangeRateTextBox.Text);
            company.DimDefaultDiscount = decimal.Parse(DimDefaultDiscountTextBox.Text);
            company.MaxAmountPayroll = decimal.Parse(MaxAmountPayrollTextBox.Text);
            company.MaxCashInCashier = decimal.Parse(MaxCashInCashierTextBox.Text);
            company.Disabled = DisabledCheckBox.Checked;

            DataHelper.FillAuditoryValues(company, HttpContext.Current);

            Dc.SubmitChanges();

            ResetValues();
        }

        private void CreateCompany()
        {
            var company = new Company
                        {
                            Id = int.Parse(IdTextBox.Text),
                            Name = NameTextBox.Text,
                            LegalCertificate = LegalCertificateTextBox.Text,
                            FullAddress = FullAddressTextBox.Text,
                            City = CityTextBox.Text,
                            CardConfirmationRequeried = CardConfirmationRequeriedCheckBox.Checked,
                            UsePediment = UsePedimentCheckBox.Checked,
                            Phone1 = Phone1TextBox.Text,
                            Phone2 = Phone2TextBox.Text,
                            Fax = FaxTextBox.Text,
                            Email = EmailTextBox.Text,
                            Email2 = Email2TextBox.Text,
                            Email3 = Email3TextBox.Text,
                            IdCurrency = int.Parse(CurrencyDropDownList.SelectedValue),
                            MaxDiffAmountInChangeRate = decimal.Parse(MaxDiffAmountInChangeRateTextBox.Text),
                            DimDefaultDiscount = decimal.Parse(DimDefaultDiscountTextBox.Text),
                            MaxAmountPayroll = decimal.Parse(MaxAmountPayrollTextBox.Text),
                            MaxCashInCashier = decimal.Parse(MaxCashInCashierTextBox.Text),
                            Disabled = DisabledCheckBox.Checked,
                        };

            DataHelper.FillAuditoryValues(company, HttpContext.Current);

            Dc.Companies.InsertOnSubmit(company);
            Dc.SubmitChanges();

            ResetValues();
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ResetValues();
        }

        private void ResetValues()
        {
            CompaniesGridView.SelectedIndex = -1;
            IdTextBox.Text = string.Empty;
            NameTextBox.Text = string.Empty;
            LegalCertificateTextBox.Text = string.Empty;
            FullAddressTextBox.Text = string.Empty;
            CityTextBox.Text = string.Empty;
            CardConfirmationRequeriedCheckBox.Checked = false;
            UsePedimentCheckBox.Checked = false;
            Phone1TextBox.Text = string.Empty;
            Phone2TextBox.Text = string.Empty;
            FaxTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;
            Email2TextBox.Text = string.Empty;
            Email3TextBox.Text = string.Empty;
            MaxDiffAmountInChangeRateTextBox.Text = string.Empty;
            DimDefaultDiscountTextBox.Text = string.Empty;
            MaxAmountPayrollTextBox.Text = string.Empty;
            MaxCashInCashierTextBox.Text = string.Empty;
            CurrencyDropDownList.SelectedValue = null;
            DisabledCheckBox.Checked = false;

            InitializeControls();
        }

        private void InitializeControls()
        {
            if (ID == null)
            {
                IdTextBox.Enabled = true;
                SaveButton.Text = "Agregar";
                HeaderLabel.Text = "Agregar nueva Compañía";
            }
            else
            {
                IdTextBox.Enabled = false;
                SaveButton.Text = "Guardar";
                HeaderLabel.Text = "Modificar Compañía seleccionada";
            }
        }

        private void InitializeValues()
        {
            var company = Dc.Companies.Where(c => c.Id == ID).Single();

            IdTextBox.Text = company.Id.ToString();
            NameTextBox.Text = company.Name;
            LegalCertificateTextBox.Text = company.LegalCertificate;
            FullAddressTextBox.Text = company.FullAddress;
            CityTextBox.Text = company.City;
            CardConfirmationRequeriedCheckBox.Checked = company.CardConfirmationRequeried;
            UsePedimentCheckBox.Checked = company.UsePediment;
            Phone1TextBox.Text = company.Phone1;
            Phone2TextBox.Text = company.Phone2;
            FaxTextBox.Text = company.Fax;
            EmailTextBox.Text = company.Email;
            Email2TextBox.Text = company.Email2;
            Email3TextBox.Text = company.Email3;
            CurrencyDropDownList.SelectedValue = company.IdCurrency.ToString();
            MaxDiffAmountInChangeRateTextBox.Text = company.MaxDiffAmountInChangeRate.ToString("N");
            DimDefaultDiscountTextBox.Text = company.DimDefaultDiscount.ToString("N");
            MaxAmountPayrollTextBox.Text = company.MaxAmountPayroll.ToString("N");
            MaxCashInCashierTextBox.Text = company.MaxCashInCashier.ToString("N");
            DisabledCheckBox.Checked = company.Disabled;

        }

        protected void IdCustomValidator_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            var company = Dc.Companies.Where(c => c.Id == int.Parse(IdTextBox.Text) && int.Parse(IdTextBox.Text) != ID).FirstOrDefault();

            if (company == null)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
                PortalHelper.ShowMessage(this, IdCustomValidator.ErrorMessage);
            }
        }

        protected void GeneralCustomValidator_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            var sb = new StringBuilder();

            float credit;
            if (string.IsNullOrWhiteSpace(MaxDiffAmountInChangeRateTextBox.Text) || !float.TryParse(MaxDiffAmountInChangeRateTextBox.Text, out credit))
                sb.AppendLine("El campo [Máximo ajuste en Tickets por Tasa de Cambio] es requerido y debe ser de tipo Numérico/Moneda.");

            if (string.IsNullOrWhiteSpace(DimDefaultDiscountTextBox.Text) || !float.TryParse(DimDefaultDiscountTextBox.Text, out credit))
                sb.AppendLine("El campo [% Descuento por defecto para Dims] es requerido y debe ser de tipo Numérico/Moneda.");

            if (string.IsNullOrWhiteSpace(MaxAmountPayrollTextBox.Text) || !float.TryParse(MaxAmountPayrollTextBox.Text, out credit))
                sb.AppendLine("El campo [Compras Mensuales máximas por Nómina de Empleado] es requerido y debe ser de tipo Numérico/Moneda.");

            if (string.IsNullOrWhiteSpace(MaxCashInCashierTextBox.Text) || !float.TryParse(MaxCashInCashierTextBox.Text, out credit))
                sb.AppendLine("El campo [Máximo Efectivo Total permitido en Caja] es requerido y debe ser de tipo Numérico/Moneda.");

            if (sb.Length > 0)
            {
                sb.Insert(0, "Antes de continuar debe corregir los siguientes errores: ");
                GeneralCustomValidator.ErrorMessage = sb.ToString();

                args.IsValid = false;
                PortalHelper.ShowMessage(this, GeneralCustomValidator.ErrorMessage.Replace("\r\n", "-"));
                return;
            }

            args.IsValid = true;
        }
    }
}