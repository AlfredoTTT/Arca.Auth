namespace Auth.Domain.ValueObjects;

public record Password
{
    public string Value { get; }

    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Password cannot be empty.");

        if (value.Length < 8)
            throw new ArgumentException("Password must be at least 8 characters long.");

        if (!value.Any(char.IsDigit))
            throw new ArgumentException("Password must contain at least one digit.");

        Value = value;
    }
}