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

        //private RequirementDto GetVersion(long companyId, long projectId, long requirementId, long versionId)
        //{
        //    var requirementSrv = new RequirementSoapClient();
        //    var requirementTxt = requirementSrv.GetRequirementVersion(companyId, projectId, requirementId, versionId);
        //    var serializer = new ObjectSerializer<RequirementDto>();
        //    return (RequirementDto)serializer.Deserialize(requirementTxt);
        //}


        protected void RequirementVersionsGrid_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var selIndex = RequirementVersionsGrid.SelectedIndex;
            var selRow = RequirementVersionsGrid.SelectedRow;
            throw new NotImplementedException();
        }

        protected void RequirementVersionsGrid_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var arguments = e.CommandArgument.ToString().Split('-');
            Response.Redirect(CommonConstants.FormsUrl.Requirement + "?" + CommonConstants.QueryStringParams.Id + "=" +
                              arguments[0] + "&" + CommonConstants.QueryStringParams.CompanyId + "=" + arguments[1] +
                              "&" + CommonConstants.QueryStringParams.ProjectId + "=" + arguments[2] + "&" +
                              CommonConstants.QueryStringParams.RequirementVersionId + "=" + arguments[3]);
            //SetRequirementView(long.Parse(arguments[1]), long.Parse(arguments[2]), long.Parse(arguments[0]), long.Parse(arguments[3]));
        }

        #region "Form setup"

        private void SetVersionsGridView(long companyId, long projectId, long requirementId)
        {
            RequirementVersionsGrid.DataSource = GetVersionHistory(companyId, projectId, requirementId);
            RequirementVersionsGrid.DataBind();
        }

        //private void SetRequirementView(long companyId, long projectId, long requirementId, long requirementVersionId)
        //{
        //    var requirementVersion = GetVersion(companyId, projectId, requirementId, requirementVersionId);
        //    SetFormData(requirementVersion);
        //    RequirementPanel.Visible = true;
        //    RequirementVersionsGrid.Visible = false;
        //}

        //protected virtual void SetFormData(RequirementDto requirement)
        //{
        //    // set requirement data in UI controls
        //    RequirementTitle.Text = requirement.Title;
        //    RequirementTitleInput.Text = requirement.Title;
        //    ProjectName.Text = requirement.Project;
        //    RequirementDescription.Text = requirement.Description;
        //    RequirementDescriptionInput.Text = requirement.Description;
        //    RequirementStatus.Text = requirement.Status;
        //    RequirementStatusId.Value = requirement.StatusId.ToString();
        //    RequirementVersion.Text = requirement.VersionNumber.ToString();
        //    CreatedByName.Text = requirement.CreatedByName;
        //    ModifiedByName.Text = requirement.ModifiedByName;
        //    CreatedOn.Text = requirement.Createdon.ToString(CultureInfo.InvariantCulture);
        //    ModifiedOn.Text = requirement.Modifiedon.ToString(CultureInfo.InvariantCulture);
        //    //LikeCounter.Text = requirement.Agreed.ToString();
        //    //DislikeCounter.Text = requirement.Disagreed.ToString();
        //    //CommentCounter.Text = requirement.CommentsQuantity.ToString();
        //    //FileName = requirement.AttachmentTitle;
        //    //DownloadButton.Visible = !string.IsNullOrWhiteSpace(FileName);
        //    //FileOverwriteWarning.Visible = !string.IsNullOrWhiteSpace(FileName);

        //    //SetPriority(requirement);

        //    // hashtags
        //    if (RequirementsHashtagsRepeater != null)
        //    {
        //        RequirementsHashtagsRepeater.DataSource = requirement.Hashtags;
        //        RequirementsHashtagsRepeater.DataBind();
        //    }

        //    // set action buttons visibility
        //    //SaveButton.Visible = false;
        //    //UndoEditButton.Visible = false;
        //    //EditButton.Visible = requirement.StatusId != (int)GeneralCatalog.Detail.RequirementStatus.PendingApproval;
        //    //SubmitButton.Visible = requirement.StatusId == (int)GeneralCatalog.Detail.RequirementStatus.Draft;
        //    //ApproveButton.Visible = requirement.StatusId ==
        //    //                        (int)GeneralCatalog.Detail.RequirementStatus.PendingApproval &&
        //    //                        CanApproveRequirement.HasValue && CanApproveRequirement.Value;
        //    //RejectButton.Visible = ApproveButton.Visible;

        //    //LoadProjects();
        //}
        #endregion
    }
}