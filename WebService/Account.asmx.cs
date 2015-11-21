using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Ninject;
using Ninject.Web;
using SocialRequirements.Domain.Repository.Account;

namespace WebService
{
    /// <summary>
    /// Summary description for Account
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Account : WebServiceBase
    {
        [Inject]
        public IPersonData PersonData { get; set; }

        [WebMethod]
        public bool CreateNewUser(string name, string lastname, string email, string password)
        {
            PersonData.Add(name, lastname, DateTime.Now, email, email, string.Empty, String.Empty, email, password);
            return true;
        }
    }
}
