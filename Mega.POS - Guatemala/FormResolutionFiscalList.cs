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
    public partial class FormResolutionFiscalList : Form
    {
        public FormResolutionFiscalList()
        {
            InitializeComponent();
        }

        private static readonly ILog Logger = LogManager.GetLogger(typeof(FormResolutionFiscal));
        private AdminDataContext dc = ApplicationHelper.GetPosDataContext();

        private void FormResolutionFiscalList_Load(object sender, EventArgs e)
        {
            FillResolution();
        }

        private void FillResolution()
        {
            try
            {
                
                var resFiscalList = from rf in dc.ResolutionFiscal
                                    select new { 
                                        IdResolution = rf.IdResolution, 
                                        FechaAccion = rf.FechaAccion, 
                                        FechaAuto = rf.FechaAuto, 
                                        Resolution = rf.Resolution, 
                                        Rango = "DE " + rf.RangeInit.ToString() + " AL " + rf.RangeFinal.ToString(),
                                        Active = rf.Active
                                        };
                
                datGridResolution.DataSource = resFiscalList;
                //datGridResolution.Refresh();
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, "Error al cargar las resoluciones fiscales");
                Logger.Error("Error al cargar las resoluciones fiscales: ", ex);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                var resFiscalList = from rf in dc.ResolutionFiscal where rf.Resolution.Contains(txtResolution.Text)
                                     select new { 
                                         IdResolution = rf.IdResolution, 
                                         FechaAccion = rf.FechaAccion,
                                         FechaAuto = rf.FechaAuto,
                                         Resolution = rf.Resolution,
                                         Rango = "DE " + rf.RangeInit.ToString() + " AL " + rf.RangeFinal.ToString(),
                                         Active  = rf.Active};

                datGridResolution.DataSource = resFiscalList;
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, "Error al buscar la resolucion fiscal");
                Logger.Error("Error al buscar la resolucion fiscal: ", ex);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var frmRf = new FormResolutionFiscal();
            frmRf.ShowDialog(this);
            this.Close();
            //FillResolution();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                var id = datGridResolution.SelectedRows[0].Cells[0].Value;
                var frmRf = new FormResolutionFiscal();
                frmRf.IdResolution = Convert.ToInt16(id);
                frmRf.FillData();
                frmRf.ShowDialog(this);
                this.Close();
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, "Error al editar la resolucion fiscal");
                Logger.Error("Error al editar la resolucion fiscal: ", ex);
            }
        }
    }

    public partial class ResolutionObj
    {
        private int idResolution;
        public int IdResolution { set { idResolution = value; } get { return idResolution; } }
        private DateTime fechaAccion;
        public DateTime FechaAccion { set { fechaAccion = value; } get { return fechaAccion; } }
        private DateTime fechaAuto;
        public DateTime FechaAuto { set { fechaAuto = value; } get { return fechaAuto; } }
        private string resolution;
        public string Resolution { set { resolution = value; } get { return resolution; } }
        private string rango;
        public string Rango { set { rango = value; } get { return rango; } }
    }
}
