namespace UniverSys.Application.Requests.Usuarios.Queries;

//[Authorize(Permissao = Permissoes.Administrador)]
public class UsuarioObterPorLoginQuery : IRequest<UsuarioDto>
{
    public string Login { get; set; }
}

public class ObterPorLoginQueryHandler : IRequestHandler<UsuarioObterPorLoginQuery, UsuarioDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ObterPorLoginQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UsuarioDto> Handle(UsuarioObterPorLoginQuery request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(x => x.Login == request.Login || x.Email == request.Login, cancellationToken: cancellationToken);

        if (usuario == null)
            return null;


        var usuarioVm = _mapper.Map<UsuarioDto>(usuario);


        return usuarioVm;
    }
}
