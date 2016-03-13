namespace SocialRequirements.Domain.General
{
    public class CommonConstants
    {
        public class SocialActionsCommands
        {
            public const string Like = "Like";
            public const string Dislike = "Dislike";
            public const string Comment = "Comment";
            public const string ReadMore = "ReadMore";
            public const string ReadEvenMore = "ReadEvenMore";
        }

        public class FormsUrl
        {
            public const string Requirement = "~/Requirements/Requirement.aspx";
            public const string RequirementModification = "~/Requirements/RequirementModify.aspx";
            public const string RequirementsList = "~/Requirements/RequirementsList.aspx";
            public const string RequirementsModificationList = "~/Requirements/RequirementsModificationList.aspx";
            public const string RequirementsQuestionsList = "~/Requirements/RequirementsQuestionsList.aspx";
            public const string RequirementQuestion = "~/Requirements/RequirementQuestion";
            public const string RecentActivities = "~/RecentActivities.aspx";
            public const string SearchResults = "~/SearchResults.aspx";
        }

        public class FormsFileName
        {
            public const string Requirement = "Requirement.aspx";
            public const string RequirementModification = "RequirementModify.aspx";
        }

        public class QueryStringParams
        {
            public const string Id = "id";
            public const string CompanyId = "cid";
            public const string ProjectId = "pid";
            public const string RequirementId = "rid";
            public const string RequirementVersionId = "rvid";
            public const string EntityId = "eid";
            public const string ActionId = "aid";
            public const string Message = "msg";
            public const string Filter = "filter";
            public const string Hashtag = "hashtag";
        }

        public class Filters
        {
            public const string Approved = "approved";
            public const string Rejected = "rejected";
            public const string PendingApproval = "pendingapproval";
            public const string Draft = "draft";
            public const string Answered = "answered";
            public const string Unanswered = "unanswered";
            public const string Hashtag = "hashtag";
        }
    }
}
