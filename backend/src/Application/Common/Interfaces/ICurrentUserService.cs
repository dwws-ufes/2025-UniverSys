namespace UniverSys.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string Login { get; }
        int? UserId { get; }
        string Nome { get; }
    }
}
