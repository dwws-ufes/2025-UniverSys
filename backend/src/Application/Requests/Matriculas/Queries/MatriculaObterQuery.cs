using Magma.Extensions.Application;
using Magma.Extensions.Application.Models;
using UniverSys.Application.Requests.Matriculas.Queries.Dto;

namespace UniverSys.Application.Requests.Matriculas.Queries;

public class MatriculaObterQuery : QueryRequestBase, IRequest<PaginatedList<MatriculaObterDto>>
{
    public int? Id { get; set; }
    public int? AlunoId { get; set; }
    public int? TurmaId { get; set; }
    public int? DisciplinaId { get; set; }
    public int? ProfessorId { get; set; }
    public int? SemestreAno { get; set; }
    public int? SemestrePeriodo { get; set; }
}

public class MatriculaObterQueryHandler : IRequestHandler<MatriculaObterQuery, PaginatedList<MatriculaObterDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MatriculaObterQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<MatriculaObterDto>> Handle(MatriculaObterQuery request, CancellationToken cancellationToken)
    {
        var consulta = _context.Matriculas.AsNoTracking();

        if (request.AlunoId.HasValue)
        {
            consulta = consulta.Where(x => x.AlunoId == request.AlunoId.Value);
        }

        if (request.TurmaId.HasValue)
        {
            consulta = consulta.Where(x => x.TurmaId == request.TurmaId.Value);
        }

        if (request.DisciplinaId.HasValue)
        {
            consulta = consulta.Where(x => x.Turma.DisciplinaId == request.DisciplinaId.Value);
        }

        if (request.ProfessorId.HasValue)
        {
            consulta = consulta.Where(x => x.Turma.ProfessorId == request.ProfessorId.Value);
        }

        if (request.SemestreAno.HasValue)
        {
            consulta = consulta.Where(x => x.Turma.SemestreAno == request.SemestreAno.Value);
        }

        if (request.SemestrePeriodo.HasValue)
        {
            consulta = consulta.Where(x => x.Turma.SemestrePeriodo == request.SemestrePeriodo.Value);
        }

        var dto = consulta.ProjectTo<MatriculaObterDto>(_mapper.ConfigurationProvider);

        var registros = await PaginatedList<MatriculaObterDto>.CreateAsync(dto, request.PageIndex, request.PageSize);

        return registros;
    }
} 