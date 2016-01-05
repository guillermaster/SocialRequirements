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
    public class RequirementQuestionAnswerBusiness : IRequirementQuestionAnswerBusiness
    {
        private readonly IPersonData _personData;
        private readonly IRequirementQuestionAnswerData _requirementQuestionAnswerData;
        private readonly IRequirementQuestionData _requirementQuestionData;
        private readonly IActivityFeedData _activityFeedData;

        public RequirementQuestionAnswerBusiness(IPersonData personData,
            IRequirementQuestionAnswerData requirementQuestionAnswerData, IActivityFeedData activityFeedData,
            IRequirementQuestionData requirementQuestionData)
        {
            _personData = personData;
            _requirementQuestionAnswerData = requirementQuestionAnswerData;
            _activityFeedData = activityFeedData;
            _requirementQuestionData = requirementQuestionData;
        }

        public void Add(long companyId, long projectId, long requirementId, long requirementVersionId, long requirementQuestionId,
            string answer, string username)
        {
            var personId = _personData.GetPersonId(username);

            var answerDto = new RequirementQuestionAnswerDto(companyId, projectId, requirementId, requirementVersionId,
                requirementQuestionId, answer, (int)GeneralCatalog.Detail.RequirementQuestionAnswerStatus.Posted, personId);

            // add answer
            var answerId = _requirementQuestionAnswerData.Add(answerDto);

            // update question, set status as answered
            _requirementQuestionData.UpdateStatus(companyId, projectId, requirementId, requirementVersionId,
                requirementQuestionId, (int) GeneralCatalog.Detail.RequirementQuestionStatus.Answered);

            // add activity feed log
            _activityFeedData.Add(companyId, projectId, (int)GeneralCatalog.Detail.Entity.RequirementQuestionAnswer,
                (int)GeneralCatalog.Detail.EntityActions.Create, answerId, DateTime.Now, personId);
        }

        public List<RequirementQuestionAnswerDto> Get(long companyId, long projectId, long requirementId,
            long requirementVersionId, long requirementQuestionId)
        {
            return _requirementQuestionAnswerData.Get(companyId, projectId, requirementId, requirementVersionId,
                requirementQuestionId);
        }
    }
}
