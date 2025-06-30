using UniverSys.Domain.Enums;

namespace UniverSys.Domain.Entities;
public class Professor
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public Especializacao Especializacao { get; set; }
    public int? UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public int DepartamentoId { get; set; }
    public Departamento Departamento { get; set; }
    public List<Turma> Turmas { get; set; }

    //TODO: notas ou avaliacoes?
}
