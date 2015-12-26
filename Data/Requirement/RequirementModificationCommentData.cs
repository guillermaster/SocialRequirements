using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.Data.Requirement
{
    public class RequirementModificationCommentData : IRequirementModificationCommentData
    {
        private readonly ContextModel _context;

        public RequirementModificationCommentData(ContextModel context)
        {
            _context = context;
        }

        public long Add(RequirementModificationCommentDto requirementModifComment)
        {
            var newComment = _context.RequirementModificationComment.Add(GetEntityFromDto(requirementModifComment));
            _context.SaveChanges();
            return newComment.id;
        }

        public List<RequirementModificationCommentDto> Get(long companyId, long projectId, long requirementId, long requirementModificationId,
            long requirementModificationVersionId)
        {
            var requirementModifComments =
                _context.RequirementModificationComment.Where(
                    c =>
                        c.company_id == companyId && c.project_id == projectId && c.requirement_id == requirementId &&
                        c.requirement_modification_id == requirementModificationId &&
                        c.requirement_modification_version_id == requirementModificationVersionId).ToList();

            return requirementModifComments.Select(GetDtoFromEntity).ToList();
        }

        public int GetQuantity(long companyId, long projectId, long requirementId, long requirementModificationId,
            long requirementModificationVersionId)
        {
            return
                _context.RequirementModificationComment.Count(
                    c =>
                        c.company_id == companyId && c.project_id == projectId && c.requirement_id == requirementId &&
                        c.requirement_modification_id == requirementModificationId &&
                        c.requirement_modification_version_id == requirementModificationVersionId);
        }

        private static RequirementModificationComment GetEntityFromDto(RequirementModificationCommentDto requirementCommentDto)
        {
            var requirementModifComment = new RequirementModificationComment
            {
                company_id = requirementCommentDto.CompanyId,
                project_id = requirementCommentDto.ProjectId,
                requirement_id = requirementCommentDto.RequirementId,
                requirement_modification_id = requirementCommentDto.RequirementModificationId,
                requirement_modification_version_id = requirementCommentDto.RequirementVersionId,
                comment = requirementCommentDto.Comment,
                createdby_id = requirementCommentDto.CreatedbyId,
                createdon = DateTime.Now
            };
            return requirementModifComment;
        }

        private static RequirementModificationCommentDto GetDtoFromEntity(RequirementModificationComment requirementComment)
        {
            var requirementModifCommentDto = new RequirementModificationCommentDto
            {
                Id = requirementComment.id,
                CompanyId = requirementComment.company_id,
                ProjectId = requirementComment.project_id,
                RequirementId = requirementComment.requirement_id,
                RequirementModificationId = requirementComment.requirement_modification_id,
                RequirementVersionId = requirementComment.requirement_modification_version_id,
                Comment = requirementComment.comment,
                CreatedbyId = requirementComment.createdby_id,
                Createdon = requirementComment.createdon,
                CreatedByName = requirementComment.Person.first_name + " " + requirementComment.Person.last_name
            };
            return requirementModifCommentDto;
        }
    }
}
