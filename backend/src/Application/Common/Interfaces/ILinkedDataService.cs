namespace UniverSys.Application.Common.Interfaces;

public interface ILinkedDataService
{
    Task<string> ObterResumoCurso(string nomeCurso);
}