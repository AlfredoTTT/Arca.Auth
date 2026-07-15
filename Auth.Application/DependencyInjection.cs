using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR;

namespace Auth.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //Registra MediatR: busca todos los IRequestHandler en este assembly
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        //Registra FluentValidation: busca todos los AbstractValidator en este assembly
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        //Registra el comportamiento de validación en la tubería de MediatR
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Common.Behaviours.ValidationBehavior<,>));

        return services;
    }
}