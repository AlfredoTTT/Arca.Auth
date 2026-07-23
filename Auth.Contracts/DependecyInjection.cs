using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Contracts;

public static class MessagingDependencyInjection
{
    public static IServiceCollection AddMessageBusInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            // Opcional: Registrar automágicamente consumers o sagas si los tienes en este assembly
            // x.AddConsumers(typeof(MessagingDependencyInjection).Assembly);

            x.UsingAzureServiceBus((context, cfg) =>
            {
                // Obtiene la cadena de conexión desde appsettings.json ("AzureServiceBus:ConnectionString")
                var connectionString = configuration.GetConnectionString("AzureServiceBus");
                
                cfg.Host(connectionString);

                // Configuración general de endpoints o políticas por defecto si se requiere
                cfg.ConfigureEndpoints(context);
            });

        });

        return services;
    }
}