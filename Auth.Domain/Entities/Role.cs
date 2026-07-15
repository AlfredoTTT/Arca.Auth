namespace Auth.Domain.Entities;

public class Role(string name)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = name;

    // Relación de navegación
    public ICollection<RolePermission> RolePermissions { get; private set; } = [];
}