using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Services;
using Ninject;
using Ninject.Web;
using SocialRequirements.Domain.BusinessLogic.Account;
using SocialRequirements.Domain.BusinessLogic.Requirement;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.Utilities;
using SocialRequirements.Utilities.Security;

namespace WebService
{
    /// <summary>
    /// Summary description for Company
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Company : WebServiceBase
    {
        [Inject]
        public ICompanyBusiness CompanyBusiness { get; set; }
        [Inject]
        public IRequirementBusiness RequirementBusiness { get; set; }
        [Inject]
        public IProjectBusiness ProjectBusiness { get; set; }

        [WebMethod(CacheDuration = 0)]
        public string GetCompanyTypes()
        {
            var types = CompanyBusiness.GetCompanyTypes();
            var serializer = new ObjectSerializer<List<GeneralCatalogDetailDto>>(types);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public void AddCompany(string name, int type, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            CompanyBusiness.Add(name, type, username);
        }

        [WebMethod(CacheDuration = 0)]
        public bool HaveRequirements(long companyId)
        {
            return RequirementBusiness.HaveRequirements(companyId);
        }

        [WebMethod(CacheDuration = 0)]
        public bool HaveProjects(long companyId)
        {
            return ProjectBusiness.HaveProjects(companyId);
        }

        [WebMethod(CacheDuration = 0)]
        public string GetAllCompanies()
        {
            var companies = CompanyBusiness.GetAll();
            var serializer = new ObjectSerializer<List<CompanyDto>>(companies);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public string GetRelatedCompanies(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var companies = CompanyBusiness.GetCompaniesByUser(username);
            var serializer = new ObjectSerializer<List<CompanyDto>>(companies);
            return serializer.ToXmlString();
        }
    }
}

