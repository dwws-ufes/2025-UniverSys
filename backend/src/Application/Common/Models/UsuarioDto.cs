using UniverSys.Domain.Enums;

namespace UniverSys.Application.Common.Models;

public class UsuarioDto
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public TipoUsuario Tipo { get; set; }
    public bool Administrador => Tipo == TipoUsuario.Administrador;
    public string AdministradorString => Administrador ? "Sim" : "Não";

}
