using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRequirements.Context.Entities
{
    [Table("RequirementModificationHashtag")]
    public partial class RequirementModificationHashtag
    {
        [Key]
        [Column(Order = 0)]
        public long id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long requirement_modification_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(144)]
        public string hashtag { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int status_id { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long createdby_id { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime createdon { get; set; }

        public virtual GeneralCatalogDetail GeneralCatalogDetail { get; set; }

        public virtual Person Person { get; set; }

        public virtual RequirementModification RequirementModification { get; set; }
    }
}
