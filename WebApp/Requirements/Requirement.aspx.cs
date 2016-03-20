using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Web;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.General;
using SocialRequirements.RequirementQuestionService;
using SocialRequirements.RequirementService;
using SocialRequirements.Utilities;

namespace SocialRequirements.Requirements
{
    public partial class Requirement : SocialRequirementsPrivatePage
    {
        #region Constants
        private const string ViewCommentsLabel = "View comments";
        private const string HideCommentsLabel = "Hide comments";
        public const string PriorityLowCss = "btn disabled btn-default btn-sm";
        public const string PriorityMedCss = "btn disabled btn-inverse btn-sm";
        public const string PriorityHighCss = "btn disabled btn-midnightblue btn-sm";
        #endregion

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

        protected bool EditionMode
        {
            get { return ViewState["EditionMode"] != null && bool.Parse(ViewState["EditionMode"].ToString()); }
            set { ViewState["EditionMode"] = value; }
        }

        protected string FileName
        {
            get { return ViewState["FileName"] != null ? ViewState["FileName"].ToString() : string.Empty; }
            set { ViewState["FileName"] = value; }
        }

        protected bool? CanApproveRequirement
        {
            get
            {
                if (Session["CanApproveRequirement"] != null)
                {
                    return bool.Parse(Session["CanApproveRequirement"].ToString());
                }
                return null;
            }
            set { Session["CanApproveRequirement"] = value; }
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
            SetFormPermissions();
            
            LoadRequirement();

            // if a message was passes in the query string, display it
            var message = Request.QueryString[CommonConstants.QueryStringParams.Message];
            if (!string.IsNullOrWhiteSpace(message))
                SetFadeOutMessage(GetMainUpdatePanel(this), PostSuccessPanel, PostSuccessMessage, message);

            RegisterTrigger(DownloadButton);
            RegisterJsBeforePostback("BeforePostback();");
        }

        protected virtual void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                var requirementSrv = new RequirementSoapClient();
                requirementSrv.SubmitRequirementForApproval(CompanyId, ProjectId, RequirementId, GetUsernameEncrypted());

                EditionMode = false;
                ToggleModification();

                LoadRequirement();

