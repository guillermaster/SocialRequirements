namespace DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Company")]
    public partial class Company
    {
        public Company()
        {
            CompanyPersonRole = new HashSet<CompanyPersonRole>();
            CompanyProject = new HashSet<CompanyProject>();
            CompanyProjectPerson = new HashSet<CompanyProjectPerson>();
            CompanyProjectPersonRole = new HashSet<CompanyProjectPersonRole>();
            Requirement = new HashSet<Requirement>();
            RequirementComment = new HashSet<RequirementComment>();
            RequirementModification = new HashSet<RequirementModification>();
            RequirementModificationComment = new HashSet<RequirementModificationComment>();
            RequirementModificationVersion = new HashSet<RequirementModificationVersion>();
            RequirementVersion = new HashSet<RequirementVersion>();
            Person = new HashSet<Person>();
        }

        public long id { get; set; }

        [Required]
        [StringLength(70)]
        public string name { get; set; }

        public short type { get; set; }

        public virtual ICollection<CompanyPersonRole> CompanyPersonRole { get; set; }

        public virtual ICollection<CompanyProject> CompanyProject { get; set; }

        public virtual ICollection<CompanyProjectPerson> CompanyProjectPerson { get; set; }

        public virtual ICollection<CompanyProjectPersonRole> CompanyProjectPersonRole { get; set; }

        public virtual ICollection<Requirement> Requirement { get; set; }

        public virtual ICollection<RequirementComment> RequirementComment { get; set; }

        public virtual ICollection<RequirementModification> RequirementModification { get; set; }

        public virtual ICollection<RequirementModificationComment> RequirementModificationComment { get; set; }

        public virtual ICollection<RequirementModificationVersion> RequirementModificationVersion { get; set; }

        public virtual ICollection<RequirementVersion> RequirementVersion { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
}
