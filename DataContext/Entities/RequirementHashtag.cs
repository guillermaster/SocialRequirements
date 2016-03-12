using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRequirements.Context.Entities
{
    [Table("RequirementHashtag")]
    public partial class RequirementHashtag
    {
        [Key]
        [Column(Order = 0)]
        public long id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long requirement_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(144)]
        public string hashtag { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long createdby_id { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime createdon { get; set; }

        public virtual Person Person { get; set; }

        public virtual Requirement Requirement { get; set; }
    }
}
