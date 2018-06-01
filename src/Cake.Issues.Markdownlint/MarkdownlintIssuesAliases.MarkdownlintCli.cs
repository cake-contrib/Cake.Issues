namespace Cake.Issues.Markdownlint
{
    using Cake.Issues.Markdownlint.MarkdownlintCli;
    using Core;
    using Core.Annotations;
    using Core.IO;

#pragma warning disable SA1601 // Partial elements must be documented
    public static partial class MarkdownlintIssuesAliases
#pragma warning restore SA1601 // Partial elements must be documented
    {
        /// <summary>
        /// Gets the name of the markdownlint-cli issue provider.
        /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Name of the markdownlint-cli issue provider.</returns>
        [CakePropertyAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        [CakeNamespaceImport("Cake.Issues.Markdownlint.MarkdownlintCli")]
        public static string MarkdownlintCliIssuesProviderTypeName(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return typeof(MarkdownlintCliIssuesProvider).FullName;
        }

        /// <summary>
        /// Gets an instance of a provider for issues reported by markdownlint-cli or Cake.Markdownlint
        /// using a log file from disk.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFilePath">Path to the the markdownlint-cli log file.</param>
        /// <returns>Instance of a provider for issues reported by markdownlint-cli.</returns>
        /// <example>
        /// <para>Read issues reported by markdownlint-cli or Cake.Markdownlint:</para>
        /// <code>
        /// <![CDATA[
        ///     var issues =
        ///         ReadIssues(
        ///             MarkdownlintCliIssuesFromFilePath(@"c:\build\Markdownlint.log"),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        [CakeNamespaceImport("Cake.Issues.Markdownlint.MarkdownlintCli")]
        public static IIssueProvider MarkdownlintCliIssuesFromFilePath(
            this ICakeContext context,
            FilePath logFilePath)
        {
            context.NotNull(nameof(context));
            logFilePath.NotNull(nameof(logFilePath));

            return context.MarkdownlintCliIssues(MarkdownlintCliIssuesSettings.FromFilePath(logFilePath));
        }

        /// <summary>
        /// Gets an instance of a provider for issues reported by markdownlint-cli or Cake.Markdownlint
        /// using log file content.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFileContent">Content of the the markdownlint-cli log file.</param>
        /// <returns>Instance of a provider for issues reported by markdownlint-cli.</returns>
        /// <example>
        /// <para>Read issues reported by markdownlint-cli or Cake.Markdownlint:</para>
        /// <code>
        /// <![CDATA[
        ///     var issues =
        ///         ReadIssues(
        ///             MarkdownlintCliIssuesFromContent(logFileContent),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        [CakeNamespaceImport("Cake.Issues.Markdownlint.MarkdownlintCli")]
        public static IIssueProvider MarkdownlintCliIssuesFromContent(
            this ICakeContext context,
            string logFileContent)
        {
            context.NotNull(nameof(context));
            logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));

            return context.MarkdownlintCliIssues(MarkdownlintCliIssuesSettings.FromContent(logFileContent));
        }

        /// <summary>
        /// Gets an instance of a provider for issues reported by markdownlint-cli or Cake.Markdownlint
        /// using specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for reading the markdownlint-cli log.</param>
        /// <returns>Instance of a provider for issues reported by markdownlint-cli.</returns>
        /// <example>
        /// <para>Read issues reported by markdownlint-cli or Cake.Markdownlint:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         new MarkdownlintCliIssuesSettings("C:\build\Markdownlint.log");
        ///
        ///     var issues =
        ///         ReadIssues(
        ///             MarkdownlintCliIssuesFromFilePath(settings),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        [CakeNamespaceImport("Cake.Issues.Markdownlint.MarkdownlintCli")]
        public static IIssueProvider MarkdownlintCliIssues(
            this ICakeContext context,
            MarkdownlintCliIssuesSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return new MarkdownlintCliIssuesProvider(context.Log, settings);
        }
    }
}
