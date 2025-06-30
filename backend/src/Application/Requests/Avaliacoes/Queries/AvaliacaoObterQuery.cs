using Magma.Extensions.Application;
using Magma.Extensions.Application.Models;
using UniverSys.Application.Requests.Avaliacoes.Queries.Dto;

namespace UniverSys.Application.Requests.Avaliacoes.Queries;

public class AvaliacaoObterQuery : QueryRequestBase, IRequest<PaginatedList<AvaliacaoObterDto>>
{
    public int? Id { get; set; }
    public string Nome { get; set; }
    public int? Peso { get; set; }
    public int? TurmaId { get; set; }
}

public class AvaliacaoObterQueryHandler : IRequestHandler<AvaliacaoObterQuery, PaginatedList<AvaliacaoObterDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AvaliacaoObterQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<AvaliacaoObterDto>> Handle(AvaliacaoObterQuery request, CancellationToken cancellationToken)
    {
        var consulta = _context.Avaliacoes.AsNoTracking();

        var dto = consulta.ProjectTo<AvaliacaoObterDto>(_mapper.ConfigurationProvider);

        var registros = await PaginatedList<AvaliacaoObterDto>.CreateAsync(dto, request.PageIndex, request.PageSize);

        return registros;
    }
} 