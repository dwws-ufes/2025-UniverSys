using UniverSys.Application.Requests.Disciplinas.Commands;
using UniverSys.Application.Requests.Disciplinas.Queries.Dto;

namespace UniverSys.Application.Common.Mappings;

public class DisciplinaProfile : Profile
{
    public DisciplinaProfile()
    {
        CreateMap<DisciplinaSalvarCommand, Disciplina>();
        CreateMap<Disciplina, DisciplinaObterPorIdDto>();
        CreateMap<Disciplina, DisciplinaObterDto>();
    }
} 