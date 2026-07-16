namespace Auth.Domain.ValueObjects;

public record HashedPassword
{
    public string Value { get; }

    public HashedPassword(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Hashed password cannot be empty.");

        Value = value;
    }
}