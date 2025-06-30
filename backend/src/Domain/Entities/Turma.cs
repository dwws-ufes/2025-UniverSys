namespace UniverSys.Domain.Entities;
public class Turma
{
    public int Id { get; set; }
    public int DisciplinaId { get; set; }
    public Disciplina Disciplina { get; set; }
    public int ProfessorId { get; set; }
    public Professor Professor { get; set; }
    public int SemestreAno { get; set; }
    public int SemestrePeriodo { get; set; }
    public string Nome { get; set; }
    public List<Matricula> Matriculas { get; set; } = [];
    public List<Avaliacao> Avaliacoes { get; set; } = [];
}
