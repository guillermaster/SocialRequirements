using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.Domain.General;
using SocialRequirements.GeneralService;
using SocialRequirements.Utilities;

namespace SocialRequirements
{
    public partial class SiteMaster : MasterPage
    {
        private const string CtrlIdNotificationLink = "NotificationLink";
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private const string CtrlIdEntityInstanceLink = "EntityInstanceLink";
        private string _antiXsrfTokenValue;
        
        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? string.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? string.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
            LoadActivityFeed();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetupRequirementsMenuLinks();
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            
        }

        public UpdatePanel GetUpdatePanel()
        {
            return MainUpdatePanel;
        }

        public Panel GetSucccessMessageParentPanel()
        {
            return PostSuccessPanelMaster;
        }

        public Label GetSuccessMessage()
        {
            return PostSuccessMessageMaster;
        }

        public Label GetErrorExceptionMessage()
        {
            return ErrorMessage;
        }

        public HyperLink GetErrorExceptionLink()
        {
            return ViewExceptionButtonLink;
        }

        public Panel GetErrorMessageParentPanel()
        {
            return PostErrorPanelMaster;
        }

        public Repeater Notifications
        {
            get { return NotificationsRepeater; }
            set { NotificationsRepeater = value; }
        }

        public Label NotificationsQuantityLabel
        {
            get { return NotificationsQuantity; }
            set { NotificationsQuantity = value; }
        }

        public Label GetErrorMessage()
        {
            return PostErrorMessageMaster;
        }

        public void SetUserFullName(string fullname)
        {
            UserFullName.Text = fullname;
        }

        protected void SignOutButton_OnClick(object sender, EventArgs e)
        {
            Logout();
        }

        protected void Logout()
        {
            Session.Clear();
            RedirectToLogin();
        }

        public void RedirectToLogin()
        {
            Response.Redirect("~/Account/Login.aspx");
        }

