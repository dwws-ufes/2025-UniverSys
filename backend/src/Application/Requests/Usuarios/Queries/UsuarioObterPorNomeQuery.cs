namespace UniverSys.Application.Requests.Usuarios.Queries;

public class UsuarioObterPorNomeQuery : IRequest<List<UsuarioDto>>
{
    public string Nome { get; set; }
}

public class UsuarioObterPorNomeQueryHandler : IRequestHandler<UsuarioObterPorNomeQuery, List<UsuarioDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UsuarioObterPorNomeQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UsuarioDto>> Handle(UsuarioObterPorNomeQuery request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios
            .Where(x => x.Nome.Contains(request.Nome))
            .OrderBy(x => x.Nome)
            .Take(20)
            .ToListAsync();

        var vm = _mapper.Map<List<UsuarioDto>>(usuario);
        return vm;
    }
}
