using System;
using SocialRequirements.Domain.General;

namespace SocialRequirements.Domain.DTO.Requirement
{
    [Serializable]
    public class RequirementDto
    {
        public RequirementDto()
        {
        }

        public RequirementDto(long companyId, long projectId, string title, string description, string[] hashtags, int priorityId, long personId)
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
            Hashtags = hashtags;
            PriorityId = priorityId;
        }

        public RequirementDto(RequirementModificationDto requirementModification)
        {
            Id = requirementModification.RequirementId;
            CompanyId = requirementModification.CompanyId;
            ProjectId = requirementModification.ProjectId;
            Title = requirementModification.Title;
            Description = requirementModification.Description;
            Agreed = requirementModification.Agreed;
            Disagreed = requirementModification.Disagreed;
            CommentsQuantity = requirementModification.CommentsQuantity;
            StatusId = requirementModification.StatusId;
            CreatedbyId = requirementModification.CreatedbyId;
            Createdon = requirementModification.Createdon;
            ModifiedbyId = requirementModification.ModifiedbyId;
            Modifiedon = requirementModification.Modifiedon;
            ApprovedbyId = requirementModification.ApprovedbyId;
            Approvedon = requirementModification.Approvedon;
            VersionId = requirementModification.VersionId;
            VersionNumber = requirementModification.VersionNumber;
            PriorityId = requirementModification.PriorityId;
        }

        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long ProjectId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Agreed { get; set; }

        public int Disagreed { get; set; }

        public int CommentsQuantity { get; set; }

        public int StatusId { get; set; }

        public int? DevelopmentStatusId { get; set; }

        public long CreatedbyId { get; set; }

        public DateTime Createdon { get; set; }

        public long ModifiedbyId { get; set; }

        public DateTime Modifiedon { get; set; }

        public long? ApprovedbyId { get; set; }

        public DateTime? Approvedon { get; set; }

        public int PriorityId { get; set; }

        public bool Attachment { get; set; }

        // Requirement version identifier
        public long VersionId { get; set; }

        public int VersionNumber { get; set; }
        
        // NON-ENTITY PROPERTIES
        public string ShortDescription { get; set; }
        public string Project { get; set; }
        public string Status { get; set; }
        public string DevelopmentStatus { get; set; }
        public string Priority { get; set; }
        public string CreatedByName { get; set; }
        public string ModifiedByName { get; set; }
        public string AttachmentTitle { get; set; }
        public string[] Hashtags { get; set; }
    }
}
