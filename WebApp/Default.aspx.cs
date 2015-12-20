using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocialRequirements.Domain;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.Domain.General;
using SocialRequirements.GeneralService;
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
        private const string CtrlIdActivityActionsPanel = "ActivityActionsPanel";
        private const string CtrlIdActivityDescription = "DescriptionLabel";
        private const string CtrlIdActivityReadEvenMore = "ReadEvenMoreButton";
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

        protected List<ActivityFeedDto> ActivityFeed
        {
            get { return ViewState["ActivityFeed"] != null ? (List<ActivityFeedDto>)ViewState["ActivityFeed"] : new List<ActivityFeedDto>(); }
            set { ViewState["ActivityFeed"] = value; }
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
            LoadActivityFeed();
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

        #region Activity Feed Events

        protected void ActivityFeedRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item) return;

            var activity = (ActivityFeedDto) e.Item.DataItem;
            var actionsPanel = (Panel) e.Item.FindControl(CtrlIdActivityActionsPanel);

            switch (activity.EntityId)
            {
                case (int)GeneralCatalog.Detail.Entity.Requirement:
                    actionsPanel.Visible = true;
                    break;
                default:
                    actionsPanel.Visible = false;
                    break;
            }
        }

        protected void ActivityFeedRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var activity = ActivityFeed[e.Item.ItemIndex];

            switch (e.CommandName)
            {
                case CommonConstants.SocialActionsCommands.ReadMore:
                    var descriptionText = (Label) e.Item.FindControl(CtrlIdActivityDescription);
                    descriptionText.Text = activity.Description;
                    var sourceButton = (LinkButton) e.CommandSource;
                    sourceButton.Visible = false;
                    var readEvenMoreButton = (LinkButton) e.Item.FindControl(CtrlIdActivityReadEvenMore);
                    readEvenMoreButton.Visible = activity.HasEvenLongerDescription;
                    break;
                case CommonConstants.SocialActionsCommands.ReadEvenMore:
                    break;
                case CommonConstants.SocialActionsCommands.Like:
                    Like(activity.CompanyId, activity.ProjectId, activity.RecordId, activity.EntityId);
                    LoadActivityFeed();
                    break;
                case CommonConstants.SocialActionsCommands.Dislike:
                    Dislike(activity.CompanyId, activity.ProjectId, activity.RecordId, activity.EntityId);
                    LoadActivityFeed();
                    break;
                case CommonConstants.SocialActionsCommands.Comment:
                    Comment(activity.EntityId, activity.RecordId);
                    break;
            }
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

        private void LoadActivityFeed()
        {
            var generalSrv = new GeneralSoapClient();
            var activityFeedXmlStr = generalSrv.LatestActivityFeed(GetUsernameEncrypted());
            var serializer = new ObjectSerializer<List<ActivityFeedDto>>();
            ActivityFeed = (List<ActivityFeedDto>)serializer.Deserialize(activityFeedXmlStr);
            ActivityFeedRepeater.DataSource = ActivityFeed;
            ActivityFeedRepeater.DataBind();
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
            catch(Exception ex)
            {
                SetPostErrorMessage("An error occurred, please try again.");
            }
        }

        private void Comment(int entity, long recordId)
        {

        }
        #endregion

        
    }
}