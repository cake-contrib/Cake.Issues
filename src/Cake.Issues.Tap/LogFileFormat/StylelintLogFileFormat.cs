namespace Cake.Issues.Tap.LogFileFormat;

using System.Collections.Generic;
using System.Linq;
using Cake.Core.Diagnostics;
using Cake.Issues.Tap.Parser;

/// <summary>
/// Log file format for parsing TAP files as written by Stylelint.
/// </summary>
/// <param name="log">The Cake log instance.</param>
internal class StylelintLogFileFormat(ICakeLog log)
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
            foreach (var diagnosticKey in tapResult.Diagnostics.Keys)
            {
                if (tapResult.Diagnostics[diagnosticKey] is List<object> diagnosticList)
                {
                    foreach (var diagnostic in diagnosticList)
                    {
                        if (diagnostic is Dictionary<object, object> diagnosticDict)
                        {
                            // Build issue.
                            var issueBuilder =
                                IssueBuilder
                                    .NewIssue(diagnosticDict["message"].ToString(), issueProvider)
                                    .InFile(
                                        tapResult.Description.ToString(),
                                        diagnosticDict.ContainsKey("line") ? int.Parse(diagnosticDict["line"].ToString()) : null,
                                        diagnosticDict.ContainsKey("endLine") ? int.Parse(diagnosticDict["endLine"].ToString()) : null,
                                        diagnosticDict.ContainsKey("column") ? int.Parse(diagnosticDict["column"].ToString()) : null,
                                        diagnosticDict.ContainsKey("endColumn") ? int.Parse(diagnosticDict["endColumn"].ToString()) : null)
                                    .OfRule(diagnosticKey)
                                    .WithPriority(GetPriority(diagnosticDict["severity"].ToString()));
                            result.Add(issueBuilder.Create());
                        }
                    }
                }
            }
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
