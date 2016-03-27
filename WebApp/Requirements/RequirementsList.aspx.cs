using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.General;
using SocialRequirements.RequirementService;
using SocialRequirements.Utilities;

namespace SocialRequirements.Requirements
{
    public partial class RequirementsList : SocialRequirementsPrivatePage
    {
        #region Constants

        private const string PriorityButtonId = "PriorityButton";

        #endregion
        #region Properties
        protected List<RequirementDto> Requirements
        {
            get
            {
                return ViewState["Requirements"] != null
                    ? (List<RequirementDto>) ViewState["Requirements"]
                    : new List<RequirementDto>();
            }
            set { ViewState["Requirements"] = value; }
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

            UserCompanies = GetRelatedCompanies();
            LoadFilters();
            SetRequirementsList();
        }

        protected void SetFilterButton_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Requirements Repeater Events

        protected void RequirementsListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var requirement = Requirements[e.Item.ItemIndex];
            
            switch (e.CommandName)
            {
                case CommonConstants.SocialActionsCommands.Like:
                    Like(requirement.CompanyId, requirement.ProjectId, requirement.Id, (int)GeneralCatalog.Detail.Entity.Requirement);
                    SetRequirementsList();
                    break;
                case CommonConstants.SocialActionsCommands.Dislike:
                    Dislike(requirement.CompanyId, requirement.ProjectId, requirement.Id, (int)GeneralCatalog.Detail.Entity.Requirement);
                    SetRequirementsList();
                    break;
                case CommonConstants.SocialActionsCommands.Comment:
                    //Comment((int)GeneralCatalog.Detail.Entity.Requirement, requirement.RecordId);
                    break;
            }
        }

