namespace Cake.Issues.Markdownlint
{
    using System;
    using Cake.Core;
    using Cake.Core.IO;
    using Core.Annotations;

    /// <summary>
    /// Contains functionality for reading issues from Markdownlint log files.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    public static partial class MarkdownlintIssuesAliases
    {
        /// <summary>
        /// Gets the name of the Markdownlint issue provider.
        /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Name of the Markdownlint issue provider.</returns>
        [CakePropertyAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static string MarkdownlintIssuesProviderTypeName(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return typeof(MarkdownlintIssuesProvider).FullName;
        }

        /// <summary>
        /// Registers a new URL resolver with default priority of 0.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="resolver">Resolver which returns an <see cref="Uri"/> linking to a site
        /// containing help for a specific <see cref="MarkdownlintRuleDescription"/>.</param>
        /// <example>
        /// <para>Adds a provider with default priority of 0 returning a link for all rules with Id smaller than 20 to
        /// search with Google for the rule:</para>
        /// <code>
        /// <![CDATA[
        /// MarkdownlintAddRuleUrlResolver(x =>
        ///     x.RuleId < 20 ?
        ///     new Uri("https://www.google.com/search?q=%22" + x.Rule + ":%22+") :
        ///     null)
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static void MarkdownlintAddRuleUrlResolver(
            this ICakeContext context,
            Func<MarkdownlintRuleDescription, Uri> resolver)
        {
            context.NotNull(nameof(context));
            resolver.NotNull(nameof(resolver));

            MarkdownlintRuleUrlResolver.Instance.AddUrlResolver(resolver);
        }

        /// <summary>
        /// Registers a new URL resolver with a specific priority.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="resolver">Resolver which returns an <see cref="Uri"/> linking to a site
        /// containing help for a specific <see cref="MarkdownlintRuleDescription"/>.</param>
        /// <param name="priority">Priority of the resolver. Resolver with a higher priority are considered
        /// first during resolving of the URL.</param>
        /// <example>
        /// <para>Adds a provider of priority 5 returning a link for all rules with Id smaller than 20 to
        /// search with Google for the rule:</para>
        /// <code>
        /// <![CDATA[
        /// MarkdownlintAddRuleUrlResolver(x =>
        ///     x.RuleId < 20 ?
        ///     new Uri("https://www.google.com/search?q=%22" + x.Rule + ":%22+") :
        ///     null,
        ///     5)
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static void MarkdownlintAddRuleUrlResolver(
            this ICakeContext context,
            Func<MarkdownlintRuleDescription, Uri> resolver,
            int priority)
        {
            context.NotNull(nameof(context));
            resolver.NotNull(nameof(resolver));

            MarkdownlintRuleUrlResolver.Instance.AddUrlResolver(resolver, priority);
        }

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

        /// <summary>
        /// Gets an instance for the log format as written by markdownlint-cli or Cake.Markdownlint.
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

        /// <summary>
        /// Gets an instance of a provider for issues reported by Markdownlint using a log file from disk.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFilePath">Path to the the Markdownlint log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided Markdownlint log file.</param>
        /// <returns>Instance of a provider for issues reported by Markdownlint.</returns>
        /// <example>
        /// <para>Read issues reported by Cake.Markdownlint:</para>
        /// <code>
        /// <![CDATA[
        ///     var issues =
        ///         ReadIssues(
        ///             MarkdownlintIssuesFromFilePath(
        ///                 @"c:\build\Markdownlint.log",
        ///                 MarkdownlintCliLogFileFormat),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider MarkdownlintIssuesFromFilePath(
            this ICakeContext context,
            FilePath logFilePath,
            BaseMarkdownlintLogFileFormat format)
        {
            context.NotNull(nameof(context));
            logFilePath.NotNull(nameof(logFilePath));
            format.NotNull(nameof(format));

            return context.MarkdownlintIssues(new MarkdownlintIssuesSettings(logFilePath, format));
        }

        /// <summary>
        /// Gets an instance of a provider for issues reported by Markdownlint using log file content.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFileContent">Content of the the Markdownlint log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided Markdownlint log file.</param>
        /// <returns>Instance of a provider for issues reported by Markdownlint.</returns>
        /// <example>
        /// <para>Read issues reported by Markdownlint:</para>
        /// <code>
        /// <![CDATA[
        ///     var issues =
        ///         ReadIssues(
        ///             MarkdownlintIssuesFromContent(
        ///                 logFileContent,
        ///                 MarkdownlintLogFileFormat),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider MarkdownlintIssuesFromContent(
            this ICakeContext context,
            string logFileContent,
            BaseMarkdownlintLogFileFormat format)
        {
            context.NotNull(nameof(context));
            logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));
            format.NotNull(nameof(format));

            return context.MarkdownlintIssues(new MarkdownlintIssuesSettings(logFileContent, format));
        }

        /// <summary>
        /// Gets an instance of a provider for issues reported by Markdownlint using specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for reading the Markdownlint log.</param>
        /// <returns>Instance of a provider for issues reported by Markdownlint.</returns>
        /// <example>
        /// <para>Read issues reported by Cake.Markdownlint:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         new MarkdownlintIssuesSettings(
        ///             @"C:\build\Markdownlint.log",
        ///             MarkdownlintCliLogFileFormat);
        ///
        ///     var issues =
        ///         ReadIssues(
        ///             MarkdownlintIssuesFromFilePath(settings),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider MarkdownlintIssues(
            this ICakeContext context,
            MarkdownlintIssuesSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return new MarkdownlintIssuesProvider(context.Log, settings);
        }
    }
}
