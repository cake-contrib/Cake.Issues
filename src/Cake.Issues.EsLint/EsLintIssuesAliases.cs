namespace Cake.Issues.EsLint
{
    using System;
    using System.Text;
    using Cake.Issues.EsLint.LogFileFormat;
    using Core;
    using Core.Annotations;
    using Core.IO;

    /// <summary>
    /// Contains functionality for reading issues reported by ESLint.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    public static class EsLintIssuesAliases
    {
        /// <summary>
        /// Gets the name of the ESLint issue provider.
        /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Name of the ESLint issue provider.</returns>
        [CakePropertyAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static string EsLintIssuesProviderTypeName(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return typeof(EsLintIssuesProvider).FullName;
        }

        /// <summary>
        /// Registers a new URL resolver with default priority of 0.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="resolver">Resolver which returns an <see cref="Uri"/> linking to a site
        /// containing help for a specific <see cref="BaseRuleDescription"/>.</param>
        /// <example>
        /// <para>Adds a provider with default priority of 0 returning a link for all rules starting
        /// with the string <c>Foo</c> to search with Google for the rule:</para>
        /// <code>
        /// <![CDATA[
        /// EsLintAddRuleUrlResolver(x =>
        ///     x.Rule.StartsWith("Foo") ?
        ///     new Uri("https://www.google.com/search?q=%22" + x.Rule + "%22") :
        ///     null)
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static void EsLintAddRuleUrlResolver(
            this ICakeContext context,
            Func<BaseRuleDescription, Uri> resolver)
        {
            context.NotNull(nameof(context));
            resolver.NotNull(nameof(resolver));

            EsLintRuleUrlResolver.Instance.AddUrlResolver(resolver);
        }

        /// <summary>
        /// Registers a new URL resolver with a specific priority.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="resolver">Resolver which returns an <see cref="Uri"/> linking to a site
        /// containing help for a specific <see cref="BaseRuleDescription"/>.</param>
        /// <param name="priority">Priority of the resolver. Resolver with a higher priority are considered
        /// first during resolving of the URL.</param>
        /// <example>
        /// <para>Adds a provider of priority 5 returning a link for all rules starting with the string
        /// <c>Foo</c> to search with Google for the rule:</para>
        /// <code>
        /// <![CDATA[
        /// EsLintAddRuleUrlResolver(x =>
        ///     x.Rule.StartsWith("Foo") ?
        ///     new Uri("https://www.google.com/search?q=%22" + x.Rule + "%22") :
        ///     null,
        ///     5)
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static void EsLintAddRuleUrlResolver(
            this ICakeContext context,
            Func<BaseRuleDescription, Uri> resolver,
            int priority)
        {
            context.NotNull(nameof(context));
            resolver.NotNull(nameof(resolver));

            EsLintRuleUrlResolver.Instance.AddUrlResolver(resolver, priority);
        }

        /// <summary>
        /// Gets an instance for the ESLint JSON log format as written by the JSON formatter.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Instance for the ESLint JSON log format.</returns>
        [CakePropertyAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static BaseEsLintLogFileFormat EsLintJsonFormat(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return new JsonLogFileFormat(context.Log);
        }

        /// <summary>
        /// Gets an instance of a provider for issues reported by ESLint using a log file from disk.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFilePath">Path to the the ESLint log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided ESLint log file.</param>
        /// <returns>Instance of a provider for issues reported by ESLint.</returns>
        /// <example>
        /// <para>Read issues reported by ESLint:</para>
        /// <code>
        /// <![CDATA[
        ///     var issues =
        ///         ReadIssues(
        ///             EsLintIssuesFromFilePath(@"c:\build\ESLint.log"),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider EsLintIssuesFromFilePath(
            this ICakeContext context,
            FilePath logFilePath,
            BaseEsLintLogFileFormat format)
        {
            context.NotNull(nameof(context));
            logFilePath.NotNull(nameof(logFilePath));
            format.NotNull(nameof(format));

            return context.EsLintIssues(new EsLintIssuesSettings(logFilePath, format));
        }

        /// <summary>
        /// Gets an instance of a provider for issues reported by ESLint using log file content.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFileContent">Content of the the ESLint log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided ESLint log file.</param>
        /// <returns>Instance of a provider for issues reported by ESLint.</returns>
        /// <example>
        /// <para>Read issues reported by ESLint:</para>
        /// <code>
        /// <![CDATA[
        ///     var issues =
        ///         ReadIssues(
        ///             EsLintIssuesFromContent(logFileContent),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider EsLintIssuesFromContent(
            this ICakeContext context,
            string logFileContent,
            BaseEsLintLogFileFormat format)
        {
            context.NotNull(nameof(context));
            logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));
            format.NotNull(nameof(format));

            return
                context.EsLintIssues(
                    new EsLintIssuesSettings(Encoding.UTF8.GetBytes(logFileContent), format));
        }

        /// <summary>
        /// Gets an instance of a provider for issues reported by ESLint using specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for reading the ESLint log.</param>
        /// <returns>Instance of a provider for issues reported by ESLint.</returns>
        /// <example>
        /// <para>Read issues reported by ESLint:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         new EsLintIssuesSettings(@"c:\build\ESLint.log");
        ///
        ///     var issues =
        ///         ReadIssues(
        ///             EsLintIssues(settings),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider EsLintIssues(
            this ICakeContext context,
            EsLintIssuesSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return new EsLintIssuesProvider(context.Log, settings);
        }
    }
}
