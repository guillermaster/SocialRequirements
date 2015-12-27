using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.BusinessLogic.Requirement
{
    public interface IRequirementQuestionAnswerBusiness
    {
        /// <summary>
        /// Adds a new answer to a specified question
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementVersionId">Requirement version ID</param>
        /// <param name="requirementQuestionId">Question ID</param>
        /// <param name="answer">The answer</param>
        /// <param name="username">User who added the answer</param>
        void Add(long companyId, long projectId, long requirementId, long requirementVersionId,
            long requirementQuestionId, string answer, string username);

        /// <summary>
        /// Get all answers for a specified question
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementVersionId">Requirement Version ID</param>
        /// <param name="requirementQuestionId">Question ID</param>
        /// <returns>List of answers</returns>
        List<RequirementQuestionAnswerDto> Get(long companyId, long projectId, long requirementId,
            long requirementVersionId, long requirementQuestionId);
    }
}
