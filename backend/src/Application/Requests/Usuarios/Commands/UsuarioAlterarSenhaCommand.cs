using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace UniverSys.Application.Requests.Usuarios.Commands;

public class UsuarioAlterarSenhaCommand : IRequest<bool>
{
    public int UsuarioId { get; set; }
    public string Senha { get; set; }
    public string ConfirmacaoSenha { get; set; }
}

public class UsuarioAlterarSenhaCommandHandler : IRequestHandler<UsuarioAlterarSenhaCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UsuarioAlterarSenhaCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UsuarioAlterarSenhaCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios.FindAsync(request.UsuarioId);

        if (usuario == null)
        {
            throw new ValidationException(new List<ValidationFailure>
                    {
                        new ValidationFailure("Usuário", "Usuário não encontrado")
                    });
        }

        var passwordHasher = new PasswordHasher<Usuario>();
        usuario.Senha = passwordHasher.HashPassword(usuario, request.Senha);

        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
