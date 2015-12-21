using System;
using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Domain.BusinessLogic.Requirement;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.General;
using SocialRequirements.Domain.Repository.Account;
using SocialRequirements.Domain.Repository.General;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.Business.Requirement
{
    public class RequirementBusiness : IRequirementBusiness
    {
        private readonly IPersonData _personData;
        private readonly IRequirementData _requirementData;
        private readonly IActivityFeedData _activityFeedData;
        private readonly IProjectData _projectData;

        public RequirementBusiness(IPersonData personData, IRequirementData requirementData,
            IActivityFeedData activityFeedData, IProjectData projectData)
        {
            _personData = personData;
            _requirementData = requirementData;
            _activityFeedData = activityFeedData;
            _projectData = projectData;
        }

        public bool HaveRequirements(long companyId)
        {
            var numRequirements = _requirementData.GetNumberOfRequirements(companyId);
            return numRequirements > 0;
        }

        public RequirementDto Add(long companyId, long projectId, string title, string description, string username)
        {
            var personId = _personData.GetPersonId(username);

            // add new requirement
            var requirement = new RequirementDto(companyId, projectId, title, description, personId);
            requirement.Id = _requirementData.Add(requirement);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int) GeneralCatalog.Detail.Entity.Requirement,
                (int) GeneralCatalog.Detail.EntityActions.Create, requirement.Id, DateTime.Now, personId);

            return requirement;
        }

        public void Like(long companyId, long projectId, long requirementId, string username)
        {
            var personId = _personData.GetPersonId(username);

            _requirementData.Like(companyId, projectId, requirementId, personId);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.Requirement,
                (int)GeneralCatalog.Detail.EntityActions.Like, requirementId, DateTime.Now, personId);
        }

        public void Dislike(long companyId, long projectId, long requirementId, string username)
        {
            var personId = _personData.GetPersonId(username);

            _requirementData.Dislike(companyId, projectId, requirementId, personId);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.Requirement,
                (int)GeneralCatalog.Detail.EntityActions.Dislike, requirementId, DateTime.Now, personId);
        }

        public void Comment(long requirementId, string username, string comment)
        {
            var personId = _personData.GetPersonId(username);
            _requirementData.Comment(requirementId, personId, comment);
        }

        public List<RequirementDto> GetList(string username)
        {
            var personId = _personData.GetPersonId(username);
            var projects = _projectData.GetProjectsByUser(personId);
            return _requirementData.GetList(projects.Select(p => p.Id).ToList());
        }

        public RequirementDto Get(long companyId, long projectId, long requirementId)
        {
            return _requirementData.Get(companyId, projectId, requirementId);
        }

        public void Approve(long companyId, long projectId, long requirementId, string username)
        {
            var personId = _personData.GetPersonId(username);

            _requirementData.UpdateStatus(companyId, projectId, requirementId,
                (int) GeneralCatalog.Detail.RequirementStatus.Approved, personId);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.Requirement,
                (int)GeneralCatalog.Detail.EntityActions.Approve, requirementId, DateTime.Now, personId);
        }

        public void Reject(long companyId, long projectId, long requirementId, string username)
        {
            var personId = _personData.GetPersonId(username);

            _requirementData.UpdateStatus(companyId, projectId, requirementId,
                (int)GeneralCatalog.Detail.RequirementStatus.Rejected, personId);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.Requirement,
                (int)GeneralCatalog.Detail.EntityActions.Reject, requirementId, DateTime.Now, personId);
        }

        public void Update(string title, string description, long companyId, long projectId, long requirementId, string username)
        {
            var personId = _personData.GetPersonId(username);

            _requirementData.Update(title, description, companyId, projectId, requirementId, personId);
        }
    }
}
