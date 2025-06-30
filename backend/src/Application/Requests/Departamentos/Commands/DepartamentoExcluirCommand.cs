namespace UniverSys.Application.Requests.Departamentos.Commands;

public class DepartamentoExcluirCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class DepartamentoExcluirCommandHandler : IRequestHandler<DepartamentoExcluirCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DepartamentoExcluirCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DepartamentoExcluirCommand request, CancellationToken cancellationToken)
    {
        var departamento = await _context.Departamentos
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        _context.Departamentos.Remove(departamento);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
} 