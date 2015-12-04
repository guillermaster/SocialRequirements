using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRequirements.Context.Entities
{
    [Table("GeneralCatalogHeader")]
    public partial class GeneralCatalogHeader
    {
        public GeneralCatalogHeader()
        {
            GeneralCatalogDetails = new HashSet<GeneralCatalogDetail>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public virtual ICollection<GeneralCatalogDetail> GeneralCatalogDetails { get; set; }
    }
}
