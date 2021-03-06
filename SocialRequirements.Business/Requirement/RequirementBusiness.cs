﻿using System;
using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Domain.BusinessLogic.Requirement;
using SocialRequirements.Domain.DTO.General;
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
        private readonly IRequirementVersionData _requirementVersionData;
        private readonly IRequirementHashtagData _requirementHashtagData;

        public RequirementBusiness(IPersonData personData, IRequirementData requirementData,
            IActivityFeedData activityFeedData, IProjectData projectData, IRequirementVersionData requirementVersionData,
            IRequirementHashtagData requirementHashtagData)
        {
            _personData = personData;
            _requirementData = requirementData;
            _activityFeedData = activityFeedData;
            _projectData = projectData;
            _requirementVersionData = requirementVersionData;
            _requirementHashtagData = requirementHashtagData;
        }

        public bool HaveRequirements(long companyId)
        {
            var projects = _projectData.GetProjectsByCompany(companyId);
            var numRequirements = _requirementData.GetNumberOfRequirements(projects);
            return numRequirements > 0;
        }

        public RequirementDto Add(long companyId, long projectId, string title, string description, string[] hashtags,
            int priorityId, string username)
        {
            var personId = _personData.GetPersonId(username);

            // add new requirement
            var requirement = new RequirementDto(companyId, projectId, title, description, hashtags, priorityId,
                personId);
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

        public List<RequirementDto> GetList(string username, int? statusId = null, int? developmentStatusId = null)
        {
            var personId = _personData.GetPersonId(username);
            var projects = _projectData.GetProjectsByUser(personId);
            return _requirementData.GetList(projects.Select(p => p.Id).ToList(), statusId, developmentStatusId);
        }

        public List<RequirementDto> GetListByHashtag(string hashtag, string username)
        {
            var personId = _personData.GetPersonId(username);
            var projects = _projectData.GetProjectsByUser(personId);
            return _requirementData.GetList(projects.Select(p => p.Id).ToList(), hashtag);
        }

        public List<RequirementDto> GetListApproved(string username)
        {
            return GetList(username, (int)GeneralCatalog.Detail.RequirementStatus.Approved);
        }

        public List<RequirementDto> GetListRejected(string username)
        {
            return GetList(username, (int)GeneralCatalog.Detail.RequirementStatus.Rejected);
        }

        public List<RequirementDto> GetListPending(string username)
        {
            return GetList(username, (int)GeneralCatalog.Detail.RequirementStatus.PendingApproval);
        }

        public List<RequirementDto> GetListDraft(string username)
        {
            return GetList(username, (int)GeneralCatalog.Detail.RequirementStatus.Draft);
        }

        public List<RequirementDto> GetListPendingDevelopment(string username)
        {
            return GetList(username, (int) GeneralCatalog.Detail.RequirementStatus.Approved,
                (int) GeneralCatalog.Detail.SoftwareDevelopmentStatus.PendingDevelopment);
        }

        public List<RequirementDto> GetListUnderDevelopment(string username)
        {
            return GetList(username, (int)GeneralCatalog.Detail.RequirementStatus.Approved,
                (int)GeneralCatalog.Detail.SoftwareDevelopmentStatus.UnderDevelopment);
        }

        public List<RequirementDto> GetListUnderTesting(string username)
        {
            return GetList(username, (int)GeneralCatalog.Detail.RequirementStatus.Approved,
                (int)GeneralCatalog.Detail.SoftwareDevelopmentStatus.UnderTesting);
        }

        public List<RequirementDto> GetListDeployed(string username)
        {
            return GetList(username, (int)GeneralCatalog.Detail.RequirementStatus.Approved,
                (int)GeneralCatalog.Detail.SoftwareDevelopmentStatus.Deployed);
        }

        public RequirementDto Get(long companyId, long projectId, long requirementId)
        {
            var requirement = _requirementData.Get(companyId, projectId, requirementId);
            requirement.Hashtags = _requirementHashtagData.Get(requirementId);
            requirement.AttachmentTitle = _requirementVersionData.GetAttachmentTitle(companyId, projectId, requirementId);
            return requirement;
        }

        public void Approve(long companyId, long projectId, long requirementId, string username)
        {
            var personId = _personData.GetPersonId(username);

            _requirementData.UpdateStatus(companyId, projectId, requirementId,
                (int) GeneralCatalog.Detail.RequirementStatus.Approved, personId);

            _requirementData.UpdateDevelopmentStatus(companyId, projectId, requirementId,
                (int)GeneralCatalog.Detail.SoftwareDevelopmentStatus.PendingDevelopment, personId);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.Requirement,
                (int)GeneralCatalog.Detail.EntityActions.Approve, requirementId, DateTime.Now, personId);
        }

        public void SetUnderDevelopment(long companyId, long projectId, long requirementId, string username)
        {
            var personId = _personData.GetPersonId(username);

            _requirementData.UpdateDevelopmentStatus(companyId, projectId, requirementId,
                (int)GeneralCatalog.Detail.SoftwareDevelopmentStatus.UnderDevelopment, personId);
        }

        public void SetUnderTesting(long companyId, long projectId, long requirementId, string username)
        {
            var personId = _personData.GetPersonId(username);

            _requirementData.UpdateDevelopmentStatus(companyId, projectId, requirementId,
                (int)GeneralCatalog.Detail.SoftwareDevelopmentStatus.UnderTesting, personId);
        }

        public void SetDeployed(long companyId, long projectId, long requirementId, string username)
        {
            var personId = _personData.GetPersonId(username);

            _requirementData.UpdateDevelopmentStatus(companyId, projectId, requirementId,
                (int)GeneralCatalog.Detail.SoftwareDevelopmentStatus.Deployed, personId);
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

        public void Update(string title, string description, long newProjectId, long companyId, long projectId,
            long requirementId, int priorityId, string username)
        {
            var personId = _personData.GetPersonId(username);

            _requirementData.Update(title, description, newProjectId, companyId, projectId, requirementId, priorityId, personId);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.Requirement,
                (int)GeneralCatalog.Detail.EntityActions.Modify, requirementId, DateTime.Now, personId);
        }

        public void SubmitForApproval(long companyId, long projectId, long requirementId, string username)
        {
            var personId = _personData.GetPersonId(username);

            _requirementData.UpdateStatus(companyId, projectId, requirementId,
                (int)GeneralCatalog.Detail.RequirementStatus.PendingApproval, personId);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.Requirement,
                (int)GeneralCatalog.Detail.EntityActions.SubmitForApproval, requirementId, DateTime.Now, personId);
        }

        public void UploadAttachment(long companyId, long projectId, long requirementId, string fileName,
            byte[] fileContent, string username)
        {
            var personId = _personData.GetPersonId(username);
            var requirementlastestVersion = _requirementVersionData.Get(companyId, projectId, requirementId);

            _requirementVersionData.UploadAttachment(companyId, projectId, requirementId, requirementlastestVersion.VersionId, fileName,
                fileContent, personId);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.Requirement,
                (int)GeneralCatalog.Detail.EntityActions.UploadAttachment, requirementId, DateTime.Now, personId);
        }

        public void UploadAttachment(long companyId, long projectId, long requirementId, long requirementVersionId, string fileName,
            byte[] fileContent, string username)
        {
            var personId = _personData.GetPersonId(username);

            _requirementVersionData.UploadAttachment(companyId, projectId, requirementId, requirementVersionId, fileName,
                fileContent, personId);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.Requirement,
                (int)GeneralCatalog.Detail.EntityActions.UploadAttachment, requirementId, DateTime.Now, personId);
        }

        public byte[] GetAttachment(long companyId, long projectId, long requirementId, long? requirementVersionId = null)
        {
            return _requirementVersionData.GetAttachment(companyId, projectId, requirementId, requirementVersionId);
        }

        public string GetAttachmentTitle(long companyId, long projectId, long requirementId, long? requirementVersionId = null)
        {
            return _requirementVersionData.GetAttachmentTitle(companyId, projectId, requirementId, requirementVersionId);
        }

        /// <summary>
        /// Search for requirements matching the text
        /// </summary>
        /// <param name="text">Search criteria</param>
        /// <returns>List of requirements</returns>
        public List<SearchResultDto> SearchRequirement(string text)
        {
            return _requirementData.SearchRequirement(text);
        }
    }
}
