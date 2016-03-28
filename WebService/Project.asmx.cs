using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        [WebMethod(CacheDuration = 0)]
        public void AddProject(string name, string description, long companyId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            ProjectBusiness.Add(name, description, companyId, username);
        }

        [WebMethod(CacheDuration = 0)]
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

        [WebMethod(CacheDuration = 0)]
        public string GetByCompany(long companyId)
        {
            var projects = ProjectBusiness.GetProjectsByCompany(companyId);
            var serializer = new ObjectSerializer<List<ProjectDto>>(projects);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public string GetUnrelatedProjects(long companyId)
        {
            var projects = ProjectBusiness.GetUnrelatedProjects(companyId);
            var serializer = new ObjectSerializer<List<ProjectDto>>(projects);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public void SetProject(long projectId, long companyId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            ProjectBusiness.AddCompanyRelationship(companyId, projectId, username);
        }

        [WebMethod(CacheDuration = 320)]
        public string GetUsers(List<long> projectsIds)
        {
            var usersDict = new Dictionary<long, PersonDto>();
            foreach (var projectId in projectsIds)
            {
                var users = ProjectBusiness.GetUsers(projectId);
                foreach (var user in users.Where(user => !usersDict.ContainsKey(user.Id)))
                {
                    usersDict.Add(user.Id, user);
                }
            }

            var userslist = new List<PersonDto>();
            userslist.AddRange(usersDict.Values);
            userslist = userslist.OrderBy(u => u.FullName).ToList();

            var serializer = new ObjectSerializer<List<PersonDto>>(userslist);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 120)]
        public string GetUsersByPermission(List<long> projectsIds, int permissionId)
        {
            var usersDict = new Dictionary<long, PersonDto>();
            foreach (var projectId in projectsIds)
            {
                var users = ProjectBusiness.GetUsers(projectId, permissionId);
                foreach (var user in users.Where(user => !usersDict.ContainsKey(user.Id)))
                {
                    usersDict.Add(user.Id, user);
                }
            }

            var userslist = new List<PersonDto>();
            userslist.AddRange(usersDict.Values);
            userslist = userslist.OrderBy(u => u.FullName).ToList();

            var serializer = new ObjectSerializer<List<PersonDto>>(userslist);
            return serializer.ToXmlString();
        }
    }
}
