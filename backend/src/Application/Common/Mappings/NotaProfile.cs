using UniverSys.Application.Requests.Notas.Commands;
using UniverSys.Application.Requests.Notas.Queries.Dto;

namespace UniverSys.Application.Common.Mappings;

public class NotaProfile : Profile
{
    public NotaProfile()
    {
        CreateMap<NotaSalvarCommand, Nota>();
        CreateMap<Nota, NotaObterPorIdDto>();
        CreateMap<Nota, NotaObterDto>();
    }
} 