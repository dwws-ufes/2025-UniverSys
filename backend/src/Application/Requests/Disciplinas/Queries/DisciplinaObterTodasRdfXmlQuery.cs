using UniverSys.Application.Common.Interfaces;
using UniverSys.Application.Requests.Disciplinas.Queries.Dto;

namespace UniverSys.Application.Requests.Disciplinas.Queries;

public class DisciplinasRdfFileContentDto
{
    public required byte[] Content { get; init; }
    public required string ContentType { get; init; }
    public required string FileName { get; init; }
}

public class DisciplinaObterTodasRdfXmlQuery : IRequest<DisciplinasRdfFileContentDto>
{
}

public class DisciplinaObterTodasRdfXmlQueryHandler : IRequestHandler<DisciplinaObterTodasRdfXmlQuery, DisciplinasRdfFileContentDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IRdfService _rdfService;

    public DisciplinaObterTodasRdfXmlQueryHandler(IApplicationDbContext context, IMapper mapper, IRdfService rdfService)
    {
        _context = context;
        _mapper = mapper;
        _rdfService = rdfService;
    }

    public async Task<DisciplinasRdfFileContentDto> Handle(DisciplinaObterTodasRdfXmlQuery request, CancellationToken cancellationToken)
    {
        var lista = await _context.Disciplinas
            .AsNoTracking()
            .ProjectTo<DisciplinaObterPorIdDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var bytes = _rdfService.BuildDisciplinasRdfXml(lista, "http://localhost:4200");
        return new DisciplinasRdfFileContentDto
        {
            Content = bytes,
            ContentType = "application/rdf+xml",
            FileName = "disciplinas.rdf"
        };
    }
}


