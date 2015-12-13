using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.Repository.Requirement
{
    public interface IRequirementVersionData
    {
        /// <summary>
        /// Inserts a new requirement version in the database
        /// </summary>
        /// <param name="requirement">Requirement data</param>
        void Add(RequirementDto requirement);

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
