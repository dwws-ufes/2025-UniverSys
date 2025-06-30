namespace UniverSys.Application.Requests.Professores.Commands;

public class ProfessorExcluirCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class ProfessorExcluirCommandHandler : IRequestHandler<ProfessorExcluirCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProfessorExcluirCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(ProfessorExcluirCommand request, CancellationToken cancellationToken)
    {
        var professor = await _context.Professores
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        _context.Professores.Remove(professor);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
} 