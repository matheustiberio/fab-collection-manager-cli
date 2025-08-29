using System.Text.Json.Serialization;

namespace Exporter.Models.Dtos;

public class SetDto
{
    [JsonPropertyName("id")] public string Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("printings")] public IEnumerable<Printing> Printings { get; set; }
}

public class Printing
{
    [JsonPropertyName("initial_release_date")]
    public string InitialReleaseDate { get; set; }
}