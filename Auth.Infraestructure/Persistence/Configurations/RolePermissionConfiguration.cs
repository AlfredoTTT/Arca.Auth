using Auth.Domain.Entities; // Asegúrate de tener tu entidad aquí
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Persistence.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolesPermissions");

        // Llave compuesta: Un rol no puede tener el mismo permiso dos veces
        builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

        // Relación con Rol
        builder.HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        // Relación con Permiso
        builder.HasOne(rp => rp.Permission)
            .WithMany()
            .HasForeignKey(rp => rp.PermissionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}