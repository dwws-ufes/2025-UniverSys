using Microsoft.AspNetCore.Authorization;
using UniverSys.Application.Requests.Cursos.Commands;
using UniverSys.Application.Requests.Cursos.Queries;
using UniverSys.Application.Requests.Cursos.Queries.Dto;

namespace UniverSys.WebUI.Controllers;

//[Authorize]
public class CursosController : ApiControllerBase
{
    [HttpPost]
    public async Task<int> Salvar(CursoSalvarCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet("obter-por-id/{id}")]
    public async Task<CursoObterPorIdDto> ObterPorId(int id)
    {
        return await Mediator.Send(new CursoObterPorIdQuery
        {
            Id = id
        });
    }

    [HttpPost("obter")]
    public async Task<PaginatedList<CursoObterDto>> Obter(CursoObterQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Excluir(int id)
    {
        await Mediator.Send(new CursoExcluirCommand
        {
            Id = id
        });
        
        return Ok();
    }
} 