namespace UniverSys.Domain.Entities;
public class Aluno
{
    public int Id { get; set; }
    public string Matricula { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public int? UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public int CursoId { get; set; }
    public Curso Curso { get; set; }
    public List<Matricula> Matriculas { get; set; } = [];
}
