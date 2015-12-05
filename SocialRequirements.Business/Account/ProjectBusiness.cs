using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialRequirements.Domain.BusinessLogic.Account;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.Exception.Account;
using SocialRequirements.Domain.Repository.Account;

namespace SocialRequirements.Business.Account
{
    public class ProjectBusiness : IProjectBusiness
    {
        private readonly IPersonData _personData;
        private readonly IProjectData _projectData;

        public ProjectBusiness(IPersonData personData, IProjectData projectData)
        {
            _personData = personData;
            _projectData = projectData;
        }
        public List<ProjectDto> GetProjectsByCompany(long companyId)
        {
            return _projectData.GetProjectsByCompany(companyId);
        }

        public void Add(string name, string description, long companyId, string username)
        {
            var personId = _personData.GetPersonId(username);
            _projectData.Add(name, description, companyId, personId);
        }
    }
}
