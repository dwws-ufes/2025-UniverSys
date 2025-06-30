using Magma.Extensions.Application;
using Magma.Extensions.Application.Models;
using UniverSys.Application.Requests.Departamentos.Queries.Dto;

namespace UniverSys.Application.Requests.Departamentos.Queries;

public class DepartamentoObterQuery : QueryRequestBase, IRequest<PaginatedList<DepartamentoObterDto>>
{
    public int? Id { get; set; }
    public string Nome { get; set; }
}

public class DepartamentoObterQueryHandler : IRequestHandler<DepartamentoObterQuery, PaginatedList<DepartamentoObterDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DepartamentoObterQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<DepartamentoObterDto>> Handle(DepartamentoObterQuery request, CancellationToken cancellationToken)
    {
        var consulta = _context.Departamentos.AsNoTracking();

        var dto = consulta.ProjectTo<DepartamentoObterDto>(_mapper.ConfigurationProvider);

        var registros = await PaginatedList<DepartamentoObterDto>.CreateAsync(dto, request.PageIndex, request.PageSize);

        return registros;
    }
} 