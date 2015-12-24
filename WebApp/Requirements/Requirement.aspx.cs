using System;
using System.Globalization;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.General;
using SocialRequirements.RequirementService;
using SocialRequirements.Utilities;

namespace SocialRequirements.Requirements
{
    public partial class Requirement : SocialRequirementsPrivatePage
    {
        #region Properties
        protected long CompanyId
        {
            get { return ViewState["CompanyId"] != null ? int.Parse(ViewState["CompanyId"].ToString()) : 0; }
            set { ViewState["CompanyId"] = value; }
        }

        protected long RequirementId
        {
            get { return ViewState["RequirementId"] != null ? int.Parse(ViewState["RequirementId"].ToString()) : 0; }
            set { ViewState["RequirementId"] = value; }
        }

        protected long ProjectId
        {
            get { return ViewState["ProjectId"] != null ? int.Parse(ViewState["ProjectId"].ToString()) : 0; }
            set { ViewState["ProjectId"] = value; }
        }
        #endregion

        #region Main Events
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (IsPostBack) return;

            // get query string params
            RequirementId = long.Parse(Request.QueryString[CommonConstants.QueryStringParams.Id]);
            CompanyId = long.Parse(Request.QueryString[CommonConstants.QueryStringParams.CompanyId]);
            ProjectId = long.Parse(Request.QueryString[CommonConstants.QueryStringParams.ProjectId]);
            
            LoadRequirement();

            // if a message was passes in the query string, display it
            var message = Request.QueryString[CommonConstants.QueryStringParams.Message];
            if (!string.IsNullOrWhiteSpace(message))
                SetFadeOutMessage(GetMainUpdatePanel(this), PostSuccessPanel, PostSuccessMessage, message);
        }

        protected virtual void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                var requirementSrv = new RequirementSoapClient();
                requirementSrv.SubmitRequirementForApproval(CompanyId, ProjectId, RequirementId, GetUsernameEncrypted());

                ToggleModification(false);

                LoadRequirement();

