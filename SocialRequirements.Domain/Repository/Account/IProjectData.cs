using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Account;

namespace SocialRequirements.Domain.Repository.Account
{
    public interface IProjectData
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
        /// <param name="personId">Creation user identifier</param>
        long Add(string name, string description, long companyId, long personId);

        /// <summary>
        /// Gets the number of projects related to a specified company
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <returns>Number of projects</returns>
        int GetNumberOfProjects(long companyId);

        /// <summary>
        /// Gets a list of projects related to the companies where the a user belongs to
        /// </summary>
        /// <param name="personId">User Id</param>
        /// <returns>List of projects</returns>
        List<ProjectDto> GetProjectsByUser(long personId);

        /// <summary>
        /// Gets a list of projects that aren't related to the specified companies
        /// </summary>
        /// <param name="companies">Companies</param>
        /// <returns>List of projects</returns>
        List<ProjectDto> GetUnrelatedProjects(List<CompanyDto> companies);

        /// <summary>
        /// Creates a relationship between a company and a project
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <param name="projectId">Project identifier</param>
        long AddCompanyRelationship(long companyId, long projectId);
    }
}
