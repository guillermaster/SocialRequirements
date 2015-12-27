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
        }

        public class QueryStringParams
        {
            public const string Id = "id";
            public const string CompanyId = "cid";
            public const string ProjectId = "pid";
            public const string RequirementId = "rid";
            public const string RequirementVersionId = "rvid";
            public const string Message = "msg";
        }
    }
}
