using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using Z.EntityFramework.Plus;

namespace UniverSys.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;

    public ApplicationDbContext(
        DbContextOptions options,
        ICurrentUserService currentUserService,
        IDateTime dateTime) : base(options)
    {
        _currentUserService = currentUserService;
        _dateTime = dateTime;
    }

    public DbSet<AuditEntry> AuditEntries { get; set; }
    public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<Disciplina> Disciplinas { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<Nota> Notas { get; set; }
    public DbSet<Professor> Professores { get; set; }
    public DbSet<Turma> Turmas { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    public override EntityEntry Entry(object entity)
    {
        return base.Entry(entity);
    }

    public override EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
    {
        return base.Entry(entity);
    }

    public override void AddRange(IEnumerable<object> entities)
    {
        base.AddRange(entities);
    }

    public override DbSet<TEntity> Set<TEntity>()
    {
        return base.Set<TEntity>();
    }
}
