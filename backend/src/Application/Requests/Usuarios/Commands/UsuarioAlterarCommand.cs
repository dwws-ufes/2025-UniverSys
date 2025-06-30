namespace UniverSys.Application.Requests.Usuarios.Commands;

public class UsuarioAlterarCommand : IRequest<UsuarioDto>
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
}

public class UsuarioAlterarCommandHandler : IRequestHandler<UsuarioAlterarCommand, UsuarioDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UsuarioAlterarCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UsuarioDto> Handle(UsuarioAlterarCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Usuarios.AnyAsync(x => x.Login == request.Login && x.Id != request.Id))
            throw new ValidationException("Login", "Já existe um cadastro com o login informado");

        var usuario = await _context.Usuarios.FindAsync(request.Id);

        _mapper.Map(request, usuario);

        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UsuarioDto>(usuario);
    }
}
