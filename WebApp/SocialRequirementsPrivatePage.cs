﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocialRequirements.AccountService;
using SocialRequirements.CompanyService;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.Domain.General;
using SocialRequirements.GeneralService;
using SocialRequirements.ProjectService;
using SocialRequirements.RequirementService;
using SocialRequirements.Utilities;
using SocialRequirements.Utilities.Security;

namespace SocialRequirements
{
    public class SocialRequirementsPrivatePage : SocialRequirementsBasePage
    {
        public List<ActivityFeedDto> ActivityFeed
        {
            get { return Session["ActivityFeed"] != null ? (List<ActivityFeedDto>)Session["ActivityFeed"] : new List<ActivityFeedDto>(); }
            set { Session["ActivityFeed"] = value; }
        }


        /// <summary>
        /// Stores all permissions IDs for current user per each project
        /// </summary>
        protected List<ProjectPermissionsDto> PermissionsByProject
        {
            get
            {
                return Session["PermissionsByProject"] != null
                    ? (List<ProjectPermissionsDto>)Session["PermissionsByProject"]
                    : null;
            }
            set { Session["PermissionsByProject"] = value; }
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            ValidateUserLoggedIn();
            SetUserData();
            SetUserFullName();
            LoadNotifications();
            ShowInfobarToggleButton();
            LoadPermissionsPerProject();
        }

        protected void RegisterJsBeforePostback(string functionName)
        {
            var siteMaster = (SiteMaster)Master;
            if (siteMaster != null) siteMaster.RegisterJsFunctionBeforePostback(this, functionName);
        }

        protected void ValidateUserLoggedIn()
        {
            if (UserLoggedIn()) return;
            var siteMaster = (SiteMaster)Master;
            if (siteMaster != null) siteMaster.RedirectToLogin();
        }

        protected void LoadPermissionsPerProject()
        {
            if (PermissionsByProject != null) return;

            var accountService = new AccountSoapClient();
            var permissionsStr = accountService.GetPermissionsPerProject(Encryption.Encrypt(Username));
            var serializer = new ObjectSerializer<List<ProjectPermissionsDto>>();
            PermissionsByProject = (List<ProjectPermissionsDto>)serializer.Deserialize(permissionsStr);
        }

        protected bool HasPermission(long projectId, Permissions.Codes permission)
        {
            if (PermissionsByProject == null) return false;
            var projectPerms = PermissionsByProject.Where(p => p.ProjectId == projectId).ToList();
            return projectPerms.Any(projectPerm => projectPerm.PermissionsIds.Contains((int) permission));
        }

        protected void RegisterTrigger(Control control)
        {
            if (!UserLoggedIn()) return;
            var siteMaster = (SiteMaster)Master;
            if (siteMaster != null) siteMaster.RegisterPostBackTrigger(control);
        }

        protected void HideInfobarToggleButton()
        {
            var siteMaster = (SiteMaster)Master;
            if (siteMaster != null) siteMaster.HideInfobarToggleButton();
        }

        protected void ShowInfobarToggleButton()
        {
            if (UserLoggedIn()) return;
            var siteMaster = (SiteMaster)Master;
            if (siteMaster != null) siteMaster.ShowInfobarToggleButton();
        }

        /// <summary>
        /// Stores in session the user data
        /// </summary>
        protected void SetUserData()
        {
            if (!string.IsNullOrWhiteSpace(UserData)) return;

            var userSrv = new AccountSoapClient();
            UserData = userSrv.GetUserData(GetUsernameEncrypted());
        }

        /// <summary>
        /// Stores in session the user data
        /// </summary>
        protected PersonDto GetUserData()
        {
            if (string.IsNullOrWhiteSpace(UserData)) return null;

            var serializer = new ObjectSerializer<PersonDto>();
            return (PersonDto)serializer.Deserialize(UserData);
        }

        /// <summary>
        /// Sets the full name label in the left column of the web site (master page)
        /// </summary>
        protected void SetUserFullName()
        {
            var siteMaster = (SiteMaster)Master;
            if (siteMaster == null) return;

            var persondata = GetUserData();
            if (persondata == null) return;
            siteMaster.SetUserFullName(persondata.FirstName + " " + persondata.LastName);
        }

