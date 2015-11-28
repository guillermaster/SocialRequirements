using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Services;
using System.Web.Services;
using Ninject;
using Ninject.Web;
using SocialRequirements.Domain.BusinessLogic.Account;
using SocialRequirements.Domain.DTO;
using SocialRequirements.Domain.Exception.Account;
using SocialRequirements.Utilities;
using SocialRequirements.Utilities.ResponseCodes.Account;
using SocialRequirements.Utilities.Security;

namespace WebService
{
    /// <summary>
    /// Summary description for Account
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class Account : WebServiceBase
    {
        [Inject]
        public IPersonBusiness PersonBusiness { get; set; }
        [Inject]
        public ICompanyBusiness CompanyBusiness { get; set; }

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
            catch (AccountException.WrongEmailFormat)
            {
                return (int)PersonResponse.PersonRegistration.WrongEmailFormat;
            }
            catch (AccountException.MissingRequiredField)
            {
                return (int)PersonResponse.PersonRegistration.MissingRequiredFields;
            }
            catch (AccountException.UserAlreadyExists)
            {
                return (int)PersonResponse.PersonRegistration.UserAlreadyExists;
            }
            catch (Exception ex)
            {
                return (int)PersonResponse.PersonRegistration.UnknownError;
            }
        }

        [WebMethod]
        public bool ValidatePassword(string encUsername, string encPassword)
        {
            var username = Encryption.Decrypt(encUsername);
            var password = Encryption.Decrypt(encPassword);
            return PersonBusiness.ValidatePassword(username, password);
        }

        [WebMethod]
        public string GetUserCompanies(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var companies = CompanyBusiness.GetCompaniesByUser(username);
            var serializer = new ObjectSerializer<List<CompanyDto>>(companies);
            return serializer.ToXmlString();
        }
    }
}
