namespace UniverSys.Application.Requests.Auth.Dto;

public class LoginModel
{
    public string Login { get; set; }
    public string Senha { get; set; }
    public int? ClienteId { get; set; }
} 