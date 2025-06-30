namespace UniverSys.Domain.Entities;
public class Avaliacao
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Peso { get; set; }
    public int TurmaId { get; set; }
    public Turma Turma { get; set; }
}
