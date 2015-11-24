using System;
using System.Web.Security;
using System.Web.UI;
using SocialRequirements.AccountService;
using SocialRequirements.Utilities.ResponseCodes.Account;

namespace SocialRequirements.Account
{
    public partial class Register : Page
    {

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if (CreateUser())
            {
                FormsAuthentication.RedirectFromLoginPage(Email.Text, true);
            }
        }

        private bool CreateUser()
        {
            var personService = new AccountSoapClient();
            var personResponse = personService.CreateNewUser(Name.Text, Lastname.Text, Email.Text, SecondaryEmail.Text,
                Password.Text, Birthdate.Text, Phone.Text, MobilePhone.Text);

            switch (personResponse)
            {
                case (int)PersonResponse.PersonRegistration.Success:
                    SetSuccessMessage("User successfully created");
                    return true;
                    break;
                case (int)PersonResponse.PersonRegistration.WrongEmailFormat:
                    SetErrorMessage("Wrong email format");
                    break;
                case (int)PersonResponse.PersonRegistration.MissingRequiredFields:
                    SetErrorMessage("Some required fields are missing");
                    break;
                case (int)PersonResponse.PersonRegistration.UserAlreadyExists:
                    SetErrorMessage("User already exists");
                    break;
                case (int)PersonResponse.PersonRegistration.UnknownError:
                    SetErrorMessage("A unknown error has occurred");
                    break;
            }
            return false;
        }

        #region Form Setup

        private void SetSuccessMessage(string message)
        {
            SuccessMessage.Text = message;
            SuccessPanel.Visible = true;
            ErrorPanel.Visible = false;
            InputFormPanel.Visible = false;
        }

        private void SetErrorMessage(string message)
        {
            ErrorMessage.Text = message;
            ErrorPanel.Visible = true;
            SuccessPanel.Visible = false;
            InputFormPanel.Visible = true;
        }
        #endregion

    }
}