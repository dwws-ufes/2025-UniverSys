namespace UniverSys.Domain.Entities;
public class Matricula
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public Aluno Aluno { get; set; }
    public int TurmaId { get; set; }
    public Turma Turma { get; set; }
    public List<Nota> Notas { get; set; } = [];
}
