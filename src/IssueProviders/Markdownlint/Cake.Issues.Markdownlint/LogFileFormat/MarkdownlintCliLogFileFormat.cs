namespace Cake.Issues.Markdownlint.LogFileFormat;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Cake.Core.Diagnostics;

/// <summary>
/// Logfile format as written by markdownlint-cli.
/// </summary>
/// <param name="log">The Cake log instance.</param>
internal partial class MarkdownlintCliLogFileFormat(ICakeLog log)
    : BaseMarkdownlintLogFileFormat(log)
{
    private static readonly string[] Separator = ["\r\n", "\r", "\n"];

    /// <inheritdoc />
    public override IEnumerable<IIssue> ReadIssues(
        MarkdownlintIssuesProvider issueProvider,
        IRepositorySettings repositorySettings,
        MarkdownlintIssuesSettings markdownlintIssuesSettings)
    {
        issueProvider.NotNull();
        repositorySettings.NotNull();
        markdownlintIssuesSettings.NotNull();

        var regex = LineParsingRegEx();

        foreach (var line in markdownlintIssuesSettings.LogFileContent.ToStringUsingEncoding().Split(Separator, StringSplitOptions.None).Where(s => !string.IsNullOrEmpty(s)))
        {
            var match = regex.Match(line);

            // Skip lines which do not match the expected format, e.g. summary lines
            // which are not describing a single issue.
            if (!match.Success)
            {
                this.Log.Verbose("Ignored line '{0}' since it doesn't match the expected format.", line);
                continue;
            }

            var groups = match.Groups;

            // Read affected file from the line.
            if (!this.TryGetFile(groups, repositorySettings, out var fileName))
            {
                continue;
            }

            var lineNumber = int.Parse(groups["lineNumber"].Value);
            int? columnNumber = null;
            if (!string.IsNullOrEmpty(groups["columnNumber"].Value))
            {
                columnNumber = int.Parse(groups["columnNumber"].Value);
            }

            var ruleId = groups["ruleId"].Value;
            var message = groups["message"].Value;

            // Since markdownlint-cli 0.47.0 the severity (`error` or `warning`) is included in the
            // output between the column number and the rule id, since rules can be configured with a
            // custom severity. `error` is mapped to IssuePriority.Error, everything else (`warning` or,
            // for older markdownlint-cli versions not reporting a severity at all, no value) is mapped
            // to IssuePriority.Warning as before.
            var priority =
                groups["severity"].Value == "error"
                    ? IssuePriority.Error
                    : IssuePriority.Warning;

            yield return
                IssueBuilder
                    .NewIssue(message, issueProvider)
                    .InFile(fileName, lineNumber, columnNumber)
                    .WithPriority(priority)
                    .OfRule(ruleId, MarkdownlintRuleUrlResolver.Instance.ResolveRuleUrl(ruleId))
                    .Create();
        }
    }

    [GeneratedRegex(@"(?<filePath>.*[^:\d+]): ?(?<lineNumber>\d+):?(?<columnNumber>\d+)? (?:(?<severity>error|warning) )?(?<ruleId>MD\d+)/(?<ruleName>(?:\w*-*/*)*) (?<message>.*)")]
    private static partial Regex LineParsingRegEx();

    /// <summary>
    /// Reads the affected file path from a parsed entry.
    /// </summary>
    /// <param name="values">Parsed values of a line in the log file.</param>
    /// <param name="repositorySettings">Repository settings to use.</param>
    /// <param name="fileName">Returns the full path to the affected file.</param>
    /// <returns>True if the file path could be parsed.</returns>
    private bool TryGetFile(
        GroupCollection values,
        IRepositorySettings repositorySettings,
        out string fileName)
    {
        values.NotNull();
        repositorySettings.NotNull();

        var filePath = values["filePath"].Value;

        // Validate file path and make relative to repository root.
        (var result, fileName) = filePath.IsValidRepositoryFilePath(repositorySettings);

        if (!result)
        {
            this.Log.Warning(
                "Ignored issue for file '{0}' since it is outside the repository folder at {1}.",
                filePath,
                repositorySettings.RepositoryRoot);
        }

        return result;
    }
}
