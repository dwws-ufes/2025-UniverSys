using Magma.Extensions.Application;
using Magma.Extensions.Application.Models;
using UniverSys.Application.Requests.Alunos.Queries.Dto;

namespace UniverSys.Application.Requests.Alunos.Queries;

public class AlunoObterQuery : QueryRequestBase, IRequest<PaginatedList<AlunoObterDto>>
{
    public int? Id { get; set; }
    public string Matricula { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public int? UsuarioId { get; set; }
    public int? CursoId { get; set; }
}

public class AlunoObterQueryHandler : IRequestHandler<AlunoObterQuery, PaginatedList<AlunoObterDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AlunoObterQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<AlunoObterDto>> Handle(AlunoObterQuery request, CancellationToken cancellationToken)
    {
        var consulta = _context.Alunos.AsNoTracking();

        if(request.CursoId.HasValue)
        {
            consulta = consulta.Where(x => x.CursoId == request.CursoId.Value);
        }

        var dto = consulta.ProjectTo<AlunoObterDto>(_mapper.ConfigurationProvider);

        var registros = await PaginatedList<AlunoObterDto>.CreateAsync(dto, request.PageIndex, request.PageSize);

        return registros;
    }
} 