using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.Repository.Requirement
{
    public interface IRequirementModificationVersionData
    {
        /// <summary>
        /// Inserts a new requirement modification request version in the database
        /// </summary>
        /// <param name="requirement">Requirement data</param>
        /// <returns>Version number for added version</returns>
        RequirementModificationDto Add(RequirementModificationDto requirement);

        /// <summary>
        /// Returns a specific version of a requirement modification request version, if version ID is null,
        /// then returns the latest version
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <param name="projectId">Project identifier</param>
        /// <param name="requirementId">Requirement identifier</param>
        /// <param name="requirementVersionId">Version identifier</param>
        RequirementDto Get(long companyId, long projectId, long requirementId, long? requirementVersionId = null);

        /// <summary>
        /// Adds like to a specific requirement modification request version
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementVersionId">Version ID</param>
        /// <param name="personId">Person ID</param>
        void Like(long companyId, long projectId, long requirementId, long requirementVersionId, long personId);

        /// <summary>
        /// Adds dislike to a specific requirement modification request version
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementVersionId">Version ID</param>
        /// <param name="personId">Person ID</param>
        void Dislike(long companyId, long projectId, long requirementId, long requirementVersionId, long personId);

        /// <summary>
        /// Updates the status of a requirement modification request version. Tf the status is approved, the approval user is set.
        /// If the status is rejected, the rejection user is set. 
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="versionId">Requirement version ID</param>
        /// <param name="statusId">Status ID to be set</param>
        /// <param name="personId">Person ID, to be set as modification user and may be the approval/rejection user.</param>
        void UpdateStatus(long companyId, long projectId, long requirementId, long versionId, int statusId, long personId);
    }
}
