using System;

namespace SocialRequirements.Domain.DTO.Requirement
{
    [Serializable]
    public class RequirementCommentDto
    {
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long ProjectId { get; set; }

        public long RequirementId { get; set; }

        public long RequirementVersionId { get; set; }

        public string Comment { get; set; }

        public long CreatedbyId { get; set; }

        public DateTime Createdon { get; set; }
    }
}
