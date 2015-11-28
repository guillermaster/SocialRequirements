using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContext.Entities
{
    [Table("GeneralCatalogDetail")]
    public partial class GeneralCatalogDetail
    {
        public GeneralCatalogDetail()
        {
            Companies = new HashSet<Company>();
        }

        public int id { get; set; }

        public int generalcatalog_id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual GeneralCatalogHeader GeneralCatalogHeader { get; set; }
    }
}
