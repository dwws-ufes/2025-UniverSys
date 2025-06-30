using Magma.Extensions.Application;
using Magma.Extensions.Application.Models;
using UniverSys.Application.Requests.Professores.Queries.Dto;
using UniverSys.Domain.Enums;

namespace UniverSys.Application.Requests.Professores.Queries;

public class ProfessorObterQuery : QueryRequestBase, IRequest<PaginatedList<ProfessorObterDto>>
{
    public int? Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public Especializacao? Especializacao { get; set; }
    public int? UsuarioId { get; set; }
    public int? DepartamentoId { get; set; }
}

public class ProfessorObterQueryHandler : IRequestHandler<ProfessorObterQuery, PaginatedList<ProfessorObterDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProfessorObterQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProfessorObterDto>> Handle(ProfessorObterQuery request, CancellationToken cancellationToken)
    {
        var consulta = _context.Professores.AsNoTracking();

        if(request.DepartamentoId.HasValue)
        {
            consulta = consulta.Where(x => x.DepartamentoId == request.DepartamentoId.Value);
        }

        var dto = consulta.ProjectTo<ProfessorObterDto>(_mapper.ConfigurationProvider);

        var registros = await PaginatedList<ProfessorObterDto>.CreateAsync(dto, request.PageIndex, request.PageSize);

        return registros;
    }
} 