namespace Cake.Issues.Markdownlint
{
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Issues.Markdownlint.LogFileFormat;

    /// <content>
    /// Aliases for provider to read issues reported by markdownlint-cli.
    /// </content>
    public static partial class MarkdownlintIssuesAliases
    {
        /// <summary>
        /// Gets an instance for the log format as written by markdownlint-cli.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Instance for the Markdownlint log format.</returns>
        [CakePropertyAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static BaseMarkdownlintLogFileFormat MarkdownlintCliLogFileFormat(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return new MarkdownlintCliLogFileFormat(context.Log);
        }
    }
}
