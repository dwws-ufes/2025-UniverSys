using UniverSys.Domain.Enums;

namespace UniverSys.Application.Requests.Professores.Queries.Dto;

public class ProfessorObterDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public Especializacao Especializacao { get; set; }
    public int? UsuarioId { get; set; }
    public int DepartamentoId { get; set; }
} 