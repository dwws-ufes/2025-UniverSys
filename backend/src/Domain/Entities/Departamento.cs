namespace UniverSys.Domain.Entities;
public class Departamento
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public List<Professor> Professores { get; set; } = [];
}