                SetFadeOutMessage(GetMainUpdatePanel(this), PostSuccessPanel, PostSuccessMessage,
                   "The requirement has been successfully submitted for approval.");
            }
            catch
            {
                SetFadeOutMessage(GetMainUpdatePanel(this), PostErrorPanel, PostErrorMessage,
                    "An error occurred while submitting the requirement.");
            }
        }

        protected virtual void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var requirementSrv = new RequirementSoapClient();
                requirementSrv.UpdateRequirement(RequirementTitleInput.Text, RequirementDescriptionInput.Text, CompanyId,
                    ProjectId, RequirementId, GetUsernameEncrypted());

                ToggleModification(false);

                LoadRequirement();

                SetFadeOutMessage(GetMainUpdatePanel(this), PostSuccessPanel, PostSuccessMessage,
                   "The requirement has been successfully updated.");
            }
            catch
            {
                SetFadeOutMessage(GetMainUpdatePanel(this), PostErrorPanel, PostErrorMessage,
                    "An error occurred while updating the requirement.");
            }
        }

        protected virtual void ApproveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var requirementSrv = new RequirementSoapClient();
                requirementSrv.ApproveRequirement(CompanyId, ProjectId, RequirementId, GetUsernameEncrypted());
                SetFadeOutMessage(GetMainUpdatePanel(this), PostSuccessPanel, PostSuccessMessage,
                    "The requirement has been successfully approved.");

                LoadRequirement();
            }
            catch
            {
                SetFadeOutMessage(GetMainUpdatePanel(this), PostErrorPanel, PostErrorMessage,
                    "An error occurred while approving the requirement..");
            }
        }

        protected virtual void RejectButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                var requirementSrv = new RequirementSoapClient();
                requirementSrv.RejectRequirement(CompanyId, ProjectId, RequirementId, GetUsernameEncrypted());
                SetFadeOutMessage(GetMainUpdatePanel(this), PostSuccessPanel, PostSuccessMessage,
                    "The requirement has been rejected.");

                LoadRequirement();
            }
            catch
            {
                SetFadeOutMessage(GetMainUpdatePanel(this), PostErrorPanel, PostErrorMessage,
                    "An error occurred while rejecting the requirement..");
            }
        }
        
        protected virtual void EditButton_OnClick(object sender, EventArgs e)
        {
            if (HasBeenApproved())
            {
                // redireto to modification request
                var currReqModifId = GetCurrentModificationId();

                var redirectUrl = "RequirementModify.aspx?" + CommonConstants.QueryStringParams.CompanyId + "=" +
                                  CompanyId +
                                  "&" + CommonConstants.QueryStringParams.ProjectId + "=" + ProjectId + "&" +
                                  CommonConstants.QueryStringParams.RequirementId + "=" + RequirementId;

                if (currReqModifId > 0)
                    redirectUrl += "&" + CommonConstants.QueryStringParams.Id + "=" + currReqModifId;

                Response.Redirect(redirectUrl);
            }
            else
            {
                // enable update form controls
                ToggleModification(true);
            }
        }

        protected virtual void UndoEditButton_OnClick(object sender, EventArgs e)
        {
            ToggleModification(false);
            LoadRequirement();
        }

        protected void LikeButton_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void DislikeButton_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void CommentsButton_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void HistoryButton_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void UploadButton_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Data Load
        protected virtual RequirementDto GetRequirement()
        {
            var requirementSrv = new RequirementSoapClient();
            var requirement = requirementSrv.GetRequirement(CompanyId, ProjectId, RequirementId);

            var serializer = new ObjectSerializer<RequirementDto>();
            return (RequirementDto)serializer.Deserialize(requirement);
        }

        private long GetCurrentModificationId()
        {
            var requirementSrv = new RequirementSoapClient();
            var requirementModif = requirementSrv.GetCurrentRequirementModification(CompanyId, ProjectId, RequirementId);

            var serializer = new ObjectSerializer<RequirementModificationDto>();
            var requirementModifDto = (RequirementModificationDto)serializer.Deserialize(requirementModif);
            return requirementModifDto.Id;
        }
        #endregion

        #region Form Setup

        protected virtual void LoadRequirement()
        {
            // get requirement data
            var requirement = GetRequirement();

            // set requirement data in form
            SetFormData(requirement);
        }

        /// <summary>
        /// Determines if a requirement has been approved or rejected
        /// </summary>
        /// <returns></returns>
        protected bool HasBeenApproved()
        {
            return int.Parse(RequirementStatusId.Value) == (int) GeneralCatalog.Detail.RequirementStatus.Approved ||
                   int.Parse(RequirementStatusId.Value) == (int) GeneralCatalog.Detail.RequirementStatus.Rejected;
        }

        protected bool IsPendingApproval()
        {
            return int.Parse(RequirementStatusId.Value) == (int) GeneralCatalog.Detail.RequirementStatus.PendingApproval;
        }

        protected virtual void SetFormData(RequirementDto requirement)
        {
            // set requirement data in UI controls
            RequirementTitle.Text = requirement.Title;
            RequirementTitleInput.Text = requirement.Title;
            RequirementDescription.Text = requirement.Description;
            RequirementDescriptionInput.Text = requirement.Description;
            RequirementStatus.Text = requirement.Status;
            RequirementStatusId.Value = requirement.StatusId.ToString();
            RequirementVersion.Text = requirement.VersionNumber.ToString();
            CreatedByName.Text = requirement.CreatedByName;
            ModifiedByName.Text = requirement.ModifiedByName;
            CreatedOn.Text = requirement.Createdon.ToString(CultureInfo.InvariantCulture);
            ModifiedOn.Text = requirement.Modifiedon.ToString(CultureInfo.InvariantCulture);

            // set action buttons visibility
            SaveButton.Visible = false;
            UndoEditButton.Visible = false;
            EditButton.Visible = true;
            SubmitButton.Visible = requirement.StatusId == (int)GeneralCatalog.Detail.RequirementStatus.Draft;
            ApproveButton.Visible = requirement.StatusId == (int)GeneralCatalog.Detail.RequirementStatus.PendingApproval;
            RejectButton.Visible = requirement.StatusId == (int)GeneralCatalog.Detail.RequirementStatus.PendingApproval;
        }

        protected void ToggleModification(bool visible)
        {
            RequirementTitle.Visible = !visible;
            RequirementTitleInput.Visible = visible;
            RequirementDescription.Visible = !visible;
            RequirementDescriptionInput.Visible = visible;
            SaveButton.Visible = visible;
            UndoEditButton.Visible = visible;
            ApproveButton.Visible = !visible && IsPendingApproval();
            RejectButton.Visible = !visible && IsPendingApproval();
            EditButton.Visible = !visible;
        }
        #endregion
    }
}