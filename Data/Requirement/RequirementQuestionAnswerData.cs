using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.Data.Requirement
{
    public class RequirementQuestionAnswerData : IRequirementQuestionAnswerData
    {
        private readonly ContextModel _context;

        public RequirementQuestionAnswerData(ContextModel context)
        {
            _context = context;
        }

        public int GetNumberOfAnswers(long companyId, long projectId, long requirementId, long requirementVersionId,
            long requirementQuestionId)
        {
            return
                _context.RequirementQuestionAnswer.Count(
                    answer =>
                        answer.company_id == companyId && answer.project_id == projectId &&
                        answer.requirement_id == requirementId && answer.requirement_version_id == requirementVersionId &&
                        answer.requirement_question_id == requirementQuestionId);
        }
    }
}
