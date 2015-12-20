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
    }
}
