using System;
using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.DTO.General;

namespace SocialRequirements.Domain.Repository.General
{
    public interface IActivityFeedData
    {
        void Add(long companyId, long? projectId, int entityId, int actionId, long recordId, DateTime createdon, long personId);

        /// <summary>
        /// Returns a list of all latest activities performed in the system
        /// under the specified company
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <returns>List of performed activities</returns>
        List<ActivityFeedDto> GetLatestActivity(long companyId);

        /// <summary>
        /// Returns a summary of most recent activities performed in the system
        /// filter by criteria specified by input params
        /// </summary>
        /// <param name="projects">Projects</param>
        /// <returns>List of Activity Feed Items</returns>
        List<ActivityFeedSummaryDto> GetRecentActivitiesSummary(List<ProjectDto> projects);
    }
}
