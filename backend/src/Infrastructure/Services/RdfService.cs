using System.IO;
using UniverSys.Application.Requests.Disciplinas.Queries.Dto;
using VDS.RDF;
using VDS.RDF.Writing;

namespace UniverSys.Infrastructure.Services;

public class RdfService : IRdfService
{
    public byte[] BuildDisciplinasRdfXml(IEnumerable<DisciplinaObterPorIdDto> disciplinas, string baseUri)
    {
        var graph = new Graph();
        graph.NamespaceMap.AddNamespace("rdf", new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#"));
        graph.NamespaceMap.AddNamespace("schema", new Uri("https://schema.org/"));
        graph.NamespaceMap.AddNamespace("xsd", new Uri("http://www.w3.org/2001/XMLSchema#"));

        var rdfType = graph.CreateUriNode("rdf:type");
        var schemaCourse = graph.CreateUriNode("schema:Course");
        var schemaName = graph.CreateUriNode("schema:name");
        var schemaCourseCode = graph.CreateUriNode("schema:courseCode");
        var schemaTimeRequired = graph.CreateUriNode("schema:timeRequired");
        var schemaDescription = graph.CreateUriNode("schema:description");

        foreach (var dto in disciplinas)
        {
            var subjectUri = new Uri($"{baseUri.TrimEnd('/')}/disciplinas/rdf/{Uri.EscapeDataString(dto.Codigo)}");
            var subject = graph.CreateUriNode(subjectUri);

            graph.Assert(subject, rdfType, schemaCourse);

            if (!string.IsNullOrWhiteSpace(dto.Nome))
            {
                graph.Assert(subject, schemaName, graph.CreateLiteralNode(dto.Nome, "pt-br"));
            }

            if (!string.IsNullOrWhiteSpace(dto.Codigo))
            {
                graph.Assert(subject, schemaCourseCode, graph.CreateLiteralNode(dto.Codigo));
            }

            if (dto.CargaHoraria > 0)
            {
                var duration = $"PT{dto.CargaHoraria}H";
                graph.Assert(subject, schemaTimeRequired, graph.CreateLiteralNode(duration, new Uri("http://www.w3.org/2001/XMLSchema#duration")));
            }

            if (!string.IsNullOrWhiteSpace(dto.Ementa))
            {
                graph.Assert(subject, schemaDescription, graph.CreateLiteralNode(dto.Ementa, "pt-br"));
            }
        }

        using var memoryStream = new MemoryStream();
        using var writer = new StreamWriter(memoryStream, Encoding.UTF8, leaveOpen: true);
        var rdfWriter = new RdfXmlWriter();
        rdfWriter.Save(graph, writer);
        writer.Flush();
        memoryStream.Position = 0;
        return memoryStream.ToArray();
    }
}


