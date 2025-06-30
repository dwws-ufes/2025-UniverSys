using Magma.Extensions.Application;
using Magma.Extensions.Application.Models;
using UniverSys.Application.Requests.Turmas.Queries.Dto;

namespace UniverSys.Application.Requests.Turmas.Queries;

public class TurmaObterQuery : QueryRequestBase, IRequest<PaginatedList<TurmaObterDto>>
{
    public int? Id { get; set; }
    public int? DisciplinaId { get; set; }
    public int? ProfessorId { get; set; }
    public int? SemestreAno { get; set; }
    public int? SemestrePeriodo { get; set; }
    public string Nome { get; set; }
}

public class TurmaObterQueryHandler : IRequestHandler<TurmaObterQuery, PaginatedList<TurmaObterDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public TurmaObterQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<TurmaObterDto>> Handle(TurmaObterQuery request, CancellationToken cancellationToken)
    {
        var consulta = _context.Turmas.AsNoTracking();

        if(request.DisciplinaId.HasValue)
        {
            consulta = consulta.Where(x => x.DisciplinaId == request.DisciplinaId.Value);
        }

        if(request.ProfessorId.HasValue)
        {
            consulta = consulta.Where(x => x.ProfessorId == request.ProfessorId.Value);
        }

        var dto = consulta.ProjectTo<TurmaObterDto>(_mapper.ConfigurationProvider);

        var registros = await PaginatedList<TurmaObterDto>.CreateAsync(dto, request.PageIndex, request.PageSize);

        return registros;
    }
} 