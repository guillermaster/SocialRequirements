using System;
using System.Runtime.Remoting.Messaging;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using SocialRequirements.AccountService;
using SocialRequirements.Domain.General;
using SocialRequirements.Utilities.Security;

namespace SocialRequirements.Account
{
    public partial class ManagePassword : SocialRequirementsPublicPage
    {
        private string Email
        {
            get { return ViewState["Email"].ToString(); }
            set { ViewState["Email"] = value; }
        }

        #region Page Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Request.QueryString.Count == 0) return;
            if (Request.QueryString[CommonConstants.QueryStringParams.Id] == null) return;

            Email = Encryption.Decrypt(Request.QueryString[CommonConstants.QueryStringParams.Id]);

            if (HasPassword())
            {
                if (UserLoggedIn())
                {
                    changePasswordHolder.Visible = true;
                    setPassword.Visible = false;
                }
                else
                {
                    SetErrorMessage("You must login to access this form", string.Empty);
                }
            }
            else
            {
                setPassword.Visible = true;
                changePasswordHolder.Visible = false;
            }
        }

        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void SetPassword_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;

            if (password.Text != confirmPassword.Text)
            {
                SetErrorMessage("The password and its confirmation does not match.", string.Empty);
                return;
            }

            try
            {
                var personService = new AccountSoapClient();
                var encEmail = Encryption.Encrypt(Email);
                var encPassword = Encryption.Encrypt(password.Text);
                personService.SetPassword(encEmail, encPassword);
                SetSuccessMessage("The password has been successfully updated");
            }
            catch (Exception ex)
            {
                SetErrorMessage("An unknown error has occurred", ex.Message);
            }
        }
        
        protected void ContinueLinkButton_Click(object sender, EventArgs e)
        {
            InitUserSession(Email);
        }
        #endregion

        private bool HasPassword()
        {
            return Request.QueryString[CommonConstants.QueryStringParams.Have] != null &&
                   int.Parse(Request.QueryString[CommonConstants.QueryStringParams.Have]) == 1;
        }

        #region Form Setup


        private void SetSuccessMessage(string message)
        {
            SuccessMessage.Text = message;
            SuccessPanel.Visible = true;
            ErrorPanel.Visible = false;
        }

        private void SetErrorMessage(string message, string exception)
        {
            ErrorMessage.Text = message;
            ExceptionMessage.Text = exception;
            ErrorPanel.Visible = true;
            SuccessPanel.Visible = false;
            ErrorPanel.Focus();
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#ErrorPanel';", true);
        }
        #endregion
    }
}