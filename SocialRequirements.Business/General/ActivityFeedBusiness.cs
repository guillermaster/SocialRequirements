using System.Collections.Generic;
using SocialRequirements.Domain.BusinessLogic.General;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.Domain.Repository.Account;
using SocialRequirements.Domain.Repository.General;

namespace SocialRequirements.Business.General
{
    public class ActivityFeedBusiness : IActivityFeedBusiness
    {
        private readonly IPersonData _personData;
        private readonly IActivityFeedData _activityFeedData;

        public ActivityFeedBusiness(IPersonData personData, IActivityFeedData activityFeedData)
        {
            _personData = personData;
            _activityFeedData = activityFeedData;
        }

        public List<ActivityFeedDto> GetLatestActivity(string username)
        {
            var personId = _personData.GetPersonId(username);
            return _activityFeedData.GetLatestActivity(personId);
        }
    }
}
