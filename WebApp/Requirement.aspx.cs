using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.General;
using SocialRequirements.RequirementService;
using SocialRequirements.Utilities;

namespace SocialRequirements
{
    public partial class Requirement : SocialRequirementsPrivatePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (IsPostBack) return;

            // get query string params
            var requirementId = long.Parse(Request.QueryString[CommonConstants.QueryStringParams.Id]);
            var companyId = long.Parse(Request.QueryString[CommonConstants.QueryStringParams.CompanyId]);
            var projectId = long.Parse(Request.QueryString[CommonConstants.QueryStringParams.ProjectId]);

            // get requirement data
            var requirement = GetRequirement(companyId, projectId, requirementId);

            // set requirement data in form
            SetFormData(requirement);
        }

        private RequirementDto GetRequirement(long companyId, long projectId, long requirementId)
        {
            var requirementSrv = new RequirementSoapClient();
            var requirement = requirementSrv.GetRequirement(companyId, projectId, requirementId);

            var serializer = new ObjectSerializer<RequirementDto>();
            return (RequirementDto)serializer.Deserialize(requirement);
        }

        private void SetFormData(RequirementDto requirement)
        {
            RequirementTitle.Text = requirement.Title;
            RequirementDescription.Text = requirement.Description;
            RequirementStatus.Text = requirement.Status;
        }
    }
}