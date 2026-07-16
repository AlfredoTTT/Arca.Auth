using System.Net;
using System.Text.Json;
using FluentValidation;
using Auth.Domain.Exceptions;

namespace Auth.Api.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            // Aquí entra la telemetría: logueamos el error real
            logger.LogError(ex, "Ocurrió una excepción no controlada");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var statusCode = exception switch
        {
            ValidationException => HttpStatusCode.BadRequest,
            UserAlreadyExistsException => HttpStatusCode.Conflict, // 409 Conflict es el estándar para esto
            InvalidCredentialsException => HttpStatusCode.Unauthorized, // 401 Unauthorized
            _ => HttpStatusCode.InternalServerError
        };

        context.Response.StatusCode = (int)statusCode;

        // Formateamos el error para que el frontend lo entienda
        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Ocurrió un error en la solicitud",
            Errors = exception switch 
            {
                ValidationException valEx => valEx.Errors.Select(e => e.ErrorMessage),
                DomainException domEx => new List<string> { domEx.Message }, // Captura tus excepciones de dominio
                _ => ["Ocurrió un error inesperado"] // Oculta el mensaje real en errores 500
            }
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}