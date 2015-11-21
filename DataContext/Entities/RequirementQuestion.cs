namespace DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequirementQuestion")]
    public partial class RequirementQuestion
    {
        public RequirementQuestion()
        {
            RequirementQuestionAnswer = new HashSet<RequirementQuestionAnswer>();
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

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long requirement_version_id { get; set; }

        [Required]
        [StringLength(500)]
        public string question { get; set; }

        public short status_id { get; set; }

        public long createdby_id { get; set; }

        public DateTime createdon { get; set; }

        public long modifiedby_id { get; set; }

        public DateTime modifiedon { get; set; }

        public int? accepted_answer_id { get; set; }

        public virtual ICollection<RequirementQuestionAnswer> RequirementQuestionAnswer { get; set; }

        public virtual RequirementVersion RequirementVersion { get; set; }

        public virtual StatusValue StatusValue { get; set; }
    }
}
