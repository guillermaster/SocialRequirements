using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRequirements.Context.Entities
{
    [Table("RequirementQuestionAnswer")]
    public partial class RequirementQuestionAnswer
    {
        public int id { get; set; }

        public long company_id { get; set; }

        public long project_id { get; set; }

        public long requirement_id { get; set; }

        public long requirement_version_id { get; set; }

        public long requirement_question_id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string answer { get; set; }

        public long createdby_id { get; set; }

        public DateTime createdon { get; set; }

        public long modifiedby_id { get; set; }

        public DateTime modifiedon { get; set; }

        public short status_id { get; set; }

        public virtual StatusValue StatusValue { get; set; }
    }
}
