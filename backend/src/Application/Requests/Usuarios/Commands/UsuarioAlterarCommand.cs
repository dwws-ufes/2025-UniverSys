using Microsoft.AspNetCore.Identity;
using UniverSys.Domain.Enums;

namespace UniverSys.Application.Requests.Usuarios.Commands;

public class UsuarioAlterarCommand : IRequest<UsuarioDto>
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public TipoUsuario Tipo { get; set; }
    public int? AlunoId { get; set; }
    public int? ProfessorId { get; set; }
}

public class UsuarioAlterarCommandHandler : IRequestHandler<UsuarioAlterarCommand, UsuarioDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UsuarioAlterarCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UsuarioDto> Handle(UsuarioAlterarCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Usuarios.AnyAsync(x => x.Login == request.Login && x.Id != request.Id))
            throw new ValidationException("Login", "Já existe um cadastro com o login informado");

        var usuario = await _context.Usuarios
            .Include(x => x.Aluno)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        ConfigurarSenhaUsuario(request, usuario);

        if (request.AlunoId.HasValue)
        {
            var aluno = await _context.Alunos.FindAsync(request.AlunoId);
            usuario.Aluno = aluno;
        }

        if (request.ProfessorId.HasValue)
        {
            var professor = await _context.Professores.FindAsync(request.ProfessorId);
            usuario.Professor = professor;
        }

        _mapper.Map(request, usuario);

        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UsuarioDto>(usuario);
    }

    private static void ConfigurarSenhaUsuario(UsuarioAlterarCommand request, Usuario usuario)
    {
        if (!string.IsNullOrWhiteSpace(request.Senha))
        {
            var passwordHasher = new PasswordHasher<Usuario>();
            usuario.Senha = passwordHasher.HashPassword(usuario, request.Senha);
        }
    }
}
