using Microsoft.AspNetCore.Authorization;
using UniverSys.Application.Requests.Avaliacoes.Commands;
using UniverSys.Application.Requests.Avaliacoes.Queries;
using UniverSys.Application.Requests.Avaliacoes.Queries.Dto;

namespace UniverSys.WebUI.Controllers;

//[Authorize]
public class AvaliacoesController : ApiControllerBase
{
    [HttpPost]
    public async Task<int> Salvar(AvaliacaoSalvarCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet("obter-por-id/{id}")]
    public async Task<AvaliacaoObterPorIdDto> ObterPorId(int id)
    {
        return await Mediator.Send(new AvaliacaoObterPorIdQuery
        {
            Id = id
        });
    }

    [HttpPost("obter")]
    public async Task<PaginatedList<AvaliacaoObterDto>> Obter(AvaliacaoObterQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Excluir(int id)
    {
        await Mediator.Send(new AvaliacaoExcluirCommand
        {
            Id = id
        });
        
        return Ok();
    }
} 