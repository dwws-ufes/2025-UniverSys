using Microsoft.AspNetCore.Authorization;
using UniverSys.Application.Requests.Matriculas.Commands;
using UniverSys.Application.Requests.Matriculas.Queries;
using UniverSys.Application.Requests.Matriculas.Queries.Dto;

namespace UniverSys.WebUI.Controllers;

//[Authorize]
public class MatriculasController : ApiControllerBase
{
    [HttpPost]
    public async Task<int> RealizarMatricula(MatriculaRealizarCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet("obter-por-id/{id}")]
    public async Task<MatriculaObterPorIdDto> ObterPorId(int id)
    {
        return await Mediator.Send(new MatriculaObterPorIdQuery
        {
            Id = id
        });
    }

    [HttpPost("obter")]
    public async Task<PaginatedList<MatriculaObterDto>> Obter(MatriculaObterQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Excluir(int id)
    {
        await Mediator.Send(new MatriculaExcluirCommand
        {
            Id = id
        });
        
        return Ok();
    }
} 