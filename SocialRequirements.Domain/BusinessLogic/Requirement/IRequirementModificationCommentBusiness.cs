using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.BusinessLogic.Requirement
{
    public interface IRequirementModificationCommentBusiness
    {
        /// <summary>
        /// Creates a new requirement modification comment record
        /// </summary>
        void Add(long companyId, long projectId, long requirementId, long requirementModificationId, string comment,
            string username);

        /// <summary>
        /// Gets all comments for the latest version of the specified requirement modification request
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementModificationId">Requirement Modification ID</param>
        /// <returns>A list of comments</returns>
        List<RequirementModificationCommentDto> Get(long companyId, long projectId, long requirementId, long requirementModificationId);
    }
}
