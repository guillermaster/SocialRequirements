using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.Repository.Requirement
{
    public interface IRequirementData
    {
        /// <summary>
        /// Returns the number of requirements related to a specified company
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <returns>Number of requirements</returns>
        int GetNumberOfRequirements(long companyId);

        /// <summary>
        /// Adds a new requirement to the database
        /// </summary>
        /// <param name="requirement">DTO class instance with data to be stored</param>
        /// <returns>Requirement identifier</returns>
        long Add(RequirementDto requirement);

        /// <summary>
        /// Returns requirement data matching criteria specified by input params
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <returns>Requirement</returns>
        RequirementDto Get(long companyId, long projectId, long requirementId);

        /// <summary>
        /// Adds a like to the specified requirement
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="personId">ID of the user who gave the like</param>
        void Like(long companyId, long projectId, long requirementId, long personId);

        /// <summary>
        /// Adds a like to the specified requirement
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="personId">ID of the user who gave the dislike</param>
        void Dislike(long companyId, long projectId, long requirementId, long personId);

        /// <summary>
        /// Adds a comment to the specified requirement
        /// </summary>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="personId">ID of the who commented</param>
        /// <param name="commentary">The comment</param>
        void Comment(long requirementId, long personId, string commentary);

        /// <summary>
        /// Query requirements by projects
        /// </summary>
        /// <param name="projectIds">List of project identifiers </param>
        /// <returns>List of requirements</returns>
        List<RequirementDto> GetList(List<long> projectIds);

        /// <summary>
        /// Updates the status of a requirement. Tf the status is approved, the approval user is set.
        /// If the status is rejected, the rejection user is set. 
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="statusId">Status ID to be set</param>
        /// <param name="personId">Person ID, to be set as modification user and may be the approval/rejection user.</param>
        void UpdateStatus(long companyId, long projectId, long requirementId, int statusId, long personId);
    }
}
