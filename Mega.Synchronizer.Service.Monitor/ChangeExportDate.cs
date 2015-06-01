using System;
using System.Linq;
using System.Windows.Forms;
using log4net;
using Mega.Common;
using Mega.Common.Helpers;
using Mega.Synchronizer.Service.Monitor.Helpers;
using Mega.Synchronizer.Service.Monitor.Properties;

namespace Mega.Synchronizer.Service.Monitor
{
    public partial class ChangeExportDate : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ChangeExportDate));
        AdminDataContext dc = ApplicationHelper.GetPosDataContext();

        public ChangeExportDate()
        {
            InitializeComponent();
        }

        private void ChangeExportDate_Load(object sender, EventArgs e)
        {
            ExportDateTimePicker.CustomFormat = ToolHelper.GetConvertionDateTimeFormat();

            var synch = dc.Synchronizations.Where(s => s.IdShop == Settings.Default.CurrentShop).FirstOrDefault();

            if (synch.LastInitialDateOut != null)
            {
                ExportDateTimePicker.Value = (DateTime)synch.LastInitialDateOut;
                NotExistsLabel.Visible = false;
            }
            else
            {
                NotExistsLabel.Visible = true;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var synchDb = dc.Synchronizations.Where(s => s.IdShop == Settings.Default.CurrentShop).FirstOrDefault();

                synchDb.LastInitialDateOut = ExportDateTimePicker.Value;

                dc.SubmitChanges();

                Logger.InfoFormat(
                    "Se ha actualizado la fecha inicial para la próxima exportación de la tienda {0} a [{1}].",
                    Settings.Default.CurrentShop, ExportDateTimePicker.Value);

                DialogHelper.ShowInformation(this, "La fecha ha sido actualizada satisfactoriamente.");
            }
            catch (Exception ex)
            {
                Logger.Error("Ha ocurrido un error actualizando la fecha inicial para la próxima exportación.", ex);
                DialogHelper.ShowError(this, "Ha ocurrido un error actualizando la fecha inicial para la próxima exportación", ex);
            }

            Close();
        }
    }
}
