namespace DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequirementComment")]
    public partial class RequirementComment
    {
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

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long requirement_version_id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string comment { get; set; }

        public long createdby_id { get; set; }

        public DateTime createdon { get; set; }

        public virtual Company Company { get; set; }

        public virtual Project Project { get; set; }

        public virtual RequirementVersion RequirementVersion { get; set; }
    }
}
