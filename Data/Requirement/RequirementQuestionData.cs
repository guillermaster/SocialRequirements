using System;
using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.Data.Requirement
{
    public class RequirementQuestionData : IRequirementQuestionData
    {
        private readonly ContextModel _context;

        public RequirementQuestionData(ContextModel context)
        {
            _context = context;
        }

        public long Add(RequirementQuestionDto questionDto)
        {
            var question = _context.RequirementQuestion.Add(GetEntityFromDto(questionDto));
            _context.SaveChanges();
            return question.id;
        }

        public RequirementQuestionDto Get(long companyId, long projectId, long requirementId, long requirementVersionId, long requirementQuestionId)
        {
            var question =
                _context.RequirementQuestion.FirstOrDefault(
                    q =>
                        q.company_id == companyId && q.project_id == projectId && q.requirement_id == requirementId &&
                        q.requirement_version_id == requirementVersionId && q.id == requirementQuestionId);

            return question != null ? GetDtoFromEntity(question) : null;
        }

        public List<RequirementQuestionDto> Get(long companyId, long projectId, long requirementId, long requirementVersionId)
        {
            var questions =
                _context.RequirementQuestion.Where(
                    q =>
                        q.company_id == companyId && q.project_id == projectId && q.requirement_id == requirementId &&
                        q.requirement_version_id == requirementVersionId).ToList();

            return questions.Select(GetDtoFromEntity).ToList();
        }

        public List<RequirementQuestionDto> GetAll(long companyId)
        {
            var questions =
                _context.RequirementQuestion.Where(
                    q => q.company_id == companyId).ToList();

            return questions.Select(GetDtoFromEntity).ToList();
        }

        private static RequirementQuestion GetEntityFromDto(RequirementQuestionDto questionDto)
        {
            var question = new RequirementQuestion
            {
                company_id = questionDto.CompanyId,
                project_id = questionDto.ProjectId,
                requirement_id = questionDto.RequirementId,
                requirement_version_id = questionDto.RequirementVersionId,
                question = questionDto.Question,
                status_id = questionDto.StatusId,
                createdby_id = questionDto.CreatedbyId,
                createdon = DateTime.Now,
                modifiedby_id = questionDto.ModifiedbyId,
                modifiedon = DateTime.Now,
                accepted_answer_id = questionDto.AcceptedAnswerId
            };
            return question;
        }

        private RequirementQuestionDto GetDtoFromEntity(RequirementQuestion question)
        {
            var questionDto = new RequirementQuestionDto
            {
                Id = question.id,
                CompanyId = question.company_id,
                ProjectId = question.project_id,
                RequirementId = question.requirement_id,
                RequirementVersionId = question.requirement_version_id,
                Question = question.question,
                StatusId = question.status_id,
                CreatedbyId = question.createdby_id,
                Createdon = question.createdon,
                ModifiedbyId = question.modifiedby_id,
                Modifiedon = question.modifiedon,
                AcceptedAnswerId = question.accepted_answer_id
            };
            return questionDto;
        }
    }
}
