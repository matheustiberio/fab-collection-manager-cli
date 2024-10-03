namespace LacExporter
{
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
            var map = new Dictionary<string, string>()
            {
                { "C", "Common" },
                { "R", "Rare" },
                { "S", "Super Rare" },
                { "M", "Majestic" },
                { "L", "Legendary" },
                { "F", "Fabled" },
                { "P", "Promo" },
                { "T", "Token" },
                { "V", "Marvel" }
            };

            return map[rarity];
        }

        private static string GetPitch(string pitch)
        {
            var map = new Dictionary<string, string>()
            {
                { "1", "R" },
                { "2", "Y" },
                { "3", "B" },
                { "", "-" },
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
    }
}

