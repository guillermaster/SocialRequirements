using System;

namespace SocialRequirements.Domain.DTO.Requirement
{
    [Serializable]
    public class RequirementQuestionAnswerDto
    {
        public RequirementQuestionAnswerDto()
        {
            
        }

        public RequirementQuestionAnswerDto(long companyId, long projectId, long requirementId, long requirementVersionId, long requirementQuestionId,
            string answer, int statusId, long personId)
        {
            CompanyId = companyId;
            ProjectId = projectId;
            RequirementId = requirementId;
            RequirementVersionId = requirementVersionId;
            RequirementQuestionId = requirementQuestionId;
            Answer = answer;
            StatusId = statusId;
            CreatedbyId = personId;
            ModifiedbyId = personId;
        }

        public int Id { get; set; }

        public long CompanyId { get; set; }

        public long ProjectId { get; set; }

        public long RequirementId { get; set; }

        public long RequirementVersionId { get; set; }

        public long RequirementQuestionId { get; set; }

        public string Answer { get; set; }

        public long CreatedbyId { get; set; }

        public DateTime Createdon { get; set; }

        public long ModifiedbyId { get; set; }

        public DateTime Modifiedon { get; set; }

        public int StatusId { get; set; }

        //non-db properties

        public string Status { get; set; }

        public string CreatedByName { get; set; }

        public string ModifiedByName { get; set; }
    }
}
