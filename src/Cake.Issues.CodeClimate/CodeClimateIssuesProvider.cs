namespace Cake.Issues.CodeClimate;

using System;
using System.Collections.Generic;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Issues;
using Newtonsoft.Json;

/// <summary>
/// Provider for issues in CodeClimate compatible format.
/// </summary>
/// <param name="log">The Cake log context.</param>
/// <param name="issueProviderSettings">Settings for the issue provider.</param>
internal class CodeClimateIssuesProvider(ICakeLog log, CodeClimateIssuesSettings issueProviderSettings) : BaseConfigurableIssueProvider<CodeClimateIssuesSettings>(log, issueProviderSettings)
{
    /// <inheritdoc />
    public override string ProviderName => "CodeClimate";

    /// <inheritdoc />
    protected override IEnumerable<IIssue> InternalReadIssues()
    {
        var result = new List<IIssue>();

        var logContent = this.IssueProviderSettings.LogFileContent.ToStringUsingEncoding();

        CodeClimateIssue[] issues;
        try
        {
            issues = JsonConvert.DeserializeObject<CodeClimateIssue[]>(logContent);
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("Error parsing CodeClimate JSON format", ex);
        }

        if (issues == null)
        {
            return result;
        }

        foreach (var issue in issues)
        {
            // Skip issues that are not of type "issue"
            if (issue.Type != "issue")
            {
                continue;
            }

            var (filePath, startLine, endLine, startColumn, endColumn) = GetLocation(issue.Location, this.Settings);
            var priority = GetPriority(issue.Severity);
            var rule = string.IsNullOrWhiteSpace(issue.CheckName) ? null : issue.CheckName;

            // Build issue.
            var issueBuilder =
                IssueBuilder
                    .NewIssue(
                        issue.Description,
                        typeof(CodeClimateIssuesProvider).FullName,
                        this.ProviderName)
                    .WithPriority(priority);

            if (!string.IsNullOrWhiteSpace(rule))
            {
                issueBuilder = issueBuilder.OfRule(rule);
            }

            if (!string.IsNullOrWhiteSpace(issue.Content))
            {
                issueBuilder = issueBuilder.WithMessageInMarkdownFormat(issue.Content);
            }

            if (filePath != null)
            {
                issueBuilder = issueBuilder.InFile(filePath, startLine, endLine, startColumn, endColumn);
            }

            result.Add(issueBuilder.Create());
        }

        return result;
    }

    /// <summary>
    /// Determines the location for a CodeClimate issue.
    /// </summary>
    /// <param name="location">Location from CodeClimate issue.</param>
    /// <param name="repositorySettings">Repository settings.</param>
    /// <returns>File path and position information.</returns>
    private static (string FilePath, int? StartLine, int? EndLine, int? StartColumn, int? EndColumn) GetLocation(
        CodeClimateLocation location,
        IRepositorySettings repositorySettings)
    {
        if (location == null || string.IsNullOrWhiteSpace(location.Path))
        {
            return (null, null, null, null, null);
        }

        var filePath = location.Path;
        var isAbsolutePath = !new FilePath(filePath).IsRelative;

        // Validate file path and make relative to repository root if it is an absolute path.
        (var pathValidationResult, filePath) = ValidateFilePath(filePath, isAbsolutePath, repositorySettings);

        if (!pathValidationResult)
        {
            return (null, null, null, null, null);
        }

        int? startLine = null;
        int? endLine = null;
        int? startColumn = null;
        int? endColumn = null;

        // Handle line-based location
        if (location.Lines != null)
        {
            startLine = location.Lines.Begin > 0 ? location.Lines.Begin : null;
            endLine = location.Lines.End > 0 ? location.Lines.End : null;
        }

        // Handle position-based location
        else if (location.Positions != null)
        {
            if (location.Positions.Begin != null)
            {
                startLine = location.Positions.Begin.Line > 0 ? location.Positions.Begin.Line : null;
                startColumn = location.Positions.Begin.Column > 0 ? location.Positions.Begin.Column : null;
            }

            if (location.Positions.End != null)
            {
                endLine = location.Positions.End.Line > 0 ? location.Positions.End.Line : null;
                endColumn = location.Positions.End.Column > 0 ? location.Positions.End.Column : null;
            }
        }

        return (filePath, startLine, endLine, startColumn, endColumn);
    }

    /// <summary>
    /// Converts CodeClimate severity to Cake.Issues priority.
    /// </summary>
    /// <param name="severity">CodeClimate severity.</param>
    /// <returns>Cake.Issues priority.</returns>
    private static IssuePriority GetPriority(string severity) =>
        severity?.ToLowerInvariant() switch
        {
            "blocker" => IssuePriority.Error,
            "critical" => IssuePriority.Error,
            "major" => IssuePriority.Warning,
            "minor" => IssuePriority.Suggestion,
            "info" => IssuePriority.Hint,
            _ => IssuePriority.Undefined,
        };

    /// <summary>
    /// Validates a file path.
    /// </summary>
    /// <param name="filePath">Full file path.</param>
    /// <param name="isAbsolutePath">Indicates if the file path is an absolute path.</param>
    /// <param name="repositorySettings">Repository settings.</param>
    /// <returns>Tuple containing a value if validation was successful, and file path relative to repository root.</returns>
    private static (bool Valid, string FilePath) ValidateFilePath(
        string filePath,
        bool isAbsolutePath,
        IRepositorySettings repositorySettings)
    {
        filePath.NotNullOrWhiteSpace();
        repositorySettings.NotNull();

        if (isAbsolutePath)
        {
            // Ignore files from outside the repository.
            if (!filePath.IsInRepository(repositorySettings))
            {
                return (false, string.Empty);
            }

            // Make path relative to repository root.
            filePath = filePath.NormalizePath().MakeFilePathRelativeToRepositoryRoot(repositorySettings);
        }

        return (true, filePath);
    }
}