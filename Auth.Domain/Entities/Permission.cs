namespace Auth.Domain.Entities;

public class Permission(string name)
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = name; // Ej: "CanUploadProduct"
}
    