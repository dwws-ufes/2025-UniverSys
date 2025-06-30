using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace UniverSys.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.Login;

        await Task.FromResult(0);

        _logger.LogInformation("UniverSys Request: {Name} {UserId} {@Request}",
            requestName, userId, request);
    }
}
