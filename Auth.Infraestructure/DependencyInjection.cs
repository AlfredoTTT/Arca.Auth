using Microsoft.Extensions.DependencyInjection;
using Auth.Application.Interfaces.Security;
using Auth.Application.Interfaces.Common;
using Auth.Application.Interfaces.Repositories;
using Auth.Infraestructure.Persistence.Repositories;
using Auth.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Auth.Infraestructure.Services;

namespace Auth.Infraestructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }
}