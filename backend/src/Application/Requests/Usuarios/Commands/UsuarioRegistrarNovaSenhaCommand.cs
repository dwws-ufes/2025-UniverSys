using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace UniverSys.Application.Requests.Usuarios.Commands;

public class UsuarioRegistrarNovaSenhaCommand : IRequest<Unit>
{
    public string TokenRecuperacaoSenha { get; set; }
    public string NovaSenha { get; set; }
    public string ConfirmacaoNovaSenha { get; set; }
}

public class UsuarioRegistrarNovaSenhaCommandHandler : IRequestHandler<UsuarioRegistrarNovaSenhaCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UsuarioRegistrarNovaSenhaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UsuarioRegistrarNovaSenhaCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios.SingleOrDefaultAsync(x => x.TokenRecuperacaoSenha == request.TokenRecuperacaoSenha, cancellationToken: cancellationToken);

        var erros = new List<ValidationFailure>();

        if (usuario == null)
            erros.Add(new ValidationFailure("Usuario", "Usuário não encontrado com o token informado."));

        if (usuario.DataSolicitacaoRecuperacaoSenha == null)
            erros.Add(new ValidationFailure("Usuario", "Data de solicitação do token não registrada."));

        // Checa se o token está dentro das quatro horas
        var timeStamp = DateTime.Now - usuario.DataSolicitacaoRecuperacaoSenha.Value;

        if (timeStamp.TotalHours >= 4)
            erros.Add(new ValidationFailure("Usuario", "Token de recuperação expirado."));

        if (erros.Any())
            throw new ValidationException(erros);

        var passwordHasher = new PasswordHasher<Usuario>();
        usuario.Senha = passwordHasher.HashPassword(usuario, request.NovaSenha);
        //usuario.Situacao = SituacaoUsuario.Ativo;

        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
