using Microsoft.AspNetCore.Authorization;
using UniverSys.Application.Requests.Departamentos.Commands;
using UniverSys.Application.Requests.Departamentos.Queries;
using UniverSys.Application.Requests.Departamentos.Queries.Dto;

namespace UniverSys.WebUI.Controllers;

//[Authorize]
public class DepartamentosController : ApiControllerBase
{
    [HttpPost]
    public async Task<int> Salvar(DepartamentoSalvarCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet("obter-por-id/{id}")]
    public async Task<DepartamentoObterPorIdDto> ObterPorId(int id)
    {
        return await Mediator.Send(new DepartamentoObterPorIdQuery
        {
            Id = id
        });
    }

    [HttpPost("obter")]
    public async Task<PaginatedList<DepartamentoObterDto>> Obter(DepartamentoObterQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Excluir(int id)
    {
        await Mediator.Send(new DepartamentoExcluirCommand
        {
            Id = id
        });
        
        return Ok();
    }
} 