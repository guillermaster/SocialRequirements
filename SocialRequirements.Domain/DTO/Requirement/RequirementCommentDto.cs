using System;

namespace SocialRequirements.Domain.DTO.Requirement
{
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
