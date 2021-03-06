﻿using System;
using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.DTO.General;

namespace SocialRequirements.Domain.Repository.General
{
    public interface IActivityFeedData
    {
        void Add(long companyId, long? projectId, int entityId, int actionId, long recordId, DateTime createdon,
            long personId, long? parentId = null, long? grandparentId = null);

        /// <summary>
        /// Returns a list of all latest activities performed in the system
        /// under the specified project
        /// </summary>
        /// <param name="projectId">Project identifier</param>
        /// <returns>List of performed activities</returns>
        List<ActivityFeedDto> GetLatestActivity(long projectId);

        /// <summary>
        /// Returns a summary of most recent activities performed in the system
        /// filter by criteria specified by input params
        /// </summary>
        /// <param name="projects">Projects</param>
        /// <param name="daysTimestamp">Number of days back</param>
        /// <returns>List of Activity Feed Items</returns>
        List<ActivityFeedSummaryDto> GetRecentActivitiesSummary(List<ProjectDto> projects, int daysTimestamp);
        
        /// <summary>
        /// Get recent activites filtered up by criteria specified by input params
        /// </summary>
        /// <param name="projectId">Project ID</param>
        /// <param name="entityId">Entity ID</param>
        /// <param name="actionId">Action ID</param>
        /// <param name="daysTimestamp">Number of days back</param>
        /// <returns>List of activities</returns>
        List<ActivityFeedDto> GetRecentActivities(long projectId, int entityId, int actionId, int daysTimestamp);

        /// <summary>
        /// Get recent activites filtered up by criteria specified by input params
        /// </summary>
        /// <param name="projects">Projects which recent activities are to be shown</param>
        /// <param name="daysTimestamp">Number of days back</param>
        /// <returns>List of activities</returns>
        List<ActivityFeedDto> GetRecentActivities(List<ProjectDto> projects, int daysTimestamp);
    }
}