        protected void RequirementsListRepeater_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var requirement = (RequirementDto) e.Item.DataItem;
            var priorityButton = (HyperLink)e.Item.FindControl(PriorityButtonId);
            if (requirement == null || priorityButton == null) return;
            SetRequirementPriority(priorityButton, requirement);
        }
        #endregion

        #region Requirements GridView Events

        protected void RequirementsListGrid_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            var requirement = (RequirementDto)e.Row.DataItem;

            e.Row.Attributes.Add("onclick",
                "location.href='" + CommonConstants.FormsFileName.Requirement + "?" + CommonConstants.QueryStringParams.Id +
                "=" + requirement.Id + "&" + CommonConstants.QueryStringParams.CompanyId + "=" + requirement.CompanyId +
                "&" + CommonConstants.QueryStringParams.ProjectId + "=" + requirement.ProjectId + "'");
        }
        #endregion

        #region Form Setup

        /// <summary>
        /// Sets the requirements list data 
        /// </summary>
        private void SetRequirementsList(bool reloadRequirements = true)
        {
            if (reloadRequirements)
                LoadRequirements();
            RequirementsListRepeater.DataSource = Requirements;
            RequirementsListRepeater.DataBind();

            SetRequirementsGrid();
        }

        private void SetRequirementsGrid()
        {
            RequirementsListGrid.DataSource = Requirements;
            RequirementsListGrid.DataBind();
        }

        private static void SetRequirementPriority(HyperLink priorityCtrl, RequirementDto requirement)
        {
            switch (requirement.PriorityId)
            {
                case (int)GeneralCatalog.Detail.RequirementPriority.Low:
                    priorityCtrl.Attributes.Add("class", Requirement.PriorityLowCss);
                    break;
                case (int)GeneralCatalog.Detail.RequirementPriority.Medium:
                    priorityCtrl.Attributes.Add("class", Requirement.PriorityMedCss);
                    break;
                case (int)GeneralCatalog.Detail.RequirementPriority.High:
                    priorityCtrl.Attributes.Add("class", Requirement.PriorityHighCss);
                    break;
            }
            priorityCtrl.Text = requirement.Priority;
        }
        #endregion

        #region Data Load

        /// <summary>
        /// Returns all requirements related to the current user
        /// ordered descendently by modified date 
        /// </summary>
        /// <returns></returns>
        private void LoadRequirements()
        {
            var requirementSrv = new RequirementSoapClient();

            var filter = Request.QueryString[CommonConstants.QueryStringParams.Filter];
            string requirementsXmlStr;

            switch (filter)
            {
                case CommonConstants.Filters.Approved:
                    requirementsXmlStr = requirementSrv.GetApprovedRequirementsList(GetUsernameEncrypted());
                    FormTitle.Text = CommonConstants.Titles.Requirements.ListApproved;
                    break;
                case CommonConstants.Filters.Rejected:
                    requirementsXmlStr = requirementSrv.GetRejectedRequirementsList(GetUsernameEncrypted());
                    FormTitle.Text = CommonConstants.Titles.Requirements.ListRejected;
                    break;
                case CommonConstants.Filters.PendingApproval:
                    requirementsXmlStr = requirementSrv.GetPendingApprovalRequirementsList(GetUsernameEncrypted());
                    FormTitle.Text = CommonConstants.Titles.Requirements.ListPendingApproval;
                    break;
                case CommonConstants.Filters.Draft:
                    requirementsXmlStr = requirementSrv.GetDraftRequirementsList(GetUsernameEncrypted());
                    FormTitle.Text = CommonConstants.Titles.Requirements.ListDraft;
                    break;
                case CommonConstants.Filters.Hashtag:
                    var hashtag = HttpUtility.UrlDecode(Request.QueryString[CommonConstants.QueryStringParams.Hashtag]);
                    requirementsXmlStr = requirementSrv.GetRequirementsByHashtag(hashtag, GetUsernameEncrypted());
                    FormTitle.Text = CommonConstants.Titles.Requirements.ListHashtag;
                    break;
                default:
                    requirementsXmlStr = requirementSrv.GetRequirementsList(GetUsernameEncrypted());
                    FormTitle.Text = CommonConstants.Titles.Requirements.ListAll;
                    break;
            }

            var serializer = new ObjectSerializer<List<RequirementDto>>();
            Requirements = (List<RequirementDto>)serializer.Deserialize(requirementsXmlStr);
        }

        #endregion

        #region Filters Data Load

        private void LoadFilters()
        {
            LoadProjectFilterOptions();
            LoadPriorityFilterOptions();
            LoadPriorityStatusOptions();
        }

        private void LoadProjectFilterOptions()
        {
            var projects = GetProjectsByCompanies(UserCompanies);
            FilterOptionsProject.DataSource = projects;
            FilterOptionsProject.DataValueField = CustomExpression.GetPropertyName<ProjectDto>(p => p.Id);
            FilterOptionsProject.DataTextField = CustomExpression.GetPropertyName<ProjectDto>(p => p.Name);
            FilterOptionsProject.DataBind();
        }

        private void LoadPriorityFilterOptions()
        {
            var priorities = GetCatalogOptions(GeneralCatalog.Header.RequirementPriority);
            FilterOptionsPriority.DataSource = priorities;
            FilterOptionsPriority.DataValueField = CustomExpression.GetPropertyName<GeneralCatalogDetailDto>(c => c.Id);
            FilterOptionsPriority.DataTextField = CustomExpression.GetPropertyName<GeneralCatalogDetailDto>(c => c.Name);
            FilterOptionsPriority.DataBind();
        }

        private void LoadPriorityStatusOptions()
        {
            var status = GetCatalogOptions(GeneralCatalog.Header.RequirementStatus);
            FilterOptionsStatus.DataSource = status;
            FilterOptionsStatus.DataValueField = CustomExpression.GetPropertyName<GeneralCatalogDetailDto>(c => c.Id);
            FilterOptionsStatus.DataTextField = CustomExpression.GetPropertyName<GeneralCatalogDetailDto>(c => c.Name);
            FilterOptionsStatus.DataBind();
        }
        #endregion
    }
}