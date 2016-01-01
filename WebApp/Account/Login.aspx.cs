using System;
using SocialRequirements.AccountService;
using SocialRequirements.Utilities.Security;

namespace SocialRequirements.Account
{
    public partial class Login : SocialRequirementsPublicPage
    {
        protected void LogIn(object sender, EventArgs e)
        {
            if (!IsValid) return;

            var personService = new AccountSoapClient();
            var encEmail = Encryption.Encrypt(Email.Text);
            var encPassword = Encryption.Encrypt(Password.Text);

            if (personService.ValidatePassword(encEmail, encPassword))
            {
                InitUserSession(Email.Text);
            }
            else
            {
                FailureText.Text = "Invalid login attempt";
                ErrorMessage.Visible = true;
            }
        }
    }
}