using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.BusinessLogic.Requirement
{
    public interface IRequirementModificationBusiness
    {
        /// <summary>
        /// Adds a new requirement modification request to the database
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="title">Requirement title</param>
        /// <param name="description">Requirement description</param>
        /// <param name="hashtagsToAdd">Hashtags to add to the requirement</param>
        /// <param name="hashtagsToRemove">Hashtags to remove from the requirement</param>
        /// <param name="priorityId">Requirement priority ID</param>
        /// <param name="username">Creation username</param>
        /// <returns>Requirement DTO with data of the newly created requirement</returns>
        RequirementModificationDto Add(long companyId, long projectId, long requirementId, string title, string description,
            string[] hashtagsToAdd, string[] hashtagsToRemove, int priorityId, string username);

        /// <summary>
        /// Returns requirement data matching criteria specified by input params
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementModificationId">Requirement Modification ID</param>
        /// <returns>Requirement</returns>
        RequirementModificationDto Get(long companyId, long projectId, long requirementId, long requirementModificationId);

        /// <summary>
        /// Returns the current active requirement modification request
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <returns>Current active requirement modification request, null when no active modifiaction request is found</returns>
        RequirementModificationDto Get(long companyId, long projectId, long requirementId);

        /// <summary>
        /// Submits a requirement for its aproval
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementModificationId">Requirement modification ID</param>
        /// <param name="username">User that submitted the requirement</param>
        void SubmitForApproval(long companyId, long projectId, long requirementId, long requirementModificationId, string username);

        /// <summary>
        /// Update a requirement modification request
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="description">Description</param>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementModificationId">Requirement modification ID</param>
        /// <param name="username">User who updates the requirement modification request</param>
        void Update(string title, string description, long companyId, long projectId, long requirementId,
            long requirementModificationId, string username);

        /// <summary>
        /// Approves a requirement modification requests and
        /// creates a new version of the requirement 
        /// (which will be automatically approved is the requirement was already approved,
        /// otherwise will require requirement aproval)
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementModificationId">Requirement Modification ID</param>
        /// <param name="username">User who approves the requirement</param>
        void Approve(long companyId, long projectId, long requirementId, long requirementModificationId, string username);

        /// <summary>
        /// Rejects a requirement modification request and
        /// does not create a new version of the requirement
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementModificationId">Requirement modification ID</param>
        /// <param name="username">User who rejects the requirement</param>
        void Reject(long companyId, long projectId, long requirementId, long requirementModificationId, string username);

        /// <summary>
        /// Adds a like to the specified requirement modification request
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <param name="projectId">Project identifier</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementModificationId">Requirement Modification ID</param>
        /// <param name="username">Username who gave the like</param>
        void Like(long companyId, long projectId, long requirementId, long requirementModificationId, string username);

        /// <summary>
        /// Adds a like to the specified requirement modification request
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <param name="projectId">Project identifier</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementModificationId">Requirement modification ID</param>
        /// <param name="username">Username who gave the dislike</param>
        void Dislike(long companyId, long projectId, long requirementId, long requirementModificationId, string username);

        /// <summary>
        /// Query requirements modification by user from all of his projects and companies
        /// </summary>
        /// <param name="username">Username related to the requirements</param>
        /// <returns>List of requirements</returns>
        List<RequirementModificationDto> GetList(string username);

        /// <summary>
        /// Query approved requirements modification by user from all of his projects and companies
        /// </summary>
        /// <param name="username">Username related to the requirements</param>
        /// <returns>List of requirements</returns>
        List<RequirementModificationDto> GetListApproved(string username);

        /// <summary>
        /// Query rejected requirements modification by user from all of his projects and companies
        /// </summary>
        /// <param name="username">Username related to the requirements</param>
        /// <returns>List of requirements</returns>
        List<RequirementModificationDto> GetListRejected(string username);

        /// <summary>
        /// Query requirements modification with pending approval by user from all of his projects and companies
        /// </summary>
        /// <param name="username">Username related to the requirements</param>
        /// <returns>List of requirements</returns>
        List<RequirementModificationDto> GetListPending(string username);

        /// <summary>
        /// Query draft requirements modification by user from all of his projects and companies
        /// </summary>
        /// <param name="username">Username related to the requirements</param>
        /// <returns>List of requirements</returns>
        List<RequirementModificationDto> GetListDraft(string username);

        void UploadAttachment(long companyId, long projectId, long requirementId, long requirementModificationId,
            string fileName, byte[] fileContent, string username);

        /// <summary>
        /// Uploads a file to the DB server as part of a specific requirement version record
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementModificationId">Requirement modification ID</param>
        /// <param name="requirementModifVersionId">Requirement version ID</param>
        /// <param name="fileName">File name</param>
        /// <param name="fileContent">File content in bytes</param>
        /// <param name="username">Modification username</param>
        void UploadAttachment(long companyId, long projectId, long requirementId, long requirementModificationId, 
            long requirementModifVersionId,
            string fileName,
            byte[] fileContent, string username);

        byte[] GetAttachment(long companyId, long projectId, long requirementId, long requirementModificationId,
            long? requirementModifVersionId = null);
    }
}
