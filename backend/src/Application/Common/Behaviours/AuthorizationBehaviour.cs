using Microsoft.Extensions.Logging;

namespace UniverSys.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<AuthorizationBehaviour<TRequest, TResponse>> logger;

    public AuthorizationBehaviour(
        ICurrentUserService currentUserService,
        ILogger<AuthorizationBehaviour<TRequest, TResponse>> logger)
    {
        _currentUserService = currentUserService;
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            // Must be authenticated user
            if (_currentUserService.Login == null)
            {
                throw new UnauthorizedAccessException();
            }

            var authorizeAttributesWithPermissions = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Permissao));

            if (authorizeAttributesWithPermissions.Any())
            {
                //var permissoesUsuarioLogado = permissaoAcessoService.ObterPermissoesDoUsuario(_currentUserService.Login);

                //if (!permissoesUsuarioLogado.Contains(Permissoes.Administrador))
                //{
                //    var permissions = authorizeAttributesWithPermissions.Select(a => a.Permissao);

                //    foreach (var permission in permissions)
                //    {
                //        var maisDeUmaPermissao = permission.Contains(',');

                //        bool authorized;

                //        if (maisDeUmaPermissao)
                //        {
                //            var permissoes = permission.Split(',');
                //            authorized = permissoesUsuarioLogado.Intersect(permissoes).Any();
                //        }
                //        else
                //        {
                //            authorized = permissoesUsuarioLogado.Contains(permission);
                //        }
                //        if (!authorized)
                //        {
                //            logger.LogWarning("Usuário: {usuario} sem permissão. Permissão: {permissao}", _currentUserService.Login, permission);
                //            throw new ForbiddenAccessException();
                //        }
                //    }
                //}
            }


        }

        // User is authorized / authorization not required
        return await next();
    }
}