using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Account;

namespace SocialRequirements.Domain.BusinessLogic.Account
{
    public interface IProjectBusiness
    {
        /// <summary>
        /// Gets all the projects related to a company
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <returns>List of projects</returns>
        List<ProjectDto> GetProjectsByCompany(long companyId);

        /// <summary>
        /// Add a new project to the database
        /// </summary>
        /// <param name="name">Project name</param>
        /// <param name="description">Project description</param>
        /// <param name="companyId">Creation Company identifier</param>
        /// <param name="username">Creation username</param>
        void Add(string name, string description, long companyId, string username);

        bool HaveProjects(long companyId);

        /// <summary>
        /// Get list of projects that aren't related to the current user
        /// </summary>
        /// <param name="companyId">Company ID/param>
        /// <returns>List of projects</returns>
        List<ProjectDto> GetUnrelatedProjects(long companyId);

        /// <summary>
        /// Creates a relationship between a company and a project
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <param name="projectId">Project identifier</param>
        /// <param name="username">User ID</param>
        void AddCompanyRelationship(long companyId, long projectId, string username);
    }
}
