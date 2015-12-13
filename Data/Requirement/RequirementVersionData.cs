using System.Linq;
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

        public RequirementDto Get(long companyId, long projectId, long requirementId, long? requirementVersionId = null)
        {
            // if version isn't specified, get the latest one
            if (requirementVersionId == null)
                return GetLastest(companyId, projectId, requirementId);

            // otherwise get specific version
            var requirementVersion = 
                _context.RequirementVersion.FirstOrDefault(
                    rv =>
                        rv.company_id == companyId && rv.project_id == projectId && rv.requirement_id == requirementId &&
                        rv.id == requirementVersionId.Value);

            return requirementVersion != null ? GetDtoFromEntity(requirementVersion) : null;
        }

        private RequirementDto GetLastest(long companyId, long projectId, long requirementId)
        {
            var requirementVersion =
                _context.RequirementVersion.Where(
                    rv => rv.company_id == companyId && rv.project_id == projectId && rv.requirement_id == requirementId)
                    .OrderByDescending(v => v.id)
                    .FirstOrDefault();

            return requirementVersion != null ? GetDtoFromEntity(requirementVersion) : null;
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

        private static RequirementDto GetDtoFromEntity(RequirementVersion requirementVersion)
        {
            var requirementDto = new RequirementDto
            {
                Id = requirementVersion.requirement_id,
                CompanyId = requirementVersion.company_id,
                ProjectId = requirementVersion.project_id,
                Title = requirementVersion.title,
                Description = requirementVersion.description,
                Agreed = requirementVersion.agreed,
                Disagreed = requirementVersion.disagreed,
                StatusId = requirementVersion.status_id,
                CreatedbyId = requirementVersion.createdby_id,
                Createdon = requirementVersion.createdon,
                ModifiedbyId = requirementVersion.modifiedby_id,
                Modifiedon = requirementVersion.modifiedon,
                ApprovedbyId = requirementVersion.approvedby_id,
                Approvedon = requirementVersion.approvedon,
                VersionId = requirementVersion.id,
                VersionNumber = requirementVersion.version_number
            };
            return requirementDto;
        }
    }
}
