/*
 * FORMULARIO PARA LA EDICION DE DATOS FISCALES DEL CLIENTE
 * Cesar Equihua
 * 25/03/2014
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class FormFiscalData : Form
    {
        #region Constructor

        public FormFiscalData(int idDim)
        {
            this.IdDim = idDim;
            InitializeComponent();
        }

        #endregion

        #region Miembros Privados

        private static readonly ILog Logger = LogManager.GetLogger(typeof(FormFiscalData));
        private AdminDataContext dc = ApplicationHelper.GetPosDataContext();
        private int _idDim;
        private Dim _dimObject;

        #endregion

        #region Propiedades Publicas

        public int IdDim { get { return _idDim; } set { _idDim = value; } }
        public Dim DimObject { get { return _dimObject; } set { _dimObject = value; } }

        #endregion

        #region Metodos Privados

        private void GetDim()
        {
            try
            {
                Dim dim = dc.Dims.Single(d => d.Id == this.IdDim);
                this.DimObject = dim;
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, "Error al consultar el cliente", ex);
            }
        }

        private void FillCountries()
        {
            try
            {
                cmbPais.DataSource = DataHelper.GetUDCCountries(ApplicationHelper.GetPosDataContext()).ToList();
                cmbPais.ValueMember = "Id";
                cmbPais.DisplayMember = "Name";
                cmbPais.SelectedValue = DataHelper.GetCountryShop(dc, Properties.Settings.Default.CurrentShop);
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, ex.Message);
            }
        }

        private void FillStates()
        {
            try
            {
                if (cmbPais.SelectedItem != null)
                {
                    dynamic c = cmbPais.SelectedItem;
                    cmbEstado.DataSource = DataHelper.GetStateComboList(dc, c.Code);
                }
                else
                {
                    cmbEstado.DataSource = DataHelper.GetEmptyList(dc);
                }

                //cmbEstado.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY;

                //cmbEstado.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY;
                //cmbEstado.DataSource = DataHelper.GetStateComboList(dc, "MX");
                cmbEstado.ValueMember = "Id";
                cmbEstado.DisplayMember = "Name";
                //cmbEstado.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY;
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, ex.Message);
            }
        }

        private void FillPopulations()
        {
            try
            {
                if (cmbEstado.SelectedItem != null)
                {
                    int tmpValue;

                    if (int.TryParse(cmbEstado.SelectedValue.ToString(), out tmpValue))
                    {
                        cmbMunicipio.DataSource = DataHelper.GetPopulationComboList(dc, tmpValue);
                        cmbMunicipio.ValueMember = "Id";
                        cmbMunicipio.DisplayMember = "Name";
                    }
                }
                else
                {
                    cmbMunicipio.DataSource = DataHelper.GetEmptyList(dc);
                    cmbMunicipio.ValueMember = "Id";
                    cmbMunicipio.DisplayMember = "Name";
                }

                //cmbMunicipio.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY;
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, ex.Message);
            }
        }

        private void CargarDimEnForm()
        {
            txtNombres.Text = DimObject.Name;
            txtApellidoPat.Text = DimObject.LastName;
            txtApellidoMat.Text = DimObject.MotherMaidenName;
            txtRFC.Text = DimObject.TaxRegister;
            txtCalle.Text = DimObject.Address1;
            txtColonia.Text = DimObject.Address2;
            //txtNoInt.Text = DimObject.InteriorNumber;
            //txtNoExt.Text = DimObject.ExteriorNumber;
            txtCP.Text = DimObject.CP;
            cmbPais.SelectedValue = DimObject.IdCountry;
            cmbEstado.SelectedValue = DimObject.IdState;
            cmbMunicipio.SelectedValue = DimObject.IdPopulation;
            txtTelefono.Text = DimObject.PhoneHome;
            txtCelular.Text = DimObject.PhoneCell;
            txtEmail.Text = DimObject.Email;
        }

        private bool ValidateFields()
        {
            bool valid = true;

            if (txtRFC.Text == "")
            {
                valid = false;
                DialogHelper.ShowWarningInfo(this, "El campo NIT es obligatorio");
                return valid;
            }

            if (txtCalle.Text == "")
            {
                valid = false;
                DialogHelper.ShowWarningInfo(this, "El campo Domicilio es obligatorio");
                return valid;
            }

            if (txtColonia.Text == "")
            {
                valid = false;
                DialogHelper.ShowWarningInfo(this, "El campo Colonia es obligatorio");
                return valid;
            }

            //if (txtNoExt.Text == "")
            //{
            //    valid = false;
            //    DialogHelper.ShowWarningInfo(this, "El campo Numero exterior es obligatorio");
            //    return valid;
            //}

            if (txtCP.Text == "")
            {
                valid = false;
                DialogHelper.ShowWarningInfo(this, "El campo Codigo Postal es obligatorio");
                return valid;
            }

            if (txtEmail.Text == "")
            {
                valid = false;
                DialogHelper.ShowWarningInfo(this, "El campo E-mail es obligatorio");
                return valid;
            }

            //if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text.ToLower(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
            //if(txtEmail.Text == "")
            //{
            //    valid = false;
            //    DialogHelper.ShowWarningInfo(this, "El e");
            //    return valid;
            //}

            return valid;
        }
        #endregion

        #region Eventos
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateFields())
                    return;

                DimObject.TaxRegister = txtRFC.Text;
                DimObject.Address1 = txtCalle.Text;
                DimObject.Address2 = txtColonia.Text;
                //DimObject.InteriorNumber = txtNoInt.Text;
                //DimObject.ExteriorNumber = txtNoExt.Text;
                DimObject.CP = txtCP.Text;
                DimObject.IdCountry = Convert.ToInt32(cmbPais.SelectedValue);
                DimObject.IdPopulation = Convert.ToInt32(cmbMunicipio.SelectedValue);
                DimObject.IdState = Convert.ToInt32(cmbEstado.SelectedValue);
                DimObject.Email = txtEmail.Text;
                DimObject.ModifiedDate = DateTime.Now;
                
                dc.SubmitChanges();
                
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Error al guardar la informacion del Dim", ex);
                DialogHelper.ShowError(this, "Error al guardar la informacion del Cliente", ex);
            }
        }

        private void FormFiscalData_Load(object sender, EventArgs e)
        {
            GetDim();
            FillCountries();
            CargarDimEnForm();
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillPopulations();
        }

        #endregion

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (DialogHelper.ShowConfirmationQuestionWarning(this, "¿ Esta usted seguro de cancelar la operacion ?") == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void cmbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStates();
        }
    }
}
