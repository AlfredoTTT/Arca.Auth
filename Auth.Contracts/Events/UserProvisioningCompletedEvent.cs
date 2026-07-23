namespace Auth.Contracts.Events;

public record UserProvisioningCompletedEvent(
    Guid UserId,
    DateTime Timestamp
);