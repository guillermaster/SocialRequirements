using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Account;

namespace SocialRequirements.Domain.Repository.Account
{
    public interface ICompanyData
    {
        List<CompanyDto> GetCompaniesByUser(long personId);

        void Add(string companyName, int companyType, long personId);

        /// <summary>
        /// Creates a relationship between a user and a company
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="personId">User ID</param>
        void AddPersonRelationship(long companyId, long personId);

        /// <summary>
        /// Gets a list of all companies
        /// </summary>
        /// <returns>List of companies</returns>
        List<CompanyDto> GetAll();
    }
}
