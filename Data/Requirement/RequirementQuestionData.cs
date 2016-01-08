using System;
using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.Repository.Account;
using SocialRequirements.Domain.Repository.General;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.Data.Requirement
{
    public class RequirementQuestionData : IRequirementQuestionData
    {
        private readonly ContextModel _context;
        private readonly IRequirementQuestionAnswerData _requirementQuestionAnswerData;
        private readonly IRequirementData _requirementData;
        private readonly IProjectData _projectData;
        private readonly IGeneralCatalogData _catalogData;
        private readonly IPersonData _personData;

        public RequirementQuestionData(ContextModel context,
            IRequirementQuestionAnswerData requirementQuestionAnswerData, IRequirementData requirementData,
            IProjectData projectData, IGeneralCatalogData catalogData, IPersonData personData)
        {
            _context = context;
            _requirementQuestionAnswerData = requirementQuestionAnswerData;
            _requirementData = requirementData;
            _projectData = projectData;
            _catalogData = catalogData;
            _personData = personData;
        }

        public long Add(RequirementQuestionDto questionDto)
        {
            var question = _context.RequirementQuestion.Add(GetEntityFromDto(questionDto));
            _context.SaveChanges();
            return question.id;
        }

        public RequirementQuestionDto Get(long companyId, long projectId, long requirementId, long requirementVersionId,
            long requirementQuestionId, bool getAnswers)
        {
            var question = Get(companyId, projectId, requirementId, requirementVersionId, requirementQuestionId);

            return question != null ? GetDtoFromEntity(question, getAnswers) : null;
        }

        public List<RequirementQuestionDto> Get(long companyId, long projectId, long requirementId, long requirementVersionId)
        {
            var questions =
                _context.RequirementQuestion.Where(
                    q =>
                        q.company_id == companyId && q.project_id == projectId && q.requirement_id == requirementId &&
                        q.requirement_version_id == requirementVersionId).ToList();

            return questions.Select(q => GetDtoFromEntity(q, false)).ToList();
        }

        public List<RequirementQuestionDto> GetAll(List<long> projectIds)
        {
            var questions =
                _context.RequirementQuestion.Where(q => projectIds.Contains(q.project_id))
                    .OrderByDescending(qDate => qDate.modifiedon)
                    .ToList();

            return questions.Select(q => GetDtoFromEntity(q, false)).ToList();
        }

        public void UpdateStatus(long companyId, long projectId, long requirementId, long requirementVersionId,
            long requirementQuestionId, int statusId)
        {
            var question = Get(companyId, projectId, requirementId, requirementVersionId, requirementQuestionId);

            if (question == null) return;

            question.status_id = statusId;
            _context.SaveChanges();
        }

        private RequirementQuestion Get(long companyId, long projectId, long requirementId, long requirementVersionId,
            long requirementQuestionId)
        {
            var question =
                _context.RequirementQuestion.FirstOrDefault(
                    q =>
                        q.company_id == companyId && q.project_id == projectId && q.requirement_id == requirementId &&
                        q.requirement_version_id == requirementVersionId && q.id == requirementQuestionId);
            return question;
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
                modifiedon = DateTime.Now
            };
            return question;
        }

        private RequirementQuestionDto GetDtoFromEntity(RequirementQuestion question, bool getAnswers)
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
                RequirementTitle = question.Requirement != null ? 
                        question.Requirement.title :
                        _requirementData.GetTitle(question.company_id, question.project_id, question.requirement_id),
                ProjectName = question.Project != null ? question.Project.name : _projectData.GetTitle(question.project_id),
                Status = question.GeneralCatalogDetail != null ? question.GeneralCatalogDetail.name : _catalogData.GetTitle(question.status_id),
                CreatedByName = question.Person != null ? Utilities.StringUtilities.GetPersonFullName(question.Person) : _personData.GetFullName(question.createdby_id),
                ModifiedByName = question.Person1 != null ? Utilities.StringUtilities.GetPersonFullName(question.Person1) : _personData.GetFullName(question.modifiedby_id),
                AnswersQuantity = _requirementQuestionAnswerData.GetNumberOfAnswers(question.company_id, question.project_id,
                                    question.requirement_id, question.requirement_version_id, question.id),
                Answers = getAnswers ? _requirementQuestionAnswerData.Get(question.company_id, question.project_id,
                                    question.requirement_id, question.requirement_version_id, question.id) :
                                    new List<RequirementQuestionAnswerDto>()
            };
            return questionDto;
        }
    }
}
