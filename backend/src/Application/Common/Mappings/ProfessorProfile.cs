using UniverSys.Application.Requests.Professores.Commands;
using UniverSys.Application.Requests.Professores.Queries.Dto;

namespace UniverSys.Application.Common.Mappings;

public class ProfessorProfile : Profile
{
    public ProfessorProfile()
    {
        CreateMap<ProfessorSalvarCommand, Professor>();
        CreateMap<Professor, ProfessorObterPorIdDto>();
        CreateMap<Professor, ProfessorObterDto>();
    }
} 