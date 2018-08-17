namespace Cake.Issues.Markdownlint
{
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Issues.Markdownlint.LogFileFormat;

    /// <summary>
    /// Provider for issues reported my Markdownlint.
    /// </summary>
    public static partial class MarkdownlintIssuesAliases
    {
        /// <summary>
        /// Gets an instance for the log format as written by Markdownlint.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Instance for the Markdownlint log format.</returns>
        [CakePropertyAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static BaseMarkdownlintLogFileFormat MarkdownlintLogFileFormat(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return new MarkdownlintLogFileFormat(context.Log);
        }
    }
}
