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
            var text = bodyPlainText;
            var html = @bodyHtml;
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
            
            // Init SmtpClient and send
            var smtpClient = new SmtpClient(Environment.GetEnvironmentVariable("emailSender"), Convert.ToInt32(587));
            var credentials =
                new NetworkCredential(
                    Environment.GetEnvironmentVariable("emailSenderUser"),
                    Environment.GetEnvironmentVariable("emailSenderPwd"));
            smtpClient.Credentials = credentials;

            if(!async)
                smtpClient.Send(mailMsg);
            else
                smtpClient.SendAsync(mailMsg, null);
        }
    }
}
