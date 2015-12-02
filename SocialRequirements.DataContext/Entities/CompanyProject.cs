using System.ComponentModel.DataAnnotations.Schema;

namespace DataContext.Entities
{
    [Table("CompanyProject")]
    public partial class CompanyProject
    {
        public long id { get; set; }

        public long company_id { get; set; }

        public long project_id { get; set; }

        public virtual Company Company { get; set; }

        public virtual Project Project { get; set; }
    }
}
