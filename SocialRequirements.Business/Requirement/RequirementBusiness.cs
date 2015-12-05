using SocialRequirements.Domain.BusinessLogic.Requirement;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.Repository.Account;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.Business.Requirement
{
    public class RequirementBusiness : IRequirementBusiness
    {
        private readonly IPersonData _personData;
        private readonly IRequirementData _requirementData;

        public RequirementBusiness(IPersonData personData, IRequirementData requirementData)
        {
            _personData = personData;
            _requirementData = requirementData;
        }

        public bool HaveRequirements(long companyId)
        {
            var numRequirements = _requirementData.GetNumberOfRequirements(companyId);
            return numRequirements > 0;
        }

        public RequirementDto Add(long companyId, long projectId, string title, string description, string username)
        {
            var personId = _personData.GetPersonId(username);
            var requirement = new RequirementDto(companyId, projectId, title, description, personId);
            requirement.Id = _requirementData.Add(requirement);
            return requirement;
        }
    }
}
