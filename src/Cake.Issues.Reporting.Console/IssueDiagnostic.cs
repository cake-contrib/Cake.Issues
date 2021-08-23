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
        }

        /// <inheritdoc />
        public override Color GetColor()
        {
            return this.issue.Priority switch
            {
                (int)IssuePriority.Error => Color.Red,
                (int)IssuePriority.Warning => Color.Yellow,
                (int)IssuePriority.Suggestion => Color.Blue,
                (int)IssuePriority.Hint => Color.LightSkyBlue1,
                _ => throw new Exception(),
            };
        }

        /// <inheritdoc />
        public override string GetPrefix()
        {
            return this.issue.PriorityName;
        }
    }
}