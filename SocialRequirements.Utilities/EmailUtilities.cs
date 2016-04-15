using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using SocialRequirements.Utilities.Security;

namespace SocialRequirements.Utilities
{
    public class EmailUtilities
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return string.Equals(addr.Address, email);
            }
            catch
            {
                return false;
            }
        }

        public static void SendEmail(string toAddress, string subject, string bodyPlainText, string bodyHtml, bool async = false)
        {
            var mailMsg = new MailMessage();

            // To
            mailMsg.To.Add(new MailAddress(toAddress));

            // From
            mailMsg.From = new MailAddress("noreply@socialrequirements.azurewebsites.net", "Social Requirements");

            // Subject and multipart/alternative Body
            mailMsg.Subject = subject;
            string text = bodyPlainText;
            string html = @bodyHtml;
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            // Init SmtpClient and send
            var smtpClient = new SmtpClient(Encryption.Decrypt("D8nV2LLnc4uNseMKLIKWqT6LEE272hiuxzWp7ZZbQUw="), Convert.ToInt32(587));
            var credentials =
                new NetworkCredential(
                    Encryption.Decrypt("uh2pDaREQtqvlCPfyAMSIs8kuX5MSn3dfBOtzUhyt60fDMjAHk5tmwdKgYR79wbZ"),
                    Encryption.Decrypt("4bDGsNDnqPJAj+r9uuP95A=="));
            smtpClient.Credentials = credentials;

            if(!async)
                smtpClient.Send(mailMsg);
            else
                smtpClient.SendAsync(mailMsg, null);
        }
    }
}
