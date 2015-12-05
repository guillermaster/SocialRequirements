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
    }
}
