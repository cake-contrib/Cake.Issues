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
                            // Read affected file from the result.
                            if (!TryGetFile(tapResult, repositorySettings, out var fileName))
                            {
                                this.Log.Information("Skip element since file path could not be parsed");
                                continue;
                            }

                            // Build issue.
                            var issueBuilder =
                                IssueBuilder
                                    .NewIssue(diagnosticDict["message"].ToString(), issueProvider)
                                    .InFile(
                                        fileName,
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

    /// <summary>
    /// Reads the affected file path from a result.
    /// </summary>
    /// <param name="tapResult">Result entry.</param>
    /// <param name="repositorySettings">Repository settings to use.</param>
    /// <param name="fileName">Returns the full path to the affected file.</param>
    /// <returns>True if the file path could be parsed.</returns>
    private static bool TryGetFile(
        TapTestPoint tapResult,
        IRepositorySettings repositorySettings,
        out string fileName)
    {
        fileName = tapResult.Description.ToString();

        // Validate file path and make relative to repository root.
        (var result, fileName) = ValidateFilePath(fileName, repositorySettings);
        return result;
    }
}
