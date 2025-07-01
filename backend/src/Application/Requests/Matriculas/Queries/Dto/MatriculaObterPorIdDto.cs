namespace UniverSys.Application.Requests.Matriculas.Queries.Dto;

public class MatriculaObterPorIdDto
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public string AlunoNome { get; set; }
    public string AlunoMatricula { get; set; }
    public int TurmaId { get; set; }
    public string TurmaNome { get; set; }
    public string DisciplinaNome { get; set; }
    public string ProfessorNome { get; set; }
    public int SemestreAno { get; set; }
    public int SemestrePeriodo { get; set; }
}
