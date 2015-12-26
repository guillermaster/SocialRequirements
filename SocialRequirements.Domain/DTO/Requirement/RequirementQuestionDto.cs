using System;

namespace SocialRequirements.Domain.DTO.Requirement
{
    public class RequirementQuestionDto
    {
        public RequirementQuestionDto()
        {
            
        }

        public RequirementQuestionDto(long companyId, long projectId, long requirementId, long requirementVersionId,
            string question, int statusId, long personId)
        {
            CompanyId = companyId;
            ProjectId = projectId;
            RequirementId = requirementId;
            RequirementVersionId = requirementVersionId;
            Question = question;
            CreatedbyId = personId;
            ModifiedbyId = personId;
            AcceptedAnswerId = null;
            StatusId = statusId;
        }

        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long ProjectId { get; set; }

        public long RequirementId { get; set; }

        public long RequirementVersionId { get; set; }

        public string Question { get; set; }

        public int StatusId { get; set; }

        public long CreatedbyId { get; set; }

        public DateTime Createdon { get; set; }

        public long ModifiedbyId { get; set; }

        public DateTime Modifiedon { get; set; }

        public int? AcceptedAnswerId { get; set; }
    }
}
