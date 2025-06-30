using Magma.Extensions.Application.Models;
using UniverSys.Domain.Enums;

namespace UniverSys.Application.Requests.Usuarios.Queries;

public class UsuarioObterQuery : QueryRequestBase, IRequest<PaginatedList<UsuarioDto>>
{
    public int? Id { get; set; }
    public string Login { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public bool? Administrador { get; set; }
    public int? GrupoId { get; set; }
    public TipoUsuario? Tipo { get; set; }
}

public class UsuarioObterQueryHandler : IRequestHandler<UsuarioObterQuery, PaginatedList<UsuarioDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UsuarioObterQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<UsuarioDto>> Handle(UsuarioObterQuery request, CancellationToken cancellationToken)
    {
        var consulta = _context.Usuarios
            .AsQueryable();

        if (request.Tipo.HasValue)
        {
            consulta = consulta.Where(x => x.Tipo == request.Tipo.Value);
        }

        var consultaVm = consulta.ProjectTo<UsuarioDto>(_mapper.ConfigurationProvider);

        return await PaginatedList<UsuarioDto>.CreateAsync(consultaVm, request.PageIndex, request.PageSize);
    }
}
