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

namespace Mega.POS
{
    public partial class FormResolutionFiscal : Form
    {
        public FormResolutionFiscal()
        {
            InitializeComponent();
        }

        #region Miembros Privados

        private static readonly ILog Logger = LogManager.GetLogger(typeof(FormResolutionFiscal));
        private AdminDataContext dc = ApplicationHelper.GetPosDataContext();
        private int idResolution;
        public int IdResolution { set { idResolution = value; } get { return idResolution; } }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateFields()
        {
            bool valid = true;
            
            if (txtFechaAcc.Value == null)
            {
                valid = false;
                DialogHelper.ShowWarningInfo(this, "El campo Fecha de Accion es obligatorio");
                return valid;
            }

            if (txtFechaAuto.Value == null)
            {
                valid = false;
                DialogHelper.ShowWarningInfo(this, "El campo Fecha de Autorizacion es obligatorio");
                return valid;
            }

            if (txtResolution.Text == "")
            {
                valid = false;
                DialogHelper.ShowWarningInfo(this, "El campo Resolucion es obligatorio");
                return valid;
            }

           
            if (txtSerie.Text == "")
            {
                valid = false;
                DialogHelper.ShowWarningInfo(this, "El campo Serie es obligatorio");
                return valid;
            }

            if (txtDe.Text == "" && txtHasta.Text == "")
            {
                valid = false;
                DialogHelper.ShowWarningInfo(this, "El campo Rango Factura es obligatorio");
                return valid;
            }

            return valid;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateFields())
                    return;

                if (IdResolution != 0)
                {
                    ResolutionFiscal resObj = dc.ResolutionFiscal.Single(r => r.IdResolution == IdResolution);
                    resObj.FechaAccion = txtFechaAcc.Value;
                    resObj.FechaAuto = txtFechaAuto.Value;
                    resObj.Resolution = txtResolution.Text;
                    resObj.RangeInit = Convert.ToInt32(txtDe.Text);
                    resObj.RangeFinal = Convert.ToInt32(txtHasta.Text);
                    resObj.Serie = txtSerie.Text;
                    resObj.Active = checkActive.Checked;

                    Shop oShopEdit = dc.Shops.Single(o => o.Id == ApplicationHelper.GetCurrentShop().Id);
                    //oShopEdit.String1 = "0";
                    string idUdcEdit = DataHelper.GetSerie(dc).Id;
                    UDC udcSerieEdit = dc.UDCs.Single(o => o.Id == idUdcEdit);
                    udcSerieEdit.Name = txtSerie.Text;

                    dc.SubmitChanges();
                    DialogHelper.ShowInformation(this, "Resolucion Fiscal Modificada");
                    this.Close();
                    return;
                }

                ResolutionFiscal resFiscal = new ResolutionFiscal();
                resFiscal.Id = Guid.NewGuid();
                resFiscal.FechaAccion = txtFechaAcc.Value;
                resFiscal.FechaAuto = txtFechaAuto.Value;
                resFiscal.Resolution = txtResolution.Text;
                resFiscal.RangeInit = Convert.ToInt32(txtDe.Text);
                resFiscal.RangeFinal = Convert.ToInt32(txtHasta.Text);
                resFiscal.Serie = txtSerie.Text;
                resFiscal.Active = checkActive.Checked; //true;

                Shop oShop = dc.Shops.Single(o=> o.Id == ApplicationHelper.GetCurrentShop().Id);
                //oShop.String1 = "0";
                string idUdc = DataHelper.GetSerie(dc).Id;
                UDC udcSerie = dc.UDCs.Single(o=> o.Id == idUdc);
                udcSerie.Name = txtSerie.Text;

                dc.ResolutionFiscal.InsertOnSubmit(resFiscal);
                dc.SubmitChanges();

                DialogHelper.ShowInformation(this, "Resolucion Fiscal Agregada");
                this.Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Error al guardar la informacion del Dim", ex);
                DialogHelper.ShowError(this, "Error al guardar la informacion del Cliente", ex);
            }
        }

        public void FillData()
        {
            try
            {
                ResolutionFiscal resObj = dc.ResolutionFiscal.Single(r => r.IdResolution == IdResolution);
                txtFechaAcc.Value = resObj.FechaAccion.Value;
                txtFechaAuto.Value = resObj.FechaAuto.Value;
                txtResolution.Text = resObj.Resolution;
                txtSerie.Text = resObj.Serie;
                txtDe.Text = resObj.RangeInit.ToString();
                txtHasta.Text = resObj.RangeFinal.ToString();
                checkActive.Checked = (bool)resObj.Active;
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, "Error al buscar la resolucion fiscal");
                Logger.Error("Error al buscar la resolucion fiscal: ", ex);
            }
        }

        private void FormResolutionFiscal_Load(object sender, EventArgs e)
        {

        }
    }
}
