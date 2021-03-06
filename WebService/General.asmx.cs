﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Services;
using Ninject;
using Ninject.Web;
using SocialRequirements.Domain.BusinessLogic.General;
using SocialRequirements.Domain.BusinessLogic.Requirement;
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
        [Inject]
        public IRequirementBusiness RequirementBusiness { get; set; }
        [Inject]
        public IRequirementHashtagBusiness RequirementHashtagBusiness { get; set; }
        [Inject]
        public IGeneralCatalogBusiness GeneralCatalogBusiness { get; set; }

        [WebMethod(CacheDuration = 0)]
        public string LatestActivityFeed(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var activityFeed = ActivityFeedBusiness.GetLatestActivity(username);
            var serializer = new ObjectSerializer<List<ActivityFeedDto>>(activityFeed);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 120)]
        public string GetActivitiesSummary(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var activityFeed = ActivityFeedBusiness.GetRecentActivitiesSummary(username);
            var serializer = new ObjectSerializer<List<ActivityFeedSummaryDto>>(activityFeed);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 120)]
        public string GetLatestActivities(long projectId, int entityId, int actionId)
        {
            var activities = ActivityFeedBusiness.GetRecentActivities(projectId, entityId, actionId);
            var serializer = new ObjectSerializer<List<ActivityFeedDto>>(activities);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 120)]
        public string GetAllActivitiesNotifications(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var activities = ActivityFeedBusiness.GetRecentActivities(username);
            var serializer = new ObjectSerializer<List<ActivityFeedDto>>(activities);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 60)]
        public string GetSearchResults(string text)
        {
            var requirements = RequirementBusiness.SearchRequirement(text);

            var serializer = new ObjectSerializer<List<SearchResultDto>>(requirements);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 120)]
        public string GetMostUsedHashtags(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var topHashtags = RequirementHashtagBusiness.GetMostUsedHashtags(20);
            var serializer = new ObjectSerializer<List<string>>(topHashtags);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 320)]
        public string GetCatalog(int catalogHeaderId)
        {
            var catalog = GeneralCatalogBusiness.Get(catalogHeaderId);
            var serializer = new ObjectSerializer<List<GeneralCatalogDetailDto>>(catalog);
            return serializer.ToXmlString();
        }
    }
}
