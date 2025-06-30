using UniverSys.Application.Requests.Professores.Queries.Dto;

namespace UniverSys.Application.Requests.Professores.Queries;

public class ProfessorObterPorIdQuery : IRequest<ProfessorObterPorIdDto>
{
    public int Id { get; set; }
}

public class ProfessorObterPorIdQueryHandler : IRequestHandler<ProfessorObterPorIdQuery, ProfessorObterPorIdDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProfessorObterPorIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProfessorObterPorIdDto> Handle(ProfessorObterPorIdQuery request, CancellationToken cancellationToken)
    {
        var dto = await _context.Professores
            .Where(x => x.Id == request.Id)
            .ProjectTo<ProfessorObterPorIdDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return dto;
    }
} 