using System.Collections.Generic;
using System.Web.UI;
using SocialRequirements.AccountService;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Utilities;
using SocialRequirements.Utilities.Security;

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