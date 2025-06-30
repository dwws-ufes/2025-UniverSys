using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using UniverSys.Domain.Enums;

namespace UniverSys.WebUI.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string Login
    {
        get
        {
            var login = _httpContextAccessor.HttpContext?.User?.FindFirstValue("login");

            return login;
        }
    }

    public int? UserId
    {
        get
        {
            var strUserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue("usuarioId");

            if (string.IsNullOrEmpty(strUserId))
                return null;

            return Convert.ToInt32(strUserId);
        }
    }

    public string Nome
    {
        get
        {
            var strUserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue("nome");

            return strUserId;
        }
    }


    public TipoUsuario? Tipo
    {
        get
        {
            var strUserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue("tipo");

            if (string.IsNullOrWhiteSpace(strUserId))
            {
                return null;
            }

            return (TipoUsuario)int.Parse(strUserId);
        }
    }


}
