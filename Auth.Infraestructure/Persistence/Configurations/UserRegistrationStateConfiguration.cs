using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Auth.Infraestructure.Persistence.Sagas;

namespace Auth.Infraestructure.Persistence.Configurations;

public class UserRegistrationStateConfiguration : IEntityTypeConfiguration<UserRegistrationState>
{
    public void Configure(EntityTypeBuilder<UserRegistrationState> entity)
    {
        // 1. Definimos explícitamente la llave primaria
        entity.HasKey(x => x.CorrelationId);

        // 2. Nombre de la tabla en PostgreSQL
        entity.ToTable("user_registration_states");

        // 3. Configuración de propiedades
        entity.Property(x => x.CurrentState)
            .HasMaxLength(64)
            .IsRequired();

        entity.Property(x => x.Email)
            .HasMaxLength(256);
            
        entity.Property(x => x.CreatedAt)
            .IsRequired();
    }
}