using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContext.Entities
{
    public partial class Status
    {
        public Status()
        {
            StatusValue = new HashSet<StatusValue>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short id { get; set; }

        [Required]
        [StringLength(30)]
        public string title { get; set; }

        public virtual ICollection<StatusValue> StatusValue { get; set; }
    }
}
