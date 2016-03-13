namespace SocialRequirements.Domain.Repository.Requirement
{
    public interface IRequirementHashtagData
    {
        void Add(long requirementId, string[] hashtags, long personId);

        string[] Get(long requirementId);

        /// <summary>
        /// Return a list of IDs of all requirements that match the specified hashtag
        /// </summary>
        /// <param name="hashtag">Hashtag to look for</param>
        /// <returns>List of requirements Ids</returns>
        long[] GetRequirementsId(string hashtag);
    }
}
