using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRequirements.Context.Entities
{
    [Table("RequirementQuestion")]
    public partial class RequirementQuestion
    {
        public long id { get; set; }

        public long company_id { get; set; }

        public long project_id { get; set; }

        public long requirement_id { get; set; }

        public long requirement_version_id { get; set; }

        [Required]
        [StringLength(500)]
        public string question { get; set; }

        public int status_id { get; set; }

        public long createdby_id { get; set; }

        public DateTime createdon { get; set; }

        public long modifiedby_id { get; set; }

        public DateTime modifiedon { get; set; }
        
        public virtual GeneralCatalogDetail GeneralCatalogDetail { get; set; }

        public virtual Company Company { get; set; }

        public virtual Person Person { get; set; }

        public virtual Person Person1 { get; set; }

        public virtual Project Project { get; set; }

        public virtual Requirement Requirement { get; set; }

        public virtual RequirementVersion RequirementVersion { get; set; }
    }
}
