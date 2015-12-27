namespace SocialRequirements.Domain.Repository.Requirement
{
    public interface IRequirementQuestionAnswerData
    {
        int GetNumberOfAnswers(long companyId, long projectId, long requirementId, long requirementVersionId,
            long requirementQuestionId);
    }
}
