using System;
using System.Web.UI;
using SocialRequirements.AccountService;
using SocialRequirements.Domain.General;
using SocialRequirements.Utilities;
using SocialRequirements.Utilities.Security;

namespace SocialRequirements.Account
{
    public partial class ForgotPassword : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Forgot(object sender, EventArgs e)
        {
            if (!IsValid) return;

            // Validate the user's email address
            var personService = new AccountSoapClient();
            var encEmail = Encryption.Encrypt(Email.Text);
            if (!personService.UserExists(encEmail))
            {
                FailureText.Text = "The user either does not exist or is not confirmed.";
                ErrorMessage.Visible = true;
                return;
            }

            try
            {
                SendEmail(Email.Text, GetSetPasswordUrl(encEmail));
                loginForm.Visible = false;
                DisplayEmail.Visible = true;
                ErrorMessage.Visible = false;
            }
            catch (Exception ex)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = ex.Message;
            }
        }

        private string GetSetPasswordUrl(string encEmail)
        {
            var url = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf('/') + 1) + CommonConstants.FormsFileName.SetPassword;            
            url += "?" + CommonConstants.QueryStringParams.Id + "=" + encEmail;
            return url;
        }

        private static void SendEmail(string to, string url)
        {
            var bodyHtml = "Please reset your password by clicking <a href=\"" + url + "\">here</a>.";
            var bodyPlain = "Please reset your password by copying this URL in your broswer address bar:  " + url;
            EmailUtilities.SendEmail(to, "Password recovery", bodyPlain, bodyHtml);
        }
    }
}