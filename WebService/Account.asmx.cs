using System;
using System.Web.Services;
using Ninject;
using Ninject.Web;
using SocialRequirements.Business.Account;
using SocialRequirements.Domain.BusinessLogic.Account;
using SocialRequirements.Utilities.ResponseCodes.Account;
using SocialRequirements.Utilities.Security;

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
        public int CreateNewUser(string name, string lastname, string encPrimaryemail, string encSecondaryemail,
            string encPassword, string birthdate, string phone, string mobilephone)
        {
            try
            {
                var primaryemail = Encryption.Decrypt(encPrimaryemail);
                var secondaryemail = Encryption.Decrypt(encSecondaryemail);
                var password = Encryption.Decrypt(encPassword);
                PersonBusiness.Add(name, lastname, birthdate, primaryemail, secondaryemail, phone, mobilephone, password);
                return (int)PersonResponse.PersonRegistration.Success;
            }
            catch (PersonBusiness.SocialRequirementsExcepction.WrongEmailFormat)
            {
                return (int)PersonResponse.PersonRegistration.WrongEmailFormat;
            }
            catch (PersonBusiness.SocialRequirementsExcepction.MissingRequiredField)
            {
                return (int)PersonResponse.PersonRegistration.MissingRequiredFields;
            }
            catch (PersonBusiness.SocialRequirementsExcepction.UserAlreadyExists)
            {
                return (int)PersonResponse.PersonRegistration.UserAlreadyExists;
            }
            catch (Exception ex)
            {
                return (int)PersonResponse.PersonRegistration.UnknownError;
            }
        }

        [WebMethod]
        public bool ValidatePassword(string username, string password)
        {
            return PersonBusiness.ValidatePassword(username, password);
        }
    }
}
