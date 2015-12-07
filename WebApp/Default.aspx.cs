using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.ProjectService;
using SocialRequirements.RequirementService;
using SocialRequirements.Utilities;

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

        /// <summary>
        /// Dictionary of projects list identified by company
        /// </summary>
        protected Dictionary<long, List<ProjectDto>> Projects
        {
            get
            {
                return ViewState["Projects"] != null
                    ? (Dictionary<long, List<ProjectDto>>)ViewState["Projects"]
                    : new Dictionary<long, List<ProjectDto>>();
            }
            set { ViewState["Projects"] = value; }
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
            SetCompanies(DdlCompanyPost);
            LoadProjectsByCompany((List<CompanyDto>)DdlCompanyPost.DataSource);
            PostContent.Visible = true;
        }

        protected void BtnPost_Click(object sender, EventArgs e)
        {
            AddRequirement();
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
            if (!haveRequirements)
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
            if (!string.IsNullOrWhiteSpace(actionUrl)) RequiredActionUrl = actionUrl;
        }
        protected void RequiredActionExecute_Click(object sender, EventArgs e)
        {
            Response.Redirect(RequiredActionUrl);
        }
        #endregion

        #region New Post Events
        protected void DdlCompanyPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddlCompany = (DropDownList) sender;
            if (string.IsNullOrWhiteSpace(ddlCompany.SelectedValue)) return;

            SetProjectsForNewPost(long.Parse(ddlCompany.SelectedValue));
            DdlProjectPost.Visible = true;
        }
        #endregion

        #region New Post UI Setup

        private void SetProjectsForNewPost(long companyId)
        {
            if (!Projects.ContainsKey(companyId)) return;

            DdlProjectPost.DataSource = Projects[companyId];
            DdlProjectPost.DataTextField = CustomExpression.GetPropertyName<ProjectDto>(p => p.Name);
            DdlProjectPost.DataValueField = CustomExpression.GetPropertyName<ProjectDto>(p => p.Id);
            DdlProjectPost.DataBind();
        }

        private void SetPostSuccessMessage(string message)
        {
            PostSuccessMessage.Text = message;
            PostSuccessPanel.Visible = true;
            ScriptManager.RegisterClientScriptBlock(PostContentUpdatePanel, PostContentUpdatePanel.GetType(),
                "posterrorfadeout", "fadeOutControl('#PostSuccessPanel')", true);
        }

        private void SetPostErrorMessage(string message)
        {
            PostErrorMessage.Text = message;
            PostErrorPanel.Visible = true;
            ScriptManager.RegisterClientScriptBlock(PostContentUpdatePanel, PostContentUpdatePanel.GetType(),
                "posterrorfadeout", "fadeOutControl('#PostErrorPanel')", true);
        }
        #endregion

        #region Data Load

        /// <summary>
        /// Loads all projects by company and store them in a ViewState var
        /// </summary>
        private void LoadProjectsByCompany(IEnumerable<CompanyDto> companies)
        {
            var projectSrv = new ProjectSoapClient();
            var projectsByComp = Projects;

            foreach (var company in companies)
            {
                var projectXmlStr = projectSrv.GetByCompany(company.Id);
                var serializer = new ObjectSerializer<List<ProjectDto>>();
                var projects = (List<ProjectDto>)serializer.Deserialize(projectXmlStr);

                foreach (var project in projects)
                {
                    if (projectsByComp.ContainsKey(company.Id))
                    {
                        projectsByComp[company.Id].Add(project);
                    }
                    else
                    {
                        projectsByComp.Add(company.Id, new List<ProjectDto> { project });
                    }
                }
            }

            Projects = projectsByComp;
        }
        #endregion

        #region Data Update

        private void AddRequirement()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(DdlCompanyPost.SelectedValue)) return;
                if (string.IsNullOrWhiteSpace(DdlProjectPost.SelectedValue)) return;

                var requirementSrv = new RequirementSoapClient();
                requirementSrv.AddRequirement(TxtContentPostTitle.Text, TxtContentPost.Text,
                    long.Parse(DdlCompanyPost.SelectedValue), long.Parse(DdlProjectPost.SelectedValue),
                    GetUsernameEncrypted());
                SetPostSuccessMessage("Requirement posted successfully");
            }
            catch
            {
                SetPostErrorMessage("An error occurred, please try again.");
            }
        }
        #endregion

    }
}