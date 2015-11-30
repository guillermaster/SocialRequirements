namespace SocialRequirements.Domain
{
    public class GeneralCatalog
    {
        public enum Header
        {
            CompanyType = 1
        }

        public class Detail
        {
            public enum CompanyType
            {
                SoftwareDeveloper = 1,
                Customer = 3
            }
        }
    }
}
