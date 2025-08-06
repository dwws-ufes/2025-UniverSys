using System.Text.Json.Serialization;

namespace UniverSys.Infrastructure.Models;

public class DbPediaResponse
{
    [JsonPropertyName("head")]
    public Head Head { get; set; }

    [JsonPropertyName("results")]
    public Results Results { get; set; }
}

public class Head
{
    [JsonPropertyName("link")]
    public List<object> Link { get; set; }

    [JsonPropertyName("vars")]
    public List<string> Vars { get; set; }
}

public class Results
{
    [JsonPropertyName("distinct")]
    public bool Distinct { get; set; }

    [JsonPropertyName("ordered")]
    public bool Ordered { get; set; }

    [JsonPropertyName("bindings")]
    public List<Binding> Bindings { get; set; }
}

public class Binding
{
    [JsonPropertyName("abstract")]
    public AbstractValue Abstract { get; set; }
}

public class AbstractValue
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("xml:lang")]
    public string Language { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}