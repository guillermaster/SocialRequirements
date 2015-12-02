using SocialRequirements.Domain.BusinessLogic.Requirement;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.Business.Requirement
{
    public class RequirementBusiness : IRequirementBusiness
    {
        private readonly IRequirementData _requirementData;

        public RequirementBusiness(IRequirementData requirementData)
        {
            _requirementData = requirementData;
        }

        public bool HaveRequirements(long companyId)
        {
            var numRequirements = _requirementData.GetNumberOfRequirements(companyId);
            return numRequirements > 0;
        }
    }
}
