namespace SocialRequirements.Domain.DTO.Requirement
{
    public class RequirementModificationCommentDto : RequirementCommentDto
    {
        public RequirementModificationCommentDto()
        {
            
        }

        public RequirementModificationCommentDto(long companyId, long projectId, long requirementId, long requirementModifId,
            long requirementModifVersionId, long createdById, string comment)
        {
            CompanyId = companyId;
            ProjectId = projectId;
            RequirementId = requirementId;
            RequirementModificationId = requirementModifId;
            RequirementVersionId = requirementModifVersionId;
            Comment = comment;
            CreatedbyId = createdById;
        }

        public long RequirementModificationId { get; set; }
    }
}
