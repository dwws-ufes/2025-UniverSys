using UniverSys.Application.Requests.Matriculas.Queries.Dto;

namespace UniverSys.Application.Requests.Matriculas.Queries;

public class MatriculaObterPorIdQuery : IRequest<MatriculaObterPorIdDto>
{
    public int Id { get; set; }
}

public class MatriculaObterPorIdQueryHandler : IRequestHandler<MatriculaObterPorIdQuery, MatriculaObterPorIdDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MatriculaObterPorIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MatriculaObterPorIdDto> Handle(MatriculaObterPorIdQuery request, CancellationToken cancellationToken)
    {
        var dto = await _context.Matriculas
            .Where(x => x.Id == request.Id)
            .ProjectTo<MatriculaObterPorIdDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return dto;
    }
} 