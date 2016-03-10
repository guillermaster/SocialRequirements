using System.Collections.Generic;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.BusinessLogic.Requirement
{
    public interface IRequirementBusiness
    {
        /// <summary>
        /// Specified whether a company has requirements or not
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <returns>True when the company has at least one requirements, otherwise returns false.</returns>
        bool HaveRequirements(long companyId);


        /// <summary>
        /// Adds a new requirement to the database
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <param name="projectId">Project identifier</param>
        /// <param name="title">Requirement title</param>
        /// <param name="description">Requirement description</param>
        /// <param name="username">Creation username</param>
        /// <returns>Requirement DTO with data of the newly created requirement</returns>
        RequirementDto Add(long companyId, long projectId, string title, string description,
            string username);

        /// <summary>
        /// Adds a like to the specified requirement
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <param name="projectId">Project identifier</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="username">Username who gave the like</param>
        void Like(long companyId, long projectId, long requirementId, string username);

        /// <summary>
        /// Adds a like to the specified requirement
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <param name="projectId">Project identifier</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="username">Username who gave the dislike</param>
        void Dislike(long companyId, long projectId, long requirementId, string username);
        
        /// <summary>
        /// Query requirements by user from all of his projects and companies
        /// </summary>
        /// <param name="username">Username related to the requirements</param>
        /// <returns>List of requirements</returns>
        List<RequirementDto> GetList(string username);

        /// <summary>
        /// Query approved requirements by user from all of his projects and companies
        /// </summary>
        /// <param name="username">Username related to the requirements</param>
        /// <returns>List of requirements</returns>
        List<RequirementDto> GetListApproved(string username);

        /// <summary>
        /// Query rejected requirements by user from all of his projects and companies
        /// </summary>
        /// <param name="username">Username related to the requirements</param>
        /// <returns>List of requirements</returns>
        List<RequirementDto> GetListRejected(string username);

        /// <summary>
        /// Query requirements with pending approval by user from all of his projects and companies
        /// </summary>
        /// <param name="username">Username related to the requirements</param>
        /// <returns>List of requirements</returns>
        List<RequirementDto> GetListPending(string username);

        /// <summary>
        /// Query draft requirements by user from all of his projects and companies
        /// </summary>
        /// <param name="username">Username related to the requirements</param>
        /// <returns>List of requirements</returns>
        List<RequirementDto> GetListDraft(string username);

        /// <summary>
        /// Returns requirement data matching criteria specified by input params
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <returns>Requirement</returns>
        RequirementDto Get(long companyId, long projectId, long requirementId);
        
        /// <summary>
        /// Sets a requirement as approved
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="username">User who approves the requirement</param>
        void Approve(long companyId, long projectId, long requirementId, string username);

        /// <summary>
        /// Sets a requirement as rejected
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="username">User who rejects the requirement</param>
        void Reject(long companyId, long projectId, long requirementId, string username);

        /// <summary>
        /// Updates the title and description for the specified requirement
        /// </summary>
        /// <param name="title">Requirement title</param>
        /// <param name="description">Description title</param>
        /// <param name="newProjectId">Project ID (to move the requirement to another project)</param>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="username">User that triggered the update</param>
        void Update(string title, string description, long newProjectId, long companyId, long projectId, long requirementId, string username);

        /// <summary>
        /// Submits a requirement for its aproval
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="username">User that submitted the requirement</param>
        void SubmitForApproval(long companyId, long projectId, long requirementId, string username);

        /// <summary>
        /// Uploads a file to the DB server as part of the lastest requirement version record
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="fileName">File name</param>
        /// <param name="fileContent">File content in bytes</param>
        /// <param name="username">Modification username</param>
        void UploadAttachment(long companyId, long projectId, long requirementId, 
            string fileName, byte[] fileContent, string username);

        /// <summary>
        /// Uploads a file to the DB server as part of a specific requirement version record
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementVersionId">Requirement version ID</param>
        /// <param name="fileName">File name</param>
        /// <param name="fileContent">File content in bytes</param>
        /// <param name="username">Modification username</param>
        void UploadAttachment(long companyId, long projectId, long requirementId, long requirementVersionId,
            string fileName,
            byte[] fileContent, string username);

        byte[] GetAttachment(long companyId, long projectId, long requirementId, long? requirementVersionId = null);

        /// <summary>
        /// Search for requirements matching the text
        /// </summary>
        /// <param name="text">Search criteria</param>
        /// <returns>List of requirements as search results</returns>
        List<SearchResultDto> SearchRequirement(string text);
    }
}
