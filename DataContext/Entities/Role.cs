using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRequirements.Context.Entities
{
    [Table("Role")]
    public partial class Role
    {
        public Role()
        {
            CompanyPersonRole = new HashSet<CompanyPersonRole>();
            CompanyProjectPersonRole = new HashSet<CompanyProjectPersonRole>();
            Permission = new HashSet<Permission>();
        }

        public long id { get; set; }

        [Required]
        [StringLength(20)]
        public string name { get; set; }

        public virtual ICollection<CompanyPersonRole> CompanyPersonRole { get; set; }

        public virtual ICollection<CompanyProjectPersonRole> CompanyProjectPersonRole { get; set; }

        public virtual ICollection<Permission> Permission { get; set; }
    }
}
