namespace UniverSys.Application.Requests.Avaliacoes.Commands;

public class AvaliacaoSalvarCommand : IRequest<int>
{
    public int? Id { get; set; }
    public string Nome { get; set; }
    public int Peso { get; set; }
    public int TurmaId { get; set; }
}

public class AvaliacaoSalvarCommandHandler : IRequestHandler<AvaliacaoSalvarCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AvaliacaoSalvarCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(AvaliacaoSalvarCommand request, CancellationToken cancellationToken)
    {
        Avaliacao avaliacao;

        if (request.Id.HasValue)
        {
            avaliacao = await _context.Avaliacoes.FirstOrDefaultAsync(x => x.Id == request.Id);
            _mapper.Map(request, avaliacao);
            _context.Avaliacoes.Update(avaliacao);
        }
        else
        {
            avaliacao = new Avaliacao();
            _mapper.Map(request, avaliacao);
            _context.Avaliacoes.Add(avaliacao);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return avaliacao.Id;
    }
} 