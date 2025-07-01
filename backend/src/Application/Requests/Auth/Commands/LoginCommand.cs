using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using UniverSys.Application.Common.Interfaces;
using UniverSys.Application.Requests.Auth.Dto;
using UniverSys.Application.Requests.Usuarios.Queries;

namespace UniverSys.Application.Requests.Auth.Commands;

public class LoginCommand : IRequest<LoginModelOutput>
{
    public string Login { get; set; }
    public string Senha { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginModelOutput>
{
    private readonly ITokenService tokenService;
    private readonly IMediator mediator;
    private readonly IWebHostEnvironment webHostEnvironment;

    public LoginCommandHandler(
        ITokenService tokenService,
        IMediator mediator,
        IWebHostEnvironment webHostEnvironment
    )
    {
        this.tokenService = tokenService;
        this.mediator = mediator;
        this.webHostEnvironment = webHostEnvironment;
    }

    public async Task<LoginModelOutput> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (request == null || string.IsNullOrEmpty(request.Login))
            throw new ArgumentException("Invalid client request");

        request.Login = request.Login.ToLower();

        var usuario = await mediator.Send(new UsuarioObterPorLoginQuery { Login = request.Login }, cancellationToken);

        bool autenticacaoValida = false;

        if (
            (webHostEnvironment.IsDevelopment() || webHostEnvironment.EnvironmentName == "Test")
            && request.Senha == "teste"
        )
        {
            autenticacaoValida = true;
        }
        else
        {
            usuario = await mediator.Send(
                new UsuarioObterPorLoginSenhaQuery { Login = request.Login, Senha = request.Senha }, 
                cancellationToken
            );
        }

        autenticacaoValida = usuario is not null ;

        if (!autenticacaoValida)
            throw new UnauthorizedAccessException("Invalid credentials");

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, request.Login),
            new("login", request.Login),
            new("usuarioId", usuario.Id.ToString()),
            new("tipo", usuario.Tipo.ToString()),
        };

        if (!string.IsNullOrEmpty(usuario.Nome))
        {
            claims.Add(new Claim("nome", usuario?.Nome));
        }

        if (usuario.AlunoId.HasValue)
        {
            claims.Add(new Claim("alunoId", usuario.AlunoId.Value.ToString()));
        }

        if (usuario.AlunoCursoId.HasValue)
        {
            claims.Add(new Claim("alunoCursoId", usuario.AlunoCursoId.Value.ToString()));
        }

        if (usuario.ProfessorId.HasValue)
        {
            claims.Add(new Claim("professorId", usuario.ProfessorId.Value.ToString()));
        }

        //gera tokens
        var tokenString = tokenService.GenerateAccessToken(claims);
        var refreshToken = tokenService.GenerateRefreshToken();

        return new LoginModelOutput 
        { 
            AccessToken = tokenString, 
            RefreshToken = refreshToken 
        };
    }
} 