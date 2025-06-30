using UniverSys.Application.Requests.Disciplinas.Queries.Dto;

namespace UniverSys.Application.Requests.Disciplinas.Queries;

public class DisciplinaObterPorIdQuery : IRequest<DisciplinaObterPorIdDto>
{
    public int Id { get; set; }
}

public class DisciplinaObterPorIdQueryHandler : IRequestHandler<DisciplinaObterPorIdQuery, DisciplinaObterPorIdDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DisciplinaObterPorIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DisciplinaObterPorIdDto> Handle(DisciplinaObterPorIdQuery request, CancellationToken cancellationToken)
    {
        var dto = await _context.Disciplinas
            .Where(x => x.Id == request.Id)
            .ProjectTo<DisciplinaObterPorIdDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return dto;
    }
} 