using UniverSys.Application.Requests.Avaliacoes.Commands;
using UniverSys.Application.Requests.Avaliacoes.Queries.Dto;

namespace UniverSys.Application.Common.Mappings;

public class AvaliacaoProfile : Profile
{
    public AvaliacaoProfile()
    {
        CreateMap<AvaliacaoSalvarCommand, Avaliacao>();
        CreateMap<Avaliacao, AvaliacaoObterPorIdDto>();
        CreateMap<Avaliacao, AvaliacaoObterDto>();
    }
} 