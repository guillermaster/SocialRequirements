using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<ActivityFeedDto> GetLatestActivity(long companyId)
        {
            var activities = _context.ActivityFeed.Where(af => af.company_id == companyId).ToList();

            return activities.Select(GetDtoFromEntity).ToList();
        }

        private static ActivityFeedDto GetDtoFromEntity(ActivityFeed activity)
        {
            var activityDto = new ActivityFeedDto
            {
                Id = activity.id,
                CompanyId = activity.company_id,
                ProjectId = activity.project_id,
                EntityId = activity.entity_id,
                RecordId = activity.record_id,
                Createdon = activity.createdon,
                CreatedbyId = activity.createdby_id,
                CreatedByLastname = activity.Person.last_name,
                CreatedByName = activity.Person.first_name,
                EntityName = activity.GeneralCatalogDetail.name
            };

            return activityDto;
        }
    }
}
