using System;
using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.Data.Requirement
{
    public class RequirementCommentData : IRequirementCommentData
    {
        private readonly ContextModel _context;

        public RequirementCommentData(ContextModel context)
        {
            _context = context;
        }

        public void Add(RequirementCommentDto requirementComment)
        {
            _context.RequirementComment.Add(GetEntityFromDto(requirementComment));
            _context.SaveChanges();
        }

        public List<RequirementCommentDto> Get(long requirementId, long companyId, long projectId, long requirementVersionId)
        {
            var requirementComments =
                _context.RequirementComment.Where(
                    c =>
                        c.company_id == companyId && c.project_id == projectId && c.requirement_id == requirementId &&
                        c.requirement_version_id == requirementVersionId).ToList();
            return requirementComments.Select(GetDtoFromEntity).ToList();
        }

        public int GetQuantity(long requirementId, long companyId, long projectId, long requirementVersionId)
        {
            return
                _context.RequirementComment.Count(
                    c =>
                        c.company_id == companyId && c.project_id == projectId && c.requirement_id == requirementId &&
                        c.requirement_version_id == requirementVersionId);
        }

        private static RequirementComment GetEntityFromDto(RequirementCommentDto requirementCommentDto)
        {
            var requirementComment = new RequirementComment
            {
                company_id = requirementCommentDto.CompanyId,
                project_id = requirementCommentDto.ProjectId,
                requirement_id = requirementCommentDto.RequirementId,
                requirement_version_id = requirementCommentDto.RequirementVersionId,
                comment = requirementCommentDto.Comment,
                createdby_id = requirementCommentDto.CreatedbyId,
                createdon = DateTime.Now
            };
            return requirementComment;
        }

        private static RequirementCommentDto GetDtoFromEntity(RequirementComment requirementComment)
        {
            var requirementCommentDto = new RequirementCommentDto
            {
                Id = requirementComment.id,
                CompanyId = requirementComment.company_id,
                ProjectId = requirementComment.project_id,
                RequirementId = requirementComment.requirement_id,
                RequirementVersionId = requirementComment.requirement_version_id,
                Comment = requirementComment.comment,
                CreatedbyId = requirementComment.createdby_id,
                Createdon = requirementComment.createdon
            };
            return requirementCommentDto;
        }
    }
}
