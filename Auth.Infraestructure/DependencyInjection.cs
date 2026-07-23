using Microsoft.Extensions.DependencyInjection;
using Auth.Application.Interfaces.Security;
using Auth.Application.Interfaces.Common;
using Auth.Domain.Interfaces.Repositories;
using Auth.Infraestructure.Persistence.Repositories;
using Auth.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Auth.Infraestructure.Services;
using MassTransit;
using Auth.Infraestructure.Sagas;
using Auth.Infraestructure.Persistence.Sagas;

namespace Auth.Infraestructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            
        services.AddMassTransit(x =>
        {
            // AQUÍ SE INTEGRA EL OUTBOX
            x.AddEntityFrameworkOutbox<AppDbContext>(o =>
            {
                o.QueryDelay = TimeSpan.FromSeconds(10);
                o.UsePostgres();
                
                // Habilita el outbox para el bus de la aplicación
                o.UseBusOutbox();
            });
            x.AddSagaStateMachine<UserRegistrationStateMachine, UserRegistrationState>()
            .EntityFrameworkRepository(r =>
            {
                r.ExistingDbContext<AppDbContext>();
                r.UsePostgres();
            });
            
            x.AddConsumers(typeof(DependencyInjection).Assembly);
            
            x.UsingAzureServiceBus((context, cfg) =>
            {
                var connectionString = configuration.GetConnectionString("AzureServiceBus");
                cfg.Host(connectionString);
                cfg.ConfigureEndpoints(context);
                cfg.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5))); // Reintentos para manejar fallos transitorios
            });

            
        });
        
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}