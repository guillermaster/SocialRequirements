using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.Repository.Account;

namespace SocialRequirements.Data.Account
{
    public class RoleData : IRoleData
    {
        private readonly ContextModel _context;

        public RoleData(ContextModel context)
        {
            _context = context;
        }

        public List<RoleDto> GetRolesByPermission(int permissionId)
        {
            var permission = _context.Permission.FirstOrDefault(p => p.id == permissionId);
            return permission == null ? new List<RoleDto>() : permission.Role.Select(GetDtoFromEntity).ToList();
        }

        private static RoleDto GetDtoFromEntity(Role role)
        {
            var roleDto = new RoleDto
            {
                Id = role.id,
                Name = role.name
            };
            return roleDto;
        }
    }
}
