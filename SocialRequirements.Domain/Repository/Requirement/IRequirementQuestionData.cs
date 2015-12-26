using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.Repository.Requirement
{
    public interface IRequirementQuestionData
    {
        /// <summary>
        /// Adds a new question to a requirement
        /// </summary>
        long Add(RequirementQuestionDto questionDto);

        /// <summary>
        /// Returns a specified requirement question
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementVersionId">Requirement version ID</param>
        /// <param name="requirementQuestionId">Requirement Question ID</param>
        RequirementQuestionDto Get(long companyId, long projectId, long requirementId, long requirementVersionId, long requirementQuestionId);

        /// <summary>
        /// Gets all questions for a specified requirement
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementVersionId">Requirement version ID</param>
        /// <returns>List of questions</returns>
        List<RequirementQuestionDto> Get(long companyId, long projectId, long requirementId, long requirementVersionId);

        /// <summary>
        /// Gets all questions for all requirements in a company
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <returns>List of questions</returns>
        List<RequirementQuestionDto> GetAll(long companyId);
    }
}
