using System;

namespace SocialRequirements.Domain.DTO.Requirement
{
    [Serializable]
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
        
        // non-db properties

        public string RequirementTitle { get; set; }

        public string ProjectName { get; set; }

        public string Status { get; set; }

        public string CreatedByName { get; set; }

        public string ModifiedByName { get; set; }

        public int AnswersQuantity { get; set; }
    }
}
