namespace Cake.Issues.Reporting.Console
{
    using System;
    using Errata;
    using Spectre.Console;

    /// <summary>
    /// Custom diagnostic for issues.
    /// </summary>
    internal sealed class IssueDiagnostic : Diagnostic
    {
        private readonly IIssue issue;

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueDiagnostic"/> class.
        /// </summary>
        /// <param name="issue">Issue which the diagnostic should describe.</param>
        public IssueDiagnostic(IIssue issue)
            : base(issue.Rule)
        {
            this.issue = issue;

            this.Category = this.issue.PriorityName;
            this.Color =
                this.issue.Priority switch
                {
                    (int)IssuePriority.Error => Color.Red,
                    (int)IssuePriority.Warning => Color.Yellow,
                    (int)IssuePriority.Suggestion => Color.Blue,
                    (int)IssuePriority.Hint => Color.LightSkyBlue1,
                    _ => throw new Exception(),
                };

            this.CreateLabels();
        }

        /// <summary>
        /// Creates labels for the issue.
        /// </summary>
        private void CreateLabels()
        {
            var color = this.issue.Priority switch
            {
                (int)IssuePriority.Error => Color.Red,
                (int)IssuePriority.Warning => Color.Yellow,
                (int)IssuePriority.Suggestion or (int)IssuePriority.Hint => Color.Blue,
                _ => throw new Exception(),
            };

            (var location, var length) = this.GetLocation(this.issue);
            var label =
                new Label(
                    this.issue.AffectedFileRelativePath.FullPath,
                    location,
                    this.issue.Message(IssueCommentFormat.PlainText))
                .WithColor(color);

            if (length > 0)
            {
                label = label.WithLength(length);
            }

            this.Labels.Add(label);
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