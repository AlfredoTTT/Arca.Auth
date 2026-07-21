using Auth.Domain.Interfaces.Repositories;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infraestructure.Persistence.Repositories;

public class PermissionRepository(AppDbContext context) : IPermissionRepository
{
    public async Task<IEnumerable<Permission>> GetAllAsync() => 
        await context.Permissions.ToListAsync();

    public async Task<Permission?> GetByIdAsync(Guid id) => 
        await context.Permissions.FirstOrDefaultAsync(p => p.Id == id);

    public async Task AddAsync(Permission permission)
    {
        await context.Permissions.AddAsync(permission);
        await context.SaveChangesAsync();
    }
}