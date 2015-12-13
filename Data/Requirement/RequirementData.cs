using System;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.Data.Requirement
{
    public class RequirementData : IRequirementData
    {
        private readonly ContextModel _context;

        public RequirementData(ContextModel context)
        {
            _context = context;
        }

        public int GetNumberOfRequirements(long companyId)
        {
            var numReq = _context.Requirement.Count(r => r.company_id == companyId);
            return numReq;
        }

        public long Add(RequirementDto requirement)
        {
            using (var scope = _context.Database.BeginTransaction())
            {
                try
                {
                    // add requirement
                    var newRequirement = _context.Requirement.Add(GetEntityFromDto(requirement));
                    _context.SaveChanges();
                    requirement.Id = newRequirement.id;

                    // add requirement version
                    var reqVersionData = new RequirementVersionData(_context);
                    reqVersionData.Add(requirement);

                    scope.Commit();

                    return requirement.Id;
                }
                catch
                {
                    scope.Rollback();
                    throw;
                }
            }
        }

        public RequirementDto Get(long companyId, long projectId, long requirementId)
        {
            var requirement =
                _context.Requirement.FirstOrDefault(r => r.company_id == companyId && r.project_id == projectId && r.id == requirementId);
            return requirement != null ? GetEntityFromDto(requirement) : null;
        }

        public void Like(long requirementId, long personId)
        {
            var requirement = _context.Requirement.FirstOrDefault(r => r.id == requirementId);
            if (requirement == null) return;

            requirement.agreed++;

            _context.SaveChanges();
        }

        public void Dislike(long requirementId, long personId)
        {
            var requirement = _context.Requirement.FirstOrDefault(r => r.id == requirementId);
            if (requirement == null) return;

            requirement.disagreed++;

            _context.SaveChanges();
        }

        public void Comment(long requirementId, long personId, string commentary)
        {
            var requirementVersion =
                _context.RequirementVersion.Where(r => r.id == requirementId)
                    .OrderByDescending(v => v.id)
                    .FirstOrDefault();
            if (requirementVersion == null) return;

            var requirementComment = new RequirementComment
            {
                company_id = requirementVersion.company_id,
                project_id = requirementVersion.project_id,
                requirement_id = requirementVersion.requirement_id,
                requirement_version_id = requirementVersion.id,
                comment = commentary,
                createdby_id = personId,
                createdon = DateTime.Now
            };

            _context.RequirementComment.Add(requirementComment);
            _context.SaveChanges();
        }

        private static Context.Entities.Requirement GetEntityFromDto(RequirementDto requirementDto)
        {
            var requirement = new Context.Entities.Requirement
            {
                company_id =  requirementDto.CompanyId,
                project_id = requirementDto.ProjectId,
                title = requirementDto.Title,
                description = requirementDto.Description,
                agreed = requirementDto.Agreed,
                disagreed = requirementDto.Disagreed,
                status_id = requirementDto.StatusId,
                createdby_id = requirementDto.CreatedbyId,
                createdon = requirementDto.Createdon,
                modifiedby_id = requirementDto.ModifiedbyId,
                modifiedon = requirementDto.Modifiedon,
                approvedby_id = requirementDto.ApprovedbyId,
                approvedon = requirementDto.Approvedon
            };
            return requirement;
        }

        private static RequirementDto GetEntityFromDto(Context.Entities.Requirement requirement)
        {
            var requirementDto = new RequirementDto
            {
                CompanyId = requirement.company_id,
                ProjectId = requirement.project_id,
                Title = requirement.title,
                Description = requirement.description,
                Agreed = requirement.agreed,
                Disagreed = requirement.disagreed,
                StatusId = requirement.status_id,
                CreatedbyId = requirement.createdby_id,
                Createdon = requirement.createdon,
                ModifiedbyId = requirement.modifiedby_id,
                Modifiedon = requirement.modifiedon,
                ApprovedbyId = requirement.approvedby_id,
                Approvedon = requirement.approvedon
            };
            return requirementDto;
        }
    }
}
