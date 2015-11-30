using System;
using System.Web.UI;
using SocialRequirements.CompanyService;
using SocialRequirements.Domain.BusinessLogic.Account;

namespace SocialRequirements.Account
{
    public partial class SetCompany : SocialRequirementsPrivatePage
    {
        #region Global Events
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
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

        }

        protected void CancelCreateCompanyButton_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Set Form

        private void SetCompanies()
        {
            
        }

        private void SetCompanyType()
        {
            var companySrv = new CompanySoapClient();
            var companyTypesRes = companySrv.GetCompanyTypes();

        }
        #endregion
    }
}