namespace Auth.Domain.Entities;

public class RolePermission(Guid roleId, Guid permissionId)
{
    public Guid RoleId { get; private set; } = roleId;
    public Guid PermissionId { get; private set; } = permissionId;

    // Propiedades de navegación para ORM
    public required Role Role { get; set; }
    public required Permission Permission { get; set; }
}