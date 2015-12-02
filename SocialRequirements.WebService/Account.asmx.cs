using System;
using System.Web.Services;
using Ninject;
using Ninject.Web;
using SocialRequirements.Business.Account;
using SocialRequirements.Domain.BusinessLogic.Account;

namespace WebService
{
    /// <summary>
    /// Summary description for Account
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Account : WebServiceBase
    {
        [Inject]
        public IPersonBusiness PersonBusiness { get; set; }

        [WebMethod]
        public int CreateNewUser(string name, string lastname, string primaryemail, string secondaryemail,
            string password, string birthdate, string phone, string mobilephone)
        {
            try
            {
                PersonBusiness.Add(name, lastname, birthdate, primaryemail, secondaryemail, phone, mobilephone, password);
                return 1;
            }
            catch (PersonBusiness.Excepction.WrongEmailFormat)
            {
                return 2;
            }
            catch (PersonBusiness.Excepction.MissingRequiredField)
            {
                return 3;
            }
            catch (PersonBusiness.Excepction.UserAlreadyExists)
            {
                return 4;
            }
            catch (Exception ex)
            {
                return 5;
            }
        }
    }
}
