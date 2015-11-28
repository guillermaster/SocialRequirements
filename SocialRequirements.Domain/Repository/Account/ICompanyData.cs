using System.Collections.Generic;
using SocialRequirements.Domain.DTO;

namespace SocialRequirements.Domain.Repository.Account
{
    public interface ICompanyData
    {
        List<CompanyDto> GetCompaniesByUser(long personId);
    }
}
