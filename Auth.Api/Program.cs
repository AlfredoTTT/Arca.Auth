using Auth.Api.Middlewares;
using Auth.Infraestructure;
using Auth.Application;


var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de servicios
builder.Services.AddControllers();
builder.Services.AddApplication().AddInfraestructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddJwtAuthentication(builder.Configuration);

// 2. Configuración de logs nativa
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Vital para que Azure capture los logs

var app = builder.Build();

// 3. Middlewares (EL ORDEN IMPORTA)
// El middleware de excepciones debe ser el primero para capturar cualquier error posterior
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// 4. Verificación de conexión segura después del inicio
var logger = app.Services.GetRequiredService<ILogger<Program>>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    logger.LogCritical("¡ERROR CRÍTICO! La Connection String 'DefaultConnection' no está configurada en Azure.");
}
else
{
    // Logueamos solo una parte para seguridad
    logger.LogInformation("Configuración cargada correctamente. Conectando a la base de datos...");
}

app.Run();