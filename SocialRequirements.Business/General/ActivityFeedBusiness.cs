using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Domain.BusinessLogic.Account;
using SocialRequirements.Domain.BusinessLogic.General;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.Domain.Repository.General;

namespace SocialRequirements.Business.General
{
    public class ActivityFeedBusiness : IActivityFeedBusiness
    {
        private readonly IActivityFeedData _activityFeedData;
        private readonly ICompanyBusiness _companyBusiness;

        public ActivityFeedBusiness(IActivityFeedData activityFeedData, ICompanyBusiness companyBusiness)
        {
            _companyBusiness = companyBusiness;
            _activityFeedData = activityFeedData;
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
    }
}
