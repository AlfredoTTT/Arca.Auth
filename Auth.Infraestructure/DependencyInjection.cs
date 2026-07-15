using Microsoft.Extensions.DependencyInjection;
using Auth.Application.Interfaces.Security;
using Auth.Infraestructure.Security;

namespace Auth.Infraestructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
    
        return services;
    }
}