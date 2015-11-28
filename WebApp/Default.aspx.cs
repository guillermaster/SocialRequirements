using System;
using System.Xml;
using SocialRequirements.AccountService;
using SocialRequirements.Utilities.Security;

namespace SocialRequirements
{
    public partial class Default : SocialRequirementsPrivatePage
    {
        #region Properties

        private string RequiredActionUrl 
        {
            get
            {
                return ViewState["RequiredActionUrl"] != null ? ViewState["RequiredActionUrl"].ToString() : string.Empty;
            }
            set { ViewState["RequiredActionUrl"] = value; }
        }
        #endregion

        #region Main Events
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            RequiredActionPanel.Visible = false;
            if (!CheckRelatedCompanies()) return;
        }
        #endregion

        #region Validations
        /// <summary>
        /// Check the related companies to the user, if none found
        /// show an alert and link to relate user to a company
        /// </summary>
        private bool CheckRelatedCompanies()
        {
            if (!UserLoggedIn()) return false;

            var personService = new AccountSoapClient();
            var encUsername = Encryption.Encrypt(Username);
            var companies = personService.GetUserCompanies(encUsername);

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(companies);
            // check if there is at least one company in the result
            if (xmlDocument.ChildNodes[1].ChildNodes.Count > 0)
            {
                return true;
            }

            SetRequiredActionPanel("You are not related to any company. Please select the company you belong to.");
            return false;
        }
        #endregion

        #region Required Action Panel
        private void SetRequiredActionPanel(string message)
        {
            RequiredActionMessage.Text = message;
            RequiredActionPanel.Visible = true;
            RequiredActionUrl = "~/Account/SetCompany.aspx";
        }
        protected void RequiredActionExecute_Click(object sender, EventArgs e)
        {
            Response.Redirect(RequiredActionUrl);
        }
        #endregion

        
    }
}