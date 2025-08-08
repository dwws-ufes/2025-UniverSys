using Microsoft.AspNetCore.Authorization;
using UniverSys.Application.Requests.Disciplinas.Commands;
using UniverSys.Application.Requests.Disciplinas.Queries;
using UniverSys.Application.Requests.Disciplinas.Queries.Dto;

namespace UniverSys.WebUI.Controllers;

//[Authorize]
public class DisciplinasController : ApiControllerBase
{
    [HttpPost]
    public async Task<int> Salvar(DisciplinaSalvarCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet("obter-por-id/{id}")]
    public async Task<DisciplinaObterPorIdDto> ObterPorId(int id)
    {
        return await Mediator.Send(new DisciplinaObterPorIdQuery
        {
            Id = id
        });
    }

    [HttpPost("obter")]
    public async Task<PaginatedList<DisciplinaObterDto>> Obter(DisciplinaObterQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Excluir(int id)
    {
        await Mediator.Send(new DisciplinaExcluirCommand
        {
            Id = id
        });
        
        return Ok();
    }

    [HttpGet("rdf")]
    public async Task<IActionResult> ObterTodasRdfXml()
    {
        var file = await Mediator.Send(new DisciplinaObterTodasRdfXmlQuery());
        return File(file.Content, file.ContentType, file.FileName);
    }
} 