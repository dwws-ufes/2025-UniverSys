using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace UniverSys.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Usuario> Usuarios { get; set; }
    DbSet<Aluno> Alunos { get; set; }
    DbSet<Avaliacao> Avaliacoes { get; set; }
    DbSet<Curso> Cursos { get; set; }
    DbSet<Departamento> Departamentos { get; set; }
    DbSet<Disciplina> Disciplinas { get; set; }
    DbSet<Matricula> Matriculas { get; set; }
    DbSet<Nota> Notas { get; set; }
    DbSet<Professor> Professores { get; set; }
    DbSet<Turma> Turmas { get; set; }
    DatabaseFacade Database { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    EntityEntry Entry(object entity);
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    void AddRange(IEnumerable<object> entities);
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
}
