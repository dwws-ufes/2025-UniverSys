namespace UniverSys.Infrastructure.Persistence.Configurations;

public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
{
    public void Configure(EntityTypeBuilder<Professor> builder)
    {
        builder
            .Property(x => x.Nome)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(x => x.Cpf)
            .HasMaxLength(11)
            .IsRequired();

        builder
            .Property(e => e.Especializacao)
            .HasMaxLength(20)
            .IsRequired()
            .HasConversion<string>();
    }
} 