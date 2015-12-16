using System;

namespace SocialRequirements.Domain.DTO.Requirement
{
    [Serializable]
    public class RequirementDto
    {
        public RequirementDto()
        {
        }

        public RequirementDto(long companyId, long projectId, string title, string description, long personId)
        {
            CompanyId = companyId;
            ProjectId = projectId;
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

        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long ProjectId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Agreed { get; set; }

        public int Disagreed { get; set; }

        public int StatusId { get; set; }

        public long CreatedbyId { get; set; }

        public DateTime Createdon { get; set; }

        public long ModifiedbyId { get; set; }

        public DateTime Modifiedon { get; set; }

        public long? ApprovedbyId { get; set; }

        public DateTime? Approvedon { get; set; }

        // Requirement version identifier
        public long VersionId { get; set; }

        public int VersionNumber { get; set; }

        // additional helper properties
        public string ShortDescription { get; set; }
    }
}
