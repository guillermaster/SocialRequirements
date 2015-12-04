using System;
using System.Collections.Generic;
using SocialRequirements.Domain.DTO;

namespace SocialRequirements
{
    public partial class Default : SocialRequirementsPrivatePage
    {
        #region Properties

        private string RequiredActionUrl 
        {
            get
            {
                return ViewState["RequiredActionUrl"] != null ? ViewState["RequiredActionUrl"].ToString() : string.Empty;
            }
            set { ViewState["RequiredActionUrl"] = value; }
        }

        protected List<CompanyDto> UserCompanies
        {
            get { return ViewState["UserCompanies"] != null ? (List<CompanyDto>)ViewState["UserCompanies"] : new List<CompanyDto>(); }
            set { ViewState["UserCompanies"] = value; }
        }
        #endregion

        #region Main Events
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            RequiredActionPanel.Visible = false;
            if (!CheckRelatedCompanies()) return;
            CheckRequirements();
            PostContent.Visible = true;
        }
        #endregion

        #region Validations
        /// <summary>
        /// Check the related companies to the user, if none found
        /// show an alert and link to relate user to a company
        /// </summary>
        private bool CheckRelatedCompanies()
        {
            UserCompanies = GetRelatedCompanies();

            // check if there is at least one company in the result
            if (UserCompanies.Count > 0)
            {
                return true;
            }

            SetRequiredActionPanel("You are not related to any company. Please select the company you belong to.");
            return false;
        }

        /// <summary>
        /// Check if there is at least one requirement in 
        /// any of the companies related to current user
        /// </summary>
        /// <returns>True if there is at least one requirement, false when none.</returns>
        private void CheckRequirements()
        {
            var haveRequirements = CheckRequirements(UserCompanies);
            if(!haveRequirements)
                SetRequiredActionPanel("There is no requirements yet. You can add one below ;)", true);
        }
        #endregion

        #region Required Action Panel
        private void SetRequiredActionPanel(string message, bool hideActionButton = false)
        {
            RequiredActionMessage.Text = message;
            RequiredActionPanel.Visible = true;
            RequiredActionExecute.Visible = !hideActionButton;
            RequiredActionUrl = "~/Account/SetCompany.aspx";
        }
        protected void RequiredActionExecute_Click(object sender, EventArgs e)
        {
            Response.Redirect(RequiredActionUrl);
        }
        #endregion

        
    }
}