using System.Collections.Generic;

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

        /// <summary>
        /// Returns the most used hashtags for requirements
        /// </summary>
        /// <param name="listSize">The size of the top hashtags chart</param>
        /// <returns>Array of hashtags words</returns>
        List<string> GetMostUsedHashtags(int listSize);
    }
}
