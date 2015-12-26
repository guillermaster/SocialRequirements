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
    public class RequirementQuestionBusiness : IRequirementQuestionBusiness
    {
        private readonly IPersonData _personData;
        private readonly IRequirementVersionData _requirementVersionData;
        private readonly IActivityFeedData _activityFeedData;
        private readonly IRequirementQuestionData _questionData;

        public RequirementQuestionBusiness(IPersonData personData, IRequirementVersionData requirementVersionData,
            IActivityFeedData activityFeedData, IRequirementQuestionData questionData)
        {
            _personData = personData;
            _requirementVersionData = requirementVersionData;
            _activityFeedData = activityFeedData;
            _questionData = questionData;
        }

        public void Add(long companyId, long projectId, long requirementId, string question, string username)
        {
            var personId = _personData.GetPersonId(username);

            // get requiremen latest version
            var requirementLatestVersion = _requirementVersionData.Get(companyId, projectId, requirementId);

            // create question
            var questionDto = new RequirementQuestionDto(requirementLatestVersion.CompanyId,
                requirementLatestVersion.ProjectId, requirementLatestVersion.Id, requirementLatestVersion.VersionId,
                question, (int)GeneralCatalog.Detail.RequirementQuestionStatus.Posted, personId);
            var questionId = _questionData.Add(questionDto);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.RequirementQuestion,
                (int)GeneralCatalog.Detail.EntityActions.Create, questionId, DateTime.Now, personId);
        }

        public RequirementQuestionDto Get(long companyId, long projectId, long requirementId, long requirementVersionId,
            long requirementQuestionId)
        {
            return _questionData.Get(companyId, projectId, requirementId, requirementVersionId,
                requirementQuestionId);
        }

        public List<RequirementQuestionDto> Get(long companyId, long projectId, long requirementId, long requirementVersionId)
        {
            return _questionData.Get(companyId, projectId, requirementId, requirementVersionId);
        }

        public List<RequirementQuestionDto> GetAll(long companyId)
        {
            return _questionData.GetAll(companyId);
        }
    }
}
