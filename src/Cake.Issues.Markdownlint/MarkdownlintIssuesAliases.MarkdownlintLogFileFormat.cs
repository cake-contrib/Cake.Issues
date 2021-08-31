namespace Cake.Issues.Markdownlint
{
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Issues.Markdownlint.LogFileFormat;

    /// <content>
    /// Provider for issues reported my Markdownlint.
    /// </content>
    public static partial class MarkdownlintIssuesAliases
    {
        /// <summary>
        /// Gets an instance for the log format as written by Markdownlint with <c>options.resultVersion</c> set to 1.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Instance for the Markdownlint V1 log format.</returns>
        [CakePropertyAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static BaseMarkdownlintLogFileFormat MarkdownlintV1LogFileFormat(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return new MarkdownlintV1LogFileFormat(context.Log);
        }
    }
}
