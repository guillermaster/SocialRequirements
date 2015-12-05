using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO;
using SocialRequirements.Domain.DTO.Account;
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
            return companies.Select(GetCompanyDto).ToList();
        }

        public void Add(string companyName, int companyType, long personId)
        {
            using (var scope = _context.Database.BeginTransaction())
            {
                try
                {
                    // add new company
                    var companyId = AddCompany(companyName, companyType);
                    // relate company to user
                    AddPersonRelationship(companyId, personId);

                    scope.Commit();
                }
                catch
                {
                    scope.Rollback();
                }
            }
        }

        private long AddCompany(string companyName, int companyType)
        {
            var company = new Company
            {
                name = companyName,
                type_id = companyType
            };
            company = _context.Company.Add(company);
            _context.SaveChanges();
            return company.id;
        }

        private void AddPersonRelationship(long companyId, long personId)
        {
            var companyPerson = new CompanyPerson
            {
                company_id = companyId,
                person_id = personId
            };
            _context.CompanyPerson.Add(companyPerson);
            _context.SaveChanges();
        }

        private static CompanyDto GetCompanyDto(CompanyPerson companyPersonRel)
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
