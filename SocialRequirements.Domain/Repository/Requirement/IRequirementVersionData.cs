using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.Repository.Requirement
{
    public interface IRequirementVersionData
    {
        /// <summary>
        /// Inserts a new requirement version in the database
        /// </summary>
        /// <param name="requirement">Requirement data</param>
        /// <returns>Version number for added version</returns>
        RequirementDto Add(RequirementDto requirement);

        /// <summary>
        /// Returns a specific version of a requirement, if version ID is null,
        /// then returns the latest version
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <param name="projectId">Project identifier</param>
        /// <param name="requirementId">Requirement identifier</param>
        /// <param name="requirementVersionId">Version identifier</param>
        RequirementDto Get(long companyId, long projectId, long requirementId, long? requirementVersionId = null);

        /// <summary>
        /// Adds like to a specific requirement version
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementVersionId">Version ID</param>
        /// <param name="personId">Person ID</param>
        void Like(long companyId, long projectId, long requirementId, long requirementVersionId, long personId);

        string GetAttachmentTitle(long companyId, long projectId, long requirementId, long? requirementVersionId = null);

        byte[] GetAttachment(long companyId, long projectId, long requirementId, long? requirementVersionId = null);

        /// <summary>
        /// Adds dislike to a specific requirement version
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementVersionId">Version ID</param>
        /// <param name="personId">Person ID</param>
        void Dislike(long companyId, long projectId, long requirementId, long requirementVersionId, long personId);

        /// <summary>
        /// Updates the status of a requirement version. Tf the status is approved, the approval user is set.
        /// If the status is rejected, the rejection user is set. 
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="versionId">Requirement version ID</param>
        /// <param name="statusId">Status ID to be set</param>
        /// <param name="personId">Person ID, to be set as modification user and may be the approval/rejection user.</param>
        void UpdateStatus(long companyId, long projectId, long requirementId, long versionId, int statusId, long personId);

        /// <summary>
        /// Updates the title and description for the specified requirement
        /// </summary>
        /// <param name="title">Requirement title</param>
        /// <param name="description">Description title</param>
        /// <param name="newProjectId">Project ID (to allow to move a requirement to another project)</param>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="versionId">Version ID</param>
        /// <param name="priorityId">Requirement priority ID</param>
        /// <param name="personId">User that triggered the update</param>
        void Update(string title, string description, long newProjectId, long companyId, long projectId,
            long requirementId, long versionId, int priorityId, long personId);

        /// <summary>
        /// Uploads a file to the DB server as part of the requirement version record
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementVersionId">Requirement version ID</param>
        /// <param name="fileName">File name</param>
        /// <param name="fileContent">File content in bytes</param>
        /// <param name="personId">Modification user ID</param>
        void UploadAttachment(long companyId, long projectId, long requirementId, long requirementVersionId,
            string fileName, byte[] fileContent, long personId);

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
