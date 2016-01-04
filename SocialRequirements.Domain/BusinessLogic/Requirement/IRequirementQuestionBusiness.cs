using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.BusinessLogic.Requirement
{
    public interface IRequirementQuestionBusiness
    {
        /// <summary>
        /// Adds a new question to a requirement
        /// </summary>
        void Add(long companyId, long projectId, long requirementId, string question, string username);

        /// <summary>
        /// Returns a specified question
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
        /// Gets all questions for the latest version of a specified requirement
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementVersionId">Requirement version ID</param>
        /// <returns>List of questions</returns>
        List<RequirementQuestionDto> Get(long companyId, long projectId, long requirementId, long requirementVersionId);

        /// <summary>
        /// Gets all questions for all requirements related to a specified user
        /// </summary>
        /// <returns>List of questions</returns>
        List<RequirementQuestionDto> GetAll(string username);

        /// <summary>
        /// Gets all questions that have been answered for all requirements related to a specified user
        /// </summary>
        /// <returns>List of questions</returns>
        List<RequirementQuestionDto> GetAllAnswered(string username);

        /// <summary>
        /// Gets all questions that have not been answered for all requirements related to a specified user
        /// </summary>
        /// <returns>List of questions</returns>
        List<RequirementQuestionDto> GetAllUnanswered(string username);
    }
}
