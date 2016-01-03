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
    public class RequirementModificationCommentBusiness : IRequirementModificationCommentBusiness
    {
        private readonly IRequirementModificationCommentData _requirementModifCommentData;
        private readonly IRequirementModificationVersionData _requirementModifVersionData;
        private readonly IPersonData _personData;
        private readonly IActivityFeedData _activityFeedData;

        public RequirementModificationCommentBusiness(IRequirementModificationCommentData requirementModifCommentData, IPersonData personData,
            IRequirementModificationVersionData requirementModifVersionData, IActivityFeedData activityFeedData)
        {
            _requirementModifCommentData = requirementModifCommentData;
            _personData = personData;
            _requirementModifVersionData = requirementModifVersionData;
            _activityFeedData = activityFeedData;
        }

        public void Add(long companyId, long projectId, long requirementId, long requirementModificationId, string comment,
            string username)
        {
            var personId = _personData.GetPersonId(username);

            // get the latest version of the requirement modification request
            var requirementModifLatestVersion = _requirementModifVersionData.Get(companyId, projectId, requirementId, requirementModificationId);

            var requirementModifComment = new RequirementModificationCommentDto(requirementModifLatestVersion.CompanyId,
                requirementModifLatestVersion.ProjectId, requirementModifLatestVersion.RequirementId,
                requirementModifLatestVersion.Id, requirementModifLatestVersion.VersionId, personId, comment);

            // add the comment
            var requirementModifCommentId = _requirementModifCommentData.Add(requirementModifComment);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.RequirementModificationComment,
                (int)GeneralCatalog.Detail.EntityActions.Create, requirementModifCommentId, DateTime.Now, personId);
        }

        public List<RequirementModificationCommentDto> Get(long companyId, long projectId, long requirementId, long requirementModificationId)
        {
            var requirementModifLatestVersion = _requirementModifVersionData.Get(companyId, projectId, requirementId, requirementModificationId);
            return _requirementModifCommentData.Get(companyId, projectId, requirementId, requirementModificationId, requirementModifLatestVersion.VersionId);
        }
    }
}
