namespace UniverSys.Application.Requests.Matriculas.Commands;

public class MatriculaRealizarCommand : IRequest<int>
{
    public int AlunoId { get; set; }
    public int TurmaId { get; set; }
}

public class MatriculaRealizarCommandHandler : IRequestHandler<MatriculaRealizarCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MatriculaRealizarCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(MatriculaRealizarCommand request, CancellationToken cancellationToken)
    {
        var matricula = new Matricula();
        _mapper.Map(request, matricula);
        
        _context.Matriculas.Add(matricula);

        await _context.SaveChangesAsync(cancellationToken);

        return matricula.Id;
    }
} 