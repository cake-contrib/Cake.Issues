﻿namespace Cake.Issues.EsLint
{
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Core.IO;

    /// <content>
    /// Contains functionality related to <see cref="EsLintIssuesProvider"/>.
    /// </content>
    public static partial class EsLintIssuesAliases
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
                    new EsLintIssuesSettings(logFileContent, format));
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
