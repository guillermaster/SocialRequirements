using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.Repository.Requirement
{
    public interface IRequirementCommentData
    {
        void Add(RequirementCommentDto requirementComment);

        List<RequirementCommentDto> Get(long requirementId, long companyId, long projectId,
            long requirementVersionId);
    }
}
