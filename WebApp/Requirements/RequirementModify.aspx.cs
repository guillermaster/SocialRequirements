using System;
using System.Globalization;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.General;
using SocialRequirements.RequirementService;
using SocialRequirements.Utilities;

namespace SocialRequirements.Requirements
{
    public partial class RequirementModify : Requirement
    {
        #region Properties
        protected long RequirementModificationId
        {
            get { return ViewState["RequirementModificationId"] != null ? int.Parse(ViewState["RequirementModificationId"].ToString()) : 0; }
            set { ViewState["RequirementModificationId"] = value; }
        }
        #endregion

        #region Main Events
        protected override void Page_Load(object sender, EventArgs e)
        {
            ValidateUserLoggedIn();

            if (IsPostBack) return;

            // get query string params
            if (Request.QueryString[CommonConstants.QueryStringParams.Id] != null)
            {
                RequirementModificationId = long.Parse(Request.QueryString[CommonConstants.QueryStringParams.Id]);
            }
            RequirementId = long.Parse(Request.QueryString[CommonConstants.QueryStringParams.RequirementId]);
            CompanyId = long.Parse(Request.QueryString[CommonConstants.QueryStringParams.CompanyId]);
            ProjectId = long.Parse(Request.QueryString[CommonConstants.QueryStringParams.ProjectId]);

            LoadRequirement();
            EditionMode = true;
            ToggleModification();
        }

        protected override void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var requirementSrv = new RequirementSoapClient();

                if (!ModificationRequestExists())
                {
                    RequirementModificationId = requirementSrv.AddRequirementModification(RequirementTitleInput.Text,
                        RequirementDescriptionInput.Text,
                        CompanyId, ProjectId, RequirementId, GetUsernameEncrypted());

                    SetFadeOutMessage(GetMainUpdatePanel(this), PostSuccessPanel, PostSuccessMessage,
                        "The requirement modification request has been successfully created.");
                }
                else
                {
                    requirementSrv.UpdateRequirementModification(RequirementTitleInput.Text,
                        RequirementDescriptionInput.Text, CompanyId, ProjectId, RequirementId, RequirementModificationId,
                        GetUsernameEncrypted());
                }
                LoadRequirement();
                EditionMode = false;
                ToggleModification();
            }
            catch
            {
                SetFadeOutMessage(GetMainUpdatePanel(this), PostErrorPanel, PostErrorMessage,
                    "An error occurred while creating the requirement modification request");
            }
        }

        protected override void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                var requirementSrv = new RequirementSoapClient();
                requirementSrv.SubmitRequirementModificationForApproval(CompanyId, ProjectId, RequirementId,
                    RequirementModificationId, GetUsernameEncrypted());

                EditionMode = false;
                ToggleModification();

                LoadRequirement();

                SetFadeOutMessage(GetMainUpdatePanel(this), PostSuccessPanel, PostSuccessMessage,
                   "The requirement modification has been successfully submitted for approval.");
            }
            catch
            {
                SetFadeOutMessage(GetMainUpdatePanel(this), PostErrorPanel, PostErrorMessage,
                    "An error occurred while submitting the requirement modification.");
            }
        }

        protected override void ApproveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var requirementSrv = new RequirementSoapClient();
                requirementSrv.ApproveRequirementModification(CompanyId, ProjectId, RequirementId, RequirementModificationId, GetUsernameEncrypted());

                Response.Redirect(CommonConstants.FormsUrl.Requirement + "?" + 
                                  CommonConstants.QueryStringParams.Id + "=" + RequirementId + "&" + 
                                  CommonConstants.QueryStringParams.CompanyId + "=" + CompanyId + "&" + 
                                  CommonConstants.QueryStringParams.ProjectId + "=" + ProjectId + "&" +
                                  CommonConstants.QueryStringParams.Message + "=" + "The requirement has been successfully updated.");
            }
            catch
            {
                SetFadeOutMessage(GetMainUpdatePanel(this), PostErrorPanel, PostErrorMessage,
                    "An error occurred while approving the requirement..");
            }
        }

        protected override void RejectButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                var requirementSrv = new RequirementSoapClient();
                requirementSrv.RejectRequirementModification(CompanyId, ProjectId, RequirementId, RequirementModificationId, GetUsernameEncrypted());
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
        
        protected override void EditButton_OnClick(object sender, EventArgs e)
        {
            EditionMode = true;
            ToggleModification();
        }

        protected override void UndoEditButton_OnClick(object sender, EventArgs e)
        {
            EditionMode = false;
            ToggleModification();
            LoadRequirement();
        }

        protected override void LikeButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                var requirementSrv = new RequirementSoapClient();
                requirementSrv.LikeRequirementModification(CompanyId, ProjectId, RequirementId, RequirementModificationId, GetUsernameEncrypted());
                
                LoadRequirement();

                ToggleModification();
            }
            catch
            {
                SetFadeOutMessage(GetMainUpdatePanel(this), PostErrorPanel, PostErrorMessage,
                    "An error occurred.");
            }
        }

        protected override void DislikeButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                var requirementSrv = new RequirementSoapClient();
                requirementSrv.DislikeRequirementModification(CompanyId, ProjectId, RequirementId, RequirementModificationId, GetUsernameEncrypted());

                LoadRequirement();

                ToggleModification();
            }
            catch
            {
                SetFadeOutMessage(GetMainUpdatePanel(this), PostErrorPanel, PostErrorMessage,
                    "An error occurred.");
            }
        }

        protected override void CommentsButton_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void HistoryButton_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void UploadButton_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Data Load

        protected override RequirementDto GetRequirement()
        {
            var requirementSrv = new RequirementSoapClient();

            // if there requirement modification request has not been created yet
            if (!ModificationRequestExists())
            {
                var requirement = requirementSrv.GetRequirement(CompanyId, ProjectId, RequirementId);
                var serializer = new ObjectSerializer<RequirementDto>();
                return (RequirementDto) serializer.Deserialize(requirement);
            }
            else
            {
                var requirementModif = requirementSrv.GetRequirementModification(CompanyId, ProjectId, RequirementId,
                    RequirementModificationId);

                var serializer = new ObjectSerializer<RequirementModificationDto>();
                return (RequirementModificationDto)serializer.Deserialize(requirementModif);
            }
        }
        #endregion

        #region Form Setup

        protected override void LoadRequirement()
        {
            // get requirement data
            var requirement = GetRequirement();

            // set requirement data in form
            SetFormData(requirement);
        }

        private bool ModificationRequestExists()
        {
            return RequirementModificationId > 0;
        }

        protected override void SetFormData(RequirementDto requirement)
        {
            base.SetFormData(requirement);
            EditButton.Visible = ModificationRequestExists();
        }

        #endregion
    }
}