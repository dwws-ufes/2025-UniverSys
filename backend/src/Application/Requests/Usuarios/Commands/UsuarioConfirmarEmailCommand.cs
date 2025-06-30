namespace UniverSys.Application.Requests.Usuarios.Commands;

public class UsuarioConfirmarEmailCommand : IRequest<Unit>
{
    public string Token { get; set; }
}

public class UsuarioConfirmarEmailCommandHandler : IRequestHandler<UsuarioConfirmarEmailCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UsuarioConfirmarEmailCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UsuarioConfirmarEmailCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.TokenRecuperacaoSenha == request.Token);

        if (usuario == null)
            throw new ValidationException("NaoEncontrado", "Usuário não encontrado");

        //if (usuario.Situacao == SituacaoUsuario.Ativo)
        //    throw new ValidationException("NaoEncontrado", "Usuário já está ativo");
        //else if (usuario.Situacao == SituacaoUsuario.Inativo)
        //    throw new ValidationException("NaoEncontrado", "Usuário está inativo");
        //else if (usuario.Situacao == SituacaoUsuario.PendenteConfirmacao)
        //{
        //    usuario.Situacao = SituacaoUsuario.Ativo;
        //    _context.Usuarios.Update(usuario);
        //    await _context.SaveChangesAsync(cancellationToken);
        //}

        return Unit.Value;
    }
}



