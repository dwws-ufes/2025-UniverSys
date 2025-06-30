namespace UniverSys.Infrastructure.Persistence.Configurations;

public class MatriculaConfiguration : IEntityTypeConfiguration<Matricula>
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
        builder
            .Property(e => e.Status)
            .HasConversion<string>();
    }
} 