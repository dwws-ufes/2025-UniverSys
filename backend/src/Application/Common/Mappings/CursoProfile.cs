using UniverSys.Application.Requests.Cursos.Commands;
using UniverSys.Application.Requests.Cursos.Queries.Dto;

namespace UniverSys.Application.Common.Mappings;

public class CursoProfile : Profile
{
    public CursoProfile()
    {
        CreateMap<CursoSalvarCommand, Curso>();
        CreateMap<Curso, CursoObterPorIdDto>();
        CreateMap<Curso, CursoObterDto>();
    }
} 