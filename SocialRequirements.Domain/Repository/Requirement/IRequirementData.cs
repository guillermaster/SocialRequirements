using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.Repository.Requirement
{
    public interface IRequirementData
    {
        /// <summary>
        /// Returns the number of requirements related to a specified company
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <returns>Number of requirements</returns>
        int GetNumberOfRequirements(long companyId);

        /// <summary>
        /// Adds a new requirement to the database
        /// </summary>
        /// <param name="requirement">DTO class instance with data to be stored</param>
        /// <returns>Requirement identifier</returns>
        long Add(RequirementDto requirement);

        /// <summary>
        /// Returns requirement data matching criteria specified by input params
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <returns>Requirement</returns>
        RequirementDto Get(long companyId, long projectId, long requirementId);

        /// <summary>
        /// Adds a like to the specified requirement
        /// </summary>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="personId">ID of the user who gave the like</param>
        void Like(long requirementId, long personId);

        /// <summary>
        /// Adds a like to the specified requirement
        /// </summary>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="personId">ID of the user who gave the dislike</param>
        void Dislike(long requirementId, long personId);

        /// <summary>
        /// Adds a comment to the specified requirement
        /// </summary>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="personId">ID of the who commented</param>
        /// <param name="commentary">The comment</param>
        void Comment(long requirementId, long personId, string commentary);
    }
}
