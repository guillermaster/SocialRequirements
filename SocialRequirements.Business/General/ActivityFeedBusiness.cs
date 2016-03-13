using System;
using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Domain.BusinessLogic.Account;
using SocialRequirements.Domain.BusinessLogic.General;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.Domain.Repository.Account;
using SocialRequirements.Domain.Repository.General;

namespace SocialRequirements.Business.General
{
    public class ActivityFeedBusiness : IActivityFeedBusiness
    {
        private readonly IActivityFeedData _activityFeedData;
        private readonly ICompanyBusiness _companyBusiness;
        private readonly IProjectData _projectData;
        private readonly IPersonData _personData;
        private const int DaysTimeSpan = 4;

        public ActivityFeedBusiness(IActivityFeedData activityFeedData, ICompanyBusiness companyBusiness,
            IProjectData projectData, IPersonData personData)
        {
            _companyBusiness = companyBusiness;
            _activityFeedData = activityFeedData;
            _projectData = projectData;
            _personData = personData;
        }

        public List<ActivityFeedDto> GetLatestActivity(string username)
        {
            var activityFeed = new List<ActivityFeedDto>();

            var personId = _personData.GetPersonId(username);
            var projects = _projectData.GetProjectsByUser(personId);

            foreach (var project in projects)
            {
                activityFeed.AddRange(_activityFeedData.GetLatestActivity(project.Id));
            }
            return activityFeed.OrderByDescending(activity => activity.Createdon).ToList();
        }

        public List<ActivityFeedSummaryDto> GetRecentActivitiesSummary(string username)
        {
            var personId = _personData.GetPersonId(username);
            var projects = _projectData.GetProjectsByUser(personId);

            return
                _activityFeedData.GetRecentActivitiesSummary(projects, DaysTimeSpan)
                    .OrderByDescending(act => act.MostRecent)
                    .ToList();
        }

        public List<ActivityFeedDto> GetRecentActivities(long projectId, int entityId, int actionId)
        {
            return
                _activityFeedData.GetRecentActivities(projectId, entityId, actionId, DaysTimeSpan)
                    .OrderByDescending(act => act.Createdon)
                    .ToList();
        }

        public List<ActivityFeedDto> GetRecentActivities(string username)
        {
            var personId = _personData.GetPersonId(username);
            var projects = _projectData.GetProjectsByUser(personId);

            return
                _activityFeedData.GetRecentActivities(projects, DaysTimeSpan)
                    .OrderByDescending(act => act.Createdon)
                    .ToList();
        }
    }
}