        private void LoadNotifications()
        {
            var siteMaster = (SiteMaster)Master;
            if (siteMaster == null) return;
            if (siteMaster.Notifications == null) return;

            var generalSrv = new GeneralSoapClient();
            var activitySummaryStr = generalSrv.GetActivitiesSummary(GetUsernameEncrypted());

            var serializer = new ObjectSerializer<List<ActivityFeedSummaryDto>>();
            var activitySummary = (List<ActivityFeedSummaryDto>)serializer.Deserialize(activitySummaryStr);

            siteMaster.Notifications.DataSource = activitySummary;
            siteMaster.Notifications.DataBind();
            siteMaster.NotificationsQuantityLabel.Text = activitySummary.Count.ToString();
        }

        /// <summary>
        /// Check if there is at least one requirement for the specified companies
        /// </summary>
        /// <returns>True if there is at least one requirement, false when none.</returns>
        protected bool CheckRequirements(List<CompanyDto> companies)
        {
            var companySrv = new CompanySoapClient();
            return companies.All(company => companySrv.HaveRequirements(company.Id));
        }

        /// <summary>
        /// Get related companies to the current user
        /// </summary>
        protected List<CompanyDto> GetRelatedCompanies()
        {
            if (!UserLoggedIn()) return new List<CompanyDto>();

            var personService = new AccountSoapClient();
            var encUsername = GetUsernameEncrypted();
            var companies = personService.GetUserCompanies(encUsername);

            var serializer = new ObjectSerializer<List<CompanyDto>>();
            return (List<CompanyDto>)serializer.Deserialize(companies);
        }

        /// <summary>
        /// Loads all projects by company and store them in a ViewState var
        /// </summary>
        protected List<ProjectDto> GetProjectsByCompany(long companyId)
        {
            var projectSrv = new ProjectSoapClient();
            var projectXmlStr = projectSrv.GetByCompany(companyId);
            var serializer = new ObjectSerializer<List<ProjectDto>>();
            var projects = (List<ProjectDto>)serializer.Deserialize(projectXmlStr);
            return projects.OrderByDescending(p => p.Id).ToList();
        }

        /// <summary>
        /// Loads all projects by company and store them in a ViewState var
        /// </summary>
        protected List<ProjectDto> GetProjectsByCompanies(List<CompanyDto> companies)
        {
            var projectSrv = new ProjectSoapClient();
            var companiesId = new ArrayOfLong();
            companiesId.AddRange(companies.Select(company => company.Id));

            var projectXmlStr = projectSrv.GetByCompanies(companiesId);
            var serializer = new ObjectSerializer<List<ProjectDto>>();
            var projects = (List<ProjectDto>)serializer.Deserialize(projectXmlStr);
            return projects.OrderByDescending(p => p.Id).ToList();
        }

        protected List<GeneralCatalogDetailDto> GetCatalogOptions(GeneralCatalog.Header catalogHeader)
        {
            var generalSrv = new GeneralSoapClient();
            var catalogOptionsStr = generalSrv.GetCatalog((int) catalogHeader);
            var serializer = new ObjectSerializer<List<GeneralCatalogDetailDto>>();
            return (List<GeneralCatalogDetailDto>) serializer.Deserialize(catalogOptionsStr);
        }

        /// <summary>
        /// Check if there is at least one project for the specified companies
        /// </summary>
        /// <returns>True if there is at least one project, false when none.</returns>
        protected bool CheckProjects(List<CompanyDto> companies)
        {
            var companySrv = new CompanySoapClient();
            return companies.All(company => companySrv.HaveProjects(company.Id));
        }

        public string GetUsernameEncrypted()
        {
            return Encryption.Encrypt(Username);
        }

