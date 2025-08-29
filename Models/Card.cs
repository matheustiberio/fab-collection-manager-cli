namespace Exporter.Models;

public class Card
{
    public Card(string name, string pitch, string rarity, string[] types, string[] cardKeywords)
    {
        Name = name;
        Pitch = GetPitch(pitch);
        Rarity = GetRarity(rarity);
        Types = types;
        CardKeywords = cardKeywords;
        Type = SetType();
    }

    public string CardId { get; set; }

    public string Name { get; set; }

    public string Pitch { get; set; }

    public string Rarity { get; set; }

    public string[] Types { get; set; }

    public string Type { get; set; }

    public string[] CardKeywords { get; set; }


    private static string GetRarity(string rarity)
    {
        var map = new Dictionary<string, string>
        {
            { "C", "Common" },
            { "R", "Rare" },
            { "S", "Super Rare" },
            { "M", "Majestic" },
            { "L", "Legendary" },
            { "F", "Fabled" },
            { "P", "Promo" },
            { "T", "Token" },
            { "V", "Marvel" },
            { "B", "Basic" }
        };

        return map[rarity];
    }

    private static string GetPitch(string pitch)
    {
        var map = new Dictionary<string, string>
        {
            { "1", "R" },
            { "2", "Y" },
            { "3", "B" },
            { "", "-" }
        };

        return map[pitch];
    }

    private string SetType()
    {
        if (Array.IndexOf(CardKeywords, "Meld") != -1)
            return $"{Types[2]} {Types[0]}";

        if (Types.Length > 3)
            return $"{Types[0]} {Types[1]}";

        return $"{Types[0]}";
    }

    /// <summary>
    ///     This method sets the type of the card to be used for calculation of play set
    ///     Example: A card with type empty is a normal card, we want a play set of 3, but a Legendary (keyword), we want just
    ///     one
    /// </summary>
    /// <returns></returns>
    public string GetRegisterCardType()
    {
        if (Types.Contains("1H"))
            return "1HW";

        if (Types.Contains("2H"))
            return "2HW";

        if (CardKeywords.Contains("Legendary") || Types.Contains("Gem"))
            return "LEGD";

        if (Types.Contains("Equipment"))
            return "EQUIP";

        return string.Empty;
    }
}