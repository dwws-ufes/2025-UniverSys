namespace UniverSys.Application.Requests.Disciplinas.Commands;

public class DisciplinaExcluirCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class DisciplinaExcluirCommandHandler : IRequestHandler<DisciplinaExcluirCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DisciplinaExcluirCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DisciplinaExcluirCommand request, CancellationToken cancellationToken)
    {
        var disciplina = await _context.Disciplinas
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        _context.Disciplinas.Remove(disciplina);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
} 