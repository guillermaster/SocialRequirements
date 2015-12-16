using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.RequirementService;
using SocialRequirements.Utilities;

namespace SocialRequirements
{
    public partial class Requirements : SocialRequirementsPrivatePage
    {
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
            
        }
        #endregion

        #region Form Setup

        /// <summary>
        /// Sets the requirements list data 
        /// </summary>
        private void SetRequirementsList()
        {
            RequirementsListRepeater.DataSource = GetRequirements();
            RequirementsListRepeater.DataBind();
        }
        #endregion

        #region Data Load

        /// <summary>
        /// Returns all requirements related to the current user
        /// ordered descendently by modified date 
        /// </summary>
        /// <returns></returns>
        private List<RequirementDto> GetRequirements()
        {
            var requirementSrv = new RequirementSoapClient();
            var requirementsXmlStr = requirementSrv.GetRequirementsList(GetUsernameEncrypted());

            var serializer = new ObjectSerializer<List<RequirementDto>>();
            var requirementsList = (List<RequirementDto>)serializer.Deserialize(requirementsXmlStr);
            return requirementsList;
        }
        #endregion
    }
}