namespace Auth.Contracts.Events;

using System;

public record UserRegistrationInitiatedEvent(
    Guid UserId,
    string Email,
    DateTime Timestamp
);