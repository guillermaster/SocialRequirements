using System;
using System.Collections.Generic;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.Domain.Repository.General;

namespace SocialRequirements.Data.General
{
    public class ActivityFeedData : IActivityFeedData
    {
        private readonly ContextModel _context;

        public ActivityFeedData(ContextModel context)
        {
            _context = context;
        }

        public void Add(long companyId, long? projectId, int entityId, long recordId, DateTime createdon, long personId)
        {
            var activityFeed = new ActivityFeed
            {
                company_id = companyId,
                project_id = projectId,
                entity_id = entityId,
                record_id = recordId,
                createdon = createdon,
                createdby_id = personId
            };
            _context.ActivityFeed.Add(activityFeed);
            _context.SaveChanges();
        }

        public List<ActivityFeedDto> GetLatestActivity(int companyId)
        {
            throw new NotImplementedException();
        }
    }
}
