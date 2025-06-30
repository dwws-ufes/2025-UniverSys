namespace UniverSys.Application.Common.Exceptions
{
    /// <summary>
    /// Caso usuário ainda não exista no sistema
    /// </summary>
    public class UsuarioNaoCadastradoException : Exception
    {
        public UsuarioNaoCadastradoException()
            : base()
        {
        }        
    }
}
