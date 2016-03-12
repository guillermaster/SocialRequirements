using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRequirements.Context.Entities
{
    [Table("RequirementModificationHashtag")]
    public partial class RequirementModificationHashtag
    {
        public long id { get; set; }

        public long requirement_modification_id { get; set; }

        [StringLength(144)]
        public string hashtag { get; set; }

        public int status_id { get; set; }

        public long createdby_id { get; set; }

        public DateTime createdon { get; set; }

        public virtual GeneralCatalogDetail GeneralCatalogDetail { get; set; }

        public virtual Person Person { get; set; }

        public virtual RequirementModification RequirementModification { get; set; }
    }
}
