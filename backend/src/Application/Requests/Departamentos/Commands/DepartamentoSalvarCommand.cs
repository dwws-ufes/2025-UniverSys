namespace UniverSys.Application.Requests.Departamentos.Commands;

public class DepartamentoSalvarCommand : IRequest<int>
{
    public int? Id { get; set; }
    public string Nome { get; set; }
}

public class DepartamentoSalvarCommandHandler : IRequestHandler<DepartamentoSalvarCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DepartamentoSalvarCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(DepartamentoSalvarCommand request, CancellationToken cancellationToken)
    {
        Departamento departamento;

        if (request.Id.HasValue)
        {
            departamento = await _context.Departamentos.FirstOrDefaultAsync(x => x.Id == request.Id);
            _mapper.Map(request, departamento);
            _context.Departamentos.Update(departamento);
        }
        else
        {
            departamento = new Departamento();
            _mapper.Map(request, departamento);
            _context.Departamentos.Add(departamento);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return departamento.Id;
    }
} 