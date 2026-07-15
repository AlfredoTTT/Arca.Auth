using System.Net;
using System.Text.Json;
using FluentValidation;

namespace Auth.Api.Middleware;

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
            _ => HttpStatusCode.InternalServerError
        };

        context.Response.StatusCode = (int)statusCode;

        // Formateamos el error para que el frontend lo entienda
        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Ocurrió un error en la solicitud",
            Errors = exception is ValidationException valEx ? valEx.Errors.Select(e => e.ErrorMessage) : null
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}