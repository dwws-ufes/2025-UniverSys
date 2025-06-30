using Microsoft.Extensions.Logging;
using System.Text.Json;
using ValidationException = UniverSys.Application.Common.Exceptions.ValidationException;

namespace UniverSys.Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService currentUserService;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        this.currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (ValidationException ex)
        {
            var requestName = typeof(TRequest).Name;
            var userId = currentUserService.Login;
            _logger.LogError(ex, "UniverSys Request: User: {UserId} Unhandled Exception for Request {Name} {Request} - ValidationErrors: {Erros}", userId, requestName, request, JsonSerializer.Serialize(ex.Errors));

            throw;
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            var userId = currentUserService.Login;
            _logger.LogError(ex, "UniverSys Request: User: {UserId} Unhandled Exception for Request {Name} {Request}", userId, requestName, request);

            throw;
        }
    }
}
