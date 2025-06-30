using UniverSys.Application.Requests.Turmas.Commands;
using UniverSys.Application.Requests.Turmas.Queries.Dto;

namespace UniverSys.Application.Common.Mappings;

public class TurmaProfile : Profile
{
    public TurmaProfile()
    {
        CreateMap<TurmaSalvarCommand, Turma>();
        CreateMap<Turma, TurmaObterPorIdDto>();
        CreateMap<Turma, TurmaObterDto>();
    }
} 