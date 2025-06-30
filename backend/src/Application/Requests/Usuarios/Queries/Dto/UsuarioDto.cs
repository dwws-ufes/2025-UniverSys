namespace UniverSys.Application.Requests.Usuarios.Queries.Dto
{
    public class UsuarioDto
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public DateTime? DataExclusao { get; set; }
    }
}