        /// <summary>
        /// Sets the datasource for a company list control
        /// </summary>
        /// <param name="companyList">Control to update</param>
        protected void SetCompanies(ListControl companyList)
        {
            var companySrv = new CompanySoapClient();
            var companies = companySrv.GetRelatedCompanies(GetUsernameEncrypted());

            var serializer = new ObjectSerializer<List<CompanyDto>>();
            var result = serializer.Deserialize(companies);

            companyList.DataSource = (List<CompanyDto>)result;
            companyList.DataTextField = CustomExpression.GetPropertyName<CompanyDto>(p => p.Name);
            companyList.DataValueField = CustomExpression.GetPropertyName<CompanyDto>(p => p.Id);
            companyList.DataBind();
            companyList.SelectedIndex = 0;
        }

        /// <summary>
        /// Sets the datasource for a company list control
        /// </summary>
        /// <param name="companyList">Control to update</param>
        protected void SetAllCompanies(ListControl companyList)
        {
            var companySrv = new CompanySoapClient();
            var companies = companySrv.GetAllCompanies();

            var serializer = new ObjectSerializer<List<CompanyDto>>();
            var result = serializer.Deserialize(companies);

            companyList.DataSource = (List<CompanyDto>)result;
            companyList.DataTextField = CustomExpression.GetPropertyName<CompanyDto>(p => p.Name);
            companyList.DataValueField = CustomExpression.GetPropertyName<CompanyDto>(p => p.Id);
            companyList.DataBind();
            companyList.Items.Insert(0, new ListItem("- Select Company -", string.Empty));
            companyList.SelectedIndex = 0;
        }

        protected void ReadEvenMore(long companyId, long? projectId, long recordId, int entity)
        {
            switch (entity)
            {
                case (int)GeneralCatalog.Detail.Entity.Requirement:
                    Response.Redirect(CommonConstants.FormsUrl.Requirement + "?" +
                                      CommonConstants.QueryStringParams.CompanyId + "=" + companyId + "&" +
                                      CommonConstants.QueryStringParams.ProjectId + "=" + projectId + "&" +
                                      CommonConstants.QueryStringParams.Id + "=" + recordId);
                    break;
            }
        }

        protected void Like(long companyId, long? projectId, long recordId, int entity, long? parentId = null)
        {
            switch (entity)
            {
                case (int)GeneralCatalog.Detail.Entity.Requirement:
                    if (!projectId.HasValue) return;
                    var requirementSrv = new RequirementSoapClient();
                    requirementSrv.LikeRequirement(companyId, projectId.Value, recordId, GetUsernameEncrypted());
                    break;
                case (int)GeneralCatalog.Detail.Entity.RequirementModification:
                    if (!projectId.HasValue || !parentId.HasValue) return;
                    var requirementModifSrv = new RequirementSoapClient();
                    requirementModifSrv.LikeRequirementModification(companyId, projectId.Value, parentId.Value, recordId,
                        GetUsernameEncrypted());
                    break;
            }
        }

        protected void Dislike(long companyId, long? projectId, long recordId, int entity, long? parentId = null)
        {
            switch (entity)
            {
                case (int)GeneralCatalog.Detail.Entity.Requirement:
                    if (!projectId.HasValue) return;
                    var requirementSrv = new RequirementSoapClient();
                    requirementSrv.DislikeRequirement(companyId, projectId.Value, recordId, GetUsernameEncrypted());
                    break;
                case (int)GeneralCatalog.Detail.Entity.RequirementModification:
                    if (!projectId.HasValue || !parentId.HasValue) return;
                    var requirementModifSrv = new RequirementSoapClient();
                    requirementModifSrv.DislikeRequirementModification(companyId, projectId.Value, parentId.Value, recordId,
                        GetUsernameEncrypted());
                    break;
            }
        }

        protected void SetFadeOutMessage(string message, bool success, string exception = null)
        {
            var messageLabel = GetMessageControl(this, success);
            messageLabel.Text = message;

            var parentPanel = GetMessageParentPanel(this, success);
            parentPanel.Visible = true;

            var viewExceptionButton = GetViewErrorExceptionLink(this);
            viewExceptionButton.Visible = false;

            if (!string.IsNullOrWhiteSpace(exception))
            {
                var exceptionLabel = GetErrorExceptionMessageControl(this);
                exceptionLabel.Text = exception;
                viewExceptionButton.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(GetMainUpdatePanel(this), parentPanel.GetType(),
                    "posterrorfadeout", "fadeOutControl('#" + parentPanel.ID + "')", true);
            }
        }

