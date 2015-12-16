﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Services;
using Ninject;
using Ninject.Web;
using SocialRequirements.Domain.BusinessLogic.Requirement;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Utilities;
using SocialRequirements.Utilities.Security;

namespace WebService
{
    /// <summary>
    /// Summary description for Requirement
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Requirement : WebServiceBase
    {
        [Inject]
        public IRequirementBusiness RequirementBusiness { get; set; }

        [WebMethod]
        public void AddRequirement(string title, string description, long companyId, long projectId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementBusiness.Add(companyId, projectId, title, description, username);
        }

        [WebMethod]
        public void LikeRequirement(long companyId, long projectId, long requirementId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementBusiness.Like(companyId, projectId, requirementId, username);
        }

        [WebMethod]
        public void DislikeRequirement(long companyId, long projectId, long requirementId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementBusiness.Dislike(companyId, projectId, requirementId, username);
        }

        [WebMethod]
        public void CommentOnRequirement(long requirementId, string encUsername, string comment)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementBusiness.Comment(requirementId, username, comment);
        }

        [WebMethod]
        public string GetRequirementsList(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var requirements = RequirementBusiness.GetList(username);
            var serializer = new ObjectSerializer<List<RequirementDto>>(requirements);
            return serializer.ToXmlString();
        }
    }
}
