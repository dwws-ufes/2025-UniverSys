using UniverSys.Application.Common.Interfaces;

namespace UniverSys.Application.Requests.Auth.Commands;

public class ValidarTokenCommand : IRequest<bool>
{
    public string Token { get; set; }
}

public class ValidarTokenCommandHandler : IRequestHandler<ValidarTokenCommand, bool>
{
    private readonly ITokenService tokenService;

    public ValidarTokenCommandHandler(ITokenService tokenService)
    {
        this.tokenService = tokenService;
    }

    public async Task<bool> Handle(ValidarTokenCommand request, CancellationToken cancellationToken)
    {
        return tokenService.ValidateCurrentToken(request.Token);
    }
} 