using Magma.Extensions.Application;
using Magma.Extensions.Application.Models;
using UniverSys.Application.Requests.Disciplinas.Queries.Dto;

namespace UniverSys.Application.Requests.Disciplinas.Queries;

public class DisciplinaObterQuery : QueryRequestBase, IRequest<PaginatedList<DisciplinaObterDto>>
{
    public int? Id { get; set; }
    public string Nome { get; set; }
    public string Codigo { get; set; }
    public int? CargaHoraria { get; set; }
    public int? CursoId { get; set; }
    public int? AlunoId { get; set; }
}

public class DisciplinaObterQueryHandler : IRequestHandler<DisciplinaObterQuery, PaginatedList<DisciplinaObterDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DisciplinaObterQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<DisciplinaObterDto>> Handle(DisciplinaObterQuery request, CancellationToken cancellationToken)
    {
        var consulta = _context.Disciplinas.AsNoTracking();

        if(request.CursoId.HasValue)
        {
            consulta = consulta.Where(x => x.CursoId == request.CursoId.Value);
        }

        if(request.AlunoId.HasValue)
        {
            consulta = consulta.Where(x => !x.Turmas.Any(t => t.Matriculas.Any(m => m.AlunoId == request.AlunoId.Value)));
        }

        var dto = consulta.ProjectTo<DisciplinaObterDto>(_mapper.ConfigurationProvider);

        var registros = await PaginatedList<DisciplinaObterDto>.CreateAsync(dto, request.PageIndex, request.PageSize);

        return registros;
    }
}