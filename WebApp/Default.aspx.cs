using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ninject;
using SocialRequirements.Domain.Repository.Account;

namespace SocialRequirements
{
    public partial class _Default : Page
    {

        //[Inject]
        //private IPersonData _personData;

        protected void Page_Load(object sender, EventArgs e)
        {
            //_personData.Add(string.Empty, string.Empty, DateTime.Now, string.Empty, string.Empty, string.Empty, String.Empty, string.Empty, string.Empty);
        }
    }
}