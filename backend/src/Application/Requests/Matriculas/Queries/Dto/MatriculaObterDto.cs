namespace UniverSys.Application.Requests.Matriculas.Queries.Dto;

public class MatriculaObterDto
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public string AlunoNome { get; set; }
    public string AlunoMatricula { get; set; }
    public int TurmaId { get; set; }
    public string TurmaNome { get; set; }
    public string TurmaDisciplinaNome { get; set; }
    public string TurmaProfessorNome { get; set; }
    public int TurmaSemestreAno { get; set; }
    public int TurmaSemestrePeriodo { get; set; }
} 