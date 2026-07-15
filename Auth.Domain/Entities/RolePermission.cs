namespace Auth.Domain.Entities;

public class RolePermission
{
    public Guid RoleId { get; private set; }
    public Guid PermissionId { get; private set; }
    
    // Propiedades de navegación para ORM
    public required Role Role { get; set; }
    public required Permission Permission { get; set; }
}