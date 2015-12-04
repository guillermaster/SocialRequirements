using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRequirements.Context.Entities
{
    [Table("StatusValue")]
    public partial class StatusValue
    {
        public StatusValue()
        {
            Requirement = new HashSet<Requirement>();
            RequirementModification = new HashSet<RequirementModification>();
            RequirementModificationVersion = new HashSet<RequirementModificationVersion>();
            RequirementQuestion = new HashSet<RequirementQuestion>();
            RequirementQuestionAnswer = new HashSet<RequirementQuestionAnswer>();
            RequirementVersion = new HashSet<RequirementVersion>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short id { get; set; }

        public short status_id { get; set; }

        [Required]
        [StringLength(30)]
        public string title { get; set; }

        public virtual ICollection<Requirement> Requirement { get; set; }

        public virtual ICollection<RequirementModification> RequirementModification { get; set; }

        public virtual ICollection<RequirementModificationVersion> RequirementModificationVersion { get; set; }

        public virtual ICollection<RequirementQuestion> RequirementQuestion { get; set; }

        public virtual ICollection<RequirementQuestionAnswer> RequirementQuestionAnswer { get; set; }

        public virtual ICollection<RequirementVersion> RequirementVersion { get; set; }

        public virtual Status Status { get; set; }
    }
}
