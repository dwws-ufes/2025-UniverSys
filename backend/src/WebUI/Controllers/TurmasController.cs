using Microsoft.AspNetCore.Authorization;
using UniverSys.Application.Requests.Turmas.Commands;
using UniverSys.Application.Requests.Turmas.Queries;
using UniverSys.Application.Requests.Turmas.Queries.Dto;

namespace UniverSys.WebUI.Controllers;

//[Authorize]
public class TurmasController : ApiControllerBase
{
    [HttpPost]
    public async Task<int> Salvar(TurmaSalvarCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet("obter-por-id/{id}")]
    public async Task<TurmaObterPorIdDto> ObterPorId(int id)
    {
        return await Mediator.Send(new TurmaObterPorIdQuery
        {
            Id = id
        });
    }

    [HttpPost("obter")]
    public async Task<PaginatedList<TurmaObterDto>> Obter(TurmaObterQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Excluir(int id)
    {
        await Mediator.Send(new TurmaExcluirCommand
        {
            Id = id
        });
        
        return Ok();
    }
} 