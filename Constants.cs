namespace LacExporter
{
    public static class Constants
    {
        const string GitHubCardsRepoUrl = "https://raw.githubusercontent.com/the-fab-cube/flesh-and-blood-cards/refs/heads/{0}/json/english/card.json";
        const string GitHubSetsRepoUrl = "https://raw.githubusercontent.com/the-fab-cube/flesh-and-blood-cards/refs/heads/{0}/json/english/set.json";

        public const string DefaultBranchName = "rosetta";

        public const string SetExportColumns = "{0};{1};{2};{3};{4}\n";

        public const string ClassExportColumns = "{0};{1};{2};{3}\n";

        public static string GetGitHubCardsRepoUrl(string branch = DefaultBranchName) => string.Format(GitHubCardsRepoUrl, branch);
        public static string GetGitHubSetsRepoUrl(string branch = DefaultBranchName) => string.Format(GitHubSetsRepoUrl, branch);

        public static string[] Sets = ["WTR", "ARC", "CRU", "MON", "ELE", "EVR", "UPR", "DYN", "OUT", "DTD", "EVO", "HVY", "MST", "ROS"];
        public static string[] IgnoredSets = ["HER", "JDG", "FAB", "LSS", "LGS", "OXO", "1HP", "WIN"];
    }
}