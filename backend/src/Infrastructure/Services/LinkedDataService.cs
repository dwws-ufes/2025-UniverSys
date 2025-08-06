using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using UniverSys.Infrastructure.Models;

namespace UniverSys.Infrastructure.Services;

public class LinkedDataService : ILinkedDataService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<LinkedDataService> _logger;

    public LinkedDataService(HttpClient httpClient, ILogger<LinkedDataService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<string> ObterResumoCurso(string nomeCurso)
    {
        try
        {
            var sparqlQuery = $@"
                PREFIX dbr: <http://dbpedia.org/resource/>
                PREFIX dbo: <http://dbpedia.org/ontology/>
                PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>

                SELECT ?abstract
                WHERE {{
                  ?resource rdfs:label ""{nomeCurso}""@pt;
                            dbo:abstract ?abstract .
                  FILTER (lang(?abstract) = 'pt')
                }}
                LIMIT 1";

            var encodedQuery = Uri.EscapeDataString(sparqlQuery);
            var url = $"https://dbpedia.org/sparql?query={encodedQuery}&format=application/json";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro na consulta ao DBpedia. Status: {StatusCode}, Content: {Content}",
                    response.StatusCode, errorContent);
                return "Não foi possível obter informações sobre este curso.";
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var dbPediaResponse = JsonSerializer.Deserialize<DbPediaResponse>(jsonResponse);

            if (dbPediaResponse?.Results?.Bindings?.Count > 0)
            {
                var abstractValue = dbPediaResponse.Results.Bindings[0]?.Abstract?.Value;
                return abstractValue ?? "Não foi possível obter informações sobre este curso.";
            }

            return "Não foi possível encontrar informações sobre este curso.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exceção ao consultar o DBpedia para o curso: {NomeCurso}", nomeCurso);
            return "Erro ao consultar informações sobre o curso.";
        }
    }
}