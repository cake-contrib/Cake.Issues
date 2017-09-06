namespace Cake.Issues.EsLint
{
    using System.Collections.Generic;
    using Core.Diagnostics;

    /// <summary>
    /// Provider for issues reported by ESLint.
    /// </summary>
    internal class EsLintIssuesProvider : IssueProvider
    {
        private readonly EsLintIssuesSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="EsLintIssuesProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reading the log file.</param>
        public EsLintIssuesProvider(ICakeLog log, EsLintIssuesSettings settings)
            : base(log)
        {
            settings.NotNull(nameof(settings));

            this.settings = settings;
        }

        /// <inheritdoc />
        protected override IEnumerable<IIssue> InternalReadIssues(IssueCommentFormat format)
        {
            return this.settings.Format.ReadIssues(this.Settings, this.settings);
        }
    }
}
