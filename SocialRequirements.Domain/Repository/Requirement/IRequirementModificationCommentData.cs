using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.Repository.Requirement
{
    public interface IRequirementModificationCommentData
    {
        long Add(RequirementModificationCommentDto requirementComment);

        /// <summary>
        /// Gets all comments for the specified requirement
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementModificationId">Requirement modification ID</param>
        /// <param name="requirementModificationVersionId">Requirement modification version ID</param>
        /// <returns></returns>
        List<RequirementModificationCommentDto> Get(long companyId, long projectId, long requirementId, 
            long requirementModificationId, long requirementModificationVersionId);

        /// <summary>
        /// Gets the number of comments made for a specific requirement version
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementModificationId">Requirement modification ID</param>
        /// <param name="requirementModificationVersionId">Rquirement modification version ID</param>
        /// <returns>Number of comments</returns>
        int GetQuantity(long companyId, long projectId, long requirementId, long requirementModificationId,
            long requirementModificationVersionId);

        RequirementModificationCommentDto Get(long companyId, long projectId, long commentId);
    }
}
