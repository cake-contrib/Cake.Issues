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

        /// <summary>
        /// Gets the name of the Markdownlint issue provider.
        /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
        /// </summary>
        public static string ProviderTypeName => typeof(MarkdownlintIssuesProvider).FullName;

        /// <inheritdoc />
        public override string ProviderName => "markdownlint";
    }
}