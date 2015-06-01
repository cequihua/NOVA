using System.IO;
using System.Net.Mail;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using Mega.Common;
using Mega.Common.Helpers;

namespace Mega.Admin.Code.Helpers
{
    public class MailTemplateHelper
    {
        private static string RetrieveMessageBody(XmlDocument body, string templatePath)
        {
            XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
            xslCompiledTransform.Load(templatePath);
            MemoryStream memoryStream = new MemoryStream();

            xslCompiledTransform.Transform(new XmlNodeReader(body.DocumentElement), null, memoryStream);

            byte[] byteArray = new byte[memoryStream.Length];

            memoryStream.Seek(0, SeekOrigin.Begin);
            memoryStream.Read(byteArray, 0, (int)memoryStream.Length);

            char[] charArray = new char[xslCompiledTransform.OutputSettings.Encoding.GetCharCount(byteArray, 0, byteArray.Length)];
            xslCompiledTransform.OutputSettings.Encoding.GetDecoder().GetChars(byteArray, 0, byteArray.Length, charArray, 0);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(charArray);

            string bodyWithoutStyle = stringBuilder.ToString();

            //bodyWithoutStyle = bodyWithoutStyle.Replace("##INCLUDE_STYLE##", File.ReadAllText(cssStylePath));

            return bodyWithoutStyle;
        }

        public static AlternateView RetrieveUserNotificationBody(string templatePath,
                string imagesPath, string providerLogoImageName, User userData)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlElement rootElement = xmlDocument.CreateElement("message");

            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "login", userData.Id));
            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "userName", userData.Name));
            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "password", userData.Password));
            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "validDate", userData.ValidDate.ToString(WebConfiguration.MailDateFormat)));

            string roles = string.Empty;
            foreach (var userRol in userData.User_Rols)
            {
                roles += "[" + userRol.UDCItem.Name + "] ";
            }

            string shops = string.Empty;
            foreach (var userShops in userData.User_Shops)
            {
                shops += "[" + userShops.Shop.Name + "] ";
            }

            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "roles", roles));
            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "shops", shops));
            
            xmlDocument.AppendChild(rootElement);

            string body = RetrieveMessageBody(xmlDocument, templatePath);

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

            LinkedResource companyLogo = new LinkedResource(imagesPath + providerLogoImageName);
            companyLogo.ContentId = "CompanyLogo";
            htmlView.LinkedResources.Add(companyLogo);

            return htmlView;
        }

        private static XmlAttribute CreateXmlAttribute(XmlDocument xmlDocument, string name, string value)
        {
            XmlAttribute xmlAttribute = xmlDocument.CreateAttribute(name);
            xmlAttribute.Value = value;

            return xmlAttribute;
        }

        public static void SendMessage(string subject, AlternateView htmlBodyView, string toAddresses, string ccAdresses, string bccAdresses)
        {
            MailHelper mail = new MailHelper(WebConfiguration.MailSmtpServer,
                         WebConfiguration.MailSmtpPort,
                         WebConfiguration.MailSSLEnabled,
                          WebConfiguration.MailSmtpNeedsAuthentication,
                          WebConfiguration.MailSmtpAuthenticationUser,
                          WebConfiguration.MailSmtpAuthenticationPassword);

            mail.SendMessage(subject, htmlBodyView, WebConfiguration.MailFromName,
                        WebConfiguration.MailFromEMail, toAddresses, ccAdresses, bccAdresses);
        }

        public static AlternateView ErrorMessageBody(
                string templatePath, string message, string source, string exception, string detail)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlElement rootElement = xmlDocument.CreateElement("message");

            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "message", message));
            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "source", source));
            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "exception", exception));
            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "detail", detail));

            xmlDocument.AppendChild(rootElement);

            string body = RetrieveMessageBody(xmlDocument, templatePath);

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

            return htmlView;
        }

        public static void SendErrorMessage(string subject, AlternateView htmlBodyView)
        {
            MailHelper mail = new MailHelper(WebConfiguration.MailSmtpServer,
                          WebConfiguration.MailSmtpPort,
                          WebConfiguration.MailSSLEnabled,
                          WebConfiguration.MailSmtpNeedsAuthentication,
                          WebConfiguration.MailSmtpAuthenticationUser,
                          WebConfiguration.MailSmtpAuthenticationPassword);

            mail.SendMessage(subject, htmlBodyView, WebConfiguration.MailFromName,
                        WebConfiguration.MailFromEMail, WebConfiguration.MailAdminAddress, string.Empty, string.Empty);
        }

        public static void SendPlainTextMessage(string subject, string plainTextBody,
             string mailToAddressList, string mailCcAddressList, string mailBccAddressList)
        {
            MailHelper mail = new MailHelper(WebConfiguration.MailSmtpServer,
                          WebConfiguration.MailSmtpPort,
                          WebConfiguration.MailSSLEnabled,
                          WebConfiguration.MailSmtpNeedsAuthentication,
                          WebConfiguration.MailSmtpAuthenticationUser,
                          WebConfiguration.MailSmtpAuthenticationPassword);

            mail.SendPlainTextMessage(subject, plainTextBody, WebConfiguration.MailFromName,
                        WebConfiguration.MailFromEMail, mailToAddressList, 
                        mailCcAddressList, mailBccAddressList);
        }
    }
}