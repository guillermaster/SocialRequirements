using System;

namespace SocialRequirements.Domain.DTO.Account
{
    [Serializable]
    public class ProjectDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
