using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Mega.Common.Helpers
{
    public class MailHelper
    {
        private readonly string mailSmtpServer;
        private readonly int mailSmtpPort;
        private readonly bool mailSSLEnabled;
        private readonly bool mailSmtpNeedsAuthentication;
        private readonly string mailSmtpAuthenticationUser;
        private readonly string mailSmtpAuthenticationPassword;

        public MailHelper(string mailSmtpServer, int mailSmtpPort, bool mailSSLEnabled, bool mailSmtpNeedsAuthentication,
                         string mailSmtpAuthenticationUser, string mailSmtpAuthenticationPassword)
        {
            this.mailSmtpServer = mailSmtpServer;
            this.mailSmtpPort = mailSmtpPort;
            this.mailSSLEnabled = mailSSLEnabled;
            this.mailSmtpNeedsAuthentication = mailSmtpNeedsAuthentication;
            this.mailSmtpAuthenticationUser = mailSmtpAuthenticationUser;
            this.mailSmtpAuthenticationPassword = mailSmtpAuthenticationPassword;
        }

        //imagesPath Server.MapPath("/")
        public void SendMessage(string mailSubject, AlternateView htmlBodyView, string mailFromName, string mailFromAddress,
                string mailToAddressList, string mailCcAddressList, string mailBccAddressList)
        {
            SendMessage(mailSubject, htmlBodyView, mailFromName, mailFromAddress,
                        mailToAddressList, mailCcAddressList, mailBccAddressList, null);
        }

        //imagesPath Server.MapPath("/")
        public void SendMessage(string mailSubject, AlternateView htmlBodyView, string mailFromName, string mailFromAddress,
                string mailToAddressList, string mailCcAddressList, string mailBccAddressList, IEnumerable<Attachment> attachments)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(mailFromAddress, mailFromName);
            mailMessage.To.Add(mailToAddressList);

            if (!string.IsNullOrEmpty(mailCcAddressList.Trim()))
            {
                mailMessage.CC.Add(mailCcAddressList);
            }

            if (!string.IsNullOrEmpty(mailBccAddressList.Trim()))
            {
                mailMessage.Bcc.Add(mailBccAddressList);
            }

            mailMessage.Subject = mailSubject;
            //mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.AlternateViews.Add(htmlBodyView);

            if (attachments != null)
            {
                attachments.ToList().ForEach(e => mailMessage.Attachments.Add(e));
            }

            SmtpClient smtpClient = new SmtpClient(mailSmtpServer, mailSmtpPort);

            smtpClient.EnableSsl = mailSSLEnabled;

            if (mailSmtpNeedsAuthentication)
            {
                NetworkCredential networkCredential = new NetworkCredential(mailSmtpAuthenticationUser,
                                                                            mailSmtpAuthenticationPassword);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = networkCredential;
            }

            smtpClient.Send(mailMessage);
        }

        public void SendPlainTextMessage(string mailSubject, string plainTextBody, string mailFromName, string mailFromAddress,
                string mailToAddressList, string mailCcAddressList, string mailBccAddressList)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(mailFromAddress, mailFromName);
            mailMessage.To.Add(mailToAddressList);

            if (!string.IsNullOrEmpty(mailCcAddressList.Trim()))
            {
                mailMessage.CC.Add(mailCcAddressList);
            }

            if (!string.IsNullOrEmpty(mailBccAddressList.Trim()))
            {
                mailMessage.Bcc.Add(mailBccAddressList);
            }

            mailMessage.Subject = mailSubject;
            mailMessage.Body = plainTextBody;

            SmtpClient smtpClient = new SmtpClient(mailSmtpServer, mailSmtpPort);

            smtpClient.EnableSsl = mailSSLEnabled;

            if (mailSmtpNeedsAuthentication)
            {
                NetworkCredential networkCredential = new NetworkCredential(mailSmtpAuthenticationUser,
                                                                            mailSmtpAuthenticationPassword);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = networkCredential;
            }

            smtpClient.Send(mailMessage);
        }
    }
}
