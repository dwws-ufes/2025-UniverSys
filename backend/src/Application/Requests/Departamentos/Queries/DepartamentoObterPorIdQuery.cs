using UniverSys.Application.Requests.Departamentos.Queries.Dto;

namespace UniverSys.Application.Requests.Departamentos.Queries;

public class DepartamentoObterPorIdQuery : IRequest<DepartamentoObterPorIdDto>
{
    public int Id { get; set; }
}

public class DepartamentoObterPorIdQueryHandler : IRequestHandler<DepartamentoObterPorIdQuery, DepartamentoObterPorIdDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DepartamentoObterPorIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DepartamentoObterPorIdDto> Handle(DepartamentoObterPorIdQuery request, CancellationToken cancellationToken)
    {
        var dto = await _context.Departamentos
            .Where(x => x.Id == request.Id)
            .ProjectTo<DepartamentoObterPorIdDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return dto;
    }
} 