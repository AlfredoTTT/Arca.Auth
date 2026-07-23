using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Auth.Infraestructure.Persistence.Sagas;
using MassTransit;

namespace Auth.Infraestructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<UserRegistrationState> UserRegistrationStates => Set<UserRegistrationState>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Aplicamos configuraciones de entidades
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        // Configuración de MassTransit para el estado de la saga
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}