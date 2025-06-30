using UniverSys.Domain.Enums;

namespace UniverSys.Domain.Entities;
public class Matricula
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public int TurmaId { get; set; }
    public Turma Turma { get; set; }
    public StatusMatricula Status { get; set; }

    //TODO: Lista notas ou avaliacao??
}
