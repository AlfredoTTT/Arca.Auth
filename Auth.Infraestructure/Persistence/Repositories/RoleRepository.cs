using Auth.Domain.Interfaces.Repositories;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infraestructure.Persistence.Repositories;

public class RoleRepository(AppDbContext context) : IRoleRepository
{
    public async Task<Role?> GetByIdAsync(Guid id) => 
        await context.Roles.FirstOrDefaultAsync(r => r.Id == id);

    public async Task<Role?> GetByNameAsync(string name) => 
        await context.Roles.FirstOrDefaultAsync(r => r.Name == name);

    public async Task AddAsync(Role role)
    {
        await context.Roles.AddAsync(role);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Role>> GetAllAsync() => 
        await context.Roles.ToListAsync();
}