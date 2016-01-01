using System;
using SocialRequirements.Domain.General;

namespace SocialRequirements.Domain.DTO.General
{
    public class ActivityFeedSummaryDto
    {
        public long ProjectId { get; set; }

        public GeneralCatalog.Detail.Entity Entity { get; set; }

        public GeneralCatalog.Detail.EntityActions Action { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public DateTime Until { get; set; }

        public DateTime MostRecent { get; set; }
    }
}
