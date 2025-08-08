namespace UniverSys.Application.Common.Interfaces;

using UniverSys.Application.Requests.Disciplinas.Queries.Dto;

public interface IRdfService
{
    byte[] BuildDisciplinasRdfXml(IEnumerable<DisciplinaObterPorIdDto> disciplinas, string baseUri);
}



