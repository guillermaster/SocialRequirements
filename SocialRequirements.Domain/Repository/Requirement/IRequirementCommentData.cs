using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.Repository.Requirement
{
    public interface IRequirementCommentData
    {
        void Add(RequirementCommentDto requirementComment);

        /// <summary>
        /// Gets all comments for the specified requirement
        /// </summary>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementVersionId">Requirement version ID</param>
        /// <returns></returns>
        List<RequirementCommentDto> Get(long requirementId, long companyId, long projectId,
            long requirementVersionId);

        /// <summary>
        /// Gets the number of comments made for a specific requirement version
        /// </summary>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementVersionId">Requirement version ID</param>
        /// <returns>Number of comments</returns>
        int GetQuantity(long requirementId, long companyId, long projectId, long requirementVersionId);
    }
}
