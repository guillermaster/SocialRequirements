using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContext.Entities
{
    [Table("CompanyPersonRole")]
    public partial class CompanyPersonRole
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long company_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long person_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long role_id { get; set; }

        public virtual Company Company { get; set; }

        public virtual Person Person { get; set; }

        public virtual Role Role { get; set; }
    }
}
