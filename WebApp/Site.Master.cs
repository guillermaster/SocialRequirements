using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.Domain.General;

namespace SocialRequirements
{
    public partial class SiteMaster : MasterPage
    {
        private const string CtrlIdNotificationLink = "NotificationLink";
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
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
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetupRequirementsMenuLinks();
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            //Context.GetOwinContext().Authentication.SignOut();
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
    }

}