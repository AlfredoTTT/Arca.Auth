using MassTransit;
using Auth.Infraestructure.Persistence.Sagas;
using Auth.Contracts.Events;    
namespace Auth.Infraestructure.Sagas;

public class UserRegistrationStateMachine : MassTransitStateMachine<UserRegistrationState>
{
    public State Registering { get; set; } = null!;
    public State Provisioning { get; set; } = null!;
    public State Completed { get; set; } = null!;
    public State Faulted { get; set; } = null!;

    public Event<UserRegistrationInitiatedEvent> RegistrationInitiated { get; set; } = null!;
    public Event<UserProvisioningCompletedEvent> ProvisioningCompleted { get; set; } = null!;
    public Event<UserProvisioningFailedEvent> ProvisioningFailed { get; set; } = null!;

    public UserRegistrationStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => RegistrationInitiated, x => x.CorrelateById(context => context.Message.UserId));
        Event(() => ProvisioningCompleted, x => x.CorrelateById(context => context.Message.UserId));
        Event(() => ProvisioningFailed, x => x.CorrelateById(context => context.Message.UserId));

        // Flujo inicial
        Initially(
            When(RegistrationInitiated)
                .Then(context => 
                {
                    context.Saga.Email = context.Message.Email;
                    context.Saga.CreatedAt = context.Message.Timestamp;
                })
                .TransitionTo(Provisioning)
        );

        // Durante el estado de Provisioning
        During(Provisioning,
            When(ProvisioningCompleted)
                .TransitionTo(Completed)
                .Finalize(),
            When(ProvisioningFailed)
                .TransitionTo(Faulted)
                // ACCIÓN COMPENSATORIA: Revertir o marcar como fallido en base de datos
                .Then(context => 
                {
                   
                })
                .Finalize()
        );
    }
}