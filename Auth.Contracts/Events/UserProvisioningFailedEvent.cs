namespace Auth.Contracts.Events;

public record UserProvisioningFailedEvent(
    Guid UserId,
    string Reason,
    DateTime Timestamp
);