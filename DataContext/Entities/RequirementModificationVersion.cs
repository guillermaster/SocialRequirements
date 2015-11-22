using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContext.Entities
{
    [Table("RequirementModificationVersion")]
    public partial class RequirementModificationVersion
    {
        public long id { get; set; }

        public long company_id { get; set; }

        public long project_id { get; set; }

        public long requirement_id { get; set; }

        public long requirement_modification_id { get; set; }

        [Required]
        [StringLength(100)]
        public string title { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string description { get; set; }

        public int agreed { get; set; }

        public int disagreed { get; set; }

        public short status_id { get; set; }

        public long createdby_id { get; set; }

        public DateTime createdon { get; set; }

        public long modifiedby_id { get; set; }

        public DateTime modifiedon { get; set; }

        public long? approvedby_id { get; set; }

        public DateTime? approvedon { get; set; }

        public virtual Company Company { get; set; }

        public virtual Project Project { get; set; }

        public virtual StatusValue StatusValue { get; set; }
    }
}
