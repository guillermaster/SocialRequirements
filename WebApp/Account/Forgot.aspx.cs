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
            
            SendEmail(GetSetPasswordUrl(encEmail));
            loginForm.Visible = false;
            DisplayEmail.Visible = true;
        }

        private string GetSetPasswordUrl(string encEmail)
        {
            var url = ResolveUrl(CommonConstants.FormsUrl.SetPassword);
            url += "?" + CommonConstants.QueryStringParams.Id + "=" + encEmail;
            return url;
        }

        private void SendEmail(string url)
        {
            var body = "Please reset your password by clicking <a href=\"" + url + "\">here</a>.";
            
        }
    }
}