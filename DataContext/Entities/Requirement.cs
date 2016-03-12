using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRequirements.Context.Entities
{
    [Table("Requirement")]
    public partial class Requirement
    {
        public Requirement()
        {
            RequirementVersion = new HashSet<RequirementVersion>();
            RequirementComment = new HashSet<RequirementComment>();
            RequirementModificationComment = new HashSet<RequirementModificationComment>();
            RequirementQuestion = new HashSet<RequirementQuestion>();
            RequirementHashtag = new HashSet<RequirementHashtag>();
        }

        public long id { get; set; }

        public long company_id { get; set; }

        public long project_id { get; set; }

        [Required]
        [StringLength(100)]
        public string title { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string description { get; set; }

        public int agreed { get; set; }

        public int disagreed { get; set; }

        public int status_id { get; set; }

        public long createdby_id { get; set; }

        public DateTime createdon { get; set; }

        public long modifiedby_id { get; set; }

        public DateTime modifiedon { get; set; }

        public long? approvedby_id { get; set; }

        public DateTime? approvedon { get; set; }

        public int version_number { get; set; }

        public long requirement_version_id { get; set; }

        public virtual Company Company { get; set; }

        public virtual Project Project { get; set; }

        public virtual GeneralCatalogDetail GeneralCatalogDetail { get; set; }

        public virtual ICollection<RequirementVersion> RequirementVersion { get; set; }

        public virtual Person Person { get; set; }

        public virtual Person Person1 { get; set; }

        public virtual ICollection<RequirementComment> RequirementComment { get; set; }

        public virtual ICollection<RequirementModificationComment> RequirementModificationComment { get; set; }

        public virtual ICollection<RequirementQuestion> RequirementQuestion { get; set; }

        public virtual ICollection<RequirementHashtag> RequirementHashtag { get; set; }
    }
}
