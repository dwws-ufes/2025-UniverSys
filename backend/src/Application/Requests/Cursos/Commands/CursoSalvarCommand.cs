namespace UniverSys.Application.Requests.Cursos.Commands;

public class CursoSalvarCommand : IRequest<int>
{
    public int? Id { get; set; }
    public string Nome { get; set; }
    public int DuracaoSemestres { get; set; }
}

public class CursoSalvarCommandHandler : IRequestHandler<CursoSalvarCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CursoSalvarCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CursoSalvarCommand request, CancellationToken cancellationToken)
    {
        Curso curso;

        if (request.Id.HasValue)
        {
            curso = await _context.Cursos.FirstOrDefaultAsync(x => x.Id == request.Id);
            _mapper.Map(request, curso);
            _context.Cursos.Update(curso);
        }
        else
        {
            curso = new Curso();
            _mapper.Map(request, curso);
            _context.Cursos.Add(curso);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return curso.Id;
    }
} 