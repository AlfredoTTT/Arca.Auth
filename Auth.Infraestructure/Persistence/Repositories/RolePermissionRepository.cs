using Auth.Domain.Interfaces.Repositories;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infraestructure.Persistence.Repositories;

public class RolePermissionRepository(AppDbContext context) : IRolePermissionRepository
{
    public async Task AddAsync(RolePermission rolePermission)
    {
        await context.Set<RolePermission>().AddAsync(rolePermission);
        await context.SaveChangesAsync();
    }

    public async Task RemoveAsync(RolePermission rolePermission)
    {
        context.Set<RolePermission>().Remove(rolePermission);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Permission>> GetPermissionsByRoleIdAsync(Guid roleId)
    {
        return await context.Set<RolePermission>()
            .Where(rp => rp.RoleId == roleId)
            .Select(rp => rp.Permission)
            .ToListAsync();
    }
}