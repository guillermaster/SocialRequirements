using System;
using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Account;

namespace SocialRequirements
{
    public partial class Default : SocialRequirementsPrivatePage
    {
        #region Constants

        private const string UrlSetCompany = "~/Account/SetCompany.aspx";
        private const string UrlSetProject = "~/Account/SetProject.aspx";

        private const string MsgNoCompanyRelated =
            "You are not related to any company. Please select the company you belong to.";

        private const string MsgNoRequirements = "There is no requirements yet. You can add one below ;)";
        private const string MsgNoProjects = "You have no projects yet. Please add one.";
        #endregion
        #region Properties

        private string RequiredActionUrl 
        {
            get
            {
                return ViewState["RequiredActionUrl"] != null ? ViewState["RequiredActionUrl"].ToString() : string.Empty;
            }
            set { ViewState["RequiredActionUrl"] = value; }
        }

        protected List<CompanyDto> UserCompanies
        {
            get { return ViewState["UserCompanies"] != null ? (List<CompanyDto>)ViewState["UserCompanies"] : new List<CompanyDto>(); }
            set { ViewState["UserCompanies"] = value; }
        }
        #endregion

        #region Main Events
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (IsPostBack) return;

            RequiredActionPanel.Visible = false;
            if (!CheckRelatedCompanies()) return;
            if (!CheckProjects()) return;
            CheckRequirements();
            PostContent.Visible = true;
        }
        #endregion

        #region Validations
        /// <summary>
        /// Check the related companies to the user, if none found
        /// show an alert and link to relate user to a company
        /// </summary>
        private bool CheckRelatedCompanies()
        {
            UserCompanies = GetRelatedCompanies();

            // check if there is at least one company in the result
            if (UserCompanies.Count > 0)
            {
                return true;
            }

            SetRequiredActionPanel(MsgNoCompanyRelated, UrlSetCompany, false);
            return false;
        }

        /// <summary>
        /// Check if there is at least one requirement in 
        /// any of the companies related to current user
        /// </summary>
        /// <returns>True if there is at least one requirement, false when none.</returns>
        private void CheckRequirements()
        {
            var haveRequirements = CheckRequirements(UserCompanies);
            if(!haveRequirements)
                SetRequiredActionPanel(MsgNoRequirements);
        }

        /// <summary>
        /// Check if there is at least one project in 
        /// any of the companies related to current user
        /// </summary>
        /// <returns>True if there is at least one requirement, false when none.</returns>
        private bool CheckProjects()
        {
            var haveProjects = CheckProjects(UserCompanies);
            if (!haveProjects)
                SetRequiredActionPanel(MsgNoProjects, UrlSetProject, false);
            return haveProjects;
        }
        #endregion

        #region Required Action Panel
        private void SetRequiredActionPanel(string message, string actionUrl = null, bool hideActionButton = true)
        {
            RequiredActionMessage.Text = message;
            RequiredActionPanel.Visible = true;
            RequiredActionExecute.Visible = !hideActionButton;
            if(!string.IsNullOrWhiteSpace(actionUrl)) RequiredActionUrl = actionUrl;
        }
        protected void RequiredActionExecute_Click(object sender, EventArgs e)
        {
            Response.Redirect(RequiredActionUrl);
        }
        #endregion

        
    }
}