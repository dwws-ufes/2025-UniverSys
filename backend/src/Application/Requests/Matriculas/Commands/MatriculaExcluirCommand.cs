namespace UniverSys.Application.Requests.Matriculas.Commands;

public class MatriculaExcluirCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class MatriculaExcluirCommandHandler : IRequestHandler<MatriculaExcluirCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MatriculaExcluirCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(MatriculaExcluirCommand request, CancellationToken cancellationToken)
    {
        var matricula = await _context.Matriculas
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        _context.Matriculas.Remove(matricula);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
} 