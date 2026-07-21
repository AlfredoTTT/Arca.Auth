using Auth.Domain.Entities;

namespace Auth.Domain.Interfaces.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(Guid id);
    Task<Role?> GetByNameAsync(string name);
    Task AddAsync(Role role);
    Task<IEnumerable<Role>> GetAllAsync();
}