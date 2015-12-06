using System;
using System.Collections.Generic;
using System.Linq;
using SocialRequirements.AccountService;
using SocialRequirements.CompanyService;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Utilities;
using SocialRequirements.Utilities.Security;

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
            //FormsAuthentication.RedirectToLoginPage();
            Response.Redirect("~/Account/Login.aspx");
        }

        /// <summary>
        /// Check if there is at least one requirement for the specified companies
        /// </summary>
        /// <returns>True if there is at least one requirement, false when none.</returns>
        protected bool CheckRequirements(List<CompanyDto> companies)
        {
            var companySrv = new CompanySoapClient();
            return companies.All(company => companySrv.HaveRequirements(company.Id));
        }

        /// <summary>
        /// Get related companies to the current user
        /// </summary>
        protected List<CompanyDto> GetRelatedCompanies()
        {
            if (!UserLoggedIn()) return new List<CompanyDto>();

            var personService = new AccountSoapClient();
            var encUsername = GetUsernameEncrypted();
            var companies = personService.GetUserCompanies(encUsername);

            var serializer = new ObjectSerializer<List<CompanyDto>>();
            return (List<CompanyDto>)serializer.Deserialize(companies);
        }

        /// <summary>
        /// Check if there is at least one project for the specified companies
        /// </summary>
        /// <returns>True if there is at least one project, false when none.</returns>
        protected bool CheckProjects(List<CompanyDto> companies)
        {
            var companySrv = new CompanySoapClient();
            return companies.All(company => companySrv.HaveProjects(company.Id));
        }

        protected string GetUsernameEncrypted()
        {
            return Encryption.Encrypt(Username);
        }
    }
}