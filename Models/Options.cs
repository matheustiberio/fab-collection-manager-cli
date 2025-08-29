using CommandLine;

namespace Exporter.Models;

public class Options
{
    [Option('c', "class", Required = false, HelpText = "Class for filter.")]
    public string Class { get; set; }

    [Option('s', "set", Required = false, HelpText = "Set for filter.")]
    public string Set { get; set; }

    [Option('b', "branch", Required = false, HelpText = "Branch name.")]
    public string BranchName { get; set; }

    public bool IsItToFilterByClass => !string.IsNullOrWhiteSpace(Class) && string.IsNullOrWhiteSpace(Set);
    public bool IsItToFilterBySet => !string.IsNullOrWhiteSpace(Set) && string.IsNullOrWhiteSpace(Class);
    public bool IsItToFilterBySetAndClass => !string.IsNullOrWhiteSpace(Class) && !string.IsNullOrWhiteSpace(Set);

    public string GetBranchName()
    {
        return string.IsNullOrWhiteSpace(BranchName) ? Constants.DefaultBranchName : BranchName;
    }
}