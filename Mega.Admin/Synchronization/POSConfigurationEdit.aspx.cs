using System;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using log4net;
using Mega.Admin.Code;
using Mega.Admin.Code.Helpers;
using Mega.Admin.Shop;
using Mega.Common;
using Mega.Common.Helpers;

namespace Mega.Admin.Synchronization
{
    public partial class POSConfigurationEdit : CommonPage
    {
        protected static readonly ILog Logger = LogManager.GetLogger(typeof(ShopEdit));
        readonly AdminDataContext Dc = PortalHelper.GetNewAdminDataContext();

        protected string Id
        {
            get { return Request["id"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(Id))
                    {
                        throw new ArgumentNullException("id", Resources.CommonStrings.PageInvalidParams);
                    }

                    LastInitialDateInLabel.Text = string.Format("Nueva Fecha de envío de actualización ({0}):", ToolHelper.GetDateTimeFormat());
                    LastFinalDateOutLabel.Text = string.Format("Nueva Fecha de Exportación ({0}):", ToolHelper.GetDateTimeFormat());

                    DateFormat.Value = ToolHelper.GetJSConvertionDateFormat();

                    LoadItemData();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error en POSConfiguration.Page_Load. Detalle: " + ex.Message);
                    throw;
                }
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    UpdateSynchronization();
                    Response.Redirect("~/Synchronization/POSConfiguration.aspx");
                }
                else
                {
                    PortalHelper.ThrowInvalidPageException();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error salvando la Configuración de Sincronización.", ex);

                PortalHelper.ShowMessage(this, DataHelper.GetDefaultAddExceptionMessage(ex));
            }
        }

        private void UpdateSynchronization()
        {
            var synchronization = Dc.Synchronizations.Where(c => c.IdShop == Id).Single();

            var daysIn = DaysInCheckBoxList.Items.Cast<object>().Aggregate(string.Empty, (current, item) => current + (((ListItem)item).Selected ? "1," : "0,")).TrimEnd(',');
            synchronization.DaysPlanIn = daysIn;
            synchronization.HoursPlanIn = HoursPlanInTextBox.Text;

            synchronization.LastInitialDateIn = !string.IsNullOrEmpty(LastInitialDateInTextBox.Text) ? (DateTime?)DateTime.Parse(LastInitialDateInTextBox.Text) : null;

            var daysOut = DaysOutCheckBoxList.Items.Cast<object>().Aggregate(string.Empty, (current, item) => current + (((ListItem)item).Selected ? "1," : "0,")).TrimEnd(',');
            synchronization.DaysPlanOut = daysOut;
            synchronization.HoursPlanOut = HoursPlanOutTextBox.Text;
            synchronization.LastFinalDateOut = !string.IsNullOrEmpty(LastFinalDateOutTextBox.Text) ? (DateTime?)DateTime.Parse(LastFinalDateOutTextBox.Text) : null;

            Dc.SubmitChanges();
        }

        private void LoadItemData()
        {
            var synchronization = Dc.Synchronizations.Where(c => c.IdShop == Id).Single();

            IdLabel.Text = synchronization.IdShop;
            NameLabel.Text = synchronization.Shop.Name;

            var daysIn = synchronization.DaysPlanIn.Split(',');

            for (var i = 0; i < daysIn.Length; i++)
            {
                DaysInCheckBoxList.Items[i].Selected = daysIn[i].Equals("1");
            }

            InitalDateInLiteral.Text = synchronization.LastInitialDateIn != null ? ((DateTime)synchronization.LastInitialDateIn).ToString(ToolHelper.GetConvertionDateTimeFormat()) : string.Empty;
            LastInitialDateInTextBox.Text = synchronization.LastInitialDateIn != null ? ((DateTime)synchronization.LastInitialDateIn).ToString(ToolHelper.GetConvertionDateTimeFormat()) : string.Empty;
            LastNotesInTextBox.Text = synchronization.LastNotesIn;

            InitalDateOutLiteral.Text = synchronization.LastInitialDateOut != null ? ((DateTime)synchronization.LastInitialDateOut).ToString(ToolHelper.GetConvertionDateTimeFormat()) : string.Empty;
            FinalDateOutLiteral.Text = synchronization.LastFinalDateOut != null ? ((DateTime)synchronization.LastFinalDateOut).ToString(ToolHelper.GetConvertionDateTimeFormat()) : string.Empty;
            LastFinalDateOutTextBox.Text = synchronization.LastFinalDateOut != null ? ((DateTime)synchronization.LastFinalDateOut).ToString(ToolHelper.GetConvertionDateTimeFormat()) : string.Empty;
            LastNotesOutTextBox.Text = synchronization.LastNotesOut;

            HoursPlanInTextBox.Text = synchronization.HoursPlanIn;

            var daysOut = synchronization.DaysPlanOut.Split(',');

            for (var i = 0; i < daysOut.Length; i++)
            {
                DaysOutCheckBoxList.Items[i].Selected = daysOut[i].Equals("1");
            }

            HoursPlanOutTextBox.Text = synchronization.HoursPlanOut;
        }

        protected void GeneralCustomValidator_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            var sb = new StringBuilder();

            DateTime date;

            //if (!string.IsNullOrWhiteSpace(LastFinalDateInTextBox.Text) && !DateTime.TryParse(LastFinalDateInTextBox.Text, out date))
            //    sb.AppendLine("El campo F. Final de la última Importación debe ser de tipo Fecha/Hora.");

            if (!string.IsNullOrWhiteSpace(LastFinalDateOutTextBox.Text) && !DateTime.TryParse(LastFinalDateOutTextBox.Text, out date))
                sb.AppendLine("El campo F. Final de la última Exportación debe ser de tipo Fecha/Hora.");

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