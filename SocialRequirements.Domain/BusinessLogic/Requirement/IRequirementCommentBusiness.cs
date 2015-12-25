using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.BusinessLogic.Requirement
{
    public interface IRequirementCommentBusiness
    {
        /// <summary>
        /// Creates a new requirement comment record
        /// </summary>
        void Add(long companyId, long projectId, long requirementId, string comment, string username);

        /// <summary>
        /// Gets all comments for the latest version of the specified requirement
        /// </summary>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <returns>A requirement comment data</returns>
        List<RequirementCommentDto> Get(long requirementId, long companyId, long projectId);
    }
}
