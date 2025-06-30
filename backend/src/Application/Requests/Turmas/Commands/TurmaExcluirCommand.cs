namespace UniverSys.Application.Requests.Turmas.Commands;

public class TurmaExcluirCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class TurmaExcluirCommandHandler : IRequestHandler<TurmaExcluirCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public TurmaExcluirCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(TurmaExcluirCommand request, CancellationToken cancellationToken)
    {
        var turma = await _context.Turmas
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        _context.Turmas.Remove(turma);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
} 