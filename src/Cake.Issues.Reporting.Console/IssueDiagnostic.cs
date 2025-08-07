namespace Cake.Issues.Reporting.Console;

using System;
using System.Collections.Generic;
using System.Linq;
using Errata;
using Spectre.Console;

/// <summary>
/// Custom diagnostic for issues.
/// </summary>
internal sealed class IssueDiagnostic : Diagnostic
{
    private readonly IEnumerable<IIssue> issues;

    /// <summary>
    /// Initializes a new instance of the <see cref="IssueDiagnostic"/> class.
    /// </summary>
    /// <param name="issue">Issue which the diagnostic should describe.</param>
    public IssueDiagnostic(IIssue issue)
        : this([issue])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IssueDiagnostic"/> class.
    /// </summary>
    /// <param name="issues">Issues which the diagnostic should describe.</param>
    public IssueDiagnostic(IEnumerable<IIssue> issues)

        : base(issues.First().RuleId)
    {
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
                _ => throw new Exception(),
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
    private static (Location Location, int Lenght) GetLocation(IIssue issue)
    {
        // Errata currently doesn't support file or line level diagnostics.
        if (!issue.Line.HasValue || !issue.Column.HasValue)
        {
            return default;
        }

        var location = new Location(issue.Line.Value, issue.Column.Value);

        var length = 0;
        if (issue.EndColumn.HasValue)
        {
            length = issue.EndColumn.Value - issue.Column.Value;
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
            _ => throw new Exception(),
        };

        foreach (var issue in this.issues)
        {
            var (location, length) = GetLocation(issue);
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