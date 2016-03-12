using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.General;
using SocialRequirements.RequirementService;
using SocialRequirements.Utilities;

namespace SocialRequirements.Requirements
{
    public partial class RequirementsList : SocialRequirementsPrivatePage
    {
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
        #endregion

        #region Main Events
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (IsPostBack) return;

            SetRequirementsList();
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
                    break;
                case CommonConstants.Filters.Rejected:
                    requirementsXmlStr = requirementSrv.GetRejectedRequirementsList(GetUsernameEncrypted());
                    break;
                case CommonConstants.Filters.PendingApproval:
                    requirementsXmlStr = requirementSrv.GetPendingApprovalRequirementsList(GetUsernameEncrypted());
                    break;
                case CommonConstants.Filters.Draft:
                    requirementsXmlStr = requirementSrv.GetDraftRequirementsList(GetUsernameEncrypted());
                    break;
                default:
                    requirementsXmlStr = requirementSrv.GetRequirementsList(GetUsernameEncrypted());
                    break;
            }

            var serializer = new ObjectSerializer<List<RequirementDto>>();
            Requirements = (List<RequirementDto>)serializer.Deserialize(requirementsXmlStr);
        }
        #endregion

    }
}