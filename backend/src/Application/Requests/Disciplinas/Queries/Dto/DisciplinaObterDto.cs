namespace UniverSys.Application.Requests.Disciplinas.Queries.Dto;

public class DisciplinaObterDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Codigo { get; set; }
    public int CargaHoraria { get; set; }
    public string Ementa { get; set; }
    public int CursoId { get; set; }
} 