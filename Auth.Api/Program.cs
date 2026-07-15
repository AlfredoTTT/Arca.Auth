
using Auth.Infraestructure;
using Auth.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplication().AddInfraestructure();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ATest - AUTH",
        Version = "v1",
        Description = "API de ejemplo",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Alfredo Tovar",
            Email = "atovar@soft-t.net"
        }
    });
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<Auth.Api.Middleware.ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();
