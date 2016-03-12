using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRequirements.Context.Entities
{
    [Table("RequirementHashtag")]
    public partial class RequirementHashtag
    {
        public long id { get; set; }

        public long requirement_id { get; set; }

        public string hashtag { get; set; }

        public long createdby_id { get; set; }

        public DateTime createdon { get; set; }

        public virtual Person Person { get; set; }

        public virtual Requirement Requirement { get; set; }
    }
}
