using System.Text;
using Exporter.Models;
using Exporter.Models.Dtos;
using TextCopy;

namespace Exporter.Services;

public class ExportService(HttpService httpService)
{
    private readonly StringBuilder _stringBuilder = new();

    public async Task Export(Options options)
    {
        var cardsDto = await httpService.GetCardsAsync(options.GetBranchName());
        var sets = await GetSets(options.GetBranchName());

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
            var cards = FilterByClass(cardsDto, options.Class, sets);
            BuildCardsFromClassExportToClipboard(cards);
        }

        SetCardsToClipboard();
    }

    private async Task<string[]> GetSets(string branchName)
    {
        var setsDto = await httpService.GetSetsAsync(branchName);

        var sets = setsDto.Where(x => !Constants.IgnoredSets.Contains(x.Id) && Constants.Sets.Contains(x.Id))
            .Select(x => new Set
            {
                Id = x.Id,
                Name = x.Name,
                InitialReleaseDate = DateTime.Parse(x.Printings.FirstOrDefault()!.InitialReleaseDate)
            });

        return sets.OrderBy(x => x.InitialReleaseDate).Select(x => x.Id).ToArray();
    }

    private void BuildCardsFromClassExportToClipboard(IEnumerable<Card> cards)
    {
        foreach (var card in cards)
            _stringBuilder.Append(string.Format(Constants.ClassExportColumns, card.Name, card.Pitch, card.Rarity,
                card.Type, card.GetRegisterCardType(), card.CardId));
    }

    private void BuildCardsFromSetExportToClipboard(IEnumerable<Card> cards)
    {
        foreach (var card in cards.OrderBy(x => x.CardId))
            _stringBuilder.Append(string.Format(Constants.SetExportColumns, card.Name, card.Pitch, card.Rarity,
                card.Type, card.CardId));
    }

    private void SetCardsToClipboard()
    {
        if (string.IsNullOrWhiteSpace(_stringBuilder.ToString()))
            Console.WriteLine("No cards exported :[");

        ClipboardService.SetText(_stringBuilder.ToString());
        Console.WriteLine("Cards copied to clipboard.");
    }

    private static List<Card> FilterByClass(IEnumerable<CardDto> cards, string clazz, string[] sets)
    {
        List<Card> cardsByClass = [];

        foreach (var set in sets)
        {
            var cardsFiltered = cards
                .Where(x => x.Types.Contains(clazz) && x.Printings.Any(printing => printing.SetId == set))
                .Select(card =>
                    new Card(card.Name, card.Pitch, card.GetRarity(set), card.Types.ToArray(), card.CardKeywords)
                    {
                        CardId = card.GetCardId(set)
                    }).OrderBy(card => card.CardId);

            foreach (var cardFiltered in cardsFiltered)
                if (!cardsByClass.Any(x => x.Name == cardFiltered.Name && x.Pitch == cardFiltered.Pitch))
                    cardsByClass.Add(cardFiltered);
        }

        return cardsByClass;
    }

    private static List<Card> FilterBySet(IEnumerable<CardDto> cards, string setId)
    {
        return cards.Where(x =>
                x.Printings.Any(printing => printing.SetId.Equals(setId, StringComparison.CurrentCultureIgnoreCase)))
            .Select(card =>
                new Card(card.Name, card.Pitch, card.GetRarity(setId), card.Types.ToArray(), card.CardKeywords)
                {
                    CardId = card.GetCardId(setId)
                })
            .OrderBy(card => card.CardId)
            .ToList();
    }

    private static List<Card> FilterBySetAndClass(IEnumerable<CardDto> cards, string setId, string clazz)
    {
        return cards.Where(x =>
                x.Printings.Any(printing => printing.SetId.Equals(setId, StringComparison.CurrentCultureIgnoreCase)) &&
                x.Types.Contains(clazz))
            .Select(card =>
                new Card(card.Name, card.Pitch, card.GetRarity(setId), card.Types.ToArray(), card.CardKeywords)
                {
                    CardId = card.GetCardId(setId)
                })
            .OrderBy(card => card.CardId)
            .ToList();
    }
}