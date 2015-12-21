namespace SocialRequirements.Domain.General
{
    public class GeneralCatalog
    {
        public enum Header
        {
            CompanyType = 1,
            Entity = 2,
            RequirementStatus = 3,
            EntityActions = 4
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
                RequirementModification = 16
            }

            public enum RequirementStatus
            {
                Draft = 5,
                Approved = 6,
                Rejected = 7
            }

            public enum EntityActions
            {
                Create = 8,
                Modify = 10,
                Approve = 11,
                Reject = 12,
                Remove = 13,
                Like = 14,
                Dislike = 15
            }
        }
    }
}
