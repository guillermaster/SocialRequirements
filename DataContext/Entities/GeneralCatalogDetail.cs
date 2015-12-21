using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRequirements.Context.Entities
{
    [Table("GeneralCatalogDetail")]
    public partial class GeneralCatalogDetail
    {
        public GeneralCatalogDetail()
        {
            ActivityFeed = new HashSet<ActivityFeed>();
            ActivityFeed1 = new HashSet<ActivityFeed>();
            Companies = new HashSet<Company>();
            Requirement = new HashSet<Requirement>();
            RequirementModification = new HashSet<RequirementModification>();
            RequirementModificationVersion = new HashSet<RequirementModificationVersion>();
            RequirementQuestion = new HashSet<RequirementQuestion>();
            RequirementQuestionAnswer = new HashSet<RequirementQuestionAnswer>();
            RequirementVersion = new HashSet<RequirementVersion>();
        }

        public int id { get; set; }

        public int generalcatalog_id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public virtual ICollection<ActivityFeed> ActivityFeed { get; set; }

        public virtual ICollection<ActivityFeed> ActivityFeed1 { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual GeneralCatalogHeader GeneralCatalogHeader { get; set; }

        public virtual ICollection<Requirement> Requirement { get; set; }

        public virtual ICollection<RequirementModification> RequirementModification { get; set; }

        public virtual ICollection<RequirementModificationVersion> RequirementModificationVersion { get; set; }

        public virtual ICollection<RequirementQuestion> RequirementQuestion { get; set; }

        public virtual ICollection<RequirementQuestionAnswer> RequirementQuestionAnswer { get; set; }

        public virtual ICollection<RequirementVersion> RequirementVersion { get; set; }
    }
}
