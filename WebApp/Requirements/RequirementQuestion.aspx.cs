using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.General;
using SocialRequirements.RequirementQuestionService;
using SocialRequirements.RequirementService;
using SocialRequirements.Utilities;

namespace SocialRequirements.Requirements
{
    public partial class RequirementQuestion : SocialRequirementsPrivatePage
    {
        #region Properties
        protected long CompanyId
        {
            get { return ViewState["CompanyId"] != null ? int.Parse(ViewState["CompanyId"].ToString()) : 0; }
            set { ViewState["CompanyId"] = value; }
        }

        protected long ProjectId
        {
            get { return ViewState["ProjectId"] != null ? int.Parse(ViewState["ProjectId"].ToString()) : 0; }
            set { ViewState["ProjectId"] = value; }
        }

        protected long RequirementId
        {
            get { return ViewState["RequirementId"] != null ? int.Parse(ViewState["RequirementId"].ToString()) : 0; }
            set { ViewState["RequirementId"] = value; }
        }

        protected long RequirementVersionId
        {
            get { return ViewState["RequirementVersionId"] != null ? int.Parse(ViewState["RequirementVersionId"].ToString()) : 0; }
            set { ViewState["RequirementVersionId"] = value; }
        }

        protected long RequirementQuestionId
        {
            get { return ViewState["RequirementQuestionId"] != null ? int.Parse(ViewState["RequirementQuestionId"].ToString()) : 0; }
            set { ViewState["RequirementQuestionId"] = value; }
        }
        
        #endregion

        #region Main Events
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (IsPostBack) return;

            // get query string params
            RequirementQuestionId = long.Parse(Request.QueryString[CommonConstants.QueryStringParams.Id]);
            RequirementId = long.Parse(Request.QueryString[CommonConstants.QueryStringParams.RequirementId]);
            RequirementVersionId = long.Parse(Request.QueryString[CommonConstants.QueryStringParams.RequirementVersionId]);
            CompanyId = long.Parse(Request.QueryString[CommonConstants.QueryStringParams.CompanyId]);
            ProjectId = long.Parse(Request.QueryString[CommonConstants.QueryStringParams.ProjectId]);
            
            LoadQuestion();

            // if a message was passes in the query string, display it
            var message = Request.QueryString[CommonConstants.QueryStringParams.Message];
            if (!string.IsNullOrWhiteSpace(message))
                SetFadeOutMessage(GetMainUpdatePanel(this), PostSuccessPanel, PostSuccessMessage, message);
        }
        
        protected virtual void AnswersButton_OnClick(object sender, EventArgs e)
        {
            SetAnswers();
        }
        #endregion

        #region Answers Events
        
        protected virtual void AddNewAnswerButton_Click(object sender, EventArgs e)
        {
            try
            {
                var questionSrv = new RequirementQuestionSoapClient();
                questionSrv.AddAnswer(CompanyId, ProjectId, RequirementId, RequirementVersionId, 
                    RequirementQuestionId, NewAnswerInput.Text, GetUsernameEncrypted());

                SetAnswers();
                // clear comment input box
                NewAnswerInput.Text = string.Empty;

                SetFadeOutMessage(GetMainUpdatePanel(this), PostSuccessPanel, PostSuccessMessage,
                   "The answer has been successfully posted.");
            }
            catch
            {
                SetFadeOutMessage(GetMainUpdatePanel(this), PostErrorPanel, PostErrorMessage,
                    "An error occurred while posting the answer.");
            }
        }
        #endregion

        #region Data Load

        protected virtual RequirementQuestionDto GetQuestion()
        {
            var questionSrv = new RequirementQuestionSoapClient();
            var question = questionSrv.GetQuestion(CompanyId, ProjectId, RequirementId, RequirementVersionId, RequirementQuestionId, true);

            var serializer = new ObjectSerializer<RequirementQuestionDto>();
            return (RequirementQuestionDto)serializer.Deserialize(question);
        }

        private List<RequirementQuestionAnswerDto> GetAnswers()
        {
            var questionSrv = new RequirementQuestionSoapClient();
            var answers = questionSrv.GetAnswers(CompanyId, ProjectId, RequirementId, RequirementVersionId,
                RequirementQuestionId);

            var serializer = new ObjectSerializer<List<RequirementQuestionAnswerDto>>();
            return (List<RequirementQuestionAnswerDto>) serializer.Deserialize(answers);
        }
        #endregion

        #region Form Setup

        protected virtual void LoadQuestion()
        {
            // get question data
            var question = GetQuestion();

            // set question data in form
            SetFormData(question);
        }

        protected virtual void SetFormData(RequirementQuestionDto question)
        {
            // set requirement data in UI controls
            QuestionText.Text = question.Question;
            QuestionStatus.Text = question.Status;
            QuestionStatusId.Value = question.StatusId.ToString();
            CreatedByName.Text = question.CreatedByName;
            CreatedOn.Text = question.Createdon.ToString(CultureInfo.InvariantCulture);
            AnswersList.DataSource = question.Answers;
            AnswersList.DataBind();
            NoAnswersPanel.Visible = question.Answers.Count == 0;
            AnswerCounter.Text = question.Answers.Count.ToString();
        }
        
        protected virtual void SetAnswers()
        {
            var answers = GetAnswers();
            AnswersList.DataSource = answers;
            AnswersList.DataBind();
            AnswerCounter.Text = answers.Count.ToString();
            NoAnswersPanel.Visible = answers.Count == 0;
        }
        #endregion
    }
}