namespace Cake.Issues.Sarif
{
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Core.IO;

    /// <summary>
    /// Contains functionality for reading issues from SARIF files.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    public static class SarifIssuesAliases
    {
        /// <summary>
        /// Gets the name of the SARIF issue provider.
        /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Name of the SARIF issue provider.</returns>
        [CakePropertyAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static string SarifIssuesProviderTypeName(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return typeof(SarifIssuesProvider).FullName;
        }

        /// <summary>
        /// Gets an instance of a provider for SARIF compatible files using a file from disk.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFilePath">Path to the SARIF file.</param>
        /// <returns>Instance of a provider for SARIF compatible files.</returns>
        /// <example>
        /// <para>Read issues from a SARIF compatible file:</para>
        /// <code>
        /// <![CDATA[
        ///     var issues =
        ///         ReadIssues(
        ///             SarifIssuesFromFilePath(@"c:\build\log.sarif"),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider SarifIssuesFromFilePath(
            this ICakeContext context,
            FilePath logFilePath)
        {
            context.NotNull(nameof(context));
            logFilePath.NotNull(nameof(logFilePath));

            return context.SarifIssues(new SarifIssuesSettings(logFilePath));
        }

        /// <summary>
        /// Gets an instance of a provider for SARIF compatible files using file content.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFileContent">Content of the SARIF compatible file.</param>
        /// <returns>Instance of a provider for SARIF compatible files.</returns>
        /// <example>
        /// <para>Read issues from a SARIF compatible file:</para>
        /// <code>
        /// <![CDATA[
        ///     var issues =
        ///         ReadIssues(
        ///             SarifIssuesFromContent(logFileContent)),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider SarifIssuesFromContent(
            this ICakeContext context,
            string logFileContent)
        {
            context.NotNull(nameof(context));
            logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));

            return context.SarifIssues(new SarifIssuesSettings(logFileContent.ToByteArray()));
        }

        /// <summary>
        /// Gets an instance of a provider for SARIF compatible files using specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for reading the SARIF compatible file.</param>
        /// <returns>Instance of a provider for SARIF compatible files.</returns>
        /// <example>
        /// <para>Read issues from a SARIF compatible file:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         new SarifIssuesSettings(
        ///             @"c:\build\log.sarif));
        ///
        ///     var issues =
        ///         ReadIssues(
        ///             SarifIssues(settings),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider SarifIssues(
            this ICakeContext context,
            SarifIssuesSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return new SarifIssuesProvider(context.Log, settings);
        }
    }
}
