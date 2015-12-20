using System.Collections.Generic;
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
        /// Adds a comment to the specified requirement
        /// </summary>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="username">Username who commented</param>
        /// <param name="comment">The comment</param>
        void Comment(long requirementId, string username, string comment);

        /// <summary>
        /// Query requirements by user from all of his projects and companies
        /// </summary>
        /// <param name="username">Username related to the requirements</param>
        /// <returns>List of requirements</returns>
        List<RequirementDto> GetList(string username);

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
    }
}
