using System;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using log4net;
using Mega.Admin.Code;
using Mega.Admin.Code.Helpers;
using Mega.Common;
using Mega.Common.Helpers;

namespace Mega.Admin.Synchronization
{
    public partial class ServerConfiguration : CommonPage
    {
        readonly AdminDataContext dc = PortalHelper.GetNewAdminDataContext();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ServerConfiguration));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadItemData();
            }
        }

        private void LoadItemData()
        {
            var config = DataHelper.GetUDCWebSynch(dc);

            var daysIn = config.Optional3.Split(',');

            for (var i = 0; i < daysIn.Length; i++)
            {
                DaysInCheckBoxList.Items[i].Selected = daysIn[i].Equals("1");
            }

            HoursPlanInTextBox.Text = config.Optional4;

            var daysOut = config.Optional1.Split(',');

            for (var i = 0; i < daysOut.Length; i++)
            {
                DaysOutCheckBoxList.Items[i].Selected = daysOut[i].Equals("1");
            }

            HoursPlanOutTextBox.Text = config.Optional2;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    var config = DataHelper.GetUDCWebSynch(dc);

                    var daysIn = DaysInCheckBoxList.Items.Cast<object>().Aggregate(string.Empty, (current, item) => current + (((ListItem)item).Selected ? "1," : "0,")).TrimEnd(',');
                    config.Optional3 = daysIn;
                    config.Optional4 = HoursPlanInTextBox.Text;

                    var daysOut = DaysOutCheckBoxList.Items.Cast<object>().Aggregate(string.Empty, (current, item) => current + (((ListItem)item).Selected ? "1," : "0,")).TrimEnd(',');
                    config.Optional1 = daysOut;
                    config.Optional2 = HoursPlanOutTextBox.Text;

                    dc.SubmitChanges();

                    PortalHelper.ShowMessage(this, "Los datos han sido guardados correctamente");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error salvando las Configuraciones de Sincronización Web.", ex);

                PortalHelper.ShowMessage(this, DataHelper.GetDefaultAddExceptionMessage(ex));
            }

        }

        protected void GeneralCustomValidator_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            var sb = new StringBuilder();

            var invalidHoursIn = ToolHelper.GetInvalidHours(HoursPlanInTextBox.Text.Split(','));

            if (!string.IsNullOrWhiteSpace(invalidHoursIn))
                sb.AppendLine(string.Format("El campo Horas de Importación tiene valores incorrectos [{0}].", invalidHoursIn));

            var invalidHoursOut = ToolHelper.GetInvalidHours(HoursPlanOutTextBox.Text.Split(','));

            if (!string.IsNullOrWhiteSpace(invalidHoursOut))
                sb.AppendLine(string.Format("El campo Horas de Exportación tiene valores incorrectos [{0}].", invalidHoursIn));

            if (sb.Length > 0)
            {
                sb.Insert(0, "Antes de continuar debe corregir los siguientes errores: ");
                GeneralCustomValidator.ErrorMessage = sb.ToString();

                args.IsValid = false;
                PortalHelper.ShowMessage(this, GeneralCustomValidator.ErrorMessage.Replace("\r\n", "-").TrimEnd('-'));
                return;
            }

            args.IsValid = true;
        }

    }


}