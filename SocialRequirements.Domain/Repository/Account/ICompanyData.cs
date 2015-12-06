using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Account;

namespace SocialRequirements.Domain.Repository.Account
{
    public interface ICompanyData
    {
        List<CompanyDto> GetCompaniesByUser(long personId);

        void Add(string companyName, int companyType, long personId);
    }
}
