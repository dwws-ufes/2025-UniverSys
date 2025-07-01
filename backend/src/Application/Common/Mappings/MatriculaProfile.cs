using UniverSys.Application.Requests.Matriculas.Commands;
using UniverSys.Application.Requests.Matriculas.Queries.Dto;

namespace UniverSys.Application.Common.Mappings;

public class MatriculaProfile : Profile
{
    public MatriculaProfile()
    {
        CreateMap<MatriculaRealizarCommand, Matricula>();
        CreateMap<Matricula, MatriculaObterPorIdDto>();
        CreateMap<Matricula, MatriculaObterDto>();
    }
} 