using Auth.Domain.Entities;

namespace Auth.Application.Interfaces.Repositories;

public interface IPermissionRepository
{
    Task<IEnumerable<Permission>> GetAllAsync();
    Task<Permission?> GetByIdAsync(Guid id);
    Task AddAsync(Permission permission);
}