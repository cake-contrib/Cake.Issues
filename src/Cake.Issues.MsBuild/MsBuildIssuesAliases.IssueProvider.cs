namespace Cake.Issues.MsBuild
{
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Core.IO;

    /// <content>
    /// Contains functionality related to <see cref="MsBuildIssuesProvider"/>.
    /// </content>
    public static partial class MsBuildIssuesAliases
    {
        /// <summary>
        /// Gets the name of the MsBuild issue provider.
        /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Name of the MsBuild issue provider.</returns>
        [CakePropertyAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static string MsBuildIssuesProviderTypeName(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return MsBuildIssuesProvider.ProviderTypeName;
        }

        /// <summary>
        /// Gets an instance of a provider for issues reported as MsBuild warnings using a log file from disk.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFilePath">Path to the MsBuild log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided MsBuild log file.</param>
        /// <returns>Instance of a provider for issues reported as MsBuild warnings.</returns>
        /// <example>
        /// <para>Read issues reported as MsBuild warnings:</para>
        /// <code>
        /// <![CDATA[
        ///     var issues =
        ///         ReadIssues(
        ///             MsBuildIssuesFromFilePath(
        ///                 @"c:\build\msbuild.xml",
        ///                 MsBuildXmlFileLoggerFormat),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider MsBuildIssuesFromFilePath(
            this ICakeContext context,
            FilePath logFilePath,
            BaseMsBuildLogFileFormat format)
        {
            context.NotNull(nameof(context));
            logFilePath.NotNull(nameof(logFilePath));
            format.NotNull(nameof(format));

            return context.MsBuildIssues(new MsBuildIssuesSettings(logFilePath, format));
        }

        /// <summary>
        /// Gets an instance of a provider for issues reported as MsBuild warnings using log content.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFileContent">Content of the MsBuild log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided MsBuild log file.</param>
        /// <returns>Instance of a provider for issues reported as MsBuild warnings.</returns>
        /// <example>
        /// <para>Read issues reported as MsBuild warnings:</para>
        /// <code>
        /// <![CDATA[
        ///     var issues =
        ///         ReadIssues(
        ///             MsBuildIssuesFromContent(
        ///                 logFileContent,
        ///                 MsBuildXmlFileLoggerFormat)),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider MsBuildIssuesFromContent(
            this ICakeContext context,
            string logFileContent,
            BaseMsBuildLogFileFormat format)
        {
            context.NotNull(nameof(context));
            logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));
            format.NotNull(nameof(format));

            return context.MsBuildIssues(new MsBuildIssuesSettings(logFileContent.ToByteArray(), format));
        }

        /// <summary>
        /// Gets an instance of a provider for issues reported as MsBuild warnings using specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for reading the MSBuild log.</param>
        /// <returns>Instance of a provider for issues reported as MsBuild warnings.</returns>
        /// <example>
        /// <para>Read issues reported as MsBuild warnings:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         new MsBuildIssuesSettings(
        ///             @"c:\build\msbuild.xml",
        ///             MsBuildXmlFileLoggerFormat);
        ///
        ///     var issues =
        ///         ReadIssues(
        ///             MsBuildIssues(settings),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider MsBuildIssues(
            this ICakeContext context,
            MsBuildIssuesSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return new MsBuildIssuesProvider(context.Log, settings);
        }
    }
}
