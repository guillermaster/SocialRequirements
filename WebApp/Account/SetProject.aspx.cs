using System;
using SocialRequirements.ProjectService;

namespace SocialRequirements.Account
{
    public partial class SetProject : SocialRequirementsPrivatePage
    {
        #region Global Events
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (Page.IsPostBack) return;
            
            SetCompanies(DdlCompany);
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