                SetFadeOutMessage(GetMainUpdatePanel(this), PostSuccessPanel, PostSuccessMessage,
                   "The requirement has been successfully submitted for approval.");
            }
            catch (Exception ex)
            {
                SetFadeOutMessage("An error has occurred, please try again.", false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        protected virtual void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var newProjectId = long.Parse(ProjectInput.SelectedValue);
                var requirementSrv = new RequirementSoapClient();
                requirementSrv.UpdateRequirement(RequirementTitleInput.Text, HdnRequirementDescriptionInput.Value, newProjectId,
                    CompanyId, ProjectId, RequirementId, GetUsernameEncrypted());

                ProjectId = newProjectId;
                EditionMode = false;
                ToggleModification();

                LoadRequirement();

                SetFadeOutMessage(GetMainUpdatePanel(this), PostSuccessPanel, PostSuccessMessage,
                   "The requirement has been successfully updated.");
            }
            catch (Exception ex)
            {
                SetFadeOutMessage("An error has occurred, please try again.", false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
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
            catch (Exception ex)
            {
                SetFadeOutMessage("An error has occurred, please try again.", false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
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
            catch (Exception ex)
            {
                SetFadeOutMessage("An error has occurred, please try again.", false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
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
                EditionMode = true;
                ToggleModification();
            }
        }

        protected virtual void UndoEditButton_OnClick(object sender, EventArgs e)
        {
            EditionMode = false;
            ToggleModification();
            LoadRequirement();
        }

        protected virtual void LikeButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                var requirementSrv = new RequirementSoapClient();
                requirementSrv.LikeRequirement(CompanyId, ProjectId, RequirementId, GetUsernameEncrypted());

                LoadRequirement();

                ToggleModification();
            }
            catch (Exception ex)
            {
                SetFadeOutMessage("An error has occurred, please try again.", false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        protected virtual void DislikeButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                var requirementSrv = new RequirementSoapClient();
                requirementSrv.DislikeRequirement(CompanyId, ProjectId, RequirementId, GetUsernameEncrypted());

                LoadRequirement();

                ToggleModification();
            }
            catch (Exception ex)
            {
                SetFadeOutMessage("An error has occurred, please try again.", false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        protected virtual void CommentsButton_OnClick(object sender, EventArgs e)
        {
            ToggleComments();
        }

        protected virtual void HistoryButton_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        
        protected void btn_PostQuestionButton_Click(object sender, EventArgs e)
        {
            AddQuestion(QuestionInput.Text);
        }

        protected void UploadFileButton_Click(object sender, EventArgs e)
        {
            if (!FileUploader.HasFile) return;

            UploadFile(FileUploader.FileName, FileUploader.FileBytes);
        }

        protected void DownloadButton_OnClick(object sender, EventArgs e)
        {
            DownloadFile();
        }
        #endregion

        #region Comments Events

        protected void ViewHideCommentsButton_Click(object sender, EventArgs e)
        {
            ToggleComments();
        }

        protected virtual void AddNewCommentButton_Click(object sender, EventArgs e)
        {
            var requirementSrv = new RequirementSoapClient();
            requirementSrv.CommentRequirement(CompanyId, ProjectId, RequirementId, NewCommentInput.Text,
                GetUsernameEncrypted());
            SetRequirementComments();
            // clear comment input box
            NewCommentInput.Text = string.Empty;
        }
        #endregion

        #region Data Load
        protected virtual RequirementDto GetRequirement()
        {
            var requirementSrv = new RequirementSoapClient();
            var requirementStr = requirementSrv.GetRequirement(CompanyId, ProjectId, RequirementId);
            

            var serializer = new ObjectSerializer<RequirementDto>();
            var requirement = (RequirementDto)serializer.Deserialize(requirementStr);

            CompanyId = requirement.CompanyId;
            ProjectId = requirement.ProjectId;

            return requirement;
        }

        private long GetCurrentModificationId()
        {
            var requirementSrv = new RequirementSoapClient();
            var requirementModif = requirementSrv.GetCurrentRequirementModification(CompanyId, ProjectId, RequirementId);

            var serializer = new ObjectSerializer<RequirementModificationDto>();
            var requirementModifDto = (RequirementModificationDto)serializer.Deserialize(requirementModif);
            return requirementModifDto.Id;
        }

        private List<RequirementCommentDto> GetComments()
        {
            var requirementSrv = new RequirementSoapClient();
            var comments = requirementSrv.GetRequirementComments(CompanyId, ProjectId, RequirementId);

            var serializer = new ObjectSerializer<List<RequirementCommentDto>>();
            return (List<RequirementCommentDto>) serializer.Deserialize(comments);
        }

        protected virtual void DownloadFile()
        {
            var requirementSrv = new RequirementSoapClient();
            var fileBytes = requirementSrv.GetAttachment(CompanyId, ProjectId, RequirementId);

            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachement;filename=" + HttpUtility.UrlEncode(FileName));
            Response.AddHeader("Content-Length", fileBytes.Length.ToString());
            Response.BinaryWrite(fileBytes);
            Response.Flush();
            Response.Close();
        }

        private void LoadProjects()
        {
            var projects = GetProjectsByCompany(CompanyId);
            ProjectInput.DataSource = projects;
            ProjectInput.DataValueField = CustomExpression.GetPropertyName<ProjectDto>(p => p.Id);
            ProjectInput.DataTextField = CustomExpression.GetPropertyName<ProjectDto>(p => p.Name);
            ProjectInput.DataBind();

            ProjectInput.SelectedValue = ProjectId.ToString();
        }
        #endregion

        #region Data Update

        private void AddQuestion(string question)
        {
            try
            {
                var questionSrv = new RequirementQuestionSoapClient();
                questionSrv.AddQuestion(CompanyId, ProjectId, RequirementId, question, GetUsernameEncrypted());
                SetFadeOutMessage("The question has been posted.", true);
            }
            catch (Exception ex)
            {
                SetFadeOutMessage("An error has occurred, please try again.", false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        protected virtual void UploadFile(string fileName, byte[] fileContent)
        {
            try
            {
                var reqSrv = new RequirementSoapClient();
                reqSrv.AddAttachment(CompanyId, ProjectId, RequirementId, fileName, fileContent, GetUsernameEncrypted());

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
            return int.Parse(RequirementStatusId.Value) == (int) GeneralCatalog.Detail.RequirementStatus.Approved;
        }

        protected bool IsPendingApproval()
        {
            return int.Parse(RequirementStatusId.Value) == (int) GeneralCatalog.Detail.RequirementStatus.PendingApproval;
        }

        protected void SetFormPermissions()
        {
            if (CanApproveRequirement != null) return;
            CanApproveRequirement = HasPermission(ProjectId, Permissions.Codes.ApproveRequirements);
        }

        protected void SetPriority(RequirementDto requirement)
        {
            switch (requirement.PriorityId)
            {
                case (int)GeneralCatalog.Detail.RequirementPriority.Low:
                    Priority.Attributes.Add("class", PriorityLowCss);
                    break;
                case (int)GeneralCatalog.Detail.RequirementPriority.Medium:
                    Priority.Attributes.Add("class", PriorityMedCss);
                    break;
                case (int)GeneralCatalog.Detail.RequirementPriority.High:
                    Priority.Attributes.Add("class", PriorityHighCss);
                    break;
            }

            Priority.InnerHtml = requirement.Priority;
        }

        protected virtual void SetFormData(RequirementDto requirement)
        {
            // set requirement data in UI controls
            RequirementTitle.Text = requirement.Title;
            RequirementTitleInput.Text = requirement.Title;
            ProjectName.Text = requirement.Project;
            RequirementDescription.Text = requirement.Description;
            RequirementDescriptionInput.Text = requirement.Description;
            RequirementStatus.Text = requirement.Status;
            RequirementStatusId.Value = requirement.StatusId.ToString();
            RequirementVersion.Text = requirement.VersionNumber.ToString();
            CreatedByName.Text = requirement.CreatedByName;
            ModifiedByName.Text = requirement.ModifiedByName;
            CreatedOn.Text = requirement.Createdon.ToString(CultureInfo.InvariantCulture);
            ModifiedOn.Text = requirement.Modifiedon.ToString(CultureInfo.InvariantCulture);
            LikeCounter.Text = requirement.Agreed.ToString();
            DislikeCounter.Text = requirement.Disagreed.ToString();
            CommentCounter.Text = requirement.CommentsQuantity.ToString();
            FileName = requirement.AttachmentTitle;
            DownloadButton.Visible = !string.IsNullOrWhiteSpace(FileName);
            FileOverwriteWarning.Visible = !string.IsNullOrWhiteSpace(FileName);

            SetPriority(requirement);

            // hashtags
            if (RequirementsHashtagsRepeater != null)
            {
                RequirementsHashtagsRepeater.DataSource = requirement.Hashtags;
                RequirementsHashtagsRepeater.DataBind();
            }

            // set action buttons visibility
            SaveButton.Visible = false;
            UndoEditButton.Visible = false;
            EditButton.Visible = requirement.StatusId != (int)GeneralCatalog.Detail.RequirementStatus.PendingApproval;
            SubmitButton.Visible = requirement.StatusId == (int)GeneralCatalog.Detail.RequirementStatus.Draft;
            ApproveButton.Visible = requirement.StatusId ==
                                    (int) GeneralCatalog.Detail.RequirementStatus.PendingApproval &&
                                    CanApproveRequirement.HasValue && CanApproveRequirement.Value;
            RejectButton.Visible = ApproveButton.Visible;

            LoadProjects();
        }

        protected virtual void ToggleModification()
        {
            RequirementTitle.Visible = !EditionMode;
            RequirementTitleInput.Visible = EditionMode;
            ProjectName.Visible = !EditionMode;
            ProjectInput.Visible = EditionMode;
            RequirementDescription.Visible = !EditionMode;
            RequirementDescriptionInput.Visible = EditionMode;
            SaveButton.Visible = EditionMode;
            UndoEditButton.Visible = EditionMode;
            ApproveButton.Visible = !EditionMode && IsPendingApproval() && CanApproveRequirement.HasValue && CanApproveRequirement.Value;
            RejectButton.Visible = ApproveButton.Visible;
            EditButton.Visible = !EditionMode;
        }

        protected void ToggleComments()
        {
            CommentsPanel.Visible = !CommentsPanel.Visible;
            ViewHideCommentsButton.Text = CommentsPanel.Visible ? HideCommentsLabel : ViewCommentsLabel;
            if (!CommentsPanel.Visible) return;
            SetRequirementComments();
        }

        protected virtual void SetRequirementComments()
        {
            var comments = GetComments();
            CommentsList.DataSource = comments;
            CommentsList.DataBind();
            CommentCounter.Text = comments.Count.ToString();
        }
        #endregion
    }
}