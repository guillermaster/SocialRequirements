using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.BusinessLogic.Requirement
{
    public interface IRequirementVersionBusiness
    {
        /// <summary>
        /// Gets all versions of the specified requirement
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <returns>List of requirement version</returns>
        List<RequirementDto> GetVersionHistory(long companyId, long projectId, long requirementId);
    }
}
