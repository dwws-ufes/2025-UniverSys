namespace UniverSys.Domain.Entities;
public class Curso
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int DuracaoSemestres { get; set; }
    public List<Aluno> Alunos { get; set; } = [];
    public List<Disciplina> Disciplinas { get; set; } = [];

}
