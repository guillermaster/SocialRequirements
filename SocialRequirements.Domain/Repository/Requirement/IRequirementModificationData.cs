using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.Repository.Requirement
{
    public interface IRequirementModificationData
    {
        /// <summary>
        /// Adds a new requirement modification request to the database
        /// </summary>
        /// <param name="requirement">DTO class instance with data to be stored</param>
        /// <returns>Requirement modification request ID</returns>
        long Add(RequirementModificationDto requirement);

        /// <summary>
        /// Returns requirement modification request data matching criteria specified by input params
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <param name="requirementModificationId">Requirement modification ID</param>
        /// <returns>Requirement</returns>
        RequirementModificationDto Get(long companyId, long projectId, long requirementId, long requirementModificationId);

        /// <summary>
        /// Returns the current active requirement modification request
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="requirementId">Requirement ID</param>
        /// <returns>Current active requirement modification request, null when no active modifiaction request is found</returns>
        RequirementModificationDto Get(long companyId, long projectId, long requirementId);
    }
}
