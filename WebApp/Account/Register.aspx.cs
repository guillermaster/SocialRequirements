using System;
using System.Web.Security;
using System.Web.UI;
using SocialRequirements.AccountService;
using SocialRequirements.Utilities.ResponseCodes.Account;

namespace SocialRequirements.Account
{
    public partial class Register : SocialRequirementsPublicPage
    {
        #region Form Events

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            CreateUser();
        }

        protected void ContinueLinkButton_Click(object sender, EventArgs e)
        {
            FormsAuthentication.RedirectFromLoginPage(Email.Text, true);
        }
        #endregion

        #region Data
        
        private void CreateUser()
        {
            var personService = new AccountSoapClient();
            var personResponse = personService.CreateNewUser(Name.Text, Lastname.Text, Email.Text, SecondaryEmail.Text,
                Password.Text, Birthdate.Text, Phone.Text, MobilePhone.Text);

            switch (personResponse)
            {
                case (int)PersonResponse.PersonRegistration.Success:
                    SetSuccessMessage("User successfully created");
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
        }
        #endregion

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