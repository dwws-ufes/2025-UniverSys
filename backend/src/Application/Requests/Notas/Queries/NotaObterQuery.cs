using Magma.Extensions.Application;
using Magma.Extensions.Application.Models;
using UniverSys.Application.Requests.Notas.Queries.Dto;

namespace UniverSys.Application.Requests.Notas.Queries;

public class NotaObterQuery : QueryRequestBase, IRequest<PaginatedList<NotaObterDto>>
{
    public int? AvaliacaoId { get; set; }
    public int? MatriculaId { get; set; }
}

public class NotaObterQueryHandler : IRequestHandler<NotaObterQuery, PaginatedList<NotaObterDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public NotaObterQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<NotaObterDto>> Handle(NotaObterQuery request, CancellationToken cancellationToken)
    {
        var consulta = _context.Notas.AsNoTracking();

        if(request.AvaliacaoId.HasValue)
        {
            consulta = consulta.Where(x => x.AvaliacaoId == request.AvaliacaoId.Value);
        }

        if(request.MatriculaId.HasValue)
        {
            consulta = consulta.Where(x => x.MatriculaId == request.MatriculaId.Value);
        }

        var dto = consulta.ProjectTo<NotaObterDto>(_mapper.ConfigurationProvider);

        var registros = await PaginatedList<NotaObterDto>.CreateAsync(dto, request.PageIndex, request.PageSize);

        return registros;
    }
} 