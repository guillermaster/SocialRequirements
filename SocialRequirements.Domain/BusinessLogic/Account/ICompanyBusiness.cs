using System.Collections.Generic;
using SocialRequirements.Domain.DTO;

namespace SocialRequirements.Domain.BusinessLogic.Account
{
    public interface ICompanyBusiness
    {
        List<CompanyDto> GetCompaniesByUser(string username);
    }
}
