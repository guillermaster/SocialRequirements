using System;
using System.Web.Security;

namespace SocialRequirements
{
    public class SocialRequirementsPrivatePage : SocialRequirementsBasePage
    {
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (!UserLoggedIn()) { RedirectToLogin(); }
        }

        protected void Logout()
        {
            Session.Clear();
            RedirectToLogin();
        }

        private void RedirectToLogin()
        {
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}