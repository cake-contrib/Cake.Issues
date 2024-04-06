namespace Cake.Issues.Reporting.Console
{
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
            : this(new List<IIssue> { issue })
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

            if (firstIssue.RuleUrl != null)
            {
                this.Note = $"See {firstIssue.RuleUrl} for more information";
            }

            this.CreateLabels();
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
                (var location, var length) = this.GetLocation(issue);
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

        /// <summary>
        /// Returns the diagnostic location of an issue.
        /// </summary>
        /// <param name="issue">Issue for which the location should be returned.</param>
        /// <returns>Location for the diagnostic.</returns>
        private (Location location, int lenght) GetLocation(IIssue issue)
        {
            // Errata currently doesn't support file or line level diagnostics.
            if (!issue.Line.HasValue || !issue.Column.HasValue)
            {
                return default;
            }

            var location = new Location(issue.Line.Value, issue.Column.Value);

            int lenght = 0;
            if (issue.EndColumn.HasValue)
            {
                lenght = issue.EndColumn.Value - issue.Column.Value;
            }

            return (location, lenght);
        }
    }
}