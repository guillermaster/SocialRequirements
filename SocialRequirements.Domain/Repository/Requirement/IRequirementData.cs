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
    }
}
