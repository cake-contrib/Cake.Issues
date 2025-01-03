namespace Cake.Issues.Tap.LogFileFormat;

using System.Collections.Generic;
using System.Linq;
using Cake.Core.Diagnostics;
using Cake.Issues.Tap.Parser;

/// <summary>
/// Log file format for parsing TAP files as written by Textlint.
/// </summary>
/// <param name="log">The Cake log instance.</param>
internal class TextlintLogFileFormat(ICakeLog log)
    : BaseTapLogFileFormat(log)
{
    /// <inheritdoc />
    public override IEnumerable<IIssue> ReadIssues(
        TapIssuesProvider issueProvider,
        IRepositorySettings repositorySettings,
        TapIssuesSettings tapIssuesSettings)
    {
        issueProvider.NotNull();
        repositorySettings.NotNull();
        tapIssuesSettings.NotNull();

        var result = new List<IIssue>();

        var parser = new TapParser();
        parser.Parse(tapIssuesSettings.LogFileContent.RemovePreamble().ToStringUsingEncoding());

        foreach (var tapResult in parser.Results.Where(x => !x.TestStatus))
        {
            var dataDict = tapResult.Diagnostics["data"] as Dictionary<object, object>;

            // Make path relative to repository root.
            var filePath = tapResult.Description.ToString();
            filePath = filePath.MakeFilePathRelativeToRepositoryRoot(repositorySettings);

            // Build issue.
            var issueBuilder =
                IssueBuilder
                    .NewIssue(tapResult.Diagnostics["message"].ToString(), issueProvider)
                    .InFile(
                        filePath,
                        dataDict != null && dataDict.ContainsKey("line") ? int.Parse(dataDict["line"].ToString()) : null,
                        dataDict != null && dataDict.ContainsKey("column") ? int.Parse(dataDict["column"].ToString()) : null)
                    .WithPriority(GetPriority(tapResult.Diagnostics["severity"].ToString()));

            if (dataDict != null && dataDict.ContainsKey("line"))
            {
                issueBuilder = issueBuilder.OfRule(dataDict["ruleId"].ToString());
            }

            result.Add(issueBuilder.Create());
        }

        return result;
    }

    private static IssuePriority GetPriority(string severity) => severity switch
    {
        "error" => IssuePriority.Error,
        "warning" => IssuePriority.Warning,
        _ => IssuePriority.Undefined,
    };
}
