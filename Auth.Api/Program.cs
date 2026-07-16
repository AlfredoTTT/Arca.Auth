using Auth.Infraestructure;
using Auth.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplication().AddInfraestructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    logger.LogCritical("¡ERROR CRÍTICO! La Connection String 'DefaultConnection' está vacía.");
}
else
{
    var serverName = connectionString.Split(';').FirstOrDefault(x => x.StartsWith("Server"));
    logger.LogInformation("Conexión detectada. Servidor: {ServerName}", serverName);
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<Auth.Api.Middleware.ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();
