using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.Data.Requirement
{
    public class RequirementVersionData : IRequirementVersionData
    {
        private readonly ContextModel _context;

        public RequirementVersionData(ContextModel context)
        {
            _context = context;
        }

        public void Add(RequirementDto requirement)
        {
            var requirementVersion = GetEntityFromRequirementDto(requirement);
            _context.RequirementVersion.Add(requirementVersion);
            _context.SaveChanges();
        }

        private static RequirementVersion GetEntityFromRequirementDto(RequirementDto requirement)
        {
            var requirementVersion = new RequirementVersion
            {
                company_id = requirement.CompanyId,
                project_id = requirement.ProjectId,
                requirement_id = requirement.Id,
                title = requirement.Title,
                description = requirement.Description,
                agreed = requirement.Agreed,
                disagreed = requirement.Disagreed,
                status_id = requirement.StatusId,
                createdby_id = requirement.CreatedbyId,
                createdon = requirement.Createdon,
                modifiedby_id = requirement.ModifiedbyId,
                modifiedon = requirement.Modifiedon,
                approvedby_id = requirement.ApprovedbyId,
                approvedon = requirement.Approvedon
            };
            return requirementVersion;
        }
    }
}
