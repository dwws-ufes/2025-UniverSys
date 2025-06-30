//using UniverSys.Application.Requests.Clientes.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using UniverSys.Application.Requests.Usuarios.Queries;

namespace UniverSys.WebUI.Controllers;

public class LoginModel
{
    public string Login { get; set; }
    public string Senha { get; set; }
    public int? ClienteId { get; set; }
}

public class LoginModelOutput
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

public class TokenApiModel
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

public class ValidateTokenModel
{
    public string Token { get; set; }
}

/// <summary>
/// APIs para gerenciamento de autenticação/tokens de acesso
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ITokenService tokenService;
    private readonly IMediator mediator;
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly ILogger<AuthController> logger;

    public AuthController(
        ITokenService tokenService,
        IMediator mediator,
        IWebHostEnvironment webHostEnvironment,
        ILogger<AuthController> logger
    )
    {
        this.tokenService = tokenService;
        this.mediator = mediator;
        this.webHostEnvironment = webHostEnvironment;
        this.logger = logger;
    }

    [HttpPost, Route("login")]
    public async Task<ActionResult<LoginModelOutput>> Login([FromBody] LoginModel user)
    {
        try
        {
            if (user == null || string.IsNullOrEmpty(user.Login))
                return BadRequest("Invalid client request");

            user.Login = user.Login.ToLower();

            var usuario = await mediator.Send(new UsuarioObterPorLoginQuery { Login = user.Login });

            bool autenticacaoValida = false;

            if (
                (webHostEnvironment.IsDevelopment() || webHostEnvironment.EnvironmentName == "Test")
                && user.Senha == "teste"
            )
            {
                autenticacaoValida = true;
            }
            else
            {
                usuario = await mediator.Send(
                    new UsuarioObterPorLoginSenhaQuery { Login = user.Login, Senha = user.Senha }
                );
            }

            autenticacaoValida = usuario is not null;

            if (autenticacaoValida)
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Login),
                    new("login", user.Login),
                    new("usuarioId", usuario.Id.ToString()),
                    new("tipo", usuario.Tipo.ToString()),
                };

                if (!string.IsNullOrEmpty(usuario.Nome))
                {
                    claims.Add(new Claim("nome", usuario?.Nome));
                }

                //gera tokens
                var tokenString = tokenService.GenerateAccessToken(claims);
                var refreshToken = tokenService.GenerateRefreshToken();

                return Ok(
                    new LoginModelOutput { AccessToken = tokenString, RefreshToken = refreshToken }
                );
            }
            else
            {
                return Unauthorized();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro não tratado ao efetuar login. Login: {login}", user.Login);
            throw;
        }
    }

    [HttpPost, Route("validar-token")]
    public ActionResult<bool> ValidarToken([FromBody] ValidateTokenModel model)
    {
        return tokenService.ValidateCurrentToken(model.Token);
    }
}
