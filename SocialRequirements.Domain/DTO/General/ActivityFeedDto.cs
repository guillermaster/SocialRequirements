using System;
using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.DTO.General
{
    [Serializable]
    public class ActivityFeedDto
    {
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long? ProjectId { get; set; }

        public int EntityId { get; set; }

        public string EntityName { get; set; }

        public long RecordId { get; set; }

        public DateTime Createdon { get; set; }

        public long CreatedbyId { get; set; }

        public string CreatedByName { get; set; }

        public string CreatedByLastname { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public bool HasEvenLongerDescription { get; set; }

        public int VersionNumber { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public List<RequirementCommentDto> Comments { get; set; }
    }
}
