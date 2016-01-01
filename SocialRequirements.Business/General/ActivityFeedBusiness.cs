using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Domain.BusinessLogic.Account;
using SocialRequirements.Domain.BusinessLogic.General;
using SocialRequirements.Domain.DTO.Account;
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
            var userCompanies = _companyBusiness.GetCompaniesByUser(username);
            foreach (var company in userCompanies)
            {
                activityFeed.AddRange(_activityFeedData.GetLatestActivity(company.Id));
            }
            return activityFeed.OrderByDescending(a => a.Createdon).ToList();
        }

        public List<ActivityFeedSummaryDto> GetRecentActivitiesSummary(string username)
        {
            var personId = _personData.GetPersonId(username);
            var projects = _projectData.GetProjectsByUser(personId);

            return _activityFeedData.GetRecentActivitiesSummary(projects).OrderByDescending(act => act.MostRecent).ToList();
        }
    }
}
