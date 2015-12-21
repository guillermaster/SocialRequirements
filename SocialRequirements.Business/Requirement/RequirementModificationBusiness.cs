﻿using System;
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

        public RequirementModificationBusiness(IPersonData personData, IRequirementModificationData requirementModifData,
            IActivityFeedData activityFeedData)
        {
            _personData = personData;
            _requirementModifData = requirementModifData;
            _activityFeedData = activityFeedData;
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
                (int)GeneralCatalog.Detail.EntityActions.Create, requirementModif.Id, DateTime.Now, personId);

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
    }
}