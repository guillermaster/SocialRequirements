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
    }
}
