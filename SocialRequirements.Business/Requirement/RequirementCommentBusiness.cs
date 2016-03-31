using System;
using System.Collections.Generic;
using SocialRequirements.Domain.BusinessLogic.Requirement;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.General;
using SocialRequirements.Domain.Repository.Account;
using SocialRequirements.Domain.Repository.General;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.Business.Requirement
{
    public class RequirementCommentBusiness : IRequirementCommentBusiness
    {
        private readonly IRequirementCommentData _requirementCommentData;
        private readonly IRequirementVersionData _requirementVersionData;
        private readonly IPersonData _personData;
        private readonly IActivityFeedData _activityFeedData;

        public RequirementCommentBusiness(IRequirementCommentData requirementCommentData, IPersonData personData,
            IRequirementVersionData requirementVersionData, IActivityFeedData activityFeedData)
        {
            _requirementCommentData = requirementCommentData;
            _personData = personData;
            _requirementVersionData = requirementVersionData;
            _activityFeedData = activityFeedData;
        }

        public void Add(long companyId, long projectId, long requirementId, string comment, string username)
        {
            var personId = _personData.GetPersonId(username);
            var requirementLatestVersion = _requirementVersionData.Get(companyId, projectId, requirementId);
            var requirementComment = new RequirementCommentDto(requirementLatestVersion.CompanyId,
                requirementLatestVersion.ProjectId, requirementLatestVersion.Id, requirementLatestVersion.VersionId,
                personId, comment);

            _requirementCommentData.Add(requirementComment);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.RequirementComment,
                (int)GeneralCatalog.Detail.EntityActions.Create, requirementId, DateTime.Now, personId);
        }

        public List<RequirementCommentDto> Get(long requirementId, long companyId, long projectId)
        {
            var requirementLatestVersion = _requirementVersionData.Get(companyId, projectId, requirementId);
            return _requirementCommentData.Get(requirementId, companyId, projectId, requirementLatestVersion.VersionId);
        }
    }
}
