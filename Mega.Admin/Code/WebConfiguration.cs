using System;
using System.Configuration;

namespace Mega.Admin.Code
{
    public class WebConfiguration
    {
        public static string PortalName = ConfigurationManager.AppSettings["PortalName"];
        public static string PortalTitle = ConfigurationManager.AppSettings["PortalTitle"];
       
        public static string MailAdminAddress = ConfigurationManager.AppSettings["MailAdminAddress"];
        public static string MailSmtpServer = ConfigurationManager.AppSettings["MailSmtpServer"];
        public static int MailSmtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["MailSmtpPort"]);
        public static string MailFromName = ConfigurationManager.AppSettings["MailFromName"];
        public static string MailFromEMail = ConfigurationManager.AppSettings["MailFromEMail"];
        public static bool MailSSLEnabled = ConfigurationManager.AppSettings["MailSSLEnabled"] == "1" ? true : false;
        public static bool MailSmtpNeedsAuthentication = ConfigurationManager.AppSettings["MailSmtpNeedsAuthentication"] == "1" ? true : false;
        public static string MailSmtpAuthenticationUser = ConfigurationManager.AppSettings["MailSmtpAuthenticationUser"];
        public static string MailSmtpAuthenticationPassword = ConfigurationManager.AppSettings["MailSmtpAuthenticationPassword"];
        public static string MailLogoImageName =  ConfigurationManager.AppSettings["MailLogoImageName"];
        public static string MailDateFormat = ConfigurationManager.AppSettings["MailDateFormat"];

        public static bool PageErrorDetailVisible = ConfigurationManager.AppSettings["PageErrorDetailVisible"] == "1" ? true : false;

        public static string DefaultHoursPlanIn = ConfigurationManager.AppSettings["DefaultHoursPlanIn"];
        public static string DefaultHoursPlanOut = ConfigurationManager.AppSettings["DefaultHoursPlanOut"];
       
    }
}