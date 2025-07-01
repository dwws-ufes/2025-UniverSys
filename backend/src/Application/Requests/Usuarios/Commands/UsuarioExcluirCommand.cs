namespace UniverSys.Application.Requests.Usuarios.Commands;

public class UsuarioExcluirCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class UsuarioExcluirCommandHandler : IRequestHandler<UsuarioExcluirCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UsuarioExcluirCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UsuarioExcluirCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        _context.Usuarios.Remove(usuario);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
} 