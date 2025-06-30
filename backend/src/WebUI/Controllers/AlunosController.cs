using Microsoft.AspNetCore.Authorization;
using UniverSys.Application.Requests.Alunos.Commands;
using UniverSys.Application.Requests.Alunos.Queries;
using UniverSys.Application.Requests.Alunos.Queries.Dto;

namespace UniverSys.WebUI.Controllers;

//[Authorize]
public class AlunosController : ApiControllerBase
{
    [HttpPost]
    public async Task<int> Salvar(AlunoSalvarCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet("obter-por-id/{id}")]
    public async Task<AlunoObterPorIdDto> ObterPorId(int id)
    {
        return await Mediator.Send(new AlunoObterPorIdQuery
        {
            Id = id
        });
    }

    [HttpPost("obter")]
    public async Task<PaginatedList<AlunoObterDto>> Obter(AlunoObterQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Excluir(int id)
    {
        await Mediator.Send(new AlunoExcluirCommand
        {
            Id = id
        });
        
        return Ok();
    }
} 