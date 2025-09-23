namespace Cake.Issues.Reporting.Console;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Errata;
using Spectre.Console;

/// <summary>
/// Custom diagnostic for issues.
/// </summary>
internal sealed class IssueDiagnostic : Diagnostic
{
    private readonly ICakeLog log;
    private readonly IEnumerable<IIssue> issues;
    private readonly DirectoryPath repositoryRoot;

    /// <summary>
    /// Initializes a new instance of the <see cref="IssueDiagnostic"/> class.
    /// </summary>
    /// <param name="log">The Cake log.</param>
    /// <param name="repositoryRoot">Root directory of the repository.</param>
    /// <param name="issue">Issue which the diagnostic should describe.</param>
    public IssueDiagnostic(ICakeLog log, DirectoryPath repositoryRoot, IIssue issue)
        : this(log, repositoryRoot, [issue])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IssueDiagnostic"/> class.
    /// </summary>
    /// <param name="log">The Cake log.</param>
    /// <param name="repositoryRoot">Root directory of the repository.</param>
    /// <param name="issues">Issues which the diagnostic should describe.</param>
    public IssueDiagnostic(ICakeLog log, DirectoryPath repositoryRoot, IEnumerable<IIssue> issues)

        : base(issues.First().RuleId)
    {
        log.NotNull();
        repositoryRoot.NotNull();

        this.log = log;
        this.repositoryRoot = repositoryRoot;
        this.issues = issues;

        var firstIssue = this.issues.First();

        this.Category = firstIssue.PriorityName;
        this.Color =
            firstIssue.Priority switch
            {
                (int)IssuePriority.Error => Color.Red,
                (int)IssuePriority.Warning => Color.Yellow,
                (int)IssuePriority.Suggestion => Color.Blue,
                (int)IssuePriority.Hint => Color.LightSkyBlue1,
                _ => this.Color,
            };

        var noteComponents = new List<string>();

        // Add project information if available
        if (!string.IsNullOrEmpty(firstIssue.ProjectName) || firstIssue.ProjectFileRelativePath != null)
        {
            if (firstIssue.ProjectFileRelativePath != null)
            {
                noteComponents.Add($"Project: {firstIssue.ProjectFileRelativePath.FullPath}");
            }
            else if (!string.IsNullOrEmpty(firstIssue.ProjectName))
            {
                noteComponents.Add($"Project: {firstIssue.ProjectName}");
            }
        }

        // Add rule URL if available
        if (firstIssue.RuleUrl != null)
        {
            noteComponents.Add($"See {firstIssue.RuleUrl} for more information");
        }

        if (noteComponents.Count > 0)
        {
            this.Note = string.Join(Environment.NewLine, noteComponents);
        }

        this.CreateLabels();
    }

    /// <summary>
    /// Returns the diagnostic location of an issue.
    /// </summary>
    /// <param name="issue">Issue for which the location should be returned.</param>
    /// <returns>Location for the diagnostic.</returns>
    private (Location Location, int Lenght) GetLocation(IIssue issue)
    {
        // Errata currently doesn't support file or line level diagnostics.
        if (!issue.Line.HasValue || !issue.Column.HasValue)
        {
            return default;
        }

        var line = issue.Line.Value;
        var column = issue.Column.Value;

        // Try to validate column position against actual file content if possible
        if (this.repositoryRoot != null && issue.AffectedFileRelativePath != null)
        {
            try
            {
                var fullPath = this.repositoryRoot.CombineWithFilePath(issue.AffectedFileRelativePath).FullPath;
                if (File.Exists(fullPath))
                {
                    // Read the required line from the file
                    var lineContent = File.ReadLines(fullPath).Skip(line - 1).FirstOrDefault();

                    if (lineContent != null)
                    {
                        var lineLength = lineContent.Length;

                        // If column is beyond the end of the line, adjust it to the last valid position
                        if (column > lineLength)
                        {
                            column = Math.Max(1, lineLength); // Position at end of line content, minimum column 1
                        }
                    }
                }
            }
            catch (Exception ex) when (ex is IOException or PathTooLongException or SecurityException or UnauthorizedAccessException)
            {
                // If file reading fails, proceed with original column position
                // This ensures we don't break functionality when files are not accessible
                this.log.Verbose(
                    "Could not read file '{0}' to validate issue location is in a valid range.",
                    issue.AffectedFileRelativePath);
            }
        }

        var location = new Location(line, column);

        var length = 0;
        if (issue.EndColumn.HasValue)
        {
            length = issue.EndColumn.Value - column;

            // Ensure length is non-negative
            if (length < 0)
            {
                length = 0;
            }
        }

        return (location, length);
    }

    /// <summary>
    /// Creates labels for the issue.
    /// </summary>
    private void CreateLabels()
    {
        var color = this.issues.First().Priority switch
        {
            (int)IssuePriority.Error => Color.Red,
            (int)IssuePriority.Warning => Color.Yellow,
            (int)IssuePriority.Suggestion or (int)IssuePriority.Hint => Color.Blue,
            _ => Color.White,
        };

        foreach (var issue in this.issues)
        {
            var (location, length) = this.GetLocation(issue);
            var label =
                new Label(
                    issue.AffectedFileRelativePath.FullPath,
                    location,
                    issue.Message(IssueCommentFormat.PlainText))
                .WithColor(color);

            if (length > 0)
            {
                label = label.WithLength(length);
            }

            this.Labels.Add(label);
        }
    }
}