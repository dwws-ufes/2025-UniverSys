namespace UniverSys.Application.Requests.Avaliacoes.Commands;

public class AvaliacaoExcluirCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class AvaliacaoExcluirCommandHandler : IRequestHandler<AvaliacaoExcluirCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AvaliacaoExcluirCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(AvaliacaoExcluirCommand request, CancellationToken cancellationToken)
    {
        var avaliacao = await _context.Avaliacoes
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        _context.Avaliacoes.Remove(avaliacao);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
} 