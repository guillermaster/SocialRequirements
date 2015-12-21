using System;
using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.Domain.General;
using SocialRequirements.Domain.Repository.General;
using SocialRequirements.Domain.Repository.Requirement;
using SocialRequirements.Utilities;

namespace SocialRequirements.Data.General
{
    public class ActivityFeedData : IActivityFeedData
    {
        private readonly ContextModel _context;
        private readonly IRequirementData _requirementData;
        private readonly IRequirementVersionData _requirementVersionData;
        private readonly IRequirementCommentData _requirementCommentData;
        private const int MaxDescriptionLength = 1700;
        private const int MaxShortDescriptionLength = 600;

        public ActivityFeedData(ContextModel context, IRequirementData requirementData,
            IRequirementVersionData requirementVersionData, IRequirementCommentData requirementCommentData)
        {
            _context = context;
            _requirementData = requirementData;
            _requirementVersionData = requirementVersionData;
            _requirementCommentData = requirementCommentData;
        }

        public void Add(long companyId, long? projectId, int entityId, int actionId, long recordId, DateTime createdon, long personId)
        {
            var activityFeed = new ActivityFeed
            {
                company_id = companyId,
                project_id = projectId,
                entity_id = entityId,
                record_id = recordId,
                createdon = createdon,
                createdby_id = personId,
                action_id = actionId
            };
            _context.ActivityFeed.Add(activityFeed);
            _context.SaveChanges();
        }

        public List<ActivityFeedDto> GetLatestActivity(long companyId)
        {
            var activities = _context.ActivityFeed.Where(af => af.company_id == companyId).ToList();

            return activities.Select(GetDtoFromEntity).ToList();
        }

        private ActivityFeedDto GetDtoFromEntity(ActivityFeed activity)
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
                EntityName = activity.GeneralCatalogDetail.name,
                EntityActionId = activity.action_id,
                EntityAction = activity.GeneralCatalogDetail1.name
            };

            // set description according to the entity
            switch (activityDto.EntityId)
            {
                case (int)GeneralCatalog.Detail.Entity.Requirement:
                    activityDto = GetRequirementActivity(activityDto);
                    break;
                default:
                    activityDto.Description = string.Empty;
                    activityDto.ShortDescription = string.Empty;
                    break;
            }

            return activityDto;
        }

        private ActivityFeedDto GetRequirementActivity(ActivityFeedDto activity)
        {
            if (activity.ProjectId == null) return activity;

            var requirement = _requirementVersionData.Get(activity.CompanyId, activity.ProjectId.Value, activity.RecordId);

            if(requirement == null) return activity;

            activity.Description = StringUtilities.GetShort(requirement.Description, MaxDescriptionLength);
            activity.ShortDescription = StringUtilities.GetShort(requirement.Description, MaxShortDescriptionLength);
            activity.HasEvenLongerDescription = activity.Description.Length <
                                                           requirement.Description.Length;
            activity.Likes = requirement.Agreed;
            activity.Dislikes = requirement.Disagreed;
            activity.VersionNumber = requirement.VersionNumber;
            activity.Comment = _requirementCommentData.Get(requirement.Id, requirement.CompanyId,
                        requirement.ProjectId, requirement.VersionId);
            activity.Comments = activity.Comment.Count;
            return activity;
        }
    }
}
