using System.Web.UI;

namespace SocialRequirements
{
    public class SocialRequirementsBasePage : Page
    {
        protected string Username
        {
            get { return Session["Username"] != null ? Session["Username"].ToString() : null; }
            set { Session["username"] = value; }
        }

        protected bool UserLoggedIn()
        {
            return Username != null;
        }
    }
}