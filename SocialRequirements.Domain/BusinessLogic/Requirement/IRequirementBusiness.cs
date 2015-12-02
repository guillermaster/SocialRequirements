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
    }
}
