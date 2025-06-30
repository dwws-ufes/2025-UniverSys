using UniverSys.Domain.Enums;

namespace UniverSys.Domain.Entities;
public class Usuario
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public TipoUsuario Tipo { get; set; }
    public string TokenRecuperacaoSenha { get; set; }
    public DateTimeOffset? DataSolicitacaoRecuperacaoSenha { get; set; }
    public Professor Professor { get; set; }
    public Aluno Aluno { get; set; }
}