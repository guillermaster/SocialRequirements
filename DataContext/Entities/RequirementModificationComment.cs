using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRequirements.Context.Entities
{
    [Table("RequirementModificationComment")]
    public partial class RequirementModificationComment
    {
        public long id { get; set; }

        public long company_id { get; set; }

        public long project_id { get; set; }

        public long requirement_id { get; set; }

        public long requirement_modification_id { get; set; }

        public long requirement_modification_version_id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string comment { get; set; }

        public long createdby_id { get; set; }

        public DateTime createdon { get; set; }

        public virtual Company Company { get; set; }

        public virtual Project Project { get; set; }
    }
}
