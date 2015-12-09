using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Ninject;
using Ninject.Web;
using SocialRequirements.Domain.BusinessLogic.General;
using SocialRequirements.Utilities.Security;

namespace WebService
{
    /// <summary>
    /// Summary description for General
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class General : WebServiceBase
    {
        [Inject]
        public IActivityFeedBusiness ActivityFeedBusiness { get; set; }

        [WebMethod]
        public string LatestActivityFeed(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            ActivityFeedBusiness.GetLatestActivity(username);
            return "Hello World";
        }
    }
}
