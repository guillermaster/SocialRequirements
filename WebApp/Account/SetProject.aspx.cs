using System;
using System.Collections.Generic;
using SocialRequirements.AccountService;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.ProjectService;
using SocialRequirements.Utilities;
using SocialRequirements.Utilities.Security;

namespace SocialRequirements.Account
{
    public partial class SetProject : SocialRequirementsPrivatePage
    {
        #region Global Events
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (Page.IsPostBack) return;
            
            SetCompanies();
        }

        protected void ContinueLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
        #endregion

        #region Create company events
        protected void CreateProjectButton_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            if (string.IsNullOrWhiteSpace(TxtTitle.Text) || string.IsNullOrWhiteSpace(TxtDescription.Text))
            {
                SetErrorMessage("Missing required fields");
                return;
            }

            AddNewProject(TxtTitle.Text, TxtDescription.Text, long.Parse(DdlCompany.SelectedValue));
            SetSuccessMessage("The company was successfully created");
            CreateProjectPanel.Visible = false;
        }

        protected void CancelCreateProjectButton_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Form Setup

        private void SetCompanies()
        {
            var accountSrv = new AccountSoapClient();
            var companiesPerUserRes = accountSrv.GetUserCompanies(GetUsernameEncrypted());

            var serializer = new ObjectSerializer<List<CompanyDto>>();
            var result = serializer.Deserialize(companiesPerUserRes);

            DdlCompany.DataSource = (List<CompanyDto>)result;
            DdlCompany.DataTextField = CustomExpression.GetPropertyName<CompanyDto>(p => p.Name);
            DdlCompany.DataValueField = CustomExpression.GetPropertyName<CompanyDto>(p => p.Id);
            DdlCompany.DataBind();
        }

        private void SetSuccessMessage(string message)
        {
            SuccessMessage.Text = message;
            SuccessPanel.Visible = true;
            ErrorPanel.Visible = false;
            CreateProjectPanel.Visible = false;
        }

        private void SetErrorMessage(string message)
        {
            ErrorMessage.Text = message;
            ErrorPanel.Visible = true;
            SuccessPanel.Visible = false;
            CreateProjectPanel.Visible = true;
            ErrorPanel.Focus();
            ClientScript.RegisterStartupScript(GetType(), "hash", "location.hash = '#ErrorPanel';", true);
        }
        #endregion

        #region Data Update

        private void AddNewProject(string name, string description, long companyId)
        {
            var projectSrv = new ProjectSoapClient();
            projectSrv.AddProject(name, description, companyId, GetUsernameEncrypted());
        }
        #endregion
    }
}