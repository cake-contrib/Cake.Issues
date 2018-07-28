namespace Cake.Issues.Markdownlint
{
    using System.Collections.Generic;
    using Core.Diagnostics;

    /// <summary>
    /// Provider for issues reported by Markdownlint.
    /// </summary>
    public class MarkdownlintIssuesProvider : IssueProvider
    {
        private readonly MarkdownlintIssuesSettings markdownlintSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownlintIssuesProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reading the log file.</param>
        public MarkdownlintIssuesProvider(ICakeLog log, MarkdownlintIssuesSettings settings)
            : base(log)
        {
            settings.NotNull(nameof(settings));

            this.markdownlintSettings = settings;
        }

        /// <inheritdoc />
        public override string ProviderName => "markdownlint";

        /// <inheritdoc />
        protected override IEnumerable<IIssue> InternalReadIssues(IssueCommentFormat format)
        {
            return this.markdownlintSettings.Format.ReadIssues(this, this.Settings, this.markdownlintSettings);
        }
    }
}