namespace UniverSys.Infrastructure.Persistence.Configurations;

public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
{
    public void Configure(EntityTypeBuilder<Professor> builder)
    {
        builder
            .Property(e => e.Especializacao)
            .HasConversion<string>();
    }
} 