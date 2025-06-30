using UniverSys.Application.Requests.Alunos.Queries.Dto;

namespace UniverSys.Application.Requests.Alunos.Queries;

public class AlunoObterPorIdQuery : IRequest<AlunoObterPorIdDto>
{
    public int Id { get; set; }
}

public class AlunoObterPorIdQueryHandler : IRequestHandler<AlunoObterPorIdQuery, AlunoObterPorIdDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AlunoObterPorIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AlunoObterPorIdDto> Handle(AlunoObterPorIdQuery request, CancellationToken cancellationToken)
    {
        var dto = await _context.Alunos
            .Where(x => x.Id == request.Id)
            .ProjectTo<AlunoObterPorIdDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return dto;
    }
} 