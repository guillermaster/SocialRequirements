using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.Repository.Requirement
{
    public interface IRequirementQuestionAnswerData
    {
        long Add(RequirementQuestionAnswerDto answerDto);

        List<RequirementQuestionAnswerDto> Get(long companyId, long projectId, long requirementId, long requirementVersionId,
            long requirementQuestionId);

        int GetNumberOfAnswers(long companyId, long projectId, long requirementId, long requirementVersionId,
            long requirementQuestionId);
    }
}
