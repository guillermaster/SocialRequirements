﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocialRequirements.Domain;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.General;
using SocialRequirements.GeneralService;
using SocialRequirements.ProjectService;
using SocialRequirements.RequirementService;
using SocialRequirements.Utilities;

namespace SocialRequirements
{
    public partial class RecentActivities : SocialRequirementsPrivatePage
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
        private const string CtrlIdCommentsList = "CommentsRepeater";
        private const string CtrlIdCommentsPanel = "CommentsPanel";
        private const string CtrlIdRecordIdComment = "RecordIdComment";
        private const string CtrlIdEntityIdComment = "EntityIdComment";
        private const string CtrlIdNewComment = "NewCommentInput";
        private const string CtrlIdCompanyIdComment = "CompanyIdComment";
        private const string CtrlIdProjectIdComment = "ProjectIdComment";
        private const string CtrlIdParentIdComment = "ParentIdComment";
        private const string CtrlIdEntityInstanceLink = "EntityInstanceLink";
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
           
            LoadActivityFeed();
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
        
        #region Activity Feed Events

        protected void ActivityFeedRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item) return;

            var activity = (ActivityFeedDto) e.Item.DataItem;
            var actionsPanel = (Panel) e.Item.FindControl(CtrlIdActivityActionsPanel);
            var link = (HyperLink)e.Item.FindControl(CtrlIdEntityInstanceLink);

            switch (activity.EntityId)
            {
                case (int)GeneralCatalog.Detail.Entity.Requirement:
                    actionsPanel.Visible = activity.EntityActionId == (int)GeneralCatalog.Detail.EntityActions.Create ||
                                           activity.EntityActionId == (int)GeneralCatalog.Detail.EntityActions.SubmitForApproval;
                    if (activity.ProjectId.HasValue)
                        link.NavigateUrl = GetUrlForRequirement(activity.CompanyId, activity.ProjectId.Value, activity.RecordId);
                    break;

                case (int)GeneralCatalog.Detail.Entity.RequirementModification:
                    actionsPanel.Visible = activity.EntityActionId == (int)GeneralCatalog.Detail.EntityActions.Create ||
                                           activity.EntityActionId == (int)GeneralCatalog.Detail.EntityActions.SubmitForApproval;
                    if (activity.ProjectId.HasValue && activity.ParentId.HasValue)
                    {
                        link.NavigateUrl = GetUrlForRequirementModification(activity.CompanyId, activity.ProjectId.Value,
                            activity.ParentId.Value, activity.RecordId);
                    }
                    break;

                case (int)GeneralCatalog.Detail.Entity.RequirementQuestion:
                case (int)GeneralCatalog.Detail.Entity.RequirementQuestionAnswer:
                    actionsPanel.Visible = false;
                    if (activity.ProjectId.HasValue && activity.ParentId.HasValue && activity.GrandparentId.HasValue)
                    {
                        link.NavigateUrl = GetUrlForRequirementQuestion(activity.CompanyId, activity.ProjectId.Value,
                            activity.ParentId.Value, activity.GrandparentId.Value, activity.RecordId);
                    }
                    break;

                case (int)GeneralCatalog.Detail.Entity.RequirementComment:
                    actionsPanel.Visible = false;
                    if (activity.ProjectId.HasValue)
                        link.NavigateUrl = GetUrlForRequirement(activity.CompanyId, activity.ProjectId.Value, activity.RecordId);
                    break;

                case (int)GeneralCatalog.Detail.Entity.RequirementModificationComment:
                    actionsPanel.Visible = false;
                    if (activity.ProjectId.HasValue && activity.ParentId.HasValue)
                    {
                        link.NavigateUrl = GetUrlForRequirementModification(activity.CompanyId, activity.ProjectId.Value,
                            activity.ParentId.Value, activity.RecordId);
                    }
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
                    Like(activity.CompanyId, activity.ProjectId, activity.RecordId, activity.EntityId, activity.ParentId);
                    LoadActivityFeed();
                    break;
                case CommonConstants.SocialActionsCommands.Dislike:
                    Dislike(activity.CompanyId, activity.ProjectId, activity.RecordId, activity.EntityId, activity.ParentId);
                    LoadActivityFeed();
                    break;
                case CommonConstants.SocialActionsCommands.Comment:
                    var commentsCtrl = (Repeater)e.Item.FindControl(CtrlIdCommentsList);
                    var commentsPanl = (Panel) e.Item.FindControl(CtrlIdCommentsPanel);
                    LoadRequirementComments(activity, commentsCtrl, commentsPanl);
                    break;
            }
        }

        protected void AddNewCommentButton_Click(object sender, EventArgs e)
        {
            var parent = ((Control)sender).Parent;

            var companyIdCtrl = (HiddenField)parent.FindControl(CtrlIdCompanyIdComment);
            if (companyIdCtrl == null) throw new InvalidDataException("No company ID hidden control found.");

            var projectIdCtrl = (HiddenField)parent.FindControl(CtrlIdProjectIdComment);
            if (projectIdCtrl == null) throw new InvalidDataException("No project ID hidden control found.");

            var parentIdCtrl = (HiddenField)parent.FindControl(CtrlIdParentIdComment);
            if (parentIdCtrl == null) throw new InvalidDataException("No parent ID hidden control found.");

            var recordIdCtrl = (HiddenField)parent.FindControl(CtrlIdRecordIdComment);
            if (recordIdCtrl == null) throw new InvalidDataException("No record ID hidden control found.");

            var entityIdCtrl = (HiddenField)parent.FindControl(CtrlIdEntityIdComment);
            if (entityIdCtrl == null) throw new InvalidDataException("No entity ID hidden control found.");

            var commentCtrl = (TextBox)parent.FindControl(CtrlIdNewComment);
            if (commentCtrl == null) throw new InvalidDataException("No comment input textbox founded.");

            var commentsListCtrl = (Repeater)parent.FindControl(CtrlIdCommentsList);
            if (commentsListCtrl == null) throw new InvalidDataException("No requirement list control found");

            long? parentId;
            if (string.IsNullOrWhiteSpace(parentIdCtrl.Value))
                parentId = null;
            else
                parentId = long.Parse(parentIdCtrl.Value);

            AddComment(long.Parse(companyIdCtrl.Value), long.Parse(projectIdCtrl.Value), parentId,
                long.Parse(recordIdCtrl.Value), int.Parse(entityIdCtrl.Value), commentCtrl.Text, commentsListCtrl);

            commentCtrl.Text = string.Empty;
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
            string activities;

            if (Request.QueryString[CommonConstants.QueryStringParams.ProjectId] != null &&
                Request.QueryString[CommonConstants.QueryStringParams.EntityId] != null &&
                Request.QueryString[CommonConstants.QueryStringParams.ActionId] != null)
            {
                var projectId = int.Parse(Request.QueryString[CommonConstants.QueryStringParams.ProjectId]);
                var entityId = int.Parse(Request.QueryString[CommonConstants.QueryStringParams.EntityId]);
                var actionId = int.Parse(Request.QueryString[CommonConstants.QueryStringParams.ActionId]);
                activities = generalSrv.GetLatestActivities(projectId, entityId, actionId);
            }
            else
            {
                activities = generalSrv.GetAllActivitiesNotifications(GetUsernameEncrypted());
            }
            
            var serializer = new ObjectSerializer<List<ActivityFeedDto>>();
            ActivityFeed = (List<ActivityFeedDto>)serializer.Deserialize(activities);
            ActivityFeedRepeater.DataSource = ActivityFeed;
            ActivityFeedRepeater.DataBind();
        }
        #endregion

        #region Data Update

        private void AddComment(long companyId, long projectId, long? parentId, long recordId, int entityId,
            string comment, Repeater commentsCtrl)
        {
            switch (entityId)
            {
                case (int)GeneralCatalog.Detail.Entity.Requirement:
                    var requirementSrv = new RequirementSoapClient();
                    var reqComments = requirementSrv.CommentRequirement(companyId, projectId, recordId, comment,
                        GetUsernameEncrypted());
                    var reqSerializer = new ObjectSerializer<List<RequirementCommentDto>>();
                    commentsCtrl.DataSource = (List<RequirementCommentDto>)reqSerializer.Deserialize(reqComments);
                    commentsCtrl.DataBind();
                    break;
                case (int)GeneralCatalog.Detail.Entity.RequirementModification:
                    if (parentId == null) throw new InvalidDataException("Requirement ID is required");
                    var requirementModifSrv = new RequirementSoapClient();
                    var reqModifComments = requirementModifSrv.CommentRequirementModification(companyId, projectId,
                        parentId.Value, recordId, comment, GetUsernameEncrypted());
                    var reqModifSerializer = new ObjectSerializer<List<RequirementModificationCommentDto>>();
                    commentsCtrl.DataSource = (List<RequirementModificationCommentDto>)reqModifSerializer.Deserialize(reqModifComments);
                    commentsCtrl.DataBind();
                    break;
            }
        }

        private static void LoadRequirementComments(ActivityFeedDto activity,
            Repeater commentsCtrl, Control commentsPnl)
        {
            if (!activity.ProjectId.HasValue) throw new InvalidDataException("Project ID cannot be null");

            switch (activity.EntityId)
            {
                case (int)GeneralCatalog.Detail.Entity.Requirement:
                    commentsCtrl.DataSource = activity.Comment;
                    commentsCtrl.DataBind();
                    commentsPnl.Visible = true;
                    break;
                case (int)GeneralCatalog.Detail.Entity.RequirementModification:
                    commentsCtrl.DataSource = activity.Comment;
                    commentsCtrl.DataBind();
                    commentsPnl.Visible = true;
                    break;
            }
        }
        #endregion
    }
}