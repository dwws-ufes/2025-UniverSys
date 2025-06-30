namespace UniverSys.Application.Requests.Turmas.Commands;

public class TurmaSalvarCommand : IRequest<int>
{
    public int? Id { get; set; }
    public int DisciplinaId { get; set; }
    public int ProfessorId { get; set; }
    public int SemestreAno { get; set; }
    public int SemestrePeriodo { get; set; }
    public string Nome { get; set; }
}

public class TurmaSalvarCommandHandler : IRequestHandler<TurmaSalvarCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public TurmaSalvarCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(TurmaSalvarCommand request, CancellationToken cancellationToken)
    {
        Turma turma;

        if (request.Id.HasValue)
        {
            turma = await _context.Turmas.FirstOrDefaultAsync(x => x.Id == request.Id);
            _mapper.Map(request, turma);
            _context.Turmas.Update(turma);
        }
        else
        {
            turma = new Turma();
            _mapper.Map(request, turma);
            _context.Turmas.Add(turma);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return turma.Id;
    }
} 