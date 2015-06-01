using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using Mega.Common;
using Mega.Common.Helpers;

namespace Mega.POS.Helper
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

        private static XmlAttribute CreateXmlAttribute(XmlDocument xmlDocument, string name, string value)
        {
            XmlAttribute xmlAttribute = xmlDocument.CreateAttribute(name);
            xmlAttribute.Value = value;

            return xmlAttribute;
        }

        public static void SendMessage(string subject, AlternateView htmlBodyView, string toAddresses, string ccAdresses, string bccAdresses)
        {
            MailHelper mail = new MailHelper(Properties.Settings.Default.MailSmtpServer,
                         Properties.Settings.Default.MailSmtpPort,
                         Properties.Settings.Default.MailSSLEnabled,
                          Properties.Settings.Default.MailSmtpNeedsAuthentication,
                          Properties.Settings.Default.MailSmtpAuthenticationUser,
                          Properties.Settings.Default.MailSmtpAuthenticationPassword);

            mail.SendMessage(subject, htmlBodyView, Properties.Settings.Default.MailFromName,
                        Properties.Settings.Default.MailFromEMail, toAddresses, ccAdresses, bccAdresses);
        }

        public static void SendMessage(string subject, AlternateView htmlBodyView, string toAddresses, string ccAdresses, string bccAdresses, IEnumerable<Attachment> attachments)
        {
            var mail = new MailHelper(Properties.Settings.Default.MailSmtpServer,
                         Properties.Settings.Default.MailSmtpPort,
                         Properties.Settings.Default.MailSSLEnabled,
                          Properties.Settings.Default.MailSmtpNeedsAuthentication,
                          Properties.Settings.Default.MailSmtpAuthenticationUser,
                          Properties.Settings.Default.MailSmtpAuthenticationPassword);

            mail.SendMessage(subject, htmlBodyView, Properties.Settings.Default.MailFromName,
                        Properties.Settings.Default.MailFromEMail, toAddresses, ccAdresses, bccAdresses, attachments);
        }

        public static AlternateView RetrieveErrorMessageBody(
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
            MailHelper mail = new MailHelper(Properties.Settings.Default.MailSmtpServer,
                          Properties.Settings.Default.MailSmtpPort,
                          Properties.Settings.Default.MailSSLEnabled,
                          Properties.Settings.Default.MailSmtpNeedsAuthentication,
                          Properties.Settings.Default.MailSmtpAuthenticationUser,
                          Properties.Settings.Default.MailSmtpAuthenticationPassword);

            mail.SendMessage(subject, htmlBodyView, Properties.Settings.Default.MailFromName,
                        Properties.Settings.Default.MailFromEMail, Properties.Settings.Default.MailAdminAddress, string.Empty, string.Empty);
        }

        public static AlternateView RetrieveCashierCloseNotOkNotificationBody(string templatePath,
               string imagesPath, CashierClose cc)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlElement rootElement = xmlDocument.CreateElement("message");

            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "shop", cc.Cashier.Shop.Name));
            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "cashier", cc.Cashier.Name));
            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "consecutive", cc.Consecutive.ToString()));
            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "modifiedBy", cc.ModifiedBy));
            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "date", cc.AddedDate.ToString(Properties.Settings.Default.MailDateFormat)));

            xmlDocument.AppendChild(rootElement);

            string body = RetrieveMessageBody(xmlDocument, templatePath);

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

            LinkedResource companyLogo = new LinkedResource(imagesPath + Properties.Settings.Default.MailLogoImageName);
            companyLogo.ContentId = "CompanyLogo";
            htmlView.LinkedResources.Add(companyLogo);

            return htmlView;
        }

        public static void SendPlainTextMessage(string subject, string plainTextBody,
             string mailToAddressList, string mailCcAddressList, string mailBccAddressList)
        {
            MailHelper mail = new MailHelper(Properties.Settings.Default.MailSmtpServer,
                          Properties.Settings.Default.MailSmtpPort,
                          Properties.Settings.Default.MailSSLEnabled,
                          Properties.Settings.Default.MailSmtpNeedsAuthentication,
                          Properties.Settings.Default.MailSmtpAuthenticationUser,
                          Properties.Settings.Default.MailSmtpAuthenticationPassword);

            mail.SendPlainTextMessage(subject, plainTextBody, Properties.Settings.Default.MailFromName,
                        Properties.Settings.Default.MailFromEMail, mailToAddressList,
                        mailCcAddressList, mailBccAddressList);
        }

        public static AlternateView RetrieveReceiptNotOkNotificationBody(string templatePath, string imagesPath, Common.Operation receipt)
        {
            var xmlDocument = new XmlDocument();
            var rootElement = xmlDocument.CreateElement("message");

            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "shop", receipt.Shop.Name));
            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "consecutive", receipt.Consecutive.ToString()));
            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "modifiedBy", receipt.ModifiedBy));
            rootElement.Attributes.Append(CreateXmlAttribute(xmlDocument, "date", receipt.AddedDate.ToString(Properties.Settings.Default.MailDateFormat)));

            xmlDocument.AppendChild(rootElement);

            var body = RetrieveMessageBody(xmlDocument, templatePath);

            var htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

            var companyLogo = new LinkedResource(imagesPath + Properties.Settings.Default.MailLogoImageName);
            companyLogo.ContentId = "CompanyLogo";
            htmlView.LinkedResources.Add(companyLogo);

            return htmlView;
        }
    }
}