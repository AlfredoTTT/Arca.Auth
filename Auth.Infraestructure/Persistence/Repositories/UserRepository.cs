using Auth.Domain.Interfaces.Repositories;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Auth.Domain.ValueObjects;

namespace Auth.Infraestructure.Persistence.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken: cancellationToken);

    public async Task<User?> GetByEmailAsync(Email email,CancellationToken cancellationToken = default) =>
        await context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken: cancellationToken);

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(user, cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        context.Users.Update(user);
        await  Task.CompletedTask;
    }
}