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
    /// Summary description for RequirementQuestion
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RequirementQuestion : WebServiceBase
    {
        [Inject]
        public IRequirementQuestionBusiness RequirementQuestionBusiness { get; set; }

        [WebMethod]
        public void AddQuestion(long companyId, long projectId, long requirementId, string question, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementQuestionBusiness.Add(companyId, projectId, requirementId, question, username);
        }
    }
}
