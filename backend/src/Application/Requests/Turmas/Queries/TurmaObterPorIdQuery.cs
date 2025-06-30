using UniverSys.Application.Requests.Turmas.Queries.Dto;

namespace UniverSys.Application.Requests.Turmas.Queries;

public class TurmaObterPorIdQuery : IRequest<TurmaObterPorIdDto>
{
    public int Id { get; set; }
}

public class TurmaObterPorIdQueryHandler : IRequestHandler<TurmaObterPorIdQuery, TurmaObterPorIdDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public TurmaObterPorIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TurmaObterPorIdDto> Handle(TurmaObterPorIdQuery request, CancellationToken cancellationToken)
    {
        var dto = await _context.Turmas
            .Where(x => x.Id == request.Id)
            .ProjectTo<TurmaObterPorIdDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return dto;
    }
} 