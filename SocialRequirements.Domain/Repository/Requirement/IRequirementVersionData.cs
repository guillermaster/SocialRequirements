using SocialRequirements.Domain.DTO.Requirement;

namespace SocialRequirements.Domain.Repository.Requirement
{
    public interface IRequirementVersionData
    {
        /// <summary>
        /// Inserts a new requirement version in the database
        /// </summary>
        /// <param name="requirement">Requirement data</param>
        void Add(RequirementDto requirement);
    }
}
