using System;
using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Domain.BusinessLogic.Account;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.General;
using SocialRequirements.Domain.Repository.Account;
using SocialRequirements.Domain.Repository.General;

namespace SocialRequirements.Business.Account
{
    public class ProjectBusiness : IProjectBusiness
    {
        private readonly IPersonData _personData;
        private readonly IProjectData _projectData;
        private readonly IActivityFeedData _activityFeedData;
        private readonly ICompanyData _companyData;
        private readonly IRoleData _roleData;

        public ProjectBusiness(IPersonData personData, IProjectData projectData, IActivityFeedData activityFeedData,
            ICompanyData companyData, IRoleData roleData)
        {
            _personData = personData;
            _projectData = projectData;
            _activityFeedData = activityFeedData;
            _companyData = companyData;
            _roleData = roleData;
        }
        public List<ProjectDto> GetProjectsByCompany(long companyId)
        {
            return _projectData.GetProjectsByCompany(companyId);
        }

        public bool HaveProjects(long companyId)
        {
            var numProjects = _projectData.GetNumberOfProjects(companyId);
            return numProjects > 0;
        }

        public void Add(string name, string description, long companyId, string username)
        {
            var personId = _personData.GetPersonId(username);
            var projectId = _projectData.Add(name, description, companyId, personId);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.Project,
                (int)GeneralCatalog.Detail.EntityActions.Create, projectId, DateTime.Now, personId);
        }

        public List<ProjectDto> GetUnrelatedProjects(long companyId)
        {
            return _projectData.GetUnrelatedProjects(companyId);
        }
        
        public void AddCompanyRelationship(long companyId, long projectId, string username)
        {
            var personId = _personData.GetPersonId(username);
            var companyProjectId = _projectData.AddCompanyRelationship(companyId, projectId);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.CompanyProject,
                (int)GeneralCatalog.Detail.EntityActions.Create, companyProjectId, DateTime.Now, personId);
        }

        public List<PersonDto> GetUsers(long projectId)
        {
            var companies = _companyData.GetCompaniesByProject(projectId);
            var users = new List<PersonDto>();
            foreach (var company in companies)
            {
                users.AddRange(_personData.GetUsersByCompany(company.Id));
            }
            return users;
        }

        public List<PersonDto> GetUsers(long projectId, int permissionId)
        {
            var roles = _roleData.GetRolesByPermission(permissionId);
            var usersDict = new Dictionary<long, PersonDto>();

            foreach (var role in roles)
            {
                var users = _personData.GetUsersByProjectRole(projectId, role.Id);
                foreach (var user in users.Where(user => !usersDict.ContainsKey(user.Id)))
                {
                    usersDict.Add(user.Id, user);
                }
            }

            var userslist = new List<PersonDto>();
            userslist.AddRange(usersDict.Values);

            return userslist;
        }
    }
}
