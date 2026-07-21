using Auth.Domain.Entities;

namespace Auth.Domain.Interfaces.Repositories;

public interface IRolePermissionRepository
{
    Task AddAsync(RolePermission rolePermission);
    Task RemoveAsync(RolePermission rolePermission);
    Task<IEnumerable<Permission>> GetPermissionsByRoleIdAsync(Guid roleId);
}