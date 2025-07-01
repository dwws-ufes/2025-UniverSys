using Microsoft.AspNetCore.Authorization;
using UniverSys.Application.Requests.Notas.Commands;
using UniverSys.Application.Requests.Notas.Queries;
using UniverSys.Application.Requests.Notas.Queries.Dto;

namespace UniverSys.WebUI.Controllers;

//[Authorize]
public class NotasController : ApiControllerBase
{
    [HttpPost]
    public async Task<int> Salvar(NotaSalvarCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet("obter-por-id/{id}")]
    public async Task<NotaObterPorIdDto> ObterPorId(int id)
    {
        return await Mediator.Send(new NotaObterPorIdQuery
        {
            Id = id
        });
    }

    [HttpPost("obter")]
    public async Task<PaginatedList<NotaObterDto>> Obter(NotaObterQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Excluir(int id)
    {
        await Mediator.Send(new NotaExcluirCommand
        {
            Id = id
        });
        
        return Ok();
    }
} 