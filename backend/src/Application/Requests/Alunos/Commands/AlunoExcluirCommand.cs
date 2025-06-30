namespace UniverSys.Application.Requests.Alunos.Commands;

public class AlunoExcluirCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class AlunoExcluirCommandHandler : IRequestHandler<AlunoExcluirCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AlunoExcluirCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(AlunoExcluirCommand request, CancellationToken cancellationToken)
    {
        var aluno = await _context.Alunos
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        _context.Alunos.Remove(aluno);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
} 