using UniverSys.Application.Requests.Alunos.Commands;
using UniverSys.Application.Requests.Alunos.Queries.Dto;

namespace UniverSys.Application.Common.Mappings;

public class AlunoProfile : Profile
{
    public AlunoProfile()
    {
        CreateMap<AlunoSalvarCommand, Aluno>();
        CreateMap<Aluno, AlunoObterPorIdDto>();
        CreateMap<Aluno, AlunoObterDto>();
    }
} 