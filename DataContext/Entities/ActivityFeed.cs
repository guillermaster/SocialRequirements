using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRequirements.Context.Entities
{
    [Table("ActivityFeed")]
    public partial class ActivityFeed
    {
        public long id { get; set; }

        public long company_id { get; set; }

        public long? project_id { get; set; }

        public int entity_id { get; set; }

        public long record_id { get; set; }

        public DateTime createdon { get; set; }

        public long createdby_id { get; set; }

        public int action_id { get; set; }

        public long? parent_id { get; set; }

        public long? grandparent_id { get; set; }

        public virtual Company Company { get; set; }

        public virtual Project Project { get; set; }

        public virtual GeneralCatalogDetail GeneralCatalogDetail { get; set; }

        public virtual GeneralCatalogDetail GeneralCatalogDetail1 { get; set; }

        public virtual Person Person { get; set; }
    }
}
