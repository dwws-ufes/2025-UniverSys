namespace UniverSys.Infrastructure.Persistence.Configurations;
public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder
            .Property(x => x.Login)
            .HasMaxLength(250)
            .IsRequired();

        builder
            .Property(x => x.Nome)
            .HasMaxLength(250)
            .IsRequired();

        builder
            .Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(x => x.Senha)
            .HasMaxLength(1000);

        builder
            .Property(e => e.Tipo)
            .HasMaxLength(50)
            .IsRequired()
            .HasConversion<string>();
    }
}
