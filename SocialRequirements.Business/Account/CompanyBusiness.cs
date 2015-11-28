using System.Collections.Generic;
using SocialRequirements.Domain.BusinessLogic.Account;
using SocialRequirements.Domain.DTO;
using SocialRequirements.Domain.Exception.Account;
using SocialRequirements.Domain.Repository.Account;

namespace SocialRequirements.Business.Account
{
    public class CompanyBusiness : ICompanyBusiness
    {
        private readonly IPersonData _personData;
        private readonly ICompanyData _companyData;

        public CompanyBusiness(IPersonData personData, ICompanyData companyData)
        {
            _personData = personData;
            _companyData = companyData;
        }

        public List<CompanyDto> GetCompaniesByUser(string username)
        {
            try
            {
                var personId = _personData.GetPersonId(username);
                var companies = _companyData.GetCompaniesByUser(personId);
                return companies;
            }
            catch (AccountException.UserNotFound)
            {
                return new List<CompanyDto> {new CompanyDto {Error = "User not found"}};
            }
        }
    }
}
