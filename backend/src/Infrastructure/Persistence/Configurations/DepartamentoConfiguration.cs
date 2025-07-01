namespace UniverSys.Infrastructure.Persistence.Configurations;

public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
{
    public void Configure(EntityTypeBuilder<Departamento> builder)
    {
        builder
            .Property(x => x.Nome)
            .HasMaxLength(50)
            .IsRequired();
    }
} 