using System;
using System.Net.Mail;
using System.Web.UI;
using log4net;
using Mega.Admin.Code;
using Mega.Admin.Code.Helpers;

namespace Mega.Admin
{
    public partial class Error : Page
    {
        protected static readonly ILog Logger = LogManager.GetLogger(typeof(CommonPage));

        protected string[] ErrorList = new[] 
            { 
                "The client disconnected.", 
                "Validation of viewstate MAC failed." 
            };


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Exception anError = Server.GetLastError();
                if (anError != null)
                {
                    if (ErrorMustBeRedirected(anError.Message))
                    {
                        Response.Redirect("~/Default.aspx");
                    }

                    string extaMessgeInfo = string.Format("Host: {0}.", Request.UserHostAddress);
                    string extaDetailInfo = string.Format("Dirección: {0}. Nombre: {1}. Método: {2}. Tipo: {3}. Agente: {4}. ", Request.UserHostAddress, Request.UserHostName, Request.HttpMethod, Request.RequestType, Request.UserAgent);

                    lblMessage.Text = extaMessgeInfo + lblMessage.Text + anError.Message;
                    lblSource.Text += anError.Source;
                    lblException.Text += anError.TargetSite + " - " + (anError.InnerException != null ? anError.InnerException.Message : "No hay Exception interna");
                    lblMoreDetail.Text = extaDetailInfo + lblMoreDetail.Text + anError.StackTrace;

                    try
                    {
                        string templatePath = Server.MapPath("~/MailTemplate/error-mail.xslt");

                        AlternateView htmlBody = MailTemplateHelper.ErrorMessageBody(
                        templatePath, lblMessage.Text, lblSource.Text, lblException.Text, lblMoreDetail.Text);

                        MailTemplateHelper.SendErrorMessage("Error en el sitio de " + WebConfiguration.PortalName, htmlBody);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(
                            string.Format("Error enviando mail de Error." + ex.Message));
                    }

                    Server.ClearError();

                    errorDetailUpdatePanel.Visible = WebConfiguration.PageErrorDetailVisible;
                }
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            lblMoreDetail.Visible = !lblMoreDetail.Visible;
            LinkDetail.Text = lblMoreDetail.Visible ? "Ocultar detalles <<" : "Mostrar detalles >>";
        }

        private bool ErrorMustBeRedirected(string message)
        {
            foreach (string s in ErrorList)
            {
                if (message.Contains(s))
                    return true;
            }

            return false;
        }
    }
}