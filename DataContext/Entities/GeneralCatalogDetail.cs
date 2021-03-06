﻿using System.Collections.Generic;
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
            Requirement1 = new HashSet<Requirement>();
            Requirement2 = new HashSet<Requirement>();
            RequirementModification = new HashSet<RequirementModification>();
            RequirementModification1 = new HashSet<RequirementModification>();
            RequirementModificationVersion = new HashSet<RequirementModificationVersion>();
            RequirementModificationVersion1 = new HashSet<RequirementModificationVersion>();
            RequirementQuestion = new HashSet<RequirementQuestion>();
            RequirementQuestionAnswer = new HashSet<RequirementQuestionAnswer>();
            RequirementVersion = new HashSet<RequirementVersion>();
            RequirementVersion1 = new HashSet<RequirementVersion>();
            RequirementModificationHashtag = new HashSet<RequirementModificationHashtag>();
        }

        public int id { get; set; }

        public int generalcatalog_id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [StringLength(100)]
        public string description { get; set; }

        public virtual ICollection<ActivityFeed> ActivityFeed { get; set; }

        public virtual ICollection<ActivityFeed> ActivityFeed1 { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual GeneralCatalogHeader GeneralCatalogHeader { get; set; }

        public virtual ICollection<Requirement> Requirement { get; set; }

        public virtual ICollection<Requirement> Requirement1 { get; set; }

        public virtual ICollection<Requirement> Requirement2 { get; set; }

        public virtual ICollection<RequirementModification> RequirementModification { get; set; }

        public virtual ICollection<RequirementModification> RequirementModification1 { get; set; }

        public virtual ICollection<RequirementModificationVersion> RequirementModificationVersion { get; set; }

        public virtual ICollection<RequirementModificationVersion> RequirementModificationVersion1 { get; set; }

        public virtual ICollection<RequirementQuestion> RequirementQuestion { get; set; }

        public virtual ICollection<RequirementQuestionAnswer> RequirementQuestionAnswer { get; set; }

        public virtual ICollection<RequirementVersion> RequirementVersion { get; set; }

        public virtual ICollection<RequirementVersion> RequirementVersion1 { get; set; }

        public virtual ICollection<RequirementModificationHashtag> RequirementModificationHashtag { get; set; }
    }
}
