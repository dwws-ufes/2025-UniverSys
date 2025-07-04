namespace UniverSys.Infrastructure.Persistence.Configurations;

public class CursoConfiguration : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder
            .Property(x => x.Nome)
            .HasMaxLength(50)
            .IsRequired();
    }
} 