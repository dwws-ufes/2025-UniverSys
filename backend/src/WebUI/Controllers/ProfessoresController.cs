using Microsoft.AspNetCore.Authorization;
using UniverSys.Application.Common.Helpers;
using UniverSys.Application.Requests.Professores.Commands;
using UniverSys.Application.Requests.Professores.Queries;
using UniverSys.Application.Requests.Professores.Queries.Dto;
using UniverSys.Domain.Enums;

namespace UniverSys.WebUI.Controllers;

//[Authorize]
public class ProfessoresController : ApiControllerBase
{
    [HttpPost]
    public async Task<int> Salvar(ProfessorSalvarCommand request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet("obter-por-id/{id}")]
    public async Task<ProfessorObterPorIdDto> ObterPorId(int id)
    {
        return await Mediator.Send(new ProfessorObterPorIdQuery
        {
            Id = id
        });
    }

    [HttpPost("obter")]
    public async Task<PaginatedList<ProfessorObterDto>> Obter(ProfessorObterQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Excluir(int id)
    {
        await Mediator.Send(new ProfessorExcluirCommand
        {
            Id = id
        });
        
        return Ok();
    }

    [HttpGet("especializacoes")]
    public List<SelectItemEnum> ObterEspecializacoes()
    {
        return EnumHelper.GetSelectList<Especializacao>();
    }
} 