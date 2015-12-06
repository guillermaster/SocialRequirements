using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Services;
using Ninject;
using Ninject.Web;
using SocialRequirements.Domain.BusinessLogic.Account;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Utilities;
using SocialRequirements.Utilities.Security;

namespace WebService
{
    /// <summary>
    /// Summary description for Project
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Project : WebServiceBase
    {
        [Inject]
        public IProjectBusiness ProjectBusiness { get; set; }

        [WebMethod]
        public void AddProject(string name, string description, long companyId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            ProjectBusiness.Add(name, description, companyId, username);
        }

        [WebMethod]
        public string GetByCompanies(List<long> companiesIdList)
        {
            var projects = new List<ProjectDto>();
            foreach (var companyId in companiesIdList)
            {
                projects.AddRange(ProjectBusiness.GetProjectsByCompany(companyId));
            }
            var serializer = new ObjectSerializer<List<ProjectDto>>(projects);
            return serializer.ToXmlString();
        }

        [WebMethod]
        public string GetByCompany(long companyId)
        {
            var projects = ProjectBusiness.GetProjectsByCompany(companyId);
            var serializer = new ObjectSerializer<List<ProjectDto>>(projects);
            return serializer.ToXmlString();
        }
    }
}
