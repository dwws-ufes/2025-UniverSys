namespace UniverSys.Application.Requests.Notas.Commands;

public class NotaSalvarCommand : IRequest<int>
{
    public int? Id { get; set; }
    public int AvaliacaoId { get; set; }
    public int MatriculaId { get; set; }
    public decimal Valor { get; set; }
}

public class NotaSalvarCommandHandler : IRequestHandler<NotaSalvarCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public NotaSalvarCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(NotaSalvarCommand request, CancellationToken cancellationToken)
    {
        Nota nota;

        if (request.Id.HasValue)
        {
            nota = await _context.Notas.FirstOrDefaultAsync(x => x.Id == request.Id);
            _mapper.Map(request, nota);
            _context.Notas.Update(nota);
        }
        else
        {
            nota = new Nota();
            _mapper.Map(request, nota);
            _context.Notas.Add(nota);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return nota.Id;
    }
} 