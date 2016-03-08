using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRequirements.Context.Entities
{
    [Table("Permission")]
    public partial class Permission
    {
        public Permission()
        {
            Role = new HashSet<Role>();
        }

        public int id { get; set; }

        [StringLength(20)]
        public string name { get; set; }

        public virtual ICollection<Role> Role { get; set; }
    }
}
