using UniverSys.Application.Common.Interfaces;
using UniverSys.Application.Requests.Cursos.Queries.Dto;

namespace UniverSys.Application.Requests.Cursos.Queries;

public class CursoObterResumoQuery : IRequest<CursoObterResumoDto>
{
    public string NomeCurso { get; set; }
}

public class CursoObterResumoQueryHandler : IRequestHandler<CursoObterResumoQuery, CursoObterResumoDto>
{
    private readonly ILinkedDataService _linkedDataService;

    public CursoObterResumoQueryHandler(ILinkedDataService linkedDataService)
    {
        _linkedDataService = linkedDataService;
    }

    public async Task<CursoObterResumoDto> Handle(CursoObterResumoQuery request, CancellationToken cancellationToken)
    {
        var resumo = await _linkedDataService.ObterResumoCurso(request.NomeCurso);

        return new CursoObterResumoDto
        {
            NomeCurso = request.NomeCurso,
            Resumo = resumo
        };
    }
}