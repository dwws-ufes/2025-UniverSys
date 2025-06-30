using UniverSys.Application.Requests.Cursos.Queries.Dto;

namespace UniverSys.Application.Requests.Cursos.Queries;

public class CursoObterPorIdQuery : IRequest<CursoObterPorIdDto>
{
    public int Id { get; set; }
}

public class CursoObterPorIdQueryHandler : IRequestHandler<CursoObterPorIdQuery, CursoObterPorIdDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CursoObterPorIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CursoObterPorIdDto> Handle(CursoObterPorIdQuery request, CancellationToken cancellationToken)
    {
        var dto = await _context.Cursos
            .Where(x => x.Id == request.Id)
            .ProjectTo<CursoObterPorIdDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return dto;
    }
} 