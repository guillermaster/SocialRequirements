using System;
using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.ProjectService;
using SocialRequirements.Utilities;

namespace SocialRequirements.Account
{
    public partial class SetProject : SocialRequirementsPrivatePage
    {
        #region Global Events
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (Page.IsPostBack) return;

            SetAvailableProjects();
            SetCompanies(DdlCompany);
            SetCompanies(CompanyAvailableProject);
        }

        protected void ContinueLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
        #endregion

        #region Set project events

        protected void ProjectDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SetProjectButton.Visible = false;
        }

        protected void ProjectNotFoundButton_Click(object sender, EventArgs e)
        {
            ChooseProjectPanel.Visible = false;
            CreateProjectPanel.Visible = true;
        }

        protected void SetProjectButton_Click(object sender, EventArgs e)
        {
            try
            {
                var projectSrv = new ProjectSoapClient();
                projectSrv.SetProject(long.Parse(ProjectDropDownList.SelectedValue),
                    long.Parse(CompanyAvailableProject.SelectedValue), GetUsernameEncrypted());

                SetSuccessMessage("The project-company relationship has been successfully created.");
            }
            catch
            {
                SetErrorMessage("An error has occurred");
            }
        }
        #endregion

        #region Create project events
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
            ChooseProjectPanel.Visible = true;
            CreateProjectPanel.Visible = false;
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

        private void SetAvailableProjects()
        {
            ProjectDropDownList.DataSource = GetUnrelatedProjects();
            ProjectDropDownList.DataTextField = CustomExpression.GetPropertyName<ProjectDto>(p => p.Name);
            ProjectDropDownList.DataValueField = CustomExpression.GetPropertyName<ProjectDto>(p => p.Id);
            ProjectDropDownList.DataBind();
        }
        #endregion

        #region Data Load

        private List<ProjectDto> GetUnrelatedProjects()
        {
            var projectSrv = new ProjectSoapClient();
            var projects = projectSrv.GetUnrelatedProjects(GetUsernameEncrypted());

            var serializer = new ObjectSerializer<List<ProjectDto>>();
            return (List<ProjectDto>)serializer.Deserialize(projects);
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