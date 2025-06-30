namespace UniverSys.Application.Requests.Cursos.Commands;

public class CursoExcluirCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class CursoExcluirCommandHandler : IRequestHandler<CursoExcluirCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CursoExcluirCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CursoExcluirCommand request, CancellationToken cancellationToken)
    {
        var curso = await _context.Cursos
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        _context.Cursos.Remove(curso);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
} 