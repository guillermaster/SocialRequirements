using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRequirements.Context.Entities
{
    [Table("Person")]
    public partial class Person
    {
        public Person()
        {
            ActivityFeed = new HashSet<ActivityFeed>();
            CompanyPerson = new HashSet<CompanyPerson>();
            CompanyPersonRole = new HashSet<CompanyPersonRole>();
            CompanyProjectPerson = new HashSet<CompanyProjectPerson>();
            CompanyProjectPersonRole = new HashSet<CompanyProjectPersonRole>();
            Company = new HashSet<Company>();
            Requirement = new HashSet<Requirement>();
            Requirement1 = new HashSet<Requirement>();
            RequirementModification = new HashSet<RequirementModification>();
            RequirementModification1 = new HashSet<RequirementModification>();
            RequirementComment = new HashSet<RequirementComment>();
            RequirementModificationComment = new HashSet<RequirementModificationComment>();
            RequirementQuestion = new HashSet<RequirementQuestion>();
            RequirementQuestion1 = new HashSet<RequirementQuestion>();
            RequirementQuestionAnswer = new HashSet<RequirementQuestionAnswer>();
            RequirementQuestionAnswer1 = new HashSet<RequirementQuestionAnswer>();
            RequirementHashtag = new HashSet<RequirementHashtag>();
            RequirementModificationHashtag = new HashSet<RequirementModificationHashtag>();
            RequirementVersion = new HashSet<RequirementVersion>();
            RequirementVersion1 = new HashSet<RequirementVersion>();
            RequirementVersion2 = new HashSet<RequirementVersion>();
        }

        public long id { get; set; }

        [Required]
        [StringLength(50)]
        public string first_name { get; set; }

        [Required]
        [StringLength(50)]
        public string last_name { get; set; }

        [Column(TypeName = "date")]
        public DateTime birth_date { get; set; }

        [Required]
        [StringLength(100)]
        public string primary_email { get; set; }

        [StringLength(100)]
        public string secondary_email { get; set; }

        [StringLength(20)]
        public string phone { get; set; }

        [StringLength(20)]
        public string mobile_phone { get; set; }

        [Required]
        [StringLength(100)]
        public string user_name { get; set; }

        [Required]
        [StringLength(250)]
        public string password { get; set; }

        public virtual ICollection<CompanyPerson> CompanyPerson { get; set; }

        public virtual ICollection<CompanyPersonRole> CompanyPersonRole { get; set; }

        public virtual ICollection<CompanyProjectPerson> CompanyProjectPerson { get; set; }

        public virtual ICollection<CompanyProjectPersonRole> CompanyProjectPersonRole { get; set; }

        public virtual ICollection<Company> Company { get; set; }

        public virtual ICollection<ActivityFeed> ActivityFeed { get; set; }

        public virtual ICollection<Requirement> Requirement { get; set; }

        public virtual ICollection<Requirement> Requirement1 { get; set; }

        public virtual ICollection<RequirementModification> RequirementModification { get; set; }

        public virtual ICollection<RequirementModification> RequirementModification1 { get; set; }

        public virtual ICollection<RequirementComment> RequirementComment { get; set; }

        public virtual ICollection<RequirementModificationComment> RequirementModificationComment { get; set; }

        public virtual ICollection<RequirementQuestion> RequirementQuestion { get; set; }

        public virtual ICollection<RequirementQuestion> RequirementQuestion1 { get; set; }

        public virtual ICollection<RequirementQuestionAnswer> RequirementQuestionAnswer { get; set; }

        public virtual ICollection<RequirementQuestionAnswer> RequirementQuestionAnswer1 { get; set; }

        public virtual ICollection<RequirementHashtag> RequirementHashtag { get; set; }

        public virtual ICollection<RequirementModificationHashtag> RequirementModificationHashtag { get; set; }

        public virtual ICollection<RequirementVersion> RequirementVersion { get; set; }

        public virtual ICollection<RequirementVersion> RequirementVersion1 { get; set; }

        public virtual ICollection<RequirementVersion> RequirementVersion2 { get; set; }
    }
}
