namespace UniverSys.Application.Requests.Notas.Commands;

public class NotaExcluirCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class NotaExcluirCommandHandler : IRequestHandler<NotaExcluirCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public NotaExcluirCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(NotaExcluirCommand request, CancellationToken cancellationToken)
    {
        var nota = await _context.Notas
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        _context.Notas.Remove(nota);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
} 