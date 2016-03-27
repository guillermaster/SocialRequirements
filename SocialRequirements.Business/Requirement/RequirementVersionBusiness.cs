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

        public RequirementDto Get(long companyId, long projectId, long requirementId, long? requirementVersionId = null)
        {
            return _requirementVersionData.Get(companyId, projectId, requirementId, requirementVersionId);
        }
    }
}
