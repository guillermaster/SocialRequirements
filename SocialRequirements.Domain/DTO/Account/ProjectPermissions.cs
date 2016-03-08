using System.Collections.Generic;

namespace SocialRequirements.Domain.DTO.Account
{
    public class ProjectPermissions
    {
        public long ProjectId { get; set; }
        public List<int> PermissionsIds { get; set; }
    }
}
