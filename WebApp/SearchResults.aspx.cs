using System;
using System.Collections.Generic;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.GeneralService;
using SocialRequirements.Utilities;

namespace SocialRequirements
{
    public partial class SearchResults : SocialRequirementsPrivatePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString.Count > 0)
                LoadSearchResults(Request.QueryString[0]);
        }

        private void LoadSearchResults(string text)
        {
            var generalSrv = new GeneralSoapClient();
            var searchResultsXmlStr = generalSrv.GetSearchResults(text);
            var serializer = new ObjectSerializer<List<SearchResultDto>>();
            SearchResultsListView.DataSource = (List<SearchResultDto>)serializer.Deserialize(searchResultsXmlStr);
            SearchResultsListView.DataBind();
        }
    }
}