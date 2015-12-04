using System.Collections.Generic;
using System.Web.UI;
using SocialRequirements.AccountService;
using SocialRequirements.Domain.DTO;
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
        
        /// <summary>
        /// Get related companies to the current user
        /// </summary>
        protected List<CompanyDto> GetRelatedCompanies()
        {
            if (!UserLoggedIn()) return new List<CompanyDto>();

            var personService = new AccountSoapClient();
            var encUsername = Encryption.Encrypt(Username);
            var companies = personService.GetUserCompanies(encUsername);

            var serializer = new ObjectSerializer<List<CompanyDto>>();
            return (List<CompanyDto>)serializer.Deserialize(companies);
        }
    }
}