using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Auth.Infraestructure.Persistence;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // 1. Intenta leer primero de las variables de entorno del sistema (ideal para CI/CD o contenedores)
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") 
                               ?? Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");

        // 2. Si no hay variable de entorno, busca en el appsettings.json y User Secrets del proyecto API
        if (string.IsNullOrEmpty(connectionString))
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Auth.Api")) // Ajusta al nombre de tu proyecto WebApi
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddUserSecrets<AppDbContextFactory>(optional: true) // Carga los User Secrets si estás localmente
                .Build();

            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        var builder = new DbContextOptionsBuilder<AppDbContext>();
        builder.UseNpgsql(connectionString);

        return new AppDbContext(builder.Options);
    }
}