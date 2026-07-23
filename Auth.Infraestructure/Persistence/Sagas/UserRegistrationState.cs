using MassTransit;

namespace Auth.Infraestructure.Persistence.Sagas;

public class UserRegistrationState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; } = null!;
    
    // Datos propios del proceso que necesites recordar entre pasos
    public string Email { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}