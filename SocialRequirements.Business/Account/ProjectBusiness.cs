using System;
using System.Collections.Generic;
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

        public ProjectBusiness(IPersonData personData, IProjectData projectData, IActivityFeedData activityFeedData,
            ICompanyData companyData)
        {
            _personData = personData;
            _projectData = projectData;
            _activityFeedData = activityFeedData;
            _companyData = companyData;
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

        public List<ProjectDto> GetUnrelatedProjects(string username)
        {
            var personId = _personData.GetPersonId(username);
            var companies = _companyData.GetCompaniesByUser(personId);
            return _projectData.GetUnrelatedProjects(companies);
        }
        
        public void AddCompanyRelationship(long companyId, long projectId, string username)
        {
            var personId = _personData.GetPersonId(username);
            var companyProjectId = _projectData.AddCompanyRelationship(companyId, projectId);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.CompanyProject,
                (int)GeneralCatalog.Detail.EntityActions.Create, companyProjectId, DateTime.Now, personId);
        }
    }
}
