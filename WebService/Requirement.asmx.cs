using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Ninject;
using Ninject.Web;
using SocialRequirements.Domain.BusinessLogic.Requirement;
using SocialRequirements.Utilities.Security;

namespace WebService
{
    /// <summary>
    /// Summary description for Requirement
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Requirement : WebServiceBase
    {
        [Inject]
        public IRequirementBusiness RequirementBusiness { get; set; }

        [WebMethod]
        public void AddRequirement(string title, string description, long companyId, long projectId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementBusiness.Add(companyId, projectId, title, description, username);
        }
    }
}
