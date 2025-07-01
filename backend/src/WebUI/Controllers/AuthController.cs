using Microsoft.Extensions.Logging;
using UniverSys.Application.Requests.Auth.Commands;
using UniverSys.Application.Requests.Auth.Dto;

namespace UniverSys.WebUI.Controllers;

/// <summary>
/// APIs para gerenciamento de autenticação/tokens de acesso
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly ILogger<AuthController> logger;

    public AuthController(
        IMediator mediator,
        ILogger<AuthController> logger
    )
    {
        this.mediator = mediator;
        this.logger = logger;
    }

    [HttpPost, Route("login")]
    public async Task<ActionResult<LoginModelOutput>> Login([FromBody] LoginModel user)
    {
        var command = new LoginCommand
        {
            Login = user.Login,
            Senha = user.Senha,
        };

        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPost, Route("validar-token")]
    public async Task<ActionResult<bool>> ValidarToken([FromBody] ValidateTokenModel model)
    {
        var command = new ValidarTokenCommand { Token = model.Token };
        var result = await mediator.Send(command);
        return Ok(result);
    }
}
