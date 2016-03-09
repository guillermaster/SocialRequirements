using System;
using System.Collections.Generic;

namespace SocialRequirements.Domain.DTO.Account
{
    [Serializable]
    public class ProjectPermissionsDto
    {
        public long ProjectId { get; set; }
        public List<int> PermissionsIds { get; set; }
    }
}
