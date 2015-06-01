using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using Mega.Admin.Code;
using Mega.Admin.Code.Helpers;
using Mega.Common;
using Mega.Common.Enum;
using Mega.Common.Helpers;

namespace Mega.Admin.Dim
{
    public partial class Dim : CommonPage
    {
        readonly AdminDataContext dc = PortalHelper.GetNewAdminDataContext();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Dim));

        private Common.Dim CurrentClient;

        private int? DimID
        {
            get { return Request["id"] != null ? (int?)int.Parse(Request["id"]) : null; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeControls();
            }
        }

        private void InitializeControls()
        {
            int dimSize = Convert.ToInt32(DataHelper.GetUDCItemRow(dc, Constant.CFG_DIM_MAX_SIZE_UDCITEM_KEY).Optional1);
            IdTextBox.MaxLength = dimSize;
            ValIdTextBox.MaxLength = dimSize;

            SponsorTextBox.MaxLength = dimSize;

            BirthDateLabel.Text = string.Format("F. Nac. ({0}):", ToolHelper.GetDateFormat());
            AddedDateLabel.Text = string.Format("Fecha Alta ({0}):", ToolHelper.GetDateFormat());
            LowDateLabel.Text = string.Format("Fecha Baja ({0}):", ToolHelper.GetDateFormat());

            DateFormat.Value = ToolHelper.GetJSConvertionDateFormat();

            DiscountTextBox.Text = "0.00";

            FillDropDownListsValues();

            TypeDropDownList.SelectedValue = ((int)DimType.DIM).ToString();

            MaxCreditTextBox.Enabled = true;
            MaxConsignableValueTextBox.Enabled = true;
            ConsignableCheckBox.Enabled = true;
            SaleRetentionCheckBox.Enabled = true;
            DisabledCheckBox.Enabled = true;



            if (DimID == null)
            {
                IdTextBox.Enabled = true;
                ValIdPanel.Visible = true;
                SaveButton.Text = "Agregar";
                HeaderLabel.Text = "Agregar nuevo Cliente";
                AddedDateDateTimeTextBox.Text = DateTime.Now.ToString(ToolHelper.GetConvertionDateFormat());

                TaxRetentionCheckBox.Checked = true;

                LowDateTimePanel.Visible = false;

                MaxCreditTextBox.Text = "0";
                MaxConsignableValueTextBox.Text = "0";
                BankDropDownList.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString();
                DiffusionMediaDropDownList.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY.ToString();

                DisabledCheckBox.Visible = false;
                DisabledPanel.Visible = false;

                CreditAmountDateTextBox.Text = DateTime.Today.ToString("d");
                ConsignableAmountDateTextBox.Text = DateTime.Today.ToString("d");
            }
            else
            {
                IdTextBox.Enabled = false;
                ValIdPanel.Visible = false;
                TypeDropDownList.Enabled = false;
                SaveButton.Text = "Guardar";
                HeaderLabel.Text = "Modificar Cliente seleccionada";
                CurrentClient = dc.Dims.Where(d => d.Id == DimID).SingleOrDefault();

                if (CurrentClient == null)
                {
                    throw new Exception("No se encontró un cliente con el Id/Dim actual");
                }

                LowDateTimePanel.Visible = CurrentClient.Disabled;

                InitializeValues();
            }

            FillStates();
            FillPopulations();
        }

        private void FillDropDownListsValues()
        {
            CountryDropDownList.DataSource = DataHelper.GetUDCCountries(dc).ToList();
            CountryDropDownList.DataBind();
            IVAGroupDropDownList.DataSource = DataHelper.GetUDCIVAGroupComboList(dc, "Aplica el de la Tienda");
            IVAGroupDropDownList.DataBind();
            BankDropDownList.DataSource = DataHelper.GetBankComboList(dc);
            BankDropDownList.DataBind();
            DiffusionMediaDropDownList.DataSource = DataHelper.GetUDCDiffusionMediaComboList(dc);
            DiffusionMediaDropDownList.DataBind();
        }

        protected void LinqDataSource1_OnContextCreating(object sender, LinqDataSourceContextEventArgs e)
        {
            e.ObjectInstance = PortalHelper.GetNewAdminDataContext();
        }

        private void UpdateClientData()
        {
            int maritalStatus = Convert.ToInt32(MaritalStatusDropDownList.SelectedValue);
            bool isMarried = maritalStatus == (int)MaritalStatus.Married;

            CurrentClient.Id = int.Parse(IdTextBox.Text);

            if (CurrentClient.IdJDE == null)
            {
                CurrentClient.IdJDE = DataHelper.GetNextSequence(dc, SequenceId.DimIdJDE);
            }

            CurrentClient.IdType = int.Parse(TypeDropDownList.SelectedValue);
            CurrentClient.IdSponsor = string.IsNullOrEmpty(SponsorTextBox.Text)
                                                      ? null
                                                      : (int?)Convert.ToInt32(SponsorTextBox.Text);
            CurrentClient.AddedDate = Convert.ToDateTime(AddedDateDateTimeTextBox.Text);
            CurrentClient.Calification = CalificationTextBox.Text[0];
            CurrentClient.Name = NameTextBox.Text;
            CurrentClient.LastName = LastNameTextBox.Text;
            CurrentClient.MotherMaidenName = MotherMiadenNameTextBox.Text;
            CurrentClient.IsOfficialId = IsOfficialIdCheckBox.Checked;
            CurrentClient.HasAddressProof = HasAddressProofCheckBox.Checked;
            CurrentClient.CoHolder = CoHolderTextBox.Text;

            CurrentClient.SpouseName = isMarried ? SpouseNameTextBox.Text : string.Empty;
            CurrentClient.SpouseLastName = isMarried ? SpouseLastNameTextBox.Text : string.Empty;
            CurrentClient.SpouseMotherMaidenName = isMarried ? SpouseMotherMaidenNameTextBox.Text : string.Empty;

            CurrentClient.CURP = CURPTextBox.Text;
            CurrentClient.TaxRegister = TaxRegisterTextBox.Text;
            CurrentClient.BirthDate = Convert.ToDateTime(BirthDateTextBox.Text);
            CurrentClient.Bank = DataHelper.GetUDCItemRow(dc, Convert.ToInt32(BankDropDownList.SelectedValue));
            CurrentClient.BankAccount = BankAccountTextBox.Text;
            CurrentClient.KeyAccount = KeyAccountTextBox.Text;

            CurrentClient.Sucursal = SucursalTextBox.Text;
            CurrentClient.Reference = ReferenceTextBox.Text;

            CurrentClient.DiscountPercent = Convert.ToDecimal(DiscountTextBox.Text);
            CurrentClient.NeedInvoice = NeedInvoiceCheckBox.Checked;
            CurrentClient.IVAToApply = DataHelper.GetUDCItemRow(dc, Convert.ToInt32(IVAGroupDropDownList.SelectedValue));
            CurrentClient.Sex = DataHelper.GetUDCItemRow(dc, Convert.ToInt32(SexDropDownList.SelectedValue));
            CurrentClient.MaritalStatus = DataHelper.GetUDCItemRow(dc, maritalStatus);
            CurrentClient.TaxRetention = TaxRetentionCheckBox.Checked;
            CurrentClient.Address1 = Address1TextBox.Text;
            CurrentClient.ExteriorNumber = ExteriorNumberTextBox.Text;
            CurrentClient.InteriorNumber = InteriorNumberTextBox.Text;
            CurrentClient.Address2 = Address2TextBox.Text;
            CurrentClient.Address3 = Address3TextBox.Text;
            CurrentClient.Population = DataHelper.GetUDCItemRow(dc,
                                                                Convert.ToInt32(PopulationDropDownList.SelectedValue));

            CurrentClient.DiffusionMedia = DataHelper.GetUDCItemRow(dc, Convert.ToInt32(DiffusionMediaDropDownList.SelectedValue));

            CurrentClient.State = DataHelper.GetUDCItemRow(dc, Convert.ToInt32(StateDropDownList.SelectedValue));
            CurrentClient.Country = DataHelper.GetUDCItemRow(dc, Convert.ToInt32(CountryDropDownList.SelectedValue));
            CurrentClient.CP = CPTextBox.Text;
            CurrentClient.PhoneCell = PhoneCellTextBox.Text;
            CurrentClient.PhoneOffice = PhoneOfficeTextBox.Text;
            CurrentClient.PhoneHome = PhoneHomeTextBox.Text;
            CurrentClient.Email = EmailTextBox.Text;
            CurrentClient.MaxCreditAmount = Convert.ToDecimal(MaxCreditTextBox.Text);
            CurrentClient.MaxConsignableAmount = Convert.ToDecimal(MaxConsignableValueTextBox.Text);
            CurrentClient.Consignable = ConsignableCheckBox.Checked;

            if (DimID == null)
            {
                CurrentClient.MaxConsignableAmount = Convert.ToDecimal(MaxConsignableValueTextBox.Text);
                CurrentClient.Consignable = ConsignableCheckBox.Checked;
                CurrentClient.ConsignableAmount = 0;
                CurrentClient.ConsignableAmountDate = DateTime.Now;
                CurrentClient.PointAmount = 0;
                CurrentClient.PointAmountDate = DateTime.Now;
                CurrentClient.CreditAmountDate = DateTime.Now;
            }

            if (DisabledCheckBox.Checked && CurrentClient.Disabled == false)
            {
                CurrentClient.LowDate = DateTime.Today;
            }

            CurrentClient.Notes = NotesTextBox.Text;
            CurrentClient.SaleRetention = SaleRetentionCheckBox.Checked;
            CurrentClient.Disabled = DisabledCheckBox.Checked;

            DataHelper.FillAuditoryValues(CurrentClient, HttpContext.Current);

        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    if (DimID == null)
                    {
                        CurrentClient = new Common.Dim();

                        UpdateClientData();

                        dc.Dims.InsertOnSubmit(CurrentClient);

                        dc.SubmitChanges();
                    }
                    else
                    {
                        CurrentClient = dc.Dims.Where(c => c.Id == DimID).Single();

                        UpdateClientData();

                        dc.SubmitChanges();
                    }

                    PortalHelper.ShowMessage(this, "Los datos han sido guardados correctamente");
                }

            }
            catch (Exception ex)
            {
                Logger.Error("Error salvando el Cliente", ex);

                PortalHelper.ShowMessage(this, DataHelper.GetDefaultAddExceptionMessage(ex));
            }
        }


        private void InitializeValues()
        {
            IdTextBox.Text = CurrentClient.Id.ToString();
            IdJDETextBox.Text = CurrentClient.IdJDE.ToString();
            TypeDropDownList.SelectedValue = CurrentClient.IdType.ToString();
            SponsorTextBox.Text = CurrentClient.IdSponsor.ToString();
            SponsorTextBox_TextChanged(null, null);
            AddedDateDateTimeTextBox.Text = CurrentClient.AddedDate.ToString(ToolHelper.GetConvertionDateFormat());
            CalificationTextBox.Text = CurrentClient.Calification.ToString();
            NameTextBox.Text = CurrentClient.Name;
            LastNameTextBox.Text = CurrentClient.LastName;
            MotherMiadenNameTextBox.Text = CurrentClient.MotherMaidenName;
            IsOfficialIdCheckBox.Checked = CurrentClient.IsOfficialId;
            HasAddressProofCheckBox.Checked = CurrentClient.HasAddressProof;
            CoHolderTextBox.Text = CurrentClient.CoHolder;
            SpouseNameTextBox.Text = CurrentClient.SpouseName;
            SpouseLastNameTextBox.Text = CurrentClient.SpouseLastName;
            SpouseMotherMaidenNameTextBox.Text = CurrentClient.SpouseMotherMaidenName;
            CURPTextBox.Text = CurrentClient.CURP;
            TaxRegisterTextBox.Text = CurrentClient.TaxRegister;
            BirthDateTextBox.Text = CurrentClient.BirthDate.ToString(ToolHelper.GetConvertionDateFormat());
            BirthDateTextBox.Text = CurrentClient.BirthDate.ToString(ToolHelper.GetConvertionDateFormat());
            LowDateTimeTextBox.Text = CurrentClient.LowDate != null ? ((DateTime)CurrentClient.LowDate).ToString(ToolHelper.GetConvertionDateFormat()) : DateTime.Today.ToString(ToolHelper.GetConvertionDateFormat());
            BankDropDownList.SelectedValue = CurrentClient.IdBank.ToString();

            KeyAccountTextBox.Text = CurrentClient.KeyAccount;
            KeyAccountValidationTextBox.Text = CurrentClient.KeyAccount;
            BankAccountTextBox.Text = CurrentClient.BankAccount;
            BankAccountValidationTextBox.Text = CurrentClient.BankAccount;
            SucursalTextBox.Text = CurrentClient.Sucursal;
            ReferenceTextBox.Text = CurrentClient.Reference;

            DiscountTextBox.Text = CurrentClient.DiscountPercent.ToString("00.00");
            NeedInvoiceCheckBox.Checked = CurrentClient.NeedInvoice;
            IVAGroupDropDownList.SelectedValue = CurrentClient.IdIVAGroup.ToString();
            SexDropDownList.SelectedValue = CurrentClient.IdSex.ToString();
            MaritalStatusDropDownList.SelectedValue = CurrentClient.IdMaritalStatus.ToString();
            CurrentClient.TaxRetention = TaxRetentionCheckBox.Checked;
            Address1TextBox.Text = CurrentClient.Address1;
            ExteriorNumberTextBox.Text = CurrentClient.ExteriorNumber;
            InteriorNumberTextBox.Text = CurrentClient.InteriorNumber;
            Address2TextBox.Text = CurrentClient.Address2;
            Address3TextBox.Text = CurrentClient.Address3;
            DiffusionMediaDropDownList.SelectedValue = CurrentClient.IdDiffusionMedia.ToString();
            CountryDropDownList.SelectedValue = CurrentClient.IdCountry.ToString();

            StateDropDownList.SelectedValue = CurrentClient.IdState.ToString();
            PopulationDropDownList.SelectedValue = CurrentClient.IdPopulation.ToString();

            CPTextBox.Text = CurrentClient.CP;
            PhoneCellTextBox.Text = CurrentClient.PhoneCell;
            PhoneOfficeTextBox.Text = CurrentClient.PhoneOffice;
            PhoneHomeTextBox.Text = CurrentClient.PhoneHome;
            EmailTextBox.Text = CurrentClient.Email;
            MaxCreditTextBox.Text = CurrentClient.MaxCreditAmount.ToString();
            CreditAmountTextBox.Text = CurrentClient.CreditAmount.ToString("N");
            CreditAmountDateTextBox.Text = CurrentClient.CreditAmountDate.ToString("d");
            MaxConsignableValueTextBox.Text = CurrentClient.MaxConsignableAmount.ToString();
            ConsignableAmountTextBox.Text = CurrentClient.ConsignableAmount.ToString("N");
            ConsignableAmountDateTextBox.Text = CurrentClient.ConsignableAmountDate.ToString("d");
            ConsignableCheckBox.Checked = CurrentClient.Consignable;
            PointTextBox.Text = CurrentClient.PointAmount.ToString("N");
            PointDateTextBox.Text = CurrentClient.PointAmountDate.ToString("d");
            NotesTextBox.Text = CurrentClient.Notes;
            SaleRetentionCheckBox.Checked = CurrentClient.SaleRetention;
            DisabledCheckBox.Checked = CurrentClient.Disabled;

        }

        protected void IdCustomValidator_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            var dim = dc.Dims.Where(c => c.Id == int.Parse(IdTextBox.Text) && int.Parse(IdTextBox.Text) != DimID).FirstOrDefault();

            if (dim == null)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
                PortalHelper.ShowMessage(this, IdCustomValidator.ErrorMessage);
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dim/DimList.aspx");
        }

        protected void CountryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStates();
            FillPopulations();
        }

        private void FillStates()
        {
            if (CurrentClient != null && CurrentClient.IdCountry != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY)
            {
                StateDropDownList.DataSource = DataHelper.GetStateComboList(dc, CurrentClient.Country.Code);
            }
            else if (CountryDropDownList.SelectedItem != null)
            {
                var id = int.Parse(CountryDropDownList.SelectedItem.Value);

                var country = DataHelper.GetUDCItemRow(dc, id);

                StateDropDownList.DataSource = DataHelper.GetStateComboList(dc, country.Code);
            }
            else
            {
                StateDropDownList.DataSource = DataHelper.GetEmptyList(dc);
            }

            StateDropDownList.DataBind();
        }

        private void FillPopulations()
        {
            if (CurrentClient != null && CurrentClient.IdState != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY)
            {
                PopulationDropDownList.DataSource = DataHelper.GetPopulationComboList(dc, CurrentClient.IdState);
            }
            else if (StateDropDownList.SelectedItem != null)
            {
                int tmpValue;

                if (int.TryParse(StateDropDownList.SelectedValue, out tmpValue))
                {
                    PopulationDropDownList.DataSource = DataHelper.GetPopulationComboList(dc, tmpValue);
                }
            }
            else
            {
                PopulationDropDownList.DataSource = DataHelper.GetEmptyList(dc);
            }

            PopulationDropDownList.DataBind();
        }

        protected void StateDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillPopulations();
        }

        protected void GeneralCustomValidator_OnServerValidate(object source, ServerValidateEventArgs args)
        {

            var sb = new StringBuilder();

            float credit;
            if (string.IsNullOrWhiteSpace(MaxCreditTextBox.Text) || !float.TryParse(MaxCreditTextBox.Text, out credit))
                sb.AppendLine("El campo Máximo Crédito es requerido y debe ser de tipo Numérico/Moneda.");

            if (string.IsNullOrWhiteSpace(MaxConsignableValueTextBox.Text) || !float.TryParse(MaxConsignableValueTextBox.Text, out credit))
                sb.AppendLine("El campo Máximo Valor Consignable es requerido y debe ser de tipo Numérico/Moneda.");

            DateTime date = DateTime.Today;
            if (string.IsNullOrWhiteSpace(BirthDateTextBox.Text) || !DateTime.TryParse(BirthDateTextBox.Text, out date))
                sb.AppendLine("El campo Fecha Nacimiento es requerido y debe ser de tipo Fecha");

            int adultAge = Convert.ToInt32(DataHelper.GetUDCItemRow(dc, Convert.ToInt32(CountryDropDownList.SelectedValue)).Optional3);

            if (SqlMethods.DateDiffYear(date, DateTime.Today) < adultAge)
                sb.AppendFormat("La edad del DIM debe ser mayor o igual a {0} años", adultAge).AppendLine();

            if (DiscountTextBox.Text == null)
                sb.AppendLine("El Porciento de descuento es requerido. Deje el valor cero por defecto.");

            if (!float.TryParse(DiscountTextBox.Text, out credit))
            {
                DiscountTextBox.Text = "0.00";
            }

            if (Convert.ToInt32(MaritalStatusDropDownList.SelectedValue) == (int)MaritalStatus.Married)
            {
                if (string.IsNullOrWhiteSpace(SpouseNameTextBox.Text))
                    sb.AppendLine("El campo Conyugue Nombre es requerido.");

                if (string.IsNullOrWhiteSpace(SpouseLastNameTextBox.Text))
                    sb.AppendLine("El campo Conyugue Apellido Paterno es requerido.");

                if (string.IsNullOrWhiteSpace(SpouseMotherMaidenNameTextBox.Text))
                    sb.AppendLine("El campo Conyugue Apellido Materno es requerido.");
            }

            if (Convert.ToInt32(BankDropDownList.SelectedValue) != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY &&
                !string.IsNullOrWhiteSpace(BankAccountTextBox.Text) && BankAccountTextBox.Text.Length > 3 &&
                BankAccountTextBox.Text.Substring(0, 3) != DataHelper.GetUDCItemRow(dc, Convert.ToInt32(BankDropDownList.SelectedValue)).Code)
            {
                sb.AppendLine(
                    "La Cuenta Bancaria no coincide en sus primeros tres dígitos con los del Banco seleccionado.");
            }

            if (Convert.ToInt32(TypeDropDownList.SelectedValue) != (int)DimType.Employment)
            {
                if (Convert.ToString(SponsorTextBox.Text) == string.Empty)
                    sb.AppendLine("El campo Patrocinador es requerido.");

                if (GetSponsorItem() == null)
                    sb.AppendLine("El Patrocinador tecleado debe existir en la Base de datos de DIMs.");
            }



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

        private Common.Dim GetSponsorItem()
        {
            int id;
            if (int.TryParse(SponsorTextBox.Text.Trim(), out id))
            {
                var dim = dc.Dims.Where(d => d.Id == id).SingleOrDefault();
                return dim;
            }

            return null;
        }

        protected void SponsorTextBox_TextChanged(object sender, EventArgs e)
        {
            if (SponsorTextBox.Text.Trim() != string.Empty)
            {
                var dim = GetSponsorItem();

                SponsorResultLabel.Text = dim == null
                                        ? "El Patrocinador tecleado no existe"
                                        : string.Format("{0} - {1}", dim.FullName, dim.Disabled ? "Desactivado" : "Activo");
            }

            SponsorResultLabel.Visible = true;
        }

    }
}