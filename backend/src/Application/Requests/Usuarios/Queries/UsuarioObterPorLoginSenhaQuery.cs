using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace UniverSys.Application.Requests.Usuarios.Queries;
public class UsuarioObterPorLoginSenhaQuery : IRequest<UsuarioDto>
{
    public string Login { get; set; }

    /// <summary>
    /// Senha não criptografada
    /// </summary>
    public string Senha { get; set; }
}

public class UsuarioObterPorLoginSenhaQueryHandler : IRequestHandler<UsuarioObterPorLoginSenhaQuery, UsuarioDto>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;
    private readonly IHostingEnvironment hostingEnvironment;

    public UsuarioObterPorLoginSenhaQueryHandler(IApplicationDbContext context,
        IMapper mapper,
        IHostingEnvironment hostingEnvironment)
    {
        this.context = context;
        this.mapper = mapper;
        this.hostingEnvironment = hostingEnvironment;
    }

    public async Task<UsuarioDto> Handle(UsuarioObterPorLoginSenhaQuery request, CancellationToken cancellationToken)
    {
        var passwordHasher = new PasswordHasher<Usuario>();
        var usuario = await context.Usuarios
                .SingleOrDefaultAsync(x => x.Login == request.Login || x.Email == request.Login);

        if (usuario == null)
            return null;

        var result = passwordHasher.VerifyHashedPassword(usuario, usuario.Senha ?? "", request.Senha);

        if (hostingEnvironment.IsDevelopment())
            result = PasswordVerificationResult.Success;

        if (result == PasswordVerificationResult.Success)
        {
            var usuarioDto = mapper.Map<UsuarioDto>(usuario);

            return usuarioDto;
        }

        return null;
    }
}