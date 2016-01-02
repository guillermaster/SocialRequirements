using System;

namespace SocialRequirements.Domain.DTO.General
{
    public class ActivityFeedSummaryDto
    {
        public long ProjectId { get; set; }

        public int EntityId { get; set; }

        public int ActionId { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public DateTime Until { get; set; }

        public DateTime MostRecent { get; set; }

        public string Url { get; set; }
    }
}
