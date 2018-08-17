namespace Cake.Issues.Markdownlint
{
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Provider for issues reported by Markdownlint.
    /// </summary>
    public class MarkdownlintIssuesProvider : BaseMultiFormatIssueProvider<MarkdownlintIssuesSettings, MarkdownlintIssuesProvider>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownlintIssuesProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for reading the log file.</param>
        public MarkdownlintIssuesProvider(ICakeLog log, MarkdownlintIssuesSettings settings)
            : base(log, settings)
        {
        }

        /// <inheritdoc />
        public override string ProviderName => "markdownlint";
    }
}