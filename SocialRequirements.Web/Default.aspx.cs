using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ninject;
using Ninject.Web;
using SocialRequirements.Domain.Repository.Account;

namespace SocialRequirements.Web
{
    public partial class Default : PageBase
    {
        [Inject]
        public IPersonData PersonData { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            PersonData.Add(string.Empty, string.Empty, DateTime.Now, string.Empty, string.Empty, string.Empty, String.Empty, string.Empty, string.Empty);
        }
    }
}