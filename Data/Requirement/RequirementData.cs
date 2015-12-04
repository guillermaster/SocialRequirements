using System;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.Repository.Requirement;
using SocialRequirements.Context.Entities;

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
            var newRequirement = _context.Requirement.Add(GetEntityFromDto(requirement));
            return newRequirement.id;
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
    }
}
