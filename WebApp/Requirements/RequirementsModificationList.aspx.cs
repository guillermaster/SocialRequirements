using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.General;
using SocialRequirements.RequirementService;
using SocialRequirements.Utilities;

namespace SocialRequirements.Requirements
{
    public partial class RequirementsModificationList : SocialRequirementsPrivatePage
    {
        #region Properties
        protected List<RequirementModificationDto> Requirements
        {
            get
            {
                return ViewState["Requirements"] != null
                    ? (List<RequirementModificationDto>) ViewState["Requirements"]
                    : new List<RequirementModificationDto>();
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
            var requirementModif = Requirements[e.Item.ItemIndex];
            
            switch (e.CommandName)
            {
                case CommonConstants.SocialActionsCommands.Like:
                    Like(requirementModif.CompanyId, requirementModif.ProjectId, requirementModif.Id,
                        (int)GeneralCatalog.Detail.Entity.RequirementModification, requirementModif.RequirementId);
                    SetRequirementsList();
                    break;
                case CommonConstants.SocialActionsCommands.Dislike:
                    Dislike(requirementModif.CompanyId, requirementModif.ProjectId, requirementModif.Id,
                        (int) GeneralCatalog.Detail.Entity.RequirementModification, requirementModif.RequirementId);
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

            var requirementModif = (RequirementModificationDto)e.Row.DataItem;

            e.Row.Attributes.Add("onclick",
                "location.href='" + CommonConstants.FormsFileName.RequirementModification + 
                "?" + CommonConstants.QueryStringParams.CompanyId + "=" + requirementModif.CompanyId + 
                "&" + CommonConstants.QueryStringParams.ProjectId + "=" + requirementModif.ProjectId +
                "&" + CommonConstants.QueryStringParams.RequirementId + "=" + requirementModif.RequirementId +
                "&" + CommonConstants.QueryStringParams.Id + "=" + requirementModif.Id +
                "'");
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
                    requirementsXmlStr = requirementSrv.GetApprovedRequirementsModificationList(GetUsernameEncrypted());
                    break;
                case CommonConstants.Filters.Rejected:
                    requirementsXmlStr = requirementSrv.GetRejectedRequirementsModificationList(GetUsernameEncrypted());
                    break;
                case CommonConstants.Filters.PendingApproval:
                    requirementsXmlStr = requirementSrv.GetPendingApprovalRequirementsModificationList(GetUsernameEncrypted());
                    break;
                case CommonConstants.Filters.Draft:
                    requirementsXmlStr = requirementSrv.GetDraftRequirementsModificationList(GetUsernameEncrypted());
                    break;
                default:
                    requirementsXmlStr = requirementSrv.GetRequirementsModificationList(GetUsernameEncrypted());
                    break;
            }

            var serializer = new ObjectSerializer<List<RequirementModificationDto>>();
            Requirements = (List<RequirementModificationDto>)serializer.Deserialize(requirementsXmlStr);
        }
        #endregion
    }
}