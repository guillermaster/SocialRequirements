using System.Collections.Generic;
using SocialRequirements.Domain.BusinessLogic.Requirement;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.Business.Requirement
{
    public class RequirementVersionBusiness : IRequirementVersionBusiness
    {
        private readonly IRequirementVersionData _requirementVersionData;

        public RequirementVersionBusiness(IRequirementVersionData requirementVersionData)
        {
            _requirementVersionData = requirementVersionData;
        }

        public List<RequirementDto> GetVersionHistory(long companyId, long projectId, long requirementId)
        {
            return _requirementVersionData.GetVersionHistory(companyId, projectId, requirementId);
        }
    }
}
