using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Services;
using Ninject;
using Ninject.Web;
using SocialRequirements.Domain.BusinessLogic.Account;
using SocialRequirements.Domain.DTO;
using SocialRequirements.Utilities;
using SocialRequirements.Utilities.Security;

namespace WebService
{
    /// <summary>
    /// Summary description for Company
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Company : WebServiceBase
    {
        [Inject]
        public ICompanyBusiness CompanyBusiness { get; set; }

        [WebMethod]
        public string GetCompanyTypes()
        {
            var types = CompanyBusiness.GetCompanyTypes();
            var serializer = new ObjectSerializer<List<GeneralCatalogDetailDto>>(types);
            return serializer.ToXmlString();
        }

        [WebMethod]
        public void AddCompany(string name, int type, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            CompanyBusiness.Add(name, type, username);
        }
    }
}

