using System;

namespace SocialRequirements.Domain.DTO.Requirement
{
    [Serializable]
    public class RequirementCommentDto
    {
        public RequirementCommentDto()
        {
            
        }

        public RequirementCommentDto(long companyId, long projectId, long requirementId, long requirementVersionId,
            long createdById, string comment)
        {
            CompanyId = companyId;
            ProjectId = projectId;
            RequirementId = requirementId;
            RequirementVersionId = requirementVersionId;
            Comment = comment;
            CreatedbyId = createdById;
        }

        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long ProjectId { get; set; }

        public long RequirementId { get; set; }

        public long RequirementVersionId { get; set; }

        public string Comment { get; set; }

        public long CreatedbyId { get; set; }

        public DateTime Createdon { get; set; }

        // NON-ENTITY PROPERTIES
        public string CreatedByName { get; set; }
    }
}
