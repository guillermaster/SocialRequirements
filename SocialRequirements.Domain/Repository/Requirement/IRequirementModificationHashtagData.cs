namespace SocialRequirements.Domain.Repository.Requirement
{
    public interface IRequirementModificationHashtagData
    {
        void Add(long requirementModifId, string[] hashtags, string[] hashtagsToAdd, string[] hashtagsToRemove, long personId);
    }
}
