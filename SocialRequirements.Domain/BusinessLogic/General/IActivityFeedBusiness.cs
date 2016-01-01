using System.Collections.Generic;
using SocialRequirements.Domain.DTO.General;

namespace SocialRequirements.Domain.BusinessLogic.General
{
    public interface IActivityFeedBusiness
    {
        /// <summary>
        /// Returns a list of all latest activities performed in the system
        /// under the specified user
        /// </summary>
        /// <param name="username">system username</param>
        /// <returns>List of performed activities</returns>
        List<ActivityFeedDto> GetLatestActivity(string username);

        /// <summary>
        /// Returns a summary of most recent activities performed in the system
        /// filter by the specified user
        /// </summary>
        /// <returns>List of Activity Feed Items</returns>
        List<ActivityFeedSummaryDto> GetRecentActivitiesSummary(string username);
    }
}
