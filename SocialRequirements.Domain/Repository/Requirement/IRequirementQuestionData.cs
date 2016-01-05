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
        /// <param name="getAnswers">Sets if answers should be loaded or not</param>
        RequirementQuestionDto Get(long companyId, long projectId, long requirementId, long requirementVersionId,
            long requirementQuestionId, bool getAnswers);

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
        /// Gets all questions for all requirements in the specified projects
        /// </summary>
        /// <param name="projectIds">Projects ID</param>
        /// <returns>List of questions</returns>
        List<RequirementQuestionDto> GetAll(List<long> projectIds);

        /// <summary>
        /// Updatdes the status of a specified requirement question
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementVersionId">Requirement version ID</param>
        /// <param name="requirementQuestionId">Requirement question ID</param>
        /// <param name="statusId">Status Id to be set</param>
        void UpdateStatus(long companyId, long projectId, long requirementId, long requirementVersionId,
            long requirementQuestionId, int statusId);
    }
}
