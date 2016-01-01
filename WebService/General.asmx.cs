using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Services;
using Ninject;
using Ninject.Web;
using SocialRequirements.Domain.BusinessLogic.General;
using SocialRequirements.Domain.DTO.General;
using SocialRequirements.Utilities;
using SocialRequirements.Utilities.Security;

namespace WebService
{
    /// <summary>
    /// Summary description for General
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class General : WebServiceBase
    {
        [Inject]
        public IActivityFeedBusiness ActivityFeedBusiness { get; set; }

        [WebMethod]
        public string LatestActivityFeed(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var activityFeed = ActivityFeedBusiness.GetLatestActivity(username);
            var serializer = new ObjectSerializer<List<ActivityFeedDto>>(activityFeed);
            return serializer.ToXmlString();
        }

        [WebMethod]
        public string GetActivitiesSummary(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var activityFeed = ActivityFeedBusiness.GetRecentActivitiesSummary(username);
            var serializer = new ObjectSerializer<List<ActivityFeedSummaryDto>>(activityFeed);
            return serializer.ToXmlString();
        }
    }
}