        protected void NotificationsRepeater_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item) return;

            var activitySumm = (ActivityFeedSummaryDto)e.Item.DataItem;
            var notifLink = (HyperLink)e.Item.FindControl(CtrlIdNotificationLink);

            notifLink.NavigateUrl = CommonConstants.FormsUrl.RecentActivities + "?" +
                                    CommonConstants.QueryStringParams.ProjectId + "=" + activitySumm.ProjectId + "&" +
                                    CommonConstants.QueryStringParams.EntityId + "=" + activitySumm.EntityId + "&" +
                                    CommonConstants.QueryStringParams.ActionId + "=" + activitySumm.ActionId;
        }

        private void SetupRequirementsMenuLinks()
        {
            // requirements links
            RequirementsAllLink.NavigateUrl = CommonConstants.FormsUrl.RequirementsList;

            RequirementsApprovedLink.NavigateUrl = CommonConstants.FormsUrl.RequirementsList + "?" +
                                                   CommonConstants.QueryStringParams.Filter + "=" +
                                                   CommonConstants.Filters.Approved;

            RequirementsRejectedLink.NavigateUrl = CommonConstants.FormsUrl.RequirementsList + "?" +
                                                   CommonConstants.QueryStringParams.Filter + "=" +
                                                   CommonConstants.Filters.Rejected;

            RequirementsPendingLink.NavigateUrl = CommonConstants.FormsUrl.RequirementsList + "?" +
                                                   CommonConstants.QueryStringParams.Filter + "=" +
                                                   CommonConstants.Filters.PendingApproval;

            RequirementsDraftLink.NavigateUrl = CommonConstants.FormsUrl.RequirementsList + "?" +
                                                   CommonConstants.QueryStringParams.Filter + "=" +
                                                   CommonConstants.Filters.Draft;

            // requirements modification links

            RequirementsModifAllLink.NavigateUrl = CommonConstants.FormsUrl.RequirementsModificationList;

            RequirementsModifApprovedLink.NavigateUrl = CommonConstants.FormsUrl.RequirementsModificationList + "?" +
                                                   CommonConstants.QueryStringParams.Filter + "=" +
                                                   CommonConstants.Filters.Approved;

            RequirementsModifRejectedLink.NavigateUrl = CommonConstants.FormsUrl.RequirementsModificationList + "?" +
                                                   CommonConstants.QueryStringParams.Filter + "=" +
                                                   CommonConstants.Filters.Rejected;

            RequirementsModifPendingLink.NavigateUrl = CommonConstants.FormsUrl.RequirementsModificationList + "?" +
                                                   CommonConstants.QueryStringParams.Filter + "=" +
                                                   CommonConstants.Filters.PendingApproval;

            RequirementsModifDraftLink.NavigateUrl = CommonConstants.FormsUrl.RequirementsModificationList + "?" +
                                                   CommonConstants.QueryStringParams.Filter + "=" +
                                                   CommonConstants.Filters.Draft;

            // requirements questions links

            RequirementsQuestionsAll.NavigateUrl = CommonConstants.FormsUrl.RequirementsQuestionsList;

            RequirementsQuestionsAnswered.NavigateUrl = CommonConstants.FormsUrl.RequirementsQuestionsList + "?" +
                                                        CommonConstants.QueryStringParams.Filter + "=" +
                                                        CommonConstants.Filters.Answered;

            RequirementsQuestionsUnanswered.NavigateUrl = CommonConstants.FormsUrl.RequirementsQuestionsList + "?" +
                                                        CommonConstants.QueryStringParams.Filter + "=" +
                                                        CommonConstants.Filters.Unanswered;
        }

        public void HideInfobarToggleButton()
        {
            ToggleInfobarLink.Visible = false;
        }

        public void ShowInfobarToggleButton()
        {
            ToggleInfobarLink.Visible = true;
        }

        protected void RecentActivityFeedRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item) return;

            var activity = (ActivityFeedDto)e.Item.DataItem;
            var link = (HyperLink)e.Item.FindControl(CtrlIdEntityInstanceLink);
            var privatePage = new SocialRequirementsPrivatePage();

            switch (activity.EntityId)
            {
                case (int)GeneralCatalog.Detail.Entity.Requirement:
                    if (activity.ProjectId.HasValue)
                        link.NavigateUrl = privatePage.GetUrlForRequirement(activity.CompanyId, activity.ProjectId.Value, activity.RecordId);
                    break;

                case (int)GeneralCatalog.Detail.Entity.RequirementModification:
                    if (activity.ProjectId.HasValue && activity.ParentId.HasValue)
                    {
                        link.NavigateUrl = privatePage.GetUrlForRequirementModification(activity.CompanyId, activity.ProjectId.Value,
                            activity.ParentId.Value, activity.RecordId);
                    }
                    break;

                case (int)GeneralCatalog.Detail.Entity.RequirementQuestion:
                case (int)GeneralCatalog.Detail.Entity.RequirementQuestionAnswer:
                    if (activity.ProjectId.HasValue && activity.ParentId.HasValue && activity.GrandparentId.HasValue)
                    {
                        link.NavigateUrl = privatePage.GetUrlForRequirementQuestion(activity.CompanyId, activity.ProjectId.Value,
                            activity.ParentId.Value, activity.GrandparentId.Value, activity.RecordId);
                    }
                    break;

                case (int)GeneralCatalog.Detail.Entity.RequirementComment:
                    if (activity.ProjectId.HasValue)
                        link.NavigateUrl = privatePage.GetUrlForRequirement(activity.CompanyId, activity.ProjectId.Value, activity.RecordId);
                    break;

                case (int)GeneralCatalog.Detail.Entity.RequirementModificationComment:
                    if (activity.ProjectId.HasValue && activity.ParentId.HasValue)
                    {
                        link.NavigateUrl = privatePage.GetUrlForRequirementModification(activity.CompanyId, activity.ProjectId.Value,
                            activity.ParentId.Value, activity.RecordId);
                    }
                    break;
            }
        }

        private void LoadActivityFeed()
        {
            var privatePage = new SocialRequirementsPrivatePage();
            if (!privatePage.UserLoggedIn()) return;

            if (privatePage.ActivityFeed == null || privatePage.ActivityFeed.Count == 0)
            {
                var generalSrv = new GeneralSoapClient();
                var activityFeedXmlStr = generalSrv.LatestActivityFeed(privatePage.GetUsernameEncrypted());
                var serializer = new ObjectSerializer<List<ActivityFeedDto>>();
                privatePage.ActivityFeed = (List<ActivityFeedDto>)serializer.Deserialize(activityFeedXmlStr);
            }
            RecentActivityFeedRepeater.DataSource = privatePage.ActivityFeed;
            RecentActivityFeedRepeater.DataBind();
        }

        public void RegisterPostBackTrigger(Control control)
        {
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null)
                scriptManager.RegisterPostBackControl(control);
        }
    }

}