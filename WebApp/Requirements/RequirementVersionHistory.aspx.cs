using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.General;
using SocialRequirements.RequirementService;
using SocialRequirements.Utilities;

namespace SocialRequirements.Requirements
{
    public partial class RequirementVersionHistory : SocialRequirementsPrivatePage
    {
        #region Properties
        protected List<RequirementDto> RequirementVersions
        {
            get
            {
                return ViewState["RequirementVersions"] != null
                    ? (List<RequirementDto>)ViewState["RequirementVersions"]
                    : new List<RequirementDto>();
            }
            set { ViewState["RequirementVersions"] = value; }
        }
        #endregion

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (IsPostBack) return;

            // get query string params
            var requirementId = long.Parse(Request.QueryString[CommonConstants.QueryStringParams.Id]);
            var companyId = long.Parse(Request.QueryString[CommonConstants.QueryStringParams.CompanyId]);
            var projectId = long.Parse(Request.QueryString[CommonConstants.QueryStringParams.ProjectId]);

            SetVersionsGridView(companyId, projectId, requirementId);
        }

        //protected void RequirementsListGrid_OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType != DataControlRowType.DataRow) return;

        //    var requirement = (RequirementDto)e.Row.DataItem;

        //    e.Row.Attributes.Add("onclick",
        //        "location.href='" + CommonConstants.FormsFileName.Requirement + "?" + CommonConstants.QueryStringParams.Id +
        //        "=" + requirement.Id + "&" + CommonConstants.QueryStringParams.CompanyId + "=" + requirement.CompanyId +
        //        "&" + CommonConstants.QueryStringParams.ProjectId + "=" + requirement.ProjectId + "'");
        //}

        private List<RequirementDto> GetVersionHistory(long companyId, long projectId, long requirementId)
        {
            var requirementSrv = new RequirementSoapClient();
            var requirementVersionsStr = requirementSrv.GetRequirementVersionHistory(companyId, projectId, requirementId);
            var serializer = new ObjectSerializer<List<RequirementDto>>();
            return (List<RequirementDto>)serializer.Deserialize(requirementVersionsStr);
        }

        protected void RequirementVersionsGrid_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var selIndex = RequirementVersionsGrid.SelectedIndex;
            var selectedVersion = RequirementVersions[selIndex];

            Response.Redirect(CommonConstants.FormsUrl.Requirement + "?" + CommonConstants.QueryStringParams.Id + "=" +
                              selectedVersion.Id + "&" + CommonConstants.QueryStringParams.CompanyId + "=" + selectedVersion.CompanyId +
                              "&" + CommonConstants.QueryStringParams.ProjectId + "=" + selectedVersion.ProjectId + "&" +
                              CommonConstants.QueryStringParams.RequirementVersionId + "=" + selectedVersion.VersionId);
        }

        #region "Form setup"

        private void SetVersionsGridView(long companyId, long projectId, long requirementId)
        {
            RequirementVersions = GetVersionHistory(companyId, projectId, requirementId);
            RequirementVersionsGrid.DataSource = RequirementVersions;
            RequirementVersionsGrid.DataBind();
        }

        #endregion

        protected void RequirementVersionsGrid_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(RequirementVersionsGrid, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row.";
        }
    }
}