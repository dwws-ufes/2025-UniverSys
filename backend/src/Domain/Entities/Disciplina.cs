namespace UniverSys.Domain.Entities;
public class Disciplina
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Codigo { get; set; }
    public int CargaHoraria { get; set; }
    public string Ementa { get; set; }
    public int CursoId { get; set; }
    public Curso Curso { get; set; }
    public List<Turma> Turmas { get; set; } = [];

}
