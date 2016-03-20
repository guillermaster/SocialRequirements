using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
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
        [Inject]
        public IRequirementModificationBusiness RequirementModificationBusiness { get; set; }
        [Inject]
        public IRequirementCommentBusiness RequirementCommentBusiness { get; set; }
        [Inject]
        public IRequirementModificationCommentBusiness RequirementModificationCommentBusiness { get; set; }

        [WebMethod(CacheDuration = 0)]
        public void AddRequirement(string title, string description, long companyId, long projectId, string[] hashtags, int priorityId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementBusiness.Add(companyId, projectId, title, description, hashtags, priorityId, username);
        }

        [WebMethod(CacheDuration = 0)]
        public string CommentRequirement(long companyId, long projectId, long requirementId, 
            string comment, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementCommentBusiness.Add(companyId, projectId, requirementId, comment, username);

            return GetRequirementComments(companyId, projectId, requirementId);
        }

        [WebMethod(CacheDuration = 0)]
        public void LikeRequirement(long companyId, long projectId, long requirementId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementBusiness.Like(companyId, projectId, requirementId, username);
        }

        [WebMethod(CacheDuration = 0)]
        public void DislikeRequirement(long companyId, long projectId, long requirementId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementBusiness.Dislike(companyId, projectId, requirementId, username);
        }

        [WebMethod(CacheDuration = 0)]
        public string GetRequirementsList(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var requirements = RequirementBusiness.GetList(username);
            var serializer = new ObjectSerializer<List<RequirementDto>>(requirements);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public string GetRequirementsByHashtag(string hashtag, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var requirements = RequirementBusiness.GetListByHashtag(hashtag, username);
            var serializer = new ObjectSerializer<List<RequirementDto>>(requirements);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public string GetApprovedRequirementsList(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var requirements = RequirementBusiness.GetListApproved(username);
            var serializer = new ObjectSerializer<List<RequirementDto>>(requirements);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public string GetRejectedRequirementsList(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var requirements = RequirementBusiness.GetListRejected(username);
            var serializer = new ObjectSerializer<List<RequirementDto>>(requirements);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public string GetPendingApprovalRequirementsList(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var requirements = RequirementBusiness.GetListPending(username);
            var serializer = new ObjectSerializer<List<RequirementDto>>(requirements);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public string GetDraftRequirementsList(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var requirements = RequirementBusiness.GetListDraft(username);
            var serializer = new ObjectSerializer<List<RequirementDto>>(requirements);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public string GetRequirement(long companyId, long projectId, long requirementId)
        {
            var requirement = RequirementBusiness.Get(companyId, projectId, requirementId);
            var serializer = new ObjectSerializer<RequirementDto>(requirement);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public string GetRequirementComments(long companyId, long projectId, long requirementId)
        {
            var comments = RequirementCommentBusiness.Get(requirementId, companyId, projectId);
            var serializer = new ObjectSerializer<List<RequirementCommentDto>>(comments);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public void ApproveRequirement(long companyId, long projectId, long requirementId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementBusiness.Approve(companyId, projectId, requirementId, username);
        }

        [WebMethod(CacheDuration = 0)]
        public void RejectRequirement(long companyId, long projectId, long requirementId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementBusiness.Reject(companyId, projectId, requirementId, username);
        }

        [WebMethod(CacheDuration = 0)]
        public long AddRequirementModification(string title, string description, long companyId, long projectId,
            long requirementId, string[] hashtagsToAdd, string[] hashtagsToRemove, int priorityId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var requirementModif = RequirementModificationBusiness.Add(companyId, projectId, requirementId, title,
                description, hashtagsToAdd, hashtagsToRemove, priorityId, username);
            return requirementModif.Id;
        }

        [WebMethod(CacheDuration = 0)]
        public string GetRequirementModification(long companyId, long projectId, long requirementId, long requirementModificationId)
        {
            var requirementModif = RequirementModificationBusiness.Get(companyId, projectId, requirementId, requirementModificationId);
            var serializer = new ObjectSerializer<RequirementModificationDto>(requirementModif);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public string GetCurrentRequirementModification(long companyId, long projectId, long requirementId)
        {
            var requirementModif = RequirementModificationBusiness.Get(companyId, projectId, requirementId) ??
                                   new RequirementModificationDto();

            var serializer = new ObjectSerializer<RequirementModificationDto>(requirementModif);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public void UpdateRequirement(string title, string description, long newProjectId, long companyId, long projectId,
            long requirementId, int priorityId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementBusiness.Update(title, description, newProjectId, companyId, projectId, requirementId, priorityId,
                username);
        }

        [WebMethod(CacheDuration = 0)]
        public void UpdateRequirementModification(string title, string description, long companyId, long projectId,
            long requirementId, long requirementModifId, int priorityId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementModificationBusiness.Update(title, description, companyId, projectId, requirementId,
                requirementModifId, priorityId, username);
        }

        [WebMethod(CacheDuration = 0)]
        public void SubmitRequirementForApproval(long companyId, long projectId, long requirementId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementBusiness.SubmitForApproval(companyId, projectId, requirementId, username);
        }

        [WebMethod(CacheDuration = 0)]
        public void SubmitRequirementModificationForApproval(long companyId, long projectId, long requirementId, long requirementModificationId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementModificationBusiness.SubmitForApproval(companyId, projectId, requirementId, requirementModificationId, username);
        }

        [WebMethod(CacheDuration = 0)]
        public void ApproveRequirementModification(long companyId, long projectId, long requirementId, long requirementModificationId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementModificationBusiness.Approve(companyId, projectId, requirementId, requirementModificationId, username);
        }

        [WebMethod(CacheDuration = 0)]
        public void RejectRequirementModification(long companyId, long projectId, long requirementId, long requirementModificationId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementModificationBusiness.Reject(companyId, projectId, requirementId, requirementModificationId, username);
        }

        [WebMethod(CacheDuration = 0)]
        public void LikeRequirementModification(long companyId, long projectId, long requirementId, long requirementModificationId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementModificationBusiness.Like(companyId, projectId, requirementId, requirementModificationId, username);
        }

        [WebMethod(CacheDuration = 0)]
        public void DislikeRequirementModification(long companyId, long projectId, long requirementId, long requirementModificationId, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementModificationBusiness.Dislike(companyId, projectId, requirementId, requirementModificationId, username);
        }

        [WebMethod(CacheDuration = 0)]
        public string CommentRequirementModification(long companyId, long projectId, long requirementId, long requirementModificationId,
            string comment, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            RequirementModificationCommentBusiness.Add(companyId, projectId, requirementId, requirementModificationId, comment, username);

            return GetRequirementModificationComments(companyId, projectId, requirementId, requirementModificationId);
        }

        [WebMethod(CacheDuration = 0)]
        public string GetRequirementModificationComments(long companyId, long projectId, long requirementId,
            long requirementModificationId)
        {
            var comments = RequirementModificationCommentBusiness.Get(companyId, projectId, requirementId, requirementModificationId);
            var serializer = new ObjectSerializer<List<RequirementModificationCommentDto>>(comments);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public string GetRequirementsModificationList(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var requirements = RequirementModificationBusiness.GetList(username);
            var serializer = new ObjectSerializer<List<RequirementModificationDto>>(requirements);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public string GetApprovedRequirementsModificationList(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var requirements = RequirementModificationBusiness.GetListApproved(username);
            var serializer = new ObjectSerializer<List<RequirementModificationDto>>(requirements);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public string GetRejectedRequirementsModificationList(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var requirements = RequirementModificationBusiness.GetListRejected(username);
            var serializer = new ObjectSerializer<List<RequirementModificationDto>>(requirements);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public string GetPendingApprovalRequirementsModificationList(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var requirements = RequirementModificationBusiness.GetListPending(username);
            var serializer = new ObjectSerializer<List<RequirementModificationDto>>(requirements);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public string GetDraftRequirementsModificationList(string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);
            var requirements = RequirementModificationBusiness.GetListDraft(username);
            var serializer = new ObjectSerializer<List<RequirementModificationDto>>(requirements);
            return serializer.ToXmlString();
        }

        [WebMethod(CacheDuration = 0)]
        public void AddAttachment(long companyId, long projectId, long requirementId,
            string fileName, byte[] fileContent, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);

            RequirementBusiness.UploadAttachment(companyId, projectId, requirementId, fileName,
                fileContent, username);
        }

        [WebMethod(CacheDuration = 120)]
        public byte[] GetAttachment(long companyId, long projectId, long requirementId)
        {
            return RequirementBusiness.GetAttachment(companyId, projectId, requirementId);
        }

        [WebMethod(CacheDuration = 0)]
        public void AddModificationAttachment(long companyId, long projectId, long requirementId, long requirementModifId,
            string fileName, byte[] fileContent, string encUsername)
        {
            var username = Encryption.Decrypt(encUsername);

            RequirementModificationBusiness.UploadAttachment(companyId, projectId, requirementId, requirementModifId, fileName,
                fileContent, username);
        }

        [WebMethod(CacheDuration = 0)]
        public byte[] GetModificationAttachment(long companyId, long projectId, long requirementId, long requirementModifId)
        {
            return RequirementModificationBusiness.GetAttachment(companyId, projectId, requirementId, requirementModifId);
        }
    }
}
