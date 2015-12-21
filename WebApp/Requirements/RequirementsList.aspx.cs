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
            get { return ViewState["Requirements"] != null ? (List<RequirementDto>)ViewState["Requirements"] : new List<RequirementDto>(); }
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

        #region Form Setup

        /// <summary>
        /// Sets the requirements list data 
        /// </summary>
        private void SetRequirementsList()
        {
            LoadRequirements();
            RequirementsListRepeater.DataSource = Requirements;
            RequirementsListRepeater.DataBind();
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
            var requirementsXmlStr = requirementSrv.GetRequirementsList(GetUsernameEncrypted());

            var serializer = new ObjectSerializer<List<RequirementDto>>();
            Requirements = (List<RequirementDto>)serializer.Deserialize(requirementsXmlStr);
        }
        #endregion
    }
}