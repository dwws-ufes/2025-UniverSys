namespace UniverSys.Infrastructure.Persistence.Configurations;

public class DisciplinaConfiguration : IEntityTypeConfiguration<Disciplina>
{
    public void Configure(EntityTypeBuilder<Disciplina> builder)
    {
        builder
            .Property(x => x.Nome)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(x => x.Codigo)
            .HasMaxLength(10)
            .IsRequired();

        builder
            .Property(x => x.Ementa)
            .HasMaxLength(500)
            .IsRequired();
    }
} 