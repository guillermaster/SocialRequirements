using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.Repository.Requirement
{
    public interface IRequirementModificationData
    {
        /// <summary>
        /// Adds a new requirement modification request to the database
        /// </summary>
        /// <param name="requirement">DTO class instance with data to be stored</param>
        /// <returns>Requirement modification request ID</returns>
        long Add(RequirementModificationDto requirement);

        /// <summary>
        /// Returns requirement modification request data matching criteria specified by input params
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementModificationId">Requirement modification ID</param>
        /// <returns>Requirement</returns>
        RequirementModificationDto Get(long companyId, long requirementId, long requirementModificationId);

        /// <summary>
        /// Returns the current active requirement modification request
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <returns>Current active requirement modification request, null when no active modifiaction request is found</returns>
        RequirementModificationDto Get(long companyId, long requirementId);

        /// <summary>
        /// Query requirements modifications by projects
        /// </summary>
        /// <param name="projectIds">List of project identifiers </param>
        /// <returns>List of requirements modification</returns>
        List<RequirementModificationDto> GetList(List<long> projectIds);

        /// <summary>
        /// Updates the status of a requirement modification request. Tf the status is approved, the approval user is set.
        /// If the status is rejected, the rejection user is set. 
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementModificationId">Requirement modification ID</param>
        /// <param name="statusId">Status ID to be set</param>
        /// <param name="personId">Person ID, to be set as modification user and may be the approval/rejection user.</param>
        void UpdateStatus(long companyId, long projectId, long requirementId, long requirementModificationId, int statusId, long personId);

        /// <summary>
        /// Updates the title and description for the specified requirement modification request
        /// </summary>
        /// <param name="title">Requirement title</param>
        /// <param name="description">Description title</param>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementModificationId">Requirement modification ID</param>
        /// <param name="personId">User that triggered the update</param>
        void Update(string title, string description, long companyId, long projectId, long requirementId, long requirementModificationId, long personId);

        /// <summary>
        /// Approves a requirement modification
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementModificationId">Requirement modification ID</param>
        /// <param name="personId">User ID that approved the requirement modification request</param>
        void Approve(long companyId, long projectId, long requirementId, long requirementModificationId, long personId);

        /// <summary>
        /// Adds a like to the specified requirement
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementModificationId">Requirement Modification ID</param>
        /// <param name="personId">ID of the user who gave the like</param>
        void Like(long companyId, long projectId, long requirementId, long requirementModificationId, long personId);

        /// <summary>
        /// Adds a like to the specified requirement modification request
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementModificationId">Requirement Modification ID</param>
        /// <param name="personId">ID of the user who gave the dislike</param>
        void Dislike(long companyId, long projectId, long requirementId, long requirementModificationId, long personId);
    }
}
