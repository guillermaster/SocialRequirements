using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Account;

namespace SocialRequirements.Domain.Repository.Account
{
    public interface IRoleData
    {
        List<RoleDto> GetRolesByPermission(int permissionId);
    }
}
