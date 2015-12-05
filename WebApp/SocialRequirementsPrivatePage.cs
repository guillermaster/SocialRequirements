using System;
using System.Collections.Generic;
using System.Linq;
using SocialRequirements.CompanyService;
using SocialRequirements.Domain.DTO;
using SocialRequirements.Domain.DTO.Account;

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
    }
}