using UniverSys.Application.Requests.Notas.Queries.Dto;

namespace UniverSys.Application.Requests.Notas.Queries;

public class NotaObterPorIdQuery : IRequest<NotaObterPorIdDto>
{
    public int Id { get; set; }
}

public class NotaObterPorIdQueryHandler : IRequestHandler<NotaObterPorIdQuery, NotaObterPorIdDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public NotaObterPorIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<NotaObterPorIdDto> Handle(NotaObterPorIdQuery request, CancellationToken cancellationToken)
    {
        var dto = await _context.Notas
            .Where(x => x.Id == request.Id)
            .ProjectTo<NotaObterPorIdDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return dto;
    }
} 