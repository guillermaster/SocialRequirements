using System;
using System.Collections.Generic;
using SocialRequirements.AccountService;
using SocialRequirements.CompanyService;
using SocialRequirements.Domain.DTO;
using SocialRequirements.Utilities;
using SocialRequirements.Utilities.Security;

namespace SocialRequirements.Account
{
    public partial class SetCompany : SocialRequirementsPrivatePage
    {
        #region Global Events
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (Page.IsPostBack) return;

            SetCompanies();
            SetCompanyType();
        }

        protected void ContinueLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
        #endregion

        #region Set existent company events
        protected void SetCompanyButton_Click(object sender, EventArgs e)
        {

        }

        protected void CompanyNotFoundButton_Click(object sender, EventArgs e)
        {
            ChooseCompanyPanel.Visible = false;
            CreateCompanyPanel.Visible = true;
        }
        #endregion

        #region Create company events
        protected void CreateCompanyButton_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            if (string.IsNullOrWhiteSpace(Name.Text) || CompanyType.SelectedItem == null)
            {
                SetErrorMessage("Missing required fields");
                return;
            }

            AddNewCompany(Name.Text, int.Parse(CompanyType.SelectedValue));
            SetSuccessMessage("The company was successfully created");
            ChooseCompanyPanel.Visible = false;
            CreateCompanyPanel.Visible = false;
        }

        protected void CancelCreateCompanyButton_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Form Setup

        private void SetCompanies()
        {
            var accountSrv = new AccountSoapClient();
            var companiesPerUserRes = accountSrv.GetUserCompanies(Encryption.Encrypt(Username));

            var serializer = new ObjectSerializer<List<GeneralCatalogDetailDto>>();
            var result = serializer.Deserialize(companiesPerUserRes);

            CompaniesDropDownList.DataSource = (List<CompanyDto>)result;
            CompaniesDropDownList.DataTextField = CustomExpression.GetPropertyName<CompanyDto>(p => p.Name);
            CompaniesDropDownList.DataValueField = CustomExpression.GetPropertyName<CompanyDto>(p => p.Id);
            CompaniesDropDownList.DataBind();
        }

        private void SetCompanyType()
        {
            var companySrv = new CompanySoapClient();
            var companyTypesRes = companySrv.GetCompanyTypes();

            var serializer = new ObjectSerializer<List<GeneralCatalogDetailDto>>();
            var result = serializer.Deserialize(companyTypesRes);

            CompanyType.DataSource = (List<GeneralCatalogDetailDto>)result;
            CompanyType.DataTextField = CustomExpression.GetPropertyName<GeneralCatalogDetailDto>(p => p.Name);
            CompanyType.DataValueField = CustomExpression.GetPropertyName<GeneralCatalogDetailDto>(p => p.Id);
            CompanyType.DataBind();
        }

        private void SetSuccessMessage(string message)
        {
            SuccessMessage.Text = message;
            SuccessPanel.Visible = true;
            ErrorPanel.Visible = false;
            ChooseCompanyPanel.Visible = false;
            CreateCompanyPanel.Visible = false;
        }

        private void SetErrorMessage(string message)
        {
            ErrorMessage.Text = message;
            ErrorPanel.Visible = true;
            SuccessPanel.Visible = false;
            ErrorPanel.Focus();
            ClientScript.RegisterStartupScript(GetType(), "hash", "location.hash = '#ErrorPanel';", true);
        }
        #endregion

        #region Data Update

        private void AddNewCompany(string name, int type)
        {
            var companySrv = new CompanySoapClient();
            companySrv.AddCompany(name, type, Encryption.Encrypt(Username));
        }
        #endregion
    }
}