using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocialRequirements.Domain.General;

namespace SocialRequirements
{
    public partial class SitePopUpMaster : SiteMaster
    {
        private const string CtrlIdNotificationLink = "NotificationLink";
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private const string CtrlIdEntityInstanceLink = "EntityInstanceLink";
        private string _antiXsrfTokenValue;

        protected string SearchResultsFormUrl
        {
            get
            {
                return Request.Url.Scheme + "://" + Request.Url.Authority +
                       VirtualPathUtility.ToAbsolute(CommonConstants.FormsUrl.SearchResults) + "?" +
                       CommonConstants.QueryStringParams.Filter + "=";
            }
        }
        
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
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            
        }
        
        public void RegisterPostBackTrigger(Control control)
        {
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null)
                scriptManager.RegisterPostBackControl(control);
        }


        protected string GetRequirementsListByHashtagUrl(string hashtag)
        {
            return CommonConstants.FormsUrl.RequirementsList + "?" + CommonConstants.QueryStringParams.Filter + "=" +
                   CommonConstants.Filters.Hashtag + "&" + CommonConstants.QueryStringParams.Hashtag + "=" +
                   HttpUtility.UrlEncode(hashtag);
        }

    }

}