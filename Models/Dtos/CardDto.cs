using System.Text.Json.Serialization;

namespace Exporter.Models.Dtos;

public class CardDto
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("pitch")] public string Pitch { get; set; }

    [JsonPropertyName("types")] public IEnumerable<string> Types { get; set; }

    [JsonPropertyName("printings")] public IEnumerable<PrintingDto> Printings { get; set; }

    [JsonPropertyName("card_keywords")] public string[] CardKeywords { get; set; }

    public string GetRarity(string setId = "")
    {
        if (!string.IsNullOrWhiteSpace(setId))
            return Printings.FirstOrDefault(x => x.SetId.Equals(setId, StringComparison.OrdinalIgnoreCase))!.Rarity;

        return Printings.FirstOrDefault()!.Rarity;
    }

    public string GetCardId(string setId = "")
    {
        if (!string.IsNullOrWhiteSpace(setId))
            return Printings.FirstOrDefault(x => x.SetId.Equals(setId, StringComparison.OrdinalIgnoreCase))!.Id;

        var cardId = Printings.FirstOrDefault(x => Constants.Sets.Contains(x.SetId))?.Id;

        if (!string.IsNullOrEmpty(cardId))
            return cardId;

        return Printings.FirstOrDefault()!.Id;
    }
}

public class PrintingDto
{
    [JsonPropertyName("id")] public string Id { get; set; }

    [JsonPropertyName("rarity")] public string Rarity { get; set; }

    [JsonPropertyName("set_id")] public string SetId { get; set; }
}