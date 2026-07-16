namespace Auth.Domain.Exceptions;
public class UserAlreadyExistsException(string email) 
    : DomainException($"'{email}' is already registered.");