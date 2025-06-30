using UniverSys.Application.Requests.Departamentos.Commands;
using UniverSys.Application.Requests.Departamentos.Queries.Dto;

namespace UniverSys.Application.Common.Mappings;

public class DepartamentoProfile : Profile
{
    public DepartamentoProfile()
    {
        CreateMap<DepartamentoSalvarCommand, Departamento>();
        CreateMap<Departamento, DepartamentoObterPorIdDto>();
        CreateMap<Departamento, DepartamentoObterDto>();
    }
} 