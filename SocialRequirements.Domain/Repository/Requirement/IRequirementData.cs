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
    }
}
