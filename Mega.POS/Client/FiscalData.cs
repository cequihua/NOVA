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

        private void FillStates()
        {
            try
            {
                cmbEstado.DataSource = DataHelper.GetStateComboList(dc, "MX");
                cmbEstado.ValueMember = "Id";
                cmbEstado.DisplayMember = "Name";
                cmbEstado.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY;
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
                }

                cmbMunicipio.SelectedValue = Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY;
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, ex.Message);
            }
        }

        private void CargarDimEnForm()
        {
            txtNombres.Text = DimObject.Name.Trim();
            txtApellidoPat.Text = DimObject.LastName.Trim();
            txtApellidoMat.Text = DimObject.MotherMaidenName.Trim();
            txtRFC.Text = DimObject.TaxRegister.Trim();
            txtCalle.Text = DimObject.Address1.Trim();
            txtColonia.Text = DimObject.Address2.Trim();
            txtNoInt.Text = DimObject.InteriorNumber.Trim();
            txtNoExt.Text = DimObject.ExteriorNumber.Trim();
            txtCP.Text = DimObject.CP.Trim();
            cmbEstado.Text = DimObject.StateName.Trim();
            cmbMunicipio.Text = DimObject.PopulationName.Trim();
            txtTelefono.Text = DimObject.PhoneHome.Trim();
            txtCelular.Text = DimObject.PhoneCell.Trim();
            txtEmail.Text = DimObject.Email.Trim();
        }

        private bool ValidateFields()
        {
            bool valid = true;

            if (txtRFC.Text == "")
            {
                valid = false;
                DialogHelper.ShowWarningInfo(this, "El campo RFC es obligatorio");
                return valid;
            }

            if (txtCalle.Text == "")
            {
                valid = false;
                DialogHelper.ShowWarningInfo(this, "El campo Calle es obligatorio");
                return valid;
            }

            if (txtColonia.Text == "")
            {
                valid = false;
                DialogHelper.ShowWarningInfo(this, "El campo Colonia es obligatorio");
                return valid;
            }

            if (txtNoExt.Text == "")
            {
                valid = false;
                DialogHelper.ShowWarningInfo(this, "El campo Numero exterior es obligatorio");
                return valid;
            }

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

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text.ToLower(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
            {
                valid = false;
                DialogHelper.ShowWarningInfo(this, "El e-mail no es valido");
                return valid;
            }

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

                DimObject.TaxRegister = txtRFC.Text.Trim();
                DimObject.Address1 = txtCalle.Text.Trim();
                DimObject.Address2 = txtColonia.Text.Trim();
                DimObject.InteriorNumber = txtNoInt.Text.Trim();
                DimObject.ExteriorNumber = txtNoExt.Text.Trim();
                DimObject.CP = txtCP.Text.Trim();
                DimObject.Population = DataHelper.GetUDCItemRow(dc, Convert.ToInt32(cmbMunicipio.SelectedValue));
                DimObject.State = DataHelper.GetUDCItemRow(dc, Convert.ToInt32(cmbEstado.SelectedValue));
                DimObject.Email = txtEmail.Text.Trim().ToLower();
                
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
            FillStates();
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
    }
}
