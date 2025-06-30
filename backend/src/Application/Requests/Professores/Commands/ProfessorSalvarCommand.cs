using UniverSys.Domain.Enums;

namespace UniverSys.Application.Requests.Professores.Commands;

public class ProfessorSalvarCommand : IRequest<int>
{
    public int? Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public Especializacao Especializacao { get; set; }
    public int? UsuarioId { get; set; }
    public int DepartamentoId { get; set; }
}

public class ProfessorSalvarCommandHandler : IRequestHandler<ProfessorSalvarCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProfessorSalvarCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(ProfessorSalvarCommand request, CancellationToken cancellationToken)
    {
        Professor professor;

        if (request.Id.HasValue)
        {
            professor = await _context.Professores.FirstOrDefaultAsync(x => x.Id == request.Id);
            _mapper.Map(request, professor);
            _context.Professores.Update(professor);
        }
        else
        {
            professor = new Professor();
            _mapper.Map(request, professor);
            _context.Professores.Add(professor);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return professor.Id;
    }
} 