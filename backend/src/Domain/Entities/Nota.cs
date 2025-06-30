namespace UniverSys.Domain.Entities;
public class Nota
{
    public int Id { get; set; }
    public int AvaliacaoId { get; set; }
    public Avaliacao Avaliacao { get; set; }
    public int AlunoId { get; set; }
    public Aluno Aluno { get; set; }
    public decimal Valor { get; set; }
}
