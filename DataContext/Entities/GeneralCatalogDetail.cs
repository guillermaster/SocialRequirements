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
            Companies = new HashSet<Company>();
        }

        public int id { get; set; }

        public int generalcatalog_id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public virtual ICollection<ActivityFeed> ActivityFeed { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual GeneralCatalogHeader GeneralCatalogHeader { get; set; }
    }
}
