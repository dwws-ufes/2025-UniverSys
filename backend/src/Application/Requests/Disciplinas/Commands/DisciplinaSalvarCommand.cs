namespace UniverSys.Application.Requests.Disciplinas.Commands;

public class DisciplinaSalvarCommand : IRequest<int>
{
    public int? Id { get; set; }
    public string Nome { get; set; }
    public string Codigo { get; set; }
    public int CargaHoraria { get; set; }
    public string Ementa { get; set; }
    public int CursoId { get; set; }
}

public class DisciplinaSalvarCommandHandler : IRequestHandler<DisciplinaSalvarCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DisciplinaSalvarCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(DisciplinaSalvarCommand request, CancellationToken cancellationToken)
    {
        Disciplina disciplina;

        if (request.Id.HasValue)
        {
            disciplina = await _context.Disciplinas.FirstOrDefaultAsync(x => x.Id == request.Id);
            _mapper.Map(request, disciplina);
            _context.Disciplinas.Update(disciplina);
        }
        else
        {
            disciplina = new Disciplina();
            _mapper.Map(request, disciplina);
            _context.Disciplinas.Add(disciplina);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return disciplina.Id;
    }
} 