using System;
using SocialRequirements.Domain.General;

namespace SocialRequirements.Domain.DTO.Requirement
{
    [Serializable]
    public class RequirementModificationDto : RequirementDto
    {
        public RequirementModificationDto()
        {
        }

        public RequirementModificationDto(long companyId, long projectId, long requirementId, string title, string description, long personId)
        {
            CompanyId = companyId;
            ProjectId = projectId;
            RequirementId = requirementId;
            Title = title;
            Description = description;
            Agreed = 0;
            Disagreed = 0;
            StatusId = (int) GeneralCatalog.Detail.RequirementStatus.Draft;
            CreatedbyId = personId;
            ModifiedbyId = personId;
            Createdon = DateTime.Now;
            Modifiedon = DateTime.Now;
            ApprovedbyId = null;
            Approvedon = null;
        }

        public long RequirementId { get; set; }
    }
}
