using UniverSys.Application.Requests.Avaliacoes.Queries.Dto;

namespace UniverSys.Application.Requests.Avaliacoes.Queries;

public class AvaliacaoObterPorIdQuery : IRequest<AvaliacaoObterPorIdDto>
{
    public int Id { get; set; }
}

public class AvaliacaoObterPorIdQueryHandler : IRequestHandler<AvaliacaoObterPorIdQuery, AvaliacaoObterPorIdDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AvaliacaoObterPorIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AvaliacaoObterPorIdDto> Handle(AvaliacaoObterPorIdQuery request, CancellationToken cancellationToken)
    {
        var dto = await _context.Avaliacoes
            .Where(x => x.Id == request.Id)
            .ProjectTo<AvaliacaoObterPorIdDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return dto;
    }
} 