namespace DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequirementVersion")]
    public partial class RequirementVersion
    {
        public RequirementVersion()
        {
            RequirementComment = new HashSet<RequirementComment>();
            RequirementQuestion = new HashSet<RequirementQuestion>();
        }

        [Key]
        [Column(Order = 0)]
        public long id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long company_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long project_id { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long requirement_id { get; set; }

        public long? requirement_modification_id { get; set; }

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

        public virtual Requirement Requirement { get; set; }

        public virtual ICollection<RequirementComment> RequirementComment { get; set; }

        public virtual ICollection<RequirementQuestion> RequirementQuestion { get; set; }

        public virtual StatusValue StatusValue { get; set; }
    }
}
