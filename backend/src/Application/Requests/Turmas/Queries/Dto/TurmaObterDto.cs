namespace UniverSys.Application.Requests.Turmas.Queries.Dto;

public class TurmaObterDto
{
    public int Id { get; set; }
    public int DisciplinaId { get; set; }
    public string DisciplinaNome { get; set; }
    public int ProfessorId { get; set; }
    public string ProfessorNome { get; set; }
    public int SemestreAno { get; set; }
    public int SemestrePeriodo { get; set; }
    public string Nome { get; set; }
} 