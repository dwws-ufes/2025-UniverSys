namespace UniverSys.Infrastructure.Persistence.Configurations;

public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder
            .Property(x => x.Nome)
            .HasMaxLength(10)
            .IsRequired();
    }
} 