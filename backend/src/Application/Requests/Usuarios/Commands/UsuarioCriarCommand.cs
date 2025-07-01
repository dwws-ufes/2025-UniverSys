using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using Microsoft.Extensions.Configuration;
using UniverSys.Domain.Enums;

namespace UniverSys.Application.Requests.Usuarios.Commands
{
    public class UsuarioCriarCommand : IRequest<UsuarioDto>
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public TipoUsuario Tipo { get; set; }
        public int? AlunoId { get; set; }
        public int? ProfessorId { get; set; }
    }

    public class UsuarioCriarCommandHandler : IRequestHandler<UsuarioCriarCommand, UsuarioDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;

        public UsuarioCriarCommandHandler(IApplicationDbContext context,
            IMapper mapper,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<UsuarioDto> Handle(UsuarioCriarCommand request, CancellationToken cancellationToken)
        {
            await VerificarUsuarioExistente(request);

            var usuario = _mapper.Map<Usuario>(request);
            ConfigurarSenhaUsuario(request, usuario);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync(cancellationToken);


            return _mapper.Map<UsuarioDto>(usuario);
        }

        private static void ConfigurarSenhaUsuario(UsuarioCriarCommand request, Usuario usuario)
        {
            var passwordHasher = new PasswordHasher<Usuario>();
            usuario.Senha = passwordHasher.HashPassword(usuario, request.Senha);
        }

        private async Task VerificarUsuarioExistente(UsuarioCriarCommand request)
        {
            if (await _context.Usuarios.AnyAsync(x => x.Login.Equals(request.Login)))
                throw new ValidationException("Login", "Já existe um cadastro com o login informado");

            if (await _context.Usuarios.AnyAsync(x => x.Email.Equals(request.Email)))
                throw new ValidationException("Email", "Já existe um cadastro com o e-mail informado");
        }

    }
}
