using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.General;
using SocialRequirements.RequirementQuestionService;
using SocialRequirements.Utilities;

namespace SocialRequirements.Requirements
{
    public partial class RequirementsQuestionsList : SocialRequirementsPrivatePage
    {
        #region Properties
        protected List<RequirementQuestionDto> RequirementsQuestions
        {
            get { return ViewState["RequirementsQuestions"] != null ? (List<RequirementQuestionDto>)ViewState["RequirementsQuestions"] : new List<RequirementQuestionDto>(); }
            set { ViewState["RequirementsQuestions"] = value; }
        }
        #endregion

        #region Main Events
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (IsPostBack) return;

            SetRequirementsQuestionsList();
        }
        #endregion

        #region Questions Repeater Events
        protected void RequirementsQuestionsListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Data Load

        /// <summary>
        /// Returns all requirements related to the current user
        /// ordered descendently by modified date 
        /// </summary>
        /// <returns></returns>
        private void LoadRequirementsQuestions()
        {
            var questionsSrv = new RequirementQuestionSoapClient();

            var filter = Request.QueryString[CommonConstants.QueryStringParams.Filter];
            string questionsXmlStr;

            switch (filter)
            {
                case CommonConstants.Filters.Answered:
                    questionsXmlStr = questionsSrv.GetAnsweredQuestions(GetUsernameEncrypted());
                    break;
                case CommonConstants.Filters.Unanswered:
                    questionsXmlStr = questionsSrv.GetUnansweredQuestions(GetUsernameEncrypted());
                    break;
                default:
                    questionsXmlStr = questionsSrv.GetAllQuestions(GetUsernameEncrypted());
                    break;
            }
            
            var serializer = new ObjectSerializer<List<RequirementQuestionDto>>();
            RequirementsQuestions = (List<RequirementQuestionDto>)serializer.Deserialize(questionsXmlStr);
        }
        #endregion
        
        #region Form Setup

        /// <summary>
        /// Sets the requirements list data 
        /// </summary>
        private void SetRequirementsQuestionsList()
        {
            LoadRequirementsQuestions();
            RequirementsQuestionsListRepeater.DataSource = RequirementsQuestions;
            RequirementsQuestionsListRepeater.DataBind();
        }
        #endregion
    }
}