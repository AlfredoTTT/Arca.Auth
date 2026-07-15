using FluentValidation;
using MediatR;

namespace Auth.Application.Common.Behaviours;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // 1. Ejecutar validaciones antes de llegar al Handler
        var context = new ValidationContext<TRequest>(request);
        var failures = validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Count != 0)
        {
            // Si hay errores, lanzamos una excepción (que luego el API puede capturar)
            throw new ValidationException(failures);
        }

        // 2. Si todo está bien, pasamos al siguiente eslabón (el Handler)
        return await next();
    }
}