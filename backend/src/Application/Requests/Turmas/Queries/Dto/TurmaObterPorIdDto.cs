namespace UniverSys.Application.Requests.Turmas.Queries.Dto;

public class TurmaObterPorIdDto
{
    public int Id { get; set; }
    public int DisciplinaId { get; set; }
    public int ProfessorId { get; set; }
    public int SemestreAno { get; set; }
    public int SemestrePeriodo { get; set; }
    public string Nome { get; set; }
} 