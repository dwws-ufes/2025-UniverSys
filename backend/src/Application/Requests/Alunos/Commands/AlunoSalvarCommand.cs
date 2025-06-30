namespace UniverSys.Application.Requests.Alunos.Commands;

public class AlunoSalvarCommand : IRequest<int>
{
    public int? Id { get; set; }
    public string Matricula { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public int? UsuarioId { get; set; }
    public int CursoId { get; set; }
}

public class AlunoSalvarCommandHandler : IRequestHandler<AlunoSalvarCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AlunoSalvarCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(AlunoSalvarCommand request, CancellationToken cancellationToken)
    {
        Aluno aluno;

        if (request.Id.HasValue)
        {
            aluno = await _context.Alunos.FirstOrDefaultAsync(x => x.Id == request.Id);
            _mapper.Map(request, aluno);
            _context.Alunos.Update(aluno);
        }
        else
        {
            aluno = new Aluno();
            _mapper.Map(request, aluno);
            _context.Alunos.Add(aluno);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return aluno.Id;
    }
} 