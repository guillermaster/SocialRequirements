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
    public class RequirementModificationBusiness : IRequirementModificationBusiness
    {
        private readonly IPersonData _personData;
        private readonly IRequirementModificationData _requirementModifData;
        private readonly IActivityFeedData _activityFeedData;
        private readonly IProjectData _projectData;

        public RequirementModificationBusiness(IPersonData personData, IRequirementModificationData requirementModifData,
            IActivityFeedData activityFeedData, IProjectData projectData)
        {
            _personData = personData;
            _requirementModifData = requirementModifData;
            _activityFeedData = activityFeedData;
            _projectData = projectData;
        }
        
        public RequirementModificationDto Add(long companyId, long projectId, long requirementId, string title, string description,
            string username)
        {
            var personId = _personData.GetPersonId(username);

            // add new requirement modification request
            var requirementModif = new RequirementModificationDto(companyId, projectId, requirementId, title, description, personId);
            requirementModif.Id = _requirementModifData.Add(requirementModif);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.RequirementModification,
                (int)GeneralCatalog.Detail.EntityActions.Create, requirementModif.Id, DateTime.Now, personId, requirementModif.RequirementId);

            return requirementModif;
        }

        public RequirementModificationDto Get(long companyId, long projectId, long requirementId, long requirementModificationId)
        {
            return _requirementModifData.Get(companyId, projectId, requirementId, requirementModificationId);
        }

        public RequirementModificationDto Get(long companyId, long projectId, long requirementId)
        {
            return _requirementModifData.Get(companyId, projectId, requirementId);
        }

        public void SubmitForApproval(long companyId, long projectId, long requirementId, long requirementModificationId, string username)
        {
            var personId = _personData.GetPersonId(username);

            _requirementModifData.UpdateStatus(companyId, projectId, requirementId, requirementModificationId,
                (int)GeneralCatalog.Detail.RequirementStatus.PendingApproval, personId);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.RequirementModification,
                (int)GeneralCatalog.Detail.EntityActions.SubmitForApproval, requirementModificationId, DateTime.Now, personId, requirementId);
        }

        public void Update(string title, string description, long companyId, long projectId, long requirementId,
            long requirementModificationId, string username)
        {
            var personId = _personData.GetPersonId(username);

            _requirementModifData.Update(title, description, companyId, projectId, requirementId,
                requirementModificationId, personId);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.RequirementModification,
                (int)GeneralCatalog.Detail.EntityActions.Modify, requirementModificationId, DateTime.Now, personId, requirementId);
        }

        public void Approve(long companyId, long projectId, long requirementId, long requirementModificationId, string username)
        {
            var personId = _personData.GetPersonId(username);

            _requirementModifData.Approve(companyId, projectId, requirementId, requirementModificationId, personId);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int) GeneralCatalog.Detail.Entity.RequirementModification,
                (int) GeneralCatalog.Detail.EntityActions.Approve, requirementModificationId, DateTime.Now, personId,
                requirementId);
        }

        public void Reject(long companyId, long projectId, long requirementId, long requirementModificationId, string username)
        {
            var personId = _personData.GetPersonId(username);

            _requirementModifData.UpdateStatus(companyId, projectId, requirementId, requirementModificationId,
                (int)GeneralCatalog.Detail.RequirementStatus.Rejected, personId);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int) GeneralCatalog.Detail.Entity.RequirementModification,
                (int) GeneralCatalog.Detail.EntityActions.Reject, requirementModificationId, DateTime.Now, personId,
                requirementId);
        }

        public void Like(long companyId, long projectId, long requirementId, long requirementModificationId, string username)
        {
            var personId = _personData.GetPersonId(username);

            _requirementModifData.Like(companyId, projectId, requirementId, requirementModificationId, personId);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int) GeneralCatalog.Detail.Entity.RequirementModification,
                (int) GeneralCatalog.Detail.EntityActions.Like, requirementModificationId, DateTime.Now, personId,
                requirementId);
        }

        public void Dislike(long companyId, long projectId, long requirementId, long requirementModificationId, string username)
        {
            var personId = _personData.GetPersonId(username);

            _requirementModifData.Dislike(companyId, projectId, requirementId, requirementModificationId, personId);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int) GeneralCatalog.Detail.Entity.RequirementModification,
                (int) GeneralCatalog.Detail.EntityActions.Dislike, requirementModificationId, DateTime.Now, personId,
                requirementId);
        }

        public List<RequirementModificationDto> GetList(string username)
        {
            var personId = _personData.GetPersonId(username);
            var projects = _projectData.GetProjectsByUser(personId);
            return _requirementModifData.GetList(projects.Select(p => p.Id).ToList());
        }

        public List<RequirementModificationDto> GetListApproved(string username)
        {
            var allRequirementsModif = GetList(username);

            return
                allRequirementsModif.Where(
                    requirementModif => requirementModif.StatusId == (int)GeneralCatalog.Detail.RequirementStatus.Approved)
                    .ToList();
        }

        public List<RequirementModificationDto> GetListRejected(string username)
        {
            var allRequirementsModif = GetList(username);

            return
                allRequirementsModif.Where(
                    requirementModif => requirementModif.StatusId == (int)GeneralCatalog.Detail.RequirementStatus.Rejected)
                    .ToList();
        }

        public List<RequirementModificationDto> GetListPending(string username)
        {
            var allRequirementsModif = GetList(username);

            return
                allRequirementsModif.Where(
                    requirementModif => requirementModif.StatusId == (int)GeneralCatalog.Detail.RequirementStatus.PendingApproval)
                    .ToList();
        }

        public List<RequirementModificationDto> GetListDraft(string username)
        {
            var allRequirementsModif = GetList(username);

            return
                allRequirementsModif.Where(
                    requirementModif => requirementModif.StatusId == (int)GeneralCatalog.Detail.RequirementStatus.Draft)
                    .ToList();
        }
    }
}
