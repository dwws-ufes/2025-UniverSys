using Magma.Extensions.Application;
using Magma.Extensions.Application.Models;
using UniverSys.Application.Requests.Cursos.Queries.Dto;

namespace UniverSys.Application.Requests.Cursos.Queries;

public class CursoObterQuery : QueryRequestBase, IRequest<PaginatedList<CursoObterDto>>
{
    public int? Id { get; set; }
    public string Nome { get; set; }
    public int? DuracaoSemestres { get; set; }
}

public class CursoObterQueryHandler : IRequestHandler<CursoObterQuery, PaginatedList<CursoObterDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CursoObterQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CursoObterDto>> Handle(CursoObterQuery request, CancellationToken cancellationToken)
    {
        var consulta = _context.Cursos.AsNoTracking();

        var dto = consulta.ProjectTo<CursoObterDto>(_mapper.ConfigurationProvider);

        var registros = await PaginatedList<CursoObterDto>.CreateAsync(dto, request.PageIndex, request.PageSize);

        return registros;
    }
} 