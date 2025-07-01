namespace UniverSys.Infrastructure.Persistence.Configurations;

public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder
            .Property(x => x.Matricula)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(x => x.Nome)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(x => x.Cpf)
            .HasMaxLength(11)
            .IsRequired();
    }
} 