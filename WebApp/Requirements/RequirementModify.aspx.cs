using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
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

        protected bool IsDraft
        {
            get { return ViewState["IsDraft"] == null || bool.Parse(ViewState["IsDraft"].ToString()); }
            set { ViewState["IsDraft"] = value; }
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
            SetFormPermissions();
            
            LoadRequirement();
            ToggleModification();
            RegisterTrigger(DownloadButton);
            RegisterJsBeforePostback("BeforePostback();");
        }

        protected override void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var requirementSrv = new RequirementSoapClient();

                if (!ModificationRequestExists())
                {
                    RequirementModificationId = requirementSrv.AddRequirementModification(RequirementTitleInput.Text,
                        HdnRequirementDescriptionInput.Value,
                        CompanyId, ProjectId, RequirementId, GetUsernameEncrypted());

                    SetFadeOutMessage(GetMainUpdatePanel(this), PostSuccessPanel, PostSuccessMessage,
                        "The requirement modification request has been successfully created.");
                }
                else
                {
                    requirementSrv.UpdateRequirementModification(RequirementTitleInput.Text,
                        HdnRequirementDescriptionInput.Value, CompanyId, ProjectId, RequirementId, RequirementModificationId,
                        GetUsernameEncrypted());
                }
                LoadRequirement();
                EditionMode = false;
                ToggleModification();
            }
            catch (Exception ex)
            {
                SetFadeOutMessage("An error has occurred, please try again.", false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
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
            catch (Exception ex)
            {
                SetFadeOutMessage("An error has occurred, please try again.", false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
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
            catch (Exception ex)
            {
                SetFadeOutMessage("An error has occurred, please try again.", false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
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
                var prevEditionMode = EditionMode;
                LoadRequirement();
                EditionMode = prevEditionMode;
                ToggleModification();
            }
            catch (Exception ex)
            {
                SetFadeOutMessage("An error has occurred, please try again.", false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        protected override void DislikeButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                var requirementSrv = new RequirementSoapClient();
                requirementSrv.DislikeRequirementModification(CompanyId, ProjectId, RequirementId, RequirementModificationId, GetUsernameEncrypted());
                var prevEditionMode = EditionMode;
                LoadRequirement();
                EditionMode = prevEditionMode;
                ToggleModification();
            }
            catch (Exception ex)
            {
                SetFadeOutMessage("An error has occurred, please try again.", false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
        
        protected override void HistoryButton_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        
        #endregion

        #region Comments events

        protected override void AddNewCommentButton_Click(object sender, EventArgs e)
        {
            var requirementSrv = new RequirementSoapClient();
            requirementSrv.CommentRequirementModification(CompanyId, ProjectId, RequirementId, RequirementModificationId,
                NewCommentInput.Text, GetUsernameEncrypted());
            SetRequirementComments();
            // clear comment input box
            NewCommentInput.Text = string.Empty;
        }
        #endregion

        #region Data Load

        protected override RequirementDto GetRequirement()
        {
            var requirementSrv = new RequirementSoapClient();

            // if there requirement modification request has not been created yet
            if (!ModificationRequestExists())
            {
                var requirementStr = requirementSrv.GetRequirement(CompanyId, ProjectId, RequirementId);
                var serializer = new ObjectSerializer<RequirementDto>();
                var requirement = (RequirementDto) serializer.Deserialize(requirementStr);
                requirement.StatusId = (int) GeneralCatalog.Detail.RequirementStatus.Draft;
                return requirement;
            }
            else
            {
                var requirementModif = requirementSrv.GetRequirementModification(CompanyId, ProjectId, RequirementId,
                    RequirementModificationId);

                var serializer = new ObjectSerializer<RequirementModificationDto>();
                return (RequirementModificationDto)serializer.Deserialize(requirementModif);
            }
        }

        private List<RequirementModificationCommentDto> GetComments()
        {
            var requirementSrv = new RequirementSoapClient();
            var comments = requirementSrv.GetRequirementModificationComments(CompanyId, ProjectId, RequirementId,
                RequirementModificationId);

            var serializer = new ObjectSerializer<List<RequirementModificationCommentDto>>();
            return (List<RequirementModificationCommentDto>)serializer.Deserialize(comments);
        }

        protected override void DownloadFile()
        {
            var requirementSrv = new RequirementSoapClient();
            var fileBytes = requirementSrv.GetModificationAttachment(CompanyId, ProjectId, RequirementId,
                RequirementModificationId);

            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachement;filename=" + HttpUtility.UrlEncode(FileName));
            Response.AddHeader("Content-Length", fileBytes.Length.ToString());
            Response.BinaryWrite(fileBytes);
            Response.Flush();
            Response.Close();
        }
        #endregion

        #region Data Update

        protected override void UploadFile(string fileName, byte[] fileContent)
        {
            try
            {
                var reqSrv = new RequirementSoapClient();
                reqSrv.AddModificationAttachment(CompanyId, ProjectId, RequirementId, RequirementModificationId,
                    fileName, fileContent, GetUsernameEncrypted());

                FileName = fileName;
                DownloadButton.Visible = true;
                SetFadeOutMessage("The file has been uploaded.", true);
            }
            catch (Exception ex)
            {
                SetFadeOutMessage("An error has occurred, please try again.", false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
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

            IsDraft = requirement.StatusId == (int) GeneralCatalog.Detail.RequirementStatus.Draft;
            EditionMode = IsDraft;
        }

        protected override void ToggleModification()
        {
            base.ToggleModification();
            EditButton.Visible = IsDraft;
        }

        private bool ModificationRequestExists()
        {
            return RequirementModificationId > 0;
        }

        protected override void SetFormData(RequirementDto requirement)
        {
            base.SetFormData(requirement);
            EditButton.Visible = ModificationRequestExists() &&
                                 requirement.StatusId == (int) GeneralCatalog.Detail.RequirementStatus.Draft;
        }

        protected override void SetRequirementComments()
        {
            var comments = GetComments();
            CommentsList.DataSource = comments;
            CommentsList.DataBind();
            CommentCounter.Text = comments.Count.ToString();
        }
        #endregion
    }
}