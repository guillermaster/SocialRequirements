using System;
using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.Repository.Account;
using SocialRequirements.Domain.Repository.General;
using SocialRequirements.Domain.Repository.Requirement;
using SocialRequirements.Utilities;

namespace SocialRequirements.Data.Requirement
{
    public class RequirementQuestionAnswerData : IRequirementQuestionAnswerData
    {
        private readonly ContextModel _context;
        private readonly IGeneralCatalogData _generalCatalogData;
        private readonly IPersonData _personData;

        public RequirementQuestionAnswerData(ContextModel context, IGeneralCatalogData generalCatalogData,
            IPersonData personData)
        {
            _context = context;
            _generalCatalogData = generalCatalogData;
            _personData = personData;
        }

        public long Add(RequirementQuestionAnswerDto answerDto)
        {
            var answer = _context.RequirementQuestionAnswer.Add(GetEntityFromDto(answerDto));
            _context.SaveChanges();
            return answer.id;
        }

        public List<RequirementQuestionAnswerDto> Get(long companyId, long projectId, long requirementId, long requirementVersionId, long requirementQuestionId)
        {
            var answers = _context.RequirementQuestionAnswer.Where(
                            answer =>
                                answer.company_id == companyId && answer.project_id == projectId &&
                                answer.requirement_id == requirementId && answer.requirement_version_id == requirementVersionId &&
                                answer.requirement_question_id == requirementQuestionId).ToList();

            return answers.Select(GetDtoFromEntity).ToList();
        }

        public int GetNumberOfAnswers(long companyId, long projectId, long requirementId, long requirementVersionId,
            long requirementQuestionId)
        {
            return
                _context.RequirementQuestionAnswer.Count(
                    answer =>
                        answer.company_id == companyId && answer.project_id == projectId &&
                        answer.requirement_id == requirementId && answer.requirement_version_id == requirementVersionId &&
                        answer.requirement_question_id == requirementQuestionId);
        }

        private RequirementQuestionAnswerDto GetDtoFromEntity(RequirementQuestionAnswer answer)
        {
            var answerDto = new RequirementQuestionAnswerDto
            {
                Id = answer.id,
                CompanyId = answer.company_id,
                ProjectId = answer.project_id,
                RequirementId = answer.requirement_id,
                RequirementVersionId = answer.requirement_version_id,
                RequirementQuestionId = answer.requirement_question_id,
                Answer = answer.answer,
                CreatedbyId = answer.createdby_id,
                Createdon = answer.createdon,
                ModifiedbyId = answer.modifiedby_id,
                Modifiedon = answer.modifiedon,
                StatusId = answer.status_id,
                Status = answer.GeneralCatalogDetail != null ? answer.GeneralCatalogDetail.name : _generalCatalogData.GetTitle(answer.status_id),
                CreatedByName = answer.Person != null ? StringUtilities.GetPersonFullName(answer.Person) : _personData.GetFullName(answer.createdby_id),
                ModifiedByName = answer.Person1 != null ? StringUtilities.GetPersonFullName(answer.Person1) : _personData.GetFullName(answer.modifiedby_id),
            };
            return answerDto;
        }

        private static RequirementQuestionAnswer GetEntityFromDto(RequirementQuestionAnswerDto answerDto)
        {
            var answer = new RequirementQuestionAnswer
            {
                company_id = answerDto.CompanyId,
                project_id = answerDto.ProjectId,
                requirement_id = answerDto.RequirementId,
                requirement_version_id = answerDto.RequirementVersionId,
                requirement_question_id = answerDto.RequirementQuestionId,
                answer = answerDto.Answer,
                createdby_id = answerDto.CreatedbyId,
                createdon = DateTime.Now,
                modifiedby_id = answerDto.ModifiedbyId,
                modifiedon = DateTime.Now,
                status_id = answerDto.StatusId
            };
            return answer;
        }
    }
}
