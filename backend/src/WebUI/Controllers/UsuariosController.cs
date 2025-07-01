using Microsoft.AspNetCore.Authorization;
using UniverSys.Application.Common.Helpers;
using UniverSys.Application.Requests.Usuarios.Commands;
using UniverSys.Application.Requests.Usuarios.Queries;
using UniverSys.Domain.Enums;

namespace UniverSys.WebUI.Controllers;

//[Authorize]
public class UsuariosController : ApiControllerBase
{

    [HttpGet("{id}")]
    public async Task<UsuarioDto> ObterPorId(int id)
    {
        return await Mediator.Send(new UsuarioObterPorIdQuery
        {
            Id = id
        });
    }

    [HttpPost("obter")]
    public async Task<PaginatedList<UsuarioDto>> Obter(UsuarioObterQuery query)
    {
        return await Mediator.Send(query);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<UsuarioDto> Criar(UsuarioCriarCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut]
    public async Task<UsuarioDto> Alterar(UsuarioAlterarCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Excluir(int id)
    {
        await Mediator.Send(new UsuarioExcluirCommand
        {
            Id = id
        });
        
        return NoContent();
    }

    [HttpGet("tipos")]
    public List<SelectItemEnum> ObterTipos()
    {
        return EnumHelper.GetSelectList<TipoUsuario>();
    }
}
