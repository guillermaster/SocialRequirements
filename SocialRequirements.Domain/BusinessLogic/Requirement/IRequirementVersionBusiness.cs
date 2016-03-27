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

        /// <summary>
        /// Returns a specific version of a requirement, if version ID is null,
        /// then returns the latest version
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <param name="projectId">Project identifier</param>
        /// <param name="requirementId">Requirement identifier</param>
        /// <param name="requirementVersionId">Version identifier</param>
        RequirementDto Get(long companyId, long projectId, long requirementId, long? requirementVersionId = null);
    }
}
