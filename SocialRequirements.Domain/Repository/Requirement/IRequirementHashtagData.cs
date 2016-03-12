namespace SocialRequirements.Domain.Repository.Requirement
{
    public interface IRequirementHashtagData
    {
        void Add(long requirementId, string[] hashtags, long personId);

        string[] Get(long requirementId);
    }
}
