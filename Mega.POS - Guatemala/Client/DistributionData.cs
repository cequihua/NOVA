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
    public partial class FormDistributionData : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(FormDistributionData));
        private AdminDataContext dc = ApplicationHelper.GetPosDataContext();
        private Guid idOperation;
        private DistributionData disDataEdit = null;
        private bool isEdit = false;

        public Guid IdOperation { set { idOperation = value; } get { return idOperation; } }

        public FormDistributionData()
        {
            InitializeComponent();
        }

        private void DistributionData_Load(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void LoadData()
        {
            try
            {
                var data_result = (from d in dc.DistributionData where d.IdOperation == this.IdOperation select d);
                if (data_result.Count() <= 0)
                    return;
                this.disDataEdit = dc.DistributionData.Single(d => d.IdOperation == this.IdOperation);

                if (this.disDataEdit != null)
                {
                    txtNombre.Text = this.disDataEdit.Nombre;
                    txtCalle.Text = this.disDataEdit.Calle;
                    txtNoInt.Text = this.disDataEdit.NoInterior;
                    txtNoExt.Text = this.disDataEdit.NoExterior;
                    txtCol.Text = this.disDataEdit.Colonia;
                    txtCd.Text = this.disDataEdit.Ciudad;
                    txtEstado.Text = this.disDataEdit.Estado;
                    txtCP.Text = this.disDataEdit.CP;
                    txtEmail.Text = this.disDataEdit.Email;
                    txtTel.Text = this.disDataEdit.Telefono;
                    txtMovil.Text = this.disDataEdit.Celular;
                    txtHorario.Text = this.disDataEdit.Horario;
                    this.isEdit = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error al guardar la informacion de distribucion", ex);
                DialogHelper.ShowError(this, "Error al guardar la informacion de distribucion", ex);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Valida())
                    return;

                if (isEdit)
                {
                    this.disDataEdit.Nombre = txtNombre.Text.Trim();
                    this.disDataEdit.Calle = txtCalle.Text.Trim();
                    this.disDataEdit.NoExterior = txtNoExt.Text.Trim();
                    this.disDataEdit.NoInterior = txtNoInt.Text.Trim();
                    this.disDataEdit.Colonia = txtCol.Text.Trim();
                    this.disDataEdit.Ciudad = txtCd.Text.Trim();
                    this.disDataEdit.Estado = txtEstado.Text.Trim();
                    this.disDataEdit.CP = txtCP.Text.Trim();
                    this.disDataEdit.Email = txtEmail.Text.Trim();
                    this.disDataEdit.Telefono = txtTel.Text.Trim();
                    this.disDataEdit.Celular = txtMovil.Text.Trim();
                    this.disDataEdit.Horario = txtHorario.Text.Trim();
                    isEdit = false;
                }
                else
                {
                    DistributionData disData = new DistributionData();
                    disData.Id = Guid.NewGuid();
                    disData.Nombre = txtNombre.Text.Trim();
                    disData.Calle = txtCalle.Text.Trim();
                    disData.NoExterior = txtNoExt.Text.Trim();
                    disData.NoInterior = txtNoInt.Text.Trim();
                    disData.Colonia = txtCol.Text.Trim();
                    disData.Ciudad = txtCd.Text.Trim();
                    disData.Estado = txtEstado.Text.Trim();
                    disData.CP = txtCP.Text.Trim();
                    disData.Email = txtEmail.Text.Trim();
                    disData.Telefono = txtTel.Text.Trim();
                    disData.Celular = txtMovil.Text.Trim();
                    disData.Horario = txtHorario.Text.Trim();
                    disData.IdOperation = this.IdOperation;
                    disData.AddedDate = DateTime.Now;
                    dc.DistributionData.InsertOnSubmit(disData);
                }
               
                dc.SubmitChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Error al guardar la informacion de distribucion", ex);
                DialogHelper.ShowError(this, "Error al guardar la informacion de distribucion", ex);
            }
        }

        private bool Valida()
        {
            bool result = true;
            if (txtNombre.Text == string.Empty)
            {
                DialogHelper.ShowWarningInfo(this, "El nombre a quien recibe es requerido");
                result = false;
                return result;
            }

            if (txtCalle.Text == string.Empty)
            {
                DialogHelper.ShowWarningInfo(this, "La calle es requerido");
                result = false;
                return result;
            }

            if (txtCol.Text == string.Empty)
            {
                DialogHelper.ShowWarningInfo(this, "La colonia es requerido");
                result = false;
                return result;
            }

            if (txtCd.Text == string.Empty)
            {
                DialogHelper.ShowWarningInfo(this, "La cd es requerido");
                result = false;
                return result;
            }

            if (txtEstado.Text == string.Empty)
            {
                DialogHelper.ShowWarningInfo(this, "El estado es requerido");
                result = false;
                return result;
            }

            if (txtCP.Text == string.Empty)
            {
                DialogHelper.ShowWarningInfo(this, "El CP es requerido");
                result = false;
                return result;
            }

            if (txtTel.Text == string.Empty)
            {
                DialogHelper.ShowWarningInfo(this, "El telefono es requerido");
                result = false;
                return result;
            }

            return result;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if (DialogHelper.ShowConfirmationQuestionWarning(this, "¿ Esta usted seguro de cancelar la operacion ?") == System.Windows.Forms.DialogResult.Yes)
            //{
            //    this.Close();
            //}
        }
    }
}
