using Auth.Domain.Entities;
using Auth.Domain.ValueObjects;

namespace Auth.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(Email email); // Necesario para el Login
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}