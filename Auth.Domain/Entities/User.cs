using Auth.Domain.ValueObjects;

namespace Auth.Domain.Entities;
public class User
{
    public Guid Id { get; private set; }
    public Email Email { get; private set; }
    public string PasswordHash { get; private set; }
    public Guid RoleId { get; private set; }
    public bool IsActive { get; private set; }
    private User() { }
    public User(Email email, string passwordHash, Guid roleId)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash is required.", nameof(passwordHash));
        Id = Guid.NewGuid();
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PasswordHash = passwordHash;
        RoleId = roleId;
        IsActive = true;
    }
}