        protected void SetFadeOutMessage(UpdatePanel updatePanel, Panel parentPanel, Label messageLabel, string message)
        {
            messageLabel.Text = message;
            parentPanel.Visible = true;
            ScriptManager.RegisterClientScriptBlock(updatePanel, updatePanel.GetType(),
                "posterrorfadeout", "fadeOutControl('#" + parentPanel.ID + "')", true);
        }

        protected UpdatePanel GetMainUpdatePanel(Page page)
        {
            var masterPage = (SiteMaster)page.Master;
            if (masterPage == null) throw new InvalidDataException("Invalid update panel or not found.");
            return masterPage.GetUpdatePanel();
        }

        protected Panel GetMessageParentPanel(Page page, bool success)
        {
            var masterPage = (SiteMaster)page.Master;
            if (masterPage == null) throw new InvalidDataException("Invalid master page or not found.");
            return success ? masterPage.GetSucccessMessageParentPanel() : masterPage.GetErrorMessageParentPanel();
        }

        protected Label GetMessageControl(Page page, bool success)
        {
            var masterPage = (SiteMaster)page.Master;
            if (masterPage == null) throw new InvalidDataException("Invalid master page or not found.");
            return success ? masterPage.GetSuccessMessage() : masterPage.GetErrorMessage();
        }

        protected Label GetErrorExceptionMessageControl(Page page)
        {
            var masterPage = (SiteMaster)page.Master;
            if (masterPage == null) throw new InvalidDataException("Invalid master page or not found.");
            return masterPage.GetErrorExceptionMessage();
        }

        protected HyperLink GetViewErrorExceptionLink(Page page)
        {
            var masterPage = (SiteMaster)page.Master;
            if (masterPage == null) throw new InvalidDataException("Invalid master page or not found.");
            return masterPage.GetErrorExceptionLink();
        }

        public string GetUrlForRequirement(long companyId, long projectId, long requirementId)
        {
            var url = CommonConstants.FormsUrl.Requirement + "?" + CommonConstants.QueryStringParams.Id + "=" +
                      requirementId + "&" + CommonConstants.QueryStringParams.CompanyId + "=" + companyId + "&" +
                      CommonConstants.QueryStringParams.ProjectId + "=" + projectId;
            return url;
        }

        public string GetUrlForRequirementModification(long companyId, long projectId, long requirementId, long requirementModificationId)
        {
            var url = CommonConstants.FormsUrl.RequirementModification + "?" + CommonConstants.QueryStringParams.Id + "=" +
                      requirementModificationId + "&" + CommonConstants.QueryStringParams.CompanyId + "=" + companyId + "&" +
                      CommonConstants.QueryStringParams.ProjectId + "=" + projectId + "&" +
                      CommonConstants.QueryStringParams.RequirementId + "=" + requirementId;
            return url;
        }

        public string GetUrlForRequirementQuestion(long companyId, long projectId, long requirementId,
            long requirementVersionId, long requirementQuestionId)
        {
            var url = CommonConstants.FormsUrl.RequirementQuestion + "?" + CommonConstants.QueryStringParams.Id + "=" +
                      requirementQuestionId + "&" + CommonConstants.QueryStringParams.CompanyId + "=" + companyId + "&" +
                      CommonConstants.QueryStringParams.ProjectId + "=" + projectId + "&" +
                      CommonConstants.QueryStringParams.RequirementId + "=" + requirementId + "&" +
                      CommonConstants.QueryStringParams.RequirementVersionId + "=" + requirementVersionId;
            return url;
        }

        protected string GetRequirementsListByHashtagUrl(string hashtag)
        {
            return CommonConstants.FormsUrl.RequirementsList + "?" + CommonConstants.QueryStringParams.Filter + "=" +
                   CommonConstants.Filters.Hashtag + "&" + CommonConstants.QueryStringParams.Hashtag + "=" +
                   HttpUtility.UrlEncode(hashtag);
        }
    }
}