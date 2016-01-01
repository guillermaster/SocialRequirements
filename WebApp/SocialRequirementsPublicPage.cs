using System;

namespace SocialRequirements
{
    public class SocialRequirementsPublicPage : SocialRequirementsBasePage
    {
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void InitUserSession(string username)
        {
            Username = username;
            RedirectToPrivateArea();
        }
        
        private void RedirectToPrivateArea()
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}