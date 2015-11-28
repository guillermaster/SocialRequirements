using System.Collections.Generic;
using System.Linq;
using DataContext;
using DataContext.Entities;
using SocialRequirements.Domain.DTO;
using SocialRequirements.Domain.Repository.Account;

namespace SocialRequirements.Data.Account
{
    public class CompanyData : ICompanyData
    {
        private readonly ContextModel _context;

        public CompanyData(ContextModel context)
        {
            _context = context;
        }

        public List<CompanyDto> GetCompaniesByUser(long personId)
        {
            var companies = _context.CompanyPerson.Where(cp => cp.person_id == personId).ToList();
            return companies.Select(GetCompanyDtoFromEntity).ToList();
        }

        private static CompanyDto GetCompanyDtoFromEntity(CompanyPerson companyPersonRel)
        {
            var company = new CompanyDto
            {
                Id = companyPersonRel.company_id,
                Name = companyPersonRel.Company.name,
                TypeId = companyPersonRel.Company.type_id,
                Type = companyPersonRel.Company.GeneralCatalogDetail.name
            };
            return company;
        }
    }
}
