using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;
using Mega.Common;
using Mega.Common.Enum;
using Mega.Common.Helpers;
using Mega.POS.Helper;

namespace Mega.POS.Client
{
    public partial class ClientAdd : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ClientAdd));

        private Dim currentClient;
        private AdminDataContext dc = ApplicationHelper.GetPosDataContext();

        public int Id { get; set; }

        public bool IsAdding
        {
            get { return Id == 0; }
        }

        public ClientAdd()
        {
            InitializeComponent();
        }

        private void ClientAdd_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsOpeningSecondClientAddForm())
                {
                    DialogHelper.ShowWarningInfo(this, "Ya existe otro formulario de Agregar Clientes abierto");
                    Close();
                    return;
                }

                int dimSize = Convert.ToInt32(DataHelper.GetUDCItemRow(dc, Constant.CFG_DIM_MAX_SIZE_UDCITEM_KEY).Optional1);
                DimTextBox.MaxLength = dimSize;
                DimValidationTextBox.MaxLength = dimSize;
                DimValidationTextBox.ShortcutsEnabled = false;

                SponsorTextBox.MaxLength = dimSize;
                DiscountTextBox.Text = ApplicationHelper.GetCurrentShop().Company.DimDefaultDiscount.ToString("N");
                NeedInvoiceCheckBox.Enabled = false;

                BankAccountValidationTextBox.ShortcutsEnabled = false;
                KeyAccountValidationTextBox.ShortcutsEnabled = false;

                FillComboBoxsValues();

                TypeComboBox.SelectedValue = (int)DimType.DIM;

                BirthDateLabel.Text = string.Format("F. Nac. ({0}):", GetDateFormat());
                AddedDateLabel.Text = string.Format("Fecha Alta ({0}):", GetDateFormat());
                LowDateLabel.Text = string.Format("Fecha Baja ({0}):", GetDateFormat());
                LowDateDateTimePicker.Enabled = false;

                //PhoneCellTextBox.Mask = DataHelper.GetPhoneMask(dc);
                //PhoneOfficeTextBox.Mask = PhoneCellTextBox.Mask;
                //PhoneHomeTextBox.Mask = PhoneCellTextBox.Mask;

                bool hasAccess = ApplicationHelper.IsCurrentUserInRole(Constant.PosAdminOrMore);
                MaxCreditTextBox.Enabled = hasAccess;
                MaxConsignableValueTextBox.Enabled = hasAccess;
                ConsignableCheckBox.Enabled = hasAccess;
                SaleRetentionCheckBox.Enabled = hasAccess;
                DisabledCheckBox.Enabled = hasAccess;

                var shop = ApplicationHelper.GetCurrentShop();

                KeyAccountLabel.Text = shop.KeyCodeName;
                KeyAccountValidationLabel.Text = "Validación " + shop.KeyCodeName;
                KeyAccountTextBox.MaxLength = shop.KeyCodeSize;
                KeyAccountValidationTextBox.MaxLength = shop.KeyCodeSize;

                if (IsAdding)
                {
                    AddedDateDateTimePicker.Value = DateTime.Today;
                    AddedDateDateTimePicker.Enabled = false;
                    TaxRetentionCheckBox.Checked = true;

                    LowDateDateTimePicker.Value = DateTime.Today;
                    LowDateLabel.Visible = false;
                    LowDateDateTimePicker.Visible = false;
                    MaxCreditTextBox.Text = "0";
                    MaxConsignableValueTextBox.Text = "0";
                    BankComboBox.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY;
                    DiffusionMediaComboBox.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY;

                    DisabledCheckBox.Visible = false;
                    DisabledLabel.Visible = false;

                    CreditAmountDateTextBox.Text = DateTime.Today.ToString("d");
                    ConsignableAmountDateTextBox.Text = DateTime.Today.ToString("d");

                    CountryComboBox.SelectedValue = DataHelper.GetCountryShop(dc, Properties.Settings.Default.CurrentShop);
                    #region CaRLoS
                    // al agregar un nuevo dim el valor por defaul del iva sera el de la tienda
                    IVAGroupComboBox.SelectedIndex = 3;
                    #endregion
                }
                else
                {
                    AddButton.Text = "Guardar";
                    DimTextBox.Enabled = false;
                    SponsorTextBox.Enabled = false;
                    FindButton.Enabled = false;

                    DimValidationTextBox.Enabled = false;
                    TypeComboBox.Enabled = false;

                    currentClient = dc.Dims.Where(d => d.Id == Id).SingleOrDefault();

                    if (currentClient == null)
                    {
                        throw new Exception("No se encontró un cliente con el Id/Dim actual");
                    }

                    if (!currentClient.Disabled)
                    {
                        LowDateLabel.Visible = false;
                        LowDateDateTimePicker.Visible = false;
                    }

                    SetValuesToControls();
                }

                DimTextBox.Focus();
                ActiveControl = DimTextBox;
            }
            catch (Exception ex)
            {
                Logger.Error("Error agregando Cliente", ex);
                DialogHelper.ShowError(this, "Error inesperado al mostrar el formulario de Agregar/editar Cliente.", ex);
                Close();
            }
        }

        private bool IsOpeningSecondClientAddForm()
        {
            if (IsAdding)
            {
                foreach (Form openForm in Application.OpenForms)
                {
                    if (openForm != this && openForm.GetType() == typeof(ClientAdd) && ((ClientAdd)openForm).IsAdding)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private string GetDateFormat()
        {
            DateTime tmp;
            return DateTime.TryParse("31/12/2011", out tmp) ? "dd/mm/aaaa" : "mm/dd/aaaa";
        }

        private string GetConvertionDateFormat()
        {
            DateTime tmp;
            return DateTime.TryParse("31/12/2011", out tmp) ? "dd/MM/yyyy" : "MM/dd/yyyy";
        }

        private void SetValuesToControls()
        {
            DimTextBox.Text = currentClient.Id.ToString();
            DimValidationTextBox.Text = DimTextBox.Text;
            TypeComboBox.SelectedValue = currentClient.IdType;
            SponsorTextBox.Text = currentClient.IdSponsor.ToString();
            SponsorTextBox_Validating(null, null);
            AddedDateDateTimePicker.Value = currentClient.AddedDate;
            CalificationTextBox.Text = currentClient.Calification.ToString();
            DimNameTextBox.Text = currentClient.Name;
            LastNameTextBox.Text = currentClient.LastName;
            MotherMiadenNameTextBox.Text = currentClient.MotherMaidenName;
            IsOfficialIdCheckBox.Checked = currentClient.IsOfficialId;
            HasAddressProofCheckBox.Checked = currentClient.HasAddressProof;
            CoHolderTextBox.Text = currentClient.CoHolder;
            SpouseNameTextBox.Text = currentClient.SpouseName;
            SpouseLastNameTextBox.Text = currentClient.SpouseLastName;
            SpouseMotherMaidenNameTextBox.Text = currentClient.SpouseMotherMaidenName;
            CURPTextBox.Text = currentClient.CURP;
            TaxRegisterTextBox.Text = currentClient.TaxRegister;
            BirthDateMaskedTextBox.Text = currentClient.BirthDate.ToString(GetConvertionDateFormat());
            LowDateDateTimePicker.Value = currentClient.LowDate ?? DateTime.Today;
            BankComboBox.SelectedValue = currentClient.IdBank;
            DiffusionMediaComboBox.SelectedValue = currentClient.IdDiffusionMedia;

            KeyAccountTextBox.Text = currentClient.KeyAccount;
            KeyAccountValidationTextBox.Text = currentClient.KeyAccount;
            BankAccountTextBox.Text = currentClient.BankAccount;
            BankAccountValidationTextBox.Text = currentClient.BankAccount;
            SucursalTextBox.Text = currentClient.Sucursal;
            ReferenceTextBox.Text = currentClient.Reference;

            DiscountTextBox.Text = currentClient.DiscountPercent.ToString("00.00");
            NeedInvoiceCheckBox.Checked = currentClient.NeedInvoice;
            IVAGroupComboBox.SelectedValue = currentClient.IdIVAGroup;
            SexComboBox.SelectedValue = currentClient.IdSex;
            MaritalStatusComboBox.SelectedValue = currentClient.IdMaritalStatus;
            currentClient.TaxRetention = TaxRetentionCheckBox.Checked;
            Address1TextBox.Text = currentClient.Address1;
            ExteriorNumberTextBox.Text = currentClient.ExteriorNumber;
            InteriorNumberTextBox.Text = currentClient.InteriorNumber;
            Address2TextBox.Text = currentClient.Address2;
            Address3TextBox.Text = currentClient.Address3;
            CountryComboBox.SelectedValue = currentClient.IdCountry;
            StateComboBox.SelectedValue = currentClient.IdState;
            PopulationComboBox.SelectedValue = currentClient.IdPopulation;
            CPTextBox.Text = currentClient.CP;
            PhoneCellTextBox.Text = currentClient.PhoneCell;
            PhoneOfficeTextBox.Text = currentClient.PhoneOffice;
            PhoneHomeTextBox.Text = currentClient.PhoneHome;
            EmailTextBox.Text = currentClient.Email;
            MaxCreditTextBox.Text = currentClient.MaxCreditAmount.ToString("N");
            CreditAmountTextBox.Text = currentClient.CreditAmount.ToString("N");
            CreditAmountDateTextBox.Text = currentClient.CreditAmountDate.ToString("d");
            MaxConsignableValueTextBox.Text = currentClient.MaxConsignableAmount.ToString("N");
            ConsignableAmountTextBox.Text = currentClient.ConsignableAmount.ToString("N");
            ConsignableAmountDateTextBox.Text = currentClient.ConsignableAmountDate.ToString("d");
            ConsignableCheckBox.Checked = currentClient.Consignable;
            PointTextBox.Text = currentClient.PointAmount.ToString("N");
            PointDateTextBox.Text = currentClient.PointAmountDate.ToString("d");
            NotesTextBox.Text = currentClient.Notes;
            SaleRetentionCheckBox.Checked = currentClient.SaleRetention;
            DisabledCheckBox.Checked = currentClient.Disabled;
        }

        private void FillComboBoxsValues()
        {
            ApplicationHelper.ConfigureComboUDCItems(TypeComboBox);

            ApplicationHelper.ConfigureComboUDCItems(BankComboBox);
            ApplicationHelper.ConfigureComboUDCItems(SexComboBox);
            ApplicationHelper.ConfigureComboUDCItems(MaritalStatusComboBox);
            ApplicationHelper.ConfigureComboUDCItems(CountryComboBox);
            ApplicationHelper.ConfigureComboUDCItems(StateComboBox);
            ApplicationHelper.ConfigureComboUDCItems(PopulationComboBox);
            ApplicationHelper.ConfigureComboUDCItems(IVAGroupComboBox);
            ApplicationHelper.ConfigureComboUDCItems(DiffusionMediaComboBox);

            TypeComboBox.DataSource = DataHelper.GetUDCDimType(ApplicationHelper.GetPosDataContext());
            SexComboBox.DataSource = DataHelper.GetUDCSexs(ApplicationHelper.GetPosDataContext());
            MaritalStatusComboBox.DataSource = DataHelper.GetUDCMaritalStatus(ApplicationHelper.GetPosDataContext());
            IVAGroupComboBox.DataSource = DataHelper.GetUDCIVAGroupComboList(ApplicationHelper.GetPosDataContext(), "Aplica el de la Tienda");
            BankComboBox.DataSource = DataHelper.GetBankComboList(ApplicationHelper.GetPosDataContext());
            DiffusionMediaComboBox.DataSource = DataHelper.GetUDCDiffusionMediaComboList(ApplicationHelper.GetPosDataContext());
            CountryComboBox.DataSource = DataHelper.GetUDCCountries(ApplicationHelper.GetPosDataContext()).ToList();

            FillStates();
            FillPopulations();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidData())
                {
                    int maritalStatus = Convert.ToInt32(MaritalStatusComboBox.SelectedValue);
                    bool isMarried = maritalStatus == (int)MaritalStatus.Married;

                    if (IsAdding)
                    {
                        var response = DialogHelper.ShowWarningQuestion(this,
                                                           "¿Está seguro que desea agregar este Cliente? No podrá eliminarlo posteriormente.");

                        if (response == DialogResult.Yes)
                        {
                            #region CreateClient

                            currentClient = new Dim
                                                {
                                                    Id = int.Parse(DimTextBox.Text),
                                                    IdType = Convert.ToInt32(TypeComboBox.SelectedValue),
                                                    IdSponsor = string.IsNullOrEmpty(SponsorTextBox.Text) ? null : (int?)Convert.ToInt32(SponsorTextBox.Text),
                                                    AddedDate = DateTime.Now,
                                                    Calification = CalificationTextBox.Text[0],
                                                    Name = DimNameTextBox.Text,
                                                    LastName = LastNameTextBox.Text,
                                                    MotherMaidenName = MotherMiadenNameTextBox.Text,
                                                    IsOfficialId = IsOfficialIdCheckBox.Checked,
                                                    HasAddressProof = HasAddressProofCheckBox.Checked,
                                                    CoHolder = CoHolderTextBox.Text,
                                                    IdMaritalStatus = maritalStatus,
                                                    SpouseName = isMarried ? SpouseNameTextBox.Text : string.Empty,
                                                    SpouseLastName =
                                                        isMarried ? SpouseLastNameTextBox.Text : string.Empty,
                                                    SpouseMotherMaidenName =
                                                        isMarried ? SpouseMotherMaidenNameTextBox.Text : string.Empty,
                                                    CURP = CURPTextBox.Text,
                                                    TaxRegister = TaxRegisterTextBox.Text,
                                                    BirthDate = Convert.ToDateTime(BirthDateMaskedTextBox.Text),
                                                    LowDate = LowDateDateTimePicker.Value,
                                                    IdBank = Convert.ToInt32(BankComboBox.SelectedValue),
                                                    BankAccount = BankAccountTextBox.Text,
                                                    KeyAccount = KeyAccountTextBox.Text,

                                                    Sucursal = SucursalTextBox.Text,
                                                    Reference = ReferenceTextBox.Text,

                                                    DiscountPercent = Convert.ToDecimal(DiscountTextBox.Text),
                                                    NeedInvoice = NeedInvoiceCheckBox.Checked,
                                                    IdIVAGroup = Convert.ToInt32(IVAGroupComboBox.SelectedValue),
                                                    IdSex = Convert.ToInt32(SexComboBox.SelectedValue),

                                                    TaxRetention = TaxRetentionCheckBox.Checked,
                                                    Address1 = Address1TextBox.Text,
                                                    ExteriorNumber = ExteriorNumberTextBox.Text,
                                                    InteriorNumber = InteriorNumberTextBox.Text,
                                                    Address2 = Address2TextBox.Text,
                                                    Address3 = Address3TextBox.Text,
                                                    IdPopulation = Convert.ToInt32(PopulationComboBox.SelectedValue),
                                                    IdState = Convert.ToInt32(StateComboBox.SelectedValue),
                                                    IdCountry = Convert.ToInt32(CountryComboBox.SelectedValue),
                                                    IdDiffusionMedia = Convert.ToInt32(DiffusionMediaComboBox.SelectedValue),
                                                    CP = CPTextBox.Text,
                                                    PhoneCell = PhoneCellTextBox.Text,
                                                    PhoneOffice = PhoneOfficeTextBox.Text,
                                                    PhoneHome = PhoneHomeTextBox.Text,
                                                    Email = EmailTextBox.Text,
                                                    MaxCreditAmount = Convert.ToDecimal(MaxCreditTextBox.Text),
                                                    CreditAmountDate = DateTime.Now,
                                                    CreditAmount = 0,
                                                    MaxConsignableAmount = Convert.ToDecimal(MaxConsignableValueTextBox.Text),
                                                    Consignable = ConsignableCheckBox.Checked,
                                                    ConsignableAmount = 0,
                                                    ConsignableAmountDate = DateTime.Now,
                                                    Notes = NotesTextBox.Text,
                                                    SaleRetention = SaleRetentionCheckBox.Checked,
                                                    PointAmount = 0,
                                                    PointAmountDate = DateTime.Now,
                                                    Disabled = false
                                                };

                            DataHelper.FillAuditoryValuesDesktop(currentClient);

                            #endregion
                            dc.Dims.InsertOnSubmit(currentClient);
                            dc.SubmitChanges();
                        }
                    }
                    else
                    {
                        if (DisabledCheckBox.Checked && currentClient.Disabled == false)
                        {
                            LowDateDateTimePicker.Value = DateTime.Today;
                        }

                        #region FillClientData

                        currentClient.Id = int.Parse(DimTextBox.Text);
                        //currentClient.Type = DataHelper.GetUDCItemRow(dc, Convert.ToInt32(TypeComboBox.SelectedValue));
                        currentClient.IdSponsor = string.IsNullOrEmpty(SponsorTextBox.Text)
                                                      ? null
                                                      : (int?) Convert.ToInt32(SponsorTextBox.Text);
                        currentClient.AddedDate = AddedDateDateTimePicker.Value;
                        currentClient.Calification = CalificationTextBox.Text[0];
                        currentClient.Name = DimNameTextBox.Text;
                        currentClient.LastName = LastNameTextBox.Text;
                        currentClient.MotherMaidenName = MotherMiadenNameTextBox.Text;
                        currentClient.IsOfficialId = IsOfficialIdCheckBox.Checked;
                        currentClient.HasAddressProof = HasAddressProofCheckBox.Checked;
                        currentClient.CoHolder = CoHolderTextBox.Text;

                        currentClient.SpouseName = isMarried ? SpouseNameTextBox.Text : string.Empty;
                        currentClient.SpouseLastName = isMarried ? SpouseLastNameTextBox.Text : string.Empty;
                        currentClient.SpouseMotherMaidenName = isMarried ? SpouseMotherMaidenNameTextBox.Text : string.Empty;

                        currentClient.CURP = CURPTextBox.Text;
                        currentClient.TaxRegister = TaxRegisterTextBox.Text;
                        currentClient.BirthDate = Convert.ToDateTime(BirthDateMaskedTextBox.Text);
                        currentClient.LowDate = LowDateDateTimePicker.Value;
                        currentClient.Bank = DataHelper.GetUDCItemRow(dc, Convert.ToInt32(BankComboBox.SelectedValue));
                        currentClient.BankAccount = BankAccountTextBox.Text;
                        currentClient.KeyAccount = KeyAccountTextBox.Text;

                        currentClient.Sucursal = SucursalTextBox.Text;
                        currentClient.Reference = ReferenceTextBox.Text;

                        currentClient.DiscountPercent = Convert.ToDecimal(DiscountTextBox.Text);
                        currentClient.NeedInvoice = NeedInvoiceCheckBox.Checked;
                        currentClient.IVAToApply = DataHelper.GetUDCItemRow(dc, Convert.ToInt32(IVAGroupComboBox.SelectedValue));
                        currentClient.Sex = DataHelper.GetUDCItemRow(dc, Convert.ToInt32(SexComboBox.SelectedValue));
                        currentClient.MaritalStatus = DataHelper.GetUDCItemRow(dc, maritalStatus);
                        currentClient.TaxRetention = TaxRetentionCheckBox.Checked;
                        currentClient.Address1 = Address1TextBox.Text;
                        currentClient.ExteriorNumber = ExteriorNumberTextBox.Text;
                        currentClient.InteriorNumber = InteriorNumberTextBox.Text;
                        currentClient.Address2 = Address2TextBox.Text;
                        currentClient.Address3 = Address3TextBox.Text;
                        currentClient.Population = DataHelper.GetUDCItemRow(dc, Convert.ToInt32(PopulationComboBox.SelectedValue));
                        currentClient.State = DataHelper.GetUDCItemRow(dc, Convert.ToInt32(StateComboBox.SelectedValue));
                        currentClient.Country = DataHelper.GetUDCItemRow(dc, Convert.ToInt32(CountryComboBox.SelectedValue));
                        currentClient.DiffusionMedia = DataHelper.GetUDCItemRow(dc, Convert.ToInt32(DiffusionMediaComboBox.SelectedValue));
                        currentClient.CP = CPTextBox.Text;
                        currentClient.PhoneCell = PhoneCellTextBox.Text;
                        currentClient.PhoneOffice = PhoneOfficeTextBox.Text;
                        currentClient.PhoneHome = PhoneHomeTextBox.Text;
                        currentClient.Email = EmailTextBox.Text;
                        currentClient.MaxCreditAmount = Convert.ToDecimal(MaxCreditTextBox.Text);
                        currentClient.MaxConsignableAmount = Convert.ToDecimal(MaxConsignableValueTextBox.Text);
                        currentClient.Consignable = ConsignableCheckBox.Checked;
                        //currentClient.ConsignableAmount = No hace falta guardarlo porque no se modifica aqui
                        //CreditAmountDate = DateTime.Now,
                        //                            CreditAmount = 0, 
                        //                            ConsignableAmountDate = DateTime.Now,
                        //                            PointAmount = 0,
                        //                            PointAmountDate = DateTime.Now,
                        currentClient.Notes = NotesTextBox.Text;
                        currentClient.ModifiedBy = ApplicationHelper.GetCurrentUser();
                        currentClient.ModifiedDate = DateTime.Today;
                        currentClient.ModifiedOn = System.Net.Dns.GetHostName();
                        currentClient.SaleRetention = SaleRetentionCheckBox.Checked;
                        currentClient.Disabled = DisabledCheckBox.Checked;


                        #endregion

                        dc.SubmitChanges();
                    }

                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error agregando Cliente", ex);
                DialogHelper.ShowError(this, DataHelper.GetDefaultAddExceptionMessage(ex));
                
                dc = ApplicationHelper.GetPosDataContextReseted();
            }
        }

        private bool IsValidData()
        {
            StringBuilder sb = new StringBuilder();

            int code;
            if (string.IsNullOrWhiteSpace(DimTextBox.Text) || !int.TryParse(DimTextBox.Text, out code))
                sb.AppendLine("El campo [Dim] es requerido y debe ser un valor Numérico Entero");

            if (DimTextBox.Text != DimValidationTextBox.Text)
            {
                sb.AppendLine("El campo [Dim] y el campo [Validación Dim] deben ser iguales");
                dimErrorProvider1.SetError(DimValidationTextBox, "El campo [Dim] y el campo [Validación Dim] deben ser iguales");
            }

            if (string.IsNullOrWhiteSpace(CalificationTextBox.Text))
                sb.AppendLine("El campo [Calificación] es requerido.");

            if (string.IsNullOrWhiteSpace(DimNameTextBox.Text))
                sb.AppendLine("El campo [Nombre] es requerido.");

            if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
                sb.AppendLine("El campo [Apellido Paterno] es requerido.");

            if (string.IsNullOrWhiteSpace(MotherMiadenNameTextBox.Text))
                sb.AppendLine("El campo [Apellido Materno] es requerido.");

            float credit;
            if (string.IsNullOrWhiteSpace(MaxCreditTextBox.Text) || !float.TryParse(MaxCreditTextBox.Text, out credit))
                sb.AppendLine("El campo [Máximo Crédito] es requerido y debe ser de tipo Numérico/Moneda.");

            if (string.IsNullOrWhiteSpace(MaxConsignableValueTextBox.Text) || !float.TryParse(MaxConsignableValueTextBox.Text, out credit))
                sb.AppendLine("El campo [Máximo Valor Consignable] es requerido y debe ser de tipo Numérico/Moneda.");

            DateTime date = DateTime.Today;
            if (string.IsNullOrWhiteSpace(BirthDateMaskedTextBox.Text) || !DateTime.TryParse(BirthDateMaskedTextBox.Text, out date))
                sb.AppendLine("El campo [Fecha Nacimiento] es requerido y debe ser de tipo Fecha");

            int adultAge = Convert.ToInt32(DataHelper.GetUDCItemRow(dc, Convert.ToInt32(CountryComboBox.SelectedValue)).Optional1);

            if (SqlMethods.DateDiffYear(date, DateTime.Today) < adultAge)
                sb.AppendFormat("La edad del DIM debe ser mayor o igual a {0} años", adultAge).AppendLine();

            if (string.IsNullOrWhiteSpace(Address1TextBox.Text))
                sb.AppendLine("El campo [Calle] es requerido.");

            if (string.IsNullOrWhiteSpace(ExteriorNumberTextBox.Text))
                sb.AppendLine("El campo [No. Exterior] es requerido.");

            if (string.IsNullOrWhiteSpace(EmailTextBox.Text) || EmailTextBox.Text == "@")
                sb.AppendLine("El campo [Email] es requerido.");

            if (Convert.ToInt32(TypeComboBox.SelectedValue) != (int)DimType.Employment)
            {
                if (Convert.ToString(SponsorTextBox.Text) == string.Empty)
                    sb.AppendLine("El campo [Patrocinador] es requerido.");

                if (GetSponsorItem() == null)
                    sb.AppendLine("El Patrocinador tecleado debe existir en la Base de datos de DIMs.");
            }

            if (DiscountTextBox.Text == null)
                sb.AppendLine("El Porciento de descuento es requerido. Deje el valor cero por defecto.");

            if (!float.TryParse(DiscountTextBox.Text, out credit))
            {
                DiscountTextBox.Text = "0.00";
            }

            if (BankAccountTextBox.Text != BankAccountValidationTextBox.Text)
            {
                sb.AppendLine("El campo [Cuenta Bancaria] y el campo [Validación Cuenta Bancaria] deben ser iguales");
                dimErrorProvider1.SetError(DimValidationTextBox, "El campo [Cuenta Bancaria] y el campo [Validación Cuenta Bancaria] deben ser iguales");
            }

            if (KeyAccountTextBox.Text != KeyAccountValidationTextBox.Text)
            {
                sb.AppendLine("El campo [Clave Bancaria] y el campo [Validación Clave Bancaria] deben ser iguales");
                dimErrorProvider1.SetError(DimValidationTextBox, "El campo [Clave Bancaria] y el campo [Validación Clave Bancaria] deben ser iguales");
            }

            if (Convert.ToInt32(MaritalStatusComboBox.SelectedValue) == (int)MaritalStatus.Married)
            {
                if (string.IsNullOrWhiteSpace(SpouseNameTextBox.Text))
                    sb.AppendLine("El campo [Conyugue Nombre] es requerido.");

                if (string.IsNullOrWhiteSpace(SpouseLastNameTextBox.Text))
                    sb.AppendLine("El campo [Conyugue Apellido Paterno] es requerido.");

                if (string.IsNullOrWhiteSpace(SpouseMotherMaidenNameTextBox.Text))
                    sb.AppendLine("El campo [Conyugue Apellido Materno] es requerido.");
            }

            if (Convert.ToInt32(BankComboBox.SelectedValue) != Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY &&
                !string.IsNullOrWhiteSpace(BankAccountTextBox.Text) && BankAccountTextBox.TextLength > 3 &&
                BankAccountTextBox.Text.Substring(0, 3) != DataHelper.GetUDCItemRow(dc, Convert.ToInt32(BankComboBox.SelectedValue)).Code)
            {
                sb.AppendLine(
                    "La Cuenta Bancaria no coincide en sus primeros tres dígitos con los del Banco seleecionado.");
            }

            #region CaRLoS
            if (StateComboBox.Text == "-Sin Seleccionar-")
                sb.AppendLine("El campo [Estado/Provincia] es requerido.");
            if (PopulationComboBox.Text == "-Sin Seleccionar-")
                sb.AppendLine("El campo [Población/Municipio] es requerido.");
            #endregion
            if (sb.Length > 0)
            {
                sb.Insert(0, "Antes de continuar debe corregir los siguientes errores: " + Environment.NewLine);
                DialogHelper.ShowError(this, sb.ToString());

                return false;
            }

            return true;
        }

        private void DisabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DisabledCheckBox.Checked && !currentClient.Disabled)
            {
                DialogHelper.ShowWarningInfo(this, "Cuidado!! Está desabilitando este Cliente/Dim para uso futuro.");
            }
        }

        private void CountryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePhoneMash();
            FillStates();
        }

        private void UpdatePhoneMash()
        {
            if (CountryComboBox.SelectedItem != null)
            {
                dynamic c = CountryComboBox.SelectedItem;
                PhoneCellTextBox.Mask = c.Optional2;
                PhoneHomeTextBox.Mask = c.Optional2;
                PhoneOfficeTextBox.Mask = c.Optional2;

                PhoneCellTextBox.Text = string.Empty;
                PhoneHomeTextBox.Text = string.Empty;
                PhoneOfficeTextBox.Text = string.Empty;
            }
        }

        private void FillStates()
        {
            if (CountryComboBox.SelectedItem != null)
            {
                dynamic c = CountryComboBox.SelectedItem;
                StateComboBox.DataSource = DataHelper.GetStateComboList(dc, c.Code);
            }
            else
            {
                StateComboBox.DataSource = DataHelper.GetEmptyList(dc);
            }

            StateComboBox.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY;
        }

        private void StateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillPopulations();
        }

        private void FillPopulations()
        {
            if (StateComboBox.SelectedItem != null)
            {
                int tmpValue;

                if (int.TryParse(StateComboBox.SelectedValue.ToString(), out tmpValue))
                {
                    PopulationComboBox.DataSource = DataHelper.GetPopulationComboList(dc, tmpValue);
                }
            }
            else
            {
                PopulationComboBox.DataSource = DataHelper.GetEmptyList(dc);
            }

            PopulationComboBox.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY;
        }

        private void DimTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (IsAdding && !string.IsNullOrWhiteSpace(DimTextBox.Text))
                {
                    int id;
                    if (int.TryParse(DimTextBox.Text.Trim(), out id))
                    {
                        if (dc.Dims.Where(d => d.Id == id).SingleOrDefault() != null)
                        {
                            DialogHelper.ShowWarningInfo(this, "El ID/Dim que intenta insertar ya existe");
                        }
                    }
                    else
                    {
                        DialogHelper.ShowError(this, "El ID/DIM que ha tecleado no es correcto. Debe ser un valor de tipo numérico entero");
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error("Error verificando la existencia del Cliente/Dim", ex);
                DialogHelper.ShowError(this, "Error verificando la existencia del Cliente/Dim", ex);
            }
        }

        private void SponsorTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (SponsorTextBox.Text.Trim() != string.Empty)
                {
                    var dim = GetSponsorItem();

                    DimNameLabel.Text = dim == null
                                            ? "El Patrocinador tecleado no existe"
                                            : string.Format("{0} - {1}", dim.FullName, dim.Disabled ? "Desactivado" : "Activo");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error verificando la existencia del Cliente/Dim", ex);
                DialogHelper.ShowError(this, "Error verificando la existencia del Cliente/Dim", ex);
            }
        }

        private Dim GetSponsorItem()
        {
            int id;
            if (int.TryParse(SponsorTextBox.Text.Trim(), out id))
            {
                var dim = dc.Dims.Where(d => d.Id == id).SingleOrDefault();
                return dim;
            }

            return null;
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                FindClient f = new FindClient(SponsorTextBox.Text, false);

                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    SponsorTextBox.Text = f.SelectedDIM;
                    DimNameTextBox.Focus();
                }
                Cursor = Cursors.Default;
            }
            catch (Exception)
            {
                Cursor = Cursors.Default;
                Logger.Error("Error abriendo dialogo de busqueda de Patrocinador");
                DialogHelper.ShowError(this, "Error inesparado abriendo el formulario de busqueda de Dim/Patrocinador");
            }
        }

        private void DimValidationTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dimErrorProvider1.SetError(DimValidationTextBox,
                                       DimTextBox.Text != DimValidationTextBox.Text
                                           ? "El valor de este campo debe ser igual a [ID/Dim]"
                                           : string.Empty);
        }


        private void BankAccountValidationTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bankAccountRrrorProvider.SetError(BankAccountValidationTextBox,
                                       BankAccountTextBox.Text != BankAccountValidationTextBox.Text
                                           ? "El valor de este campo debe ser igual a [Cuenta Bancaria]"
                                           : string.Empty);
        }

        private void KeyAccountValidationTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            keyAccountErrorProvider.SetError(KeyAccountValidationTextBox,
                                       KeyAccountTextBox.Text != KeyAccountValidationTextBox.Text
                                           ? "El valor de este campo debe ser igual a [Cuenta Clave]"
                                           : string.Empty);
        }

        private void MaritalStatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpouseGroupBox.Enabled = Convert.ToInt32(MaritalStatusComboBox.SelectedValue) == (int)MaritalStatus.Married;
        }

        private void SponsorTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                FindButton.PerformClick();
            }
        }

        private void BirthDateMaskedTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DateTime d;
            if (BirthDateMaskedTextBox.Text != string.Empty && !DateTime.TryParse(BirthDateMaskedTextBox.Text, out d))
            {
                birthDateErrorProvider.SetError(BirthDateMaskedTextBox, "Formato de Fecha incorrecto.");
            }
        }

        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(TypeComboBox.SelectedValue) == (int)DimType.Employment)
            {
                SponsorTextBox.Text = string.Empty;
                SponsorTextBox.Enabled = false;
                FindButton.Enabled = false;
            }
            else
            {
                SponsorTextBox.Enabled = true;
                FindButton.Enabled = true;
            }
        }
    }
}
