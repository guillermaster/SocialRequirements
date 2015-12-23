using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocialRequirements.AccountService;
using SocialRequirements.CompanyService;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.General;
using SocialRequirements.RequirementService;
using SocialRequirements.Utilities;
using SocialRequirements.Utilities.Security;

namespace SocialRequirements
{
    public class SocialRequirementsPrivatePage : SocialRequirementsBasePage
    {
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            ValidateUserLoggedIn();
        }

        protected void ValidateUserLoggedIn()
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

        /// <summary>
        /// Sets the datasource for a company list control
        /// </summary>
        /// <param name="companyList">Control to update</param>
        protected void SetCompanies(ListControl companyList)
        {
            var accountSrv = new AccountSoapClient();
            var companiesPerUserRes = accountSrv.GetUserCompanies(GetUsernameEncrypted());

            var serializer = new ObjectSerializer<List<CompanyDto>>();
            var result = serializer.Deserialize(companiesPerUserRes);

            companyList.DataSource = (List<CompanyDto>)result;
            companyList.DataTextField = CustomExpression.GetPropertyName<CompanyDto>(p => p.Name);
            companyList.DataValueField = CustomExpression.GetPropertyName<CompanyDto>(p => p.Id);
            companyList.DataBind();
            companyList.Items.Insert(0, new ListItem("- Select Company -", string.Empty));
            companyList.SelectedIndex = 0;
        }

        protected void Like(long companyId, long? projectId, long recordId, int entity)
        {
            switch (entity)
            {
                case (int)GeneralCatalog.Detail.Entity.Requirement:
                    if (!projectId.HasValue) return;
                    var requirementSrv = new RequirementSoapClient();
                    requirementSrv.LikeRequirement(companyId, projectId.Value, recordId, GetUsernameEncrypted());
                    break;
            }
        }

        protected void Dislike(long companyId, long? projectId, long recordId, int entity)
        {
            switch (entity)
            {
                case (int)GeneralCatalog.Detail.Entity.Requirement:
                    if (!projectId.HasValue) return;
                    var requirementSrv = new RequirementSoapClient();
                    requirementSrv.DislikeRequirement(companyId, projectId.Value, recordId, GetUsernameEncrypted());
                    break;
            }
        }

        protected void SetFadeOutMessage(UpdatePanel updatePanel, Panel parentPanel, Label messageLabel, string message)
        {
            messageLabel.Text = message;
            parentPanel.Visible = true;
            ScriptManager.RegisterClientScriptBlock(updatePanel, updatePanel.GetType(),
                "posterrorfadeout", "fadeOutControl('#" + parentPanel.ID + "')", true);
        }

        protected UpdatePanel GetMainUpdatePanel(Page page)
        {
            var masterPage = (SiteMaster) page.Master;
            if(masterPage == null) throw new InvalidDataException("Invalid update panel or not found.");
            return masterPage.GetUpdatePanel();
        }
    }
}