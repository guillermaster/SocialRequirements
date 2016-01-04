using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.Domain.DTO.Requirement;
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
        private readonly IRequirementModificationVersionData _requirementModificationVersionData;
        private readonly IRequirementCommentData _requirementCommentData;
        private readonly IRequirementModificationCommentData _requirementModificationCommentData;
        private const int MaxDescriptionLength = 1700;
        private const int MaxShortDescriptionLength = 600;

        public ActivityFeedData(ContextModel context, IRequirementData requirementData,
            IRequirementVersionData requirementVersionData, IRequirementCommentData requirementCommentData,
            IRequirementModificationVersionData requirementModificationVersionData,
            IRequirementModificationCommentData requirementModificationCommentData)
        {
            _context = context;
            _requirementData = requirementData;
            _requirementVersionData = requirementVersionData;
            _requirementCommentData = requirementCommentData;
            _requirementModificationVersionData = requirementModificationVersionData;
            _requirementModificationCommentData = requirementModificationCommentData;
        }

        public void Add(long companyId, long? projectId, int entityId, int actionId, long recordId, DateTime createdon,
            long personId, long? parentId = null)
        {
            var activityFeed = new ActivityFeed
            {
                company_id = companyId,
                project_id = projectId,
                entity_id = entityId,
                record_id = recordId,
                createdon = createdon,
                createdby_id = personId,
                action_id = actionId,
                parent_id = parentId
            };

            try
            {
                _context.ActivityFeed.Add(activityFeed);
                _context.SaveChanges();
            }
            catch (DbUpdateException) { }
        }

        public List<ActivityFeedDto> GetLatestActivity(long companyId)
        {
            var activities = _context.ActivityFeed.Where(af => af.company_id == companyId).ToList();

            return activities.Select(GetDtoFromEntity).ToList();
        }

        public List<ActivityFeedSummaryDto> GetRecentActivitiesSummary(List<ProjectDto> projects, int daysTimestamp)
        {
            // init list of activities to be returned
            var latestActivitiesSummary = new List<ActivityFeedSummaryDto>();

            // set timestamp for oldest record
            var untilDatetime = DateTime.Now.AddDays(daysTimestamp * -1);

            foreach (var project in projects)
            {
                // requirements related activities

                var activity = GetActivitySummary(project.Id, GeneralCatalog.Detail.Entity.Requirement,
                    GeneralCatalog.Detail.EntityActions.Create, untilDatetime);
                if (activity != null) latestActivitiesSummary.Add(activity);

                activity = GetActivitySummary(project.Id, GeneralCatalog.Detail.Entity.Requirement,
                    GeneralCatalog.Detail.EntityActions.Modify, untilDatetime);
                if (activity != null) latestActivitiesSummary.Add(activity);

                activity = GetActivitySummary(project.Id, GeneralCatalog.Detail.Entity.Requirement,
                    GeneralCatalog.Detail.EntityActions.Approve, untilDatetime);
                if (activity != null) latestActivitiesSummary.Add(activity);

                activity = GetActivitySummary(project.Id, GeneralCatalog.Detail.Entity.Requirement,
                    GeneralCatalog.Detail.EntityActions.Reject, untilDatetime);
                if (activity != null) latestActivitiesSummary.Add(activity);

                activity = GetActivitySummary(project.Id, GeneralCatalog.Detail.Entity.Requirement,
                    GeneralCatalog.Detail.EntityActions.SubmitForApproval, untilDatetime);
                if (activity != null) latestActivitiesSummary.Add(activity);

                // requirement modifications related activities

                activity = GetActivitySummary(project.Id, GeneralCatalog.Detail.Entity.RequirementModification,
                    GeneralCatalog.Detail.EntityActions.Approve, untilDatetime);
                if (activity != null) latestActivitiesSummary.Add(activity);

                activity = GetActivitySummary(project.Id, GeneralCatalog.Detail.Entity.RequirementModification,
                    GeneralCatalog.Detail.EntityActions.Reject, untilDatetime);
                if (activity != null) latestActivitiesSummary.Add(activity);

                activity = GetActivitySummary(project.Id, GeneralCatalog.Detail.Entity.RequirementModification,
                    GeneralCatalog.Detail.EntityActions.SubmitForApproval, untilDatetime);
                if (activity != null) latestActivitiesSummary.Add(activity);

                // requirement questions related activities

                activity = GetActivitySummary(project.Id, GeneralCatalog.Detail.Entity.RequirementQuestion,
                    GeneralCatalog.Detail.EntityActions.Create, untilDatetime);
                if (activity != null) latestActivitiesSummary.Add(activity);

                // requirement question answers related activities

                activity = GetActivitySummary(project.Id, GeneralCatalog.Detail.Entity.RequirementQuestionAnswer,
                    GeneralCatalog.Detail.EntityActions.Create, untilDatetime);
                if (activity != null) latestActivitiesSummary.Add(activity);
            }

            return latestActivitiesSummary;
        }

        public List<ActivityFeedDto> GetRecentActivities(long projectId, int entityId, int actionId, int daysTimestamp)
        {
            // set timestamp for oldest record
            var untilDatetime = DateTime.Now.AddDays(daysTimestamp * -1);

            var activities =
                _context.ActivityFeed.Where(
                    af =>
                        af.project_id == projectId && af.entity_id == entityId && af.action_id == actionId &&
                        af.createdon >= untilDatetime).ToList();

            return activities.Select(GetDtoFromEntity).ToList();
        }

        public List<ActivityFeedDto> GetRecentActivities(List<ProjectDto> projects, int daysTimestamp)
        {
            var projectsIds = projects.Select(project => project.Id).Select(dummy => (long?)dummy).ToList();

            // set timestamp for oldest record
            var untilDatetime = DateTime.Now.AddDays(daysTimestamp * -1);

            var activities = new List<ActivityFeed>();

            // requirements

            activities.AddRange(
                _context.ActivityFeed.Where(
                    af =>
                        projectsIds.Contains(af.project_id) &&
                        af.entity_id == (int)GeneralCatalog.Detail.Entity.Requirement &&
                        af.action_id == (int)GeneralCatalog.Detail.EntityActions.Create &&
                        af.createdon >= untilDatetime).ToList());

            activities.AddRange(
                _context.ActivityFeed.Where(
                    af =>
                        projectsIds.Contains(af.project_id) &&
                        af.entity_id == (int)GeneralCatalog.Detail.Entity.Requirement &&
                        af.action_id == (int)GeneralCatalog.Detail.EntityActions.Modify &&
                        af.createdon >= untilDatetime).ToList());

            activities.AddRange(
                _context.ActivityFeed.Where(
                    af =>
                        projectsIds.Contains(af.project_id) &&
                        af.entity_id == (int)GeneralCatalog.Detail.Entity.Requirement &&
                        af.action_id == (int)GeneralCatalog.Detail.EntityActions.Approve &&
                        af.createdon >= untilDatetime).ToList());

            activities.AddRange(
                _context.ActivityFeed.Where(
                    af =>
                        projectsIds.Contains(af.project_id) &&
                        af.entity_id == (int)GeneralCatalog.Detail.Entity.Requirement &&
                        af.action_id == (int)GeneralCatalog.Detail.EntityActions.Reject &&
                        af.createdon >= untilDatetime).ToList());

            activities.AddRange(
                _context.ActivityFeed.Where(
                    af =>
                        projectsIds.Contains(af.project_id) &&
                        af.entity_id == (int)GeneralCatalog.Detail.Entity.Requirement &&
                        af.action_id == (int)GeneralCatalog.Detail.EntityActions.SubmitForApproval &&
                        af.createdon >= untilDatetime).ToList());

            // requirements modifications

            activities.AddRange(
                _context.ActivityFeed.Where(
                    af =>
                        projectsIds.Contains(af.project_id) &&
                        af.entity_id == (int)GeneralCatalog.Detail.Entity.RequirementModification &&
                        af.action_id == (int)GeneralCatalog.Detail.EntityActions.Approve &&
                        af.createdon >= untilDatetime).ToList());

            activities.AddRange(
                _context.ActivityFeed.Where(
                    af =>
                        projectsIds.Contains(af.project_id) &&
                        af.entity_id == (int)GeneralCatalog.Detail.Entity.RequirementModification &&
                        af.action_id == (int)GeneralCatalog.Detail.EntityActions.Reject &&
                        af.createdon >= untilDatetime).ToList());

            activities.AddRange(
                _context.ActivityFeed.Where(
                    af =>
                        projectsIds.Contains(af.project_id) &&
                        af.entity_id == (int)GeneralCatalog.Detail.Entity.RequirementModification &&
                        af.action_id == (int)GeneralCatalog.Detail.EntityActions.SubmitForApproval &&
                        af.createdon >= untilDatetime).ToList());

            // requirements questions

            activities.AddRange(
                _context.ActivityFeed.Where(
                    af =>
                        projectsIds.Contains(af.project_id) &&
                        af.entity_id == (int)GeneralCatalog.Detail.Entity.RequirementQuestion &&
                        af.action_id == (int)GeneralCatalog.Detail.EntityActions.Create &&
                        af.createdon >= untilDatetime).ToList());

            // requirements questions answers

            activities.AddRange(
                _context.ActivityFeed.Where(
                    af =>
                        projectsIds.Contains(af.project_id) &&
                        af.entity_id == (int)GeneralCatalog.Detail.Entity.RequirementQuestionAnswer &&
                        af.action_id == (int)GeneralCatalog.Detail.EntityActions.Create &&
                        af.createdon >= untilDatetime).ToList());

            return activities.Select(GetDtoFromEntity).ToList();
        }

        private ActivityFeedSummaryDto GetActivitySummary(long projectId, GeneralCatalog.Detail.Entity recordType,
            GeneralCatalog.Detail.EntityActions actionType, DateTime until)
        {
            var newReqQty = GetQuantityOfActivities(projectId, recordType, actionType, until);
            if (newReqQty == 0) return null;

            var earliest = GetEarliestActivityTime(projectId, recordType, actionType, until);

            var description = newReqQty + " " +
                StringUtilities.GetEntityName(recordType, newReqQty > 1) + " " +
                StringUtilities.GetActionOccurredDescription(actionType, newReqQty > 1);

            var activitySumm = new ActivityFeedSummaryDto
            {
                Quantity = newReqQty,
                Description = description,
                ProjectId = projectId,
                EntityId = (int)recordType,
                ActionId = (int)actionType,
                Until = until,
                MostRecent = earliest
            };

            return activitySumm;
        }

        private int GetQuantityOfActivities(long projectId, GeneralCatalog.Detail.Entity recordType,
            GeneralCatalog.Detail.EntityActions actionType, DateTime untilTime)
        {
            return
                _context.ActivityFeed.Count(
                    af =>
                        af.project_id == projectId &&
                        af.entity_id == (int)recordType && af.action_id == (int)actionType &&
                        af.createdon >= untilTime);
        }

        private DateTime GetEarliestActivityTime(long projectId, GeneralCatalog.Detail.Entity recordType,
            GeneralCatalog.Detail.EntityActions actionType, DateTime untilTime)
        {
            var activity =
                _context.ActivityFeed.Where(
                    af =>
                        af.project_id == projectId &&
                        af.entity_id == (int)recordType && af.action_id == (int)actionType &&
                        af.createdon >= untilTime).OrderByDescending(d => d.createdon).FirstOrDefault();
            return activity != null ? activity.createdon : DateTime.Now;
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
                ParentId = activity.parent_id,
                Createdon = activity.createdon,
                CreatedbyId = activity.createdby_id,
                CreatedByLastname = activity.Person.last_name,
                CreatedByName = activity.Person.first_name,
                EntityName = activity.GeneralCatalogDetail.name,
                EntitySingular = activity.GeneralCatalogDetail.description,
                EntityActionId = activity.action_id,
                EntityAction = activity.GeneralCatalogDetail1.name,
                EntityActionPastTense = activity.GeneralCatalogDetail1.description
            };

            // set description according to the entity
            switch (activityDto.EntityId)
            {
                case (int)GeneralCatalog.Detail.Entity.Requirement:
                    activityDto = GetRequirementActivity(activityDto);
                    break;
                case (int)GeneralCatalog.Detail.Entity.RequirementModification:
                    activityDto = GetRequirementModificationActivity(activityDto);
                    break;
                default:
                    activityDto.Description = string.Empty;
                    activityDto.ShortDescription = string.Empty;
                    activityDto.Comment = new List<RequirementCommentDto>();
                    break;
            }

            return activityDto;
        }

        private ActivityFeedDto GetRequirementActivity(ActivityFeedDto activity)
        {
            if (activity.ProjectId == null) return activity;

            var requirement = _requirementVersionData.Get(activity.CompanyId, activity.ProjectId.Value, activity.RecordId);

            if (requirement == null) return activity;

            switch (activity.EntityActionId)
            {
                case (int)GeneralCatalog.Detail.EntityActions.Create:
                case (int)GeneralCatalog.Detail.EntityActions.SubmitForApproval:
                    activity.Description = StringUtilities.GetShort(requirement.Description, MaxDescriptionLength);
                    activity.ShortDescription = StringUtilities.GetShort(requirement.Description, MaxShortDescriptionLength);
                    activity.HasEvenLongerDescription = activity.Description.Length < requirement.Description.Length;
                    activity.Comment = _requirementCommentData.Get(requirement.Id, requirement.CompanyId,
                                requirement.ProjectId, requirement.VersionId);
                    break;
                case (int)GeneralCatalog.Detail.EntityActions.Like:
                case (int)GeneralCatalog.Detail.EntityActions.Dislike:
                case (int)GeneralCatalog.Detail.EntityActions.Modify:
                case (int)GeneralCatalog.Detail.EntityActions.Approve:
                case (int)GeneralCatalog.Detail.EntityActions.Reject:
                case (int)GeneralCatalog.Detail.EntityActions.Remove:
                    activity.Description = StringUtilities.GetShort(requirement.Title, MaxDescriptionLength);
                    activity.ShortDescription = StringUtilities.GetShort(requirement.Title, MaxShortDescriptionLength);
                    activity.HasEvenLongerDescription = false;
                    activity.Comment = new List<RequirementCommentDto>();
                    break;
                default:
                    activity.Description = string.Empty;
                    activity.ShortDescription = string.Empty;
                    activity.HasEvenLongerDescription = false;
                    activity.Comment = new List<RequirementCommentDto>();
                    break;
            }

            activity.Likes = requirement.Agreed;
            activity.Dislikes = requirement.Disagreed;
            activity.VersionNumber = requirement.VersionNumber;
            activity.Comments = activity.Comment.Count;
            
            return activity;
        }

        private ActivityFeedDto GetRequirementModificationActivity(ActivityFeedDto activity)
        {
            if (activity.ProjectId == null || activity.ParentId == null) return activity;

            var requirement = _requirementModificationVersionData.Get(activity.CompanyId, activity.ProjectId.Value,
                activity.ParentId.Value, activity.RecordId);

            if (requirement == null) return activity;

            switch (activity.EntityActionId)
            {
                case (int) GeneralCatalog.Detail.EntityActions.Create:
                case (int) GeneralCatalog.Detail.EntityActions.SubmitForApproval:
                    activity.Description = StringUtilities.GetShort(requirement.Description, MaxDescriptionLength);
                    activity.ShortDescription = StringUtilities.GetShort(requirement.Description, MaxShortDescriptionLength);
                    activity.HasEvenLongerDescription = activity.Description.Length < requirement.Description.Length;
                    activity.Comment = _requirementModificationCommentData.Get(requirement.CompanyId, requirement.ProjectId,
                        requirement.RequirementId, requirement.Id, requirement.VersionId)
                        .Select(GetRequirementComment)
                        .ToList();
                    break;
                case (int) GeneralCatalog.Detail.EntityActions.Like:
                case (int) GeneralCatalog.Detail.EntityActions.Dislike:
                case (int) GeneralCatalog.Detail.EntityActions.Modify:
                case (int) GeneralCatalog.Detail.EntityActions.Approve:
                case (int) GeneralCatalog.Detail.EntityActions.Reject:
                case (int) GeneralCatalog.Detail.EntityActions.Remove:
                    activity.Description = StringUtilities.GetShort(requirement.Title, MaxDescriptionLength);
                    activity.ShortDescription = StringUtilities.GetShort(requirement.Title, MaxShortDescriptionLength);
                    activity.HasEvenLongerDescription = false;
                    activity.Comment = new List<RequirementCommentDto>();
                    break;
                default:
                    activity.Description = string.Empty;
                    activity.ShortDescription = string.Empty;
                    activity.HasEvenLongerDescription = false;
                    activity.Comment = new List<RequirementCommentDto>();
                    break;
            }

            activity.Likes = requirement.Agreed;
            activity.Dislikes = requirement.Disagreed;
            activity.VersionNumber = requirement.VersionNumber;
            activity.Comments = activity.Comment.Count;

            return activity;
        }

        private static RequirementCommentDto GetRequirementComment(RequirementModificationCommentDto modifComment)
        {
            var comment = new RequirementCommentDto
            {
                Id = modifComment.RequirementId,
                Comment = modifComment.Comment,
                CompanyId = modifComment.CompanyId,
                CreatedbyId = modifComment.CreatedbyId,
                CreatedByName = modifComment.CreatedByName,
                Createdon = modifComment.Createdon,
                ProjectId = modifComment.ProjectId,
                RequirementId = modifComment.RequirementId,
                RequirementVersionId = modifComment.RequirementVersionId
            };
            return comment;
        }
    }
}
