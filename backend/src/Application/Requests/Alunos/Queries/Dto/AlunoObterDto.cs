namespace UniverSys.Application.Requests.Alunos.Queries.Dto;

public class AlunoObterDto
{
    public int Id { get; set; }
    public string Matricula { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public int? UsuarioId { get; set; }
    public int CursoId { get; set; }
} 