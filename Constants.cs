namespace Exporter;

public static class Constants
{
    private const string GitHubCardsRepoUrl =
        "https://raw.githubusercontent.com/the-fab-cube/flesh-and-blood-cards/refs/heads/{0}/json/english/card.json";

    private const string GitHubSetsRepoUrl =
        "https://raw.githubusercontent.com/the-fab-cube/flesh-and-blood-cards/refs/heads/{0}/json/english/set.json";

    public const string DefaultBranchName = "develop";

    public const string SetExportColumns = "{0};{1};{2};{3};{4}\n";
    public const string ClassExportColumns = "0;{0};{1};{2};{3};;{4};{5}\n";

    public static string[] Sets =
    [
        "WTR", "ARC", "CRU", "MON", "ELE", "EVR", "UPR", "DYN", "OUT", "DTD", "EVO", "HVY", "MST", "ROS", "TER", "SUP",
        "SEA", "HNT"
    ];

    public static string[] IgnoredSets =
        ["HER", "JDG", "FAB", "LSS", "LGS", "OXO", "1HP", "WIN", "XXX", "1HB", "1HD", "1HT", "1HK", "1HR", "1HV"];

    public static string[] Classes =
    [
        "Generic",
        "Adjudicator",
        "Assassin",
        "Bard",
        "Brute",
        "Guardian",
        "Illusionist",
        "Mechanologist",
        "Merchant",
        "Necromancer",
        "Ninja",
        "Pirate",
        "Ranger",
        "Runeblade",
        "Shapeshifter",
        "Thief",
        "Warrior",
        "Wizard"
    ];

    public static string GetGitHubCardsRepoUrl(string branch = DefaultBranchName)
    {
        return string.Format(GitHubCardsRepoUrl, branch);
    }

    public static string GetGitHubSetsRepoUrl(string branch = DefaultBranchName)
    {
        return string.Format(GitHubSetsRepoUrl, branch);
    }
}