using MassTransit;

using Auth.Contracts.Events;

namespace Auth.Infraestructure.Messaging;
public class UserRegistrationInitiatedConsumer : IConsumer<UserRegistrationInitiatedEvent>
{
    public async Task Consume(ConsumeContext<UserRegistrationInitiatedEvent> context)
    {
        var message = context.Message;
        
        // El CancellationToken viene integrado en el contexto del mensaje
        var cancellationToken = context.CancellationToken;

        // Aquí realizarías operaciones asíncronas secundarias (ej. llamar a otro servicio o mandar email)
        await Task.Delay(100, cancellationToken); 
    }
}