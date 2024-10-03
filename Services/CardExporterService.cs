using System.Text;
using LacExporter.Models.Dtos;
using TextCopy;

namespace LacExporter.Services
{
    public class ExportService
    {
        private readonly HttpService _httpService;
        private readonly StringBuilder _stringBuilder = new();
        public ExportService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task Export(Options options)
        {
            var cardsDto = await _httpService.GetCardsAsync(options.GetBranchName());

            if (options.IsItToFilterBySetAndClass)
            {
                var cards = FilterBySetAndClass(cardsDto, options.Set, options.Class);
                BuildCardsFromClassExportToClipboard(cards);
            }

            else if (options.IsItToFilterBySet)
            {
                var cards = FilterBySet(cardsDto, options.Set);
                BuildCardsFromSetExportToClipboard(cards);
            }

            else if (options.IsItToFilterByClass)
            {
                var cards = FilterByClass(cardsDto, options.Class);
                BuildCardsFromClassExportToClipboard(cards);
            }

            SetCardsToClipboard();
        }

        private void BuildCardsFromClassExportToClipboard(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
                _stringBuilder.Append(string.Format(Constants.ClassExportColumns, card.CardId, card.Name, card.Pitch, card.Rarity));
        }

        private void BuildCardsFromSetExportToClipboard(IEnumerable<Card> cards)
        {
            foreach (var card in cards.OrderBy(x => x.CardId))
                _stringBuilder.Append(string.Format(Constants.SetExportColumns, card.CardId, card.Name, card.Pitch, card.Rarity, card.Type));
        }

        private void SetCardsToClipboard()
        {
            ClipboardService.SetText(_stringBuilder.ToString());

            Console.WriteLine($"Cards copied to clipboard.");
        }

        private static List<Card> FilterByClass(IEnumerable<CardDto> cards, string clazz)
        {
            return cards.Where(x => x.Types.Contains(clazz))
                .Select(x => new Card(x.Name, x.Pitch, x.GetRarity(), x.Types.ToArray(), x.CardKeywords)
                {
                    CardId = x.GetCardId()
                }).OrderBy(x => Array.IndexOf(Constants.Sets, x.CardId.Substring(0, 3)))
                .ToList();
        }

        private static List<Card> FilterBySet(IEnumerable<CardDto> cards, string setId)
        {
            return cards.Where(x => x.Printings.Any(x => x.SetId.Equals(setId, StringComparison.CurrentCultureIgnoreCase)))
                .Select(x => new Card(x.Name, x.Pitch, x.GetRarity(setId), x.Types.ToArray(), x.CardKeywords)
                {
                    CardId = x.GetCardId(setId),
                })
                .OrderBy(x => x.CardId)
                .ToList();
        }

        private static List<Card> FilterBySetAndClass(IEnumerable<CardDto> cards, string setId, string clazz)
        {
            return cards.Where(x => x.Printings.Any(x => x.SetId.Equals(setId, StringComparison.CurrentCultureIgnoreCase)) && x.Types.Contains(clazz))
                .Select(x => new Card(x.Name, x.Pitch, x.GetRarity(setId), x.Types.ToArray(), x.CardKeywords)
                {
                    CardId = x.GetCardId(setId),
                })
                .OrderBy(x => x.CardId)
                .ToList();
        }
    }
}