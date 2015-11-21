namespace DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
