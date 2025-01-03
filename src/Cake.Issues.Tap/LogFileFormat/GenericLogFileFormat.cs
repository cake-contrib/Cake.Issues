namespace Cake.Issues.Tap.LogFileFormat;

using System.Collections.Generic;
using System.Linq;
using Cake.Core.Diagnostics;
using Cake.Issues.Tap.Parser;

/// <summary>
/// Generic log file format for parsing TAP files.
/// Ignores any tooling specific information in YAML blocks and just reads the issues.
/// </summary>
/// <param name="log">The Cake log instance.</param>
internal class GenericLogFileFormat(ICakeLog log)
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
            // Build issue.
            var issueBuilder =
                IssueBuilder
                    .NewIssue(tapResult.Description, issueProvider);
            result.Add(issueBuilder.Create());
        }

        return result;
    }
}
