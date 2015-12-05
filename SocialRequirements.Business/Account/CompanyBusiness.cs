using System.Collections.Generic;
using SocialRequirements.Domain;
using SocialRequirements.Domain.BusinessLogic.Account;
using SocialRequirements.Domain.DTO;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.Domain.Exception.Account;
using SocialRequirements.Domain.Repository.Account;
using SocialRequirements.Domain.Repository.General;

namespace SocialRequirements.Business.Account
{
    public class CompanyBusiness : ICompanyBusiness
    {
        private readonly IPersonData _personData;
        private readonly ICompanyData _companyData;
        private readonly IGeneralCatalogData _generalCatalogData;

        public CompanyBusiness(IPersonData personData, ICompanyData companyData, IGeneralCatalogData generalCatalogData)
        {
            _personData = personData;
            _companyData = companyData;
            _generalCatalogData = generalCatalogData;
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

        public List<GeneralCatalogDetailDto> GetCompanyTypes()
        {
            return _generalCatalogData.Get((int) GeneralCatalog.Header.CompanyType);
        }

        public void Add(string companyName, int companyType, string username)
        {
            var personId = _personData.GetPersonId(username);
            _companyData.Add(companyName, companyType, personId);
        }
    }
}
