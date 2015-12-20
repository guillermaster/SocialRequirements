namespace SocialRequirements.Domain.General
{
    public class GeneralCatalog
    {
        public enum Header
        {
            CompanyType = 1,
            Entity = 2,
            RequirementStatus = 3
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
                Requirement = 4
            }

            public enum RequirementStatus
            {
                Draft = 5,
                Approved = 6,
                Rejected = 7
            }
        }
    }
}
