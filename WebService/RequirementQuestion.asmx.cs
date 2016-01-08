using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Services;
using Ninject;
using Ninject.Web;
using SocialRequirements.Domain.BusinessLogic.Requirement;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Utilities;
using SocialRequirements.Utilities.Security;

namespace WebService
{
    /// <summary>
    /// Summary description for RequirementQuestion
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RequirementQuestion : WebServiceBase
    {
        [Inject]
        public IRequirementQuestionBusiness RequirementQuestionBusiness { get; set; }
        [Inject]
        public IRequirementQuestionAnswerBusiness RequirementQuestionAnswerBusiness { get; set; }

        [WebMethod]
        public void AddQuestion(long companyId, long projectId, long requirementId, string question, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementQuestionBusiness.Add(companyId, projectId, requirementId, question, username);
        }

        [WebMethod]
        public string GetQuestion(long companyId, long projectId, long requirementId, long requirementVersionId,
            long requirementQuestionId, bool getAnswers)
        {
            var question = RequirementQuestionBusiness.Get(companyId, projectId, requirementId, requirementVersionId,
                requirementQuestionId, getAnswers);
            var serializer = new ObjectSerializer<RequirementQuestionDto>(question);
            return serializer.ToXmlString();
        }

        [WebMethod]
        public string GetAllQuestions(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var questions = RequirementQuestionBusiness.GetAll(username);
            var serializer = new ObjectSerializer<List<RequirementQuestionDto>>(questions);
            return serializer.ToXmlString();
        }

        [WebMethod]
        public string GetAnsweredQuestions(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var questions = RequirementQuestionBusiness.GetAllAnswered(username);
            var serializer = new ObjectSerializer<List<RequirementQuestionDto>>(questions);
            return serializer.ToXmlString();
        }

        [WebMethod]
        public string GetUnansweredQuestions(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var questions = RequirementQuestionBusiness.GetAllUnanswered(username);
            var serializer = new ObjectSerializer<List<RequirementQuestionDto>>(questions);
            return serializer.ToXmlString();
        }

        [WebMethod]
        public void AddAnswer(long companyId, long projectId, long requirementId, long requirementVersionId,
            long requirementQuestionId, string answer, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementQuestionAnswerBusiness.Add(companyId, projectId, requirementId, requirementVersionId,
                requirementQuestionId, answer, username);
        }

        [WebMethod]
        public string GetAnswers(long companyId, long projectId, long requirementId, long requirementVersionId,
            long requirementQuestionId)
        {
            var answers = RequirementQuestionAnswerBusiness.Get(companyId, projectId, requirementId, requirementVersionId,
                requirementQuestionId);

            var serializer = new ObjectSerializer<List<RequirementQuestionAnswerDto>>(answers);
            return serializer.ToXmlString();
        }
    }
}
