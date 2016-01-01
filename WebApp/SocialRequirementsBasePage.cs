using System.Web.UI;

namespace SocialRequirements
{
    public class SocialRequirementsBasePage : Page
    {
        protected string Username
        {
            get { return Session["Username"] != null ? Session["Username"].ToString() : null; }
            set { Session["Username"] = value; }
        }

        /// <summary>
        /// Stores current user data in XML format
        /// </summary>
        protected string UserData
        {
            get { return Session["UserData"] != null ? Session["UserData"].ToString() : null; }
            set { Session["UserData"] = value; }
        }
        
        protected bool UserLoggedIn()
        {
            return Username != null;
        }
    }
}