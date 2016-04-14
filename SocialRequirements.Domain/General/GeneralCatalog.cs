namespace SocialRequirements.Domain.General
{
    public class GeneralCatalog
    {
        public enum Header
        {
            CompanyType = 1,
            Entity = 2,
            RequirementStatus = 3,
            EntityActions = 4,
            RequirementQuestionStatus = 5,
            RequirementQuestionAnswerStatus = 6,
            RequirementPriority = 8,
            RequirementDevelopment = 9
        }

        public class Detail
        {
            public enum CompanyType
            {
                SoftwareDeveloper = 1,
                Customer = 3
            }

            public enum Entity
            {
                Requirement = 4,
                RequirementModification = 16,
                RequirementComment = 19,
                RequirementModificationComment = 20,
                RequirementQuestion = 21,
                RequirementQuestionAnswer = 22,
                Project = 28,
                CompanyProject = 29
            }

            public enum RequirementStatus
            {
                Draft = 5,
                Approved = 6,
                Rejected = 7,
                PendingApproval = 17
            }

            public enum SoftwareDevelopmentStatus
            {
                UnderDevelopment = 37,
                UnderTesting = 38,
                Deployed = 39,
                PendingDevelopment = 40
            }

            public enum RequirementQuestionStatus
            {
                Posted = 23,
                Answered = 24
            }

            public enum RequirementQuestionAnswerStatus
            {
                Posted = 25,
                Approved = 27
            }

            public enum EntityActions
            {
                Create = 8,
                Modify = 10,
                Approve = 11,
                Reject = 12,
                Remove = 13,
                Like = 14,
                Dislike = 15,
                SubmitForApproval = 18,
                UploadAttachment = 30
            }

            public enum HashtagsStatus
            {
                Approved = 31,
                New = 32,
                Remove = 33
            }

            public enum RequirementPriority
            {
                Low = 34,
                Medium = 35,
                High = 36
            }
        }
    }
}
