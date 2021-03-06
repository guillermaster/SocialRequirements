﻿using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SocialRequirements.CompanyService;
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

            SetCompanies(DdlCompany);
            SetCompanies(CompanyAvailableProject);
            SetAvailableProjects();
        }

        protected void ContinueLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
        #endregion

        #region Set project events

        protected void ProjectDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SetProjectButton.Visible = long.Parse(ProjectDropDownList.SelectedValue) > 0;
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

        protected void CompanyAvailableProject_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SetAvailableProjects();
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
            try
            {
                var companyId = long.Parse(CompanyAvailableProject.SelectedValue);
                ProjectDropDownList.DataSource = GetUnrelatedProjects(companyId);
                ProjectDropDownList.DataTextField = CustomExpression.GetPropertyName<ProjectDto>(p => p.Name);
                ProjectDropDownList.DataValueField = CustomExpression.GetPropertyName<ProjectDto>(p => p.Id);
                ProjectDropDownList.DataBind();
                ProjectDropDownList.Items.Insert(0, new ListItem("- Select -", "0"));
            }
            catch { }
        }
        #endregion

        #region Data Load

        private List<ProjectDto> GetUnrelatedProjects(long companyId)
        {
            var projectSrv = new ProjectSoapClient();
            var projects = projectSrv.GetUnrelatedProjects(companyId);

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