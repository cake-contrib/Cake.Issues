namespace Cake.Issues.Markdownlint
{
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Provider for issues reported by Markdownlint.
    /// </summary>
    /// <param name="log">The Cake log context.</param>
    /// <param name="settings">Settings for reading the log file.</param>
    public class MarkdownlintIssuesProvider(ICakeLog log, MarkdownlintIssuesSettings settings)
        : BaseMultiFormatIssueProvider<MarkdownlintIssuesSettings, MarkdownlintIssuesProvider>(log, settings)
    {
        /// <summary>
        /// Gets the name of the Markdownlint issue provider.
        /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
        /// </summary>
        public static string ProviderTypeName => typeof(MarkdownlintIssuesProvider).FullName;

        /// <inheritdoc />
        public override string ProviderName => "markdownlint";
    }
}