namespace UniverSys.Infrastructure.Persistence.Configurations;
public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder
            .Property(x => x.Login)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(x => x.Nome)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(x => x.Email)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(x => x.Senha)
            .HasMaxLength(1000);

        builder
            .Property(e => e.Tipo)
            .HasMaxLength(20)
            .IsRequired()
            .HasConversion<string>();
    }
}
