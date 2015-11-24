using System;
using System.Web.Security;
using System.Web.UI;

namespace SocialRequirements
{
    public class SocialRequirementsPage : Page
    {
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated == false) { FormsAuthentication.RedirectToLoginPage(); };
        }
    }
}