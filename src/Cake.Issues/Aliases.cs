namespace Cake.Issues
{
    using System.Collections.Generic;
    using Core;
    using Core.Annotations;
    using Core.IO;

    /// <summary>
    /// Contains functionality related to reading issues.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    public static class Aliases
    {
        /// <summary>
        /// Reads issues from a single issue provider.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="issueProvider">The provider for issues.</param>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        /// <returns>Issues reported by issue provider.</returns>
        /// <example>
        /// <para>Read issues reported as MsBuild warnings:</para>
        /// <code>
        /// <![CDATA[
        ///     var issues = ReadIssues(
        ///         MsBuildIssuesFromFilePath(
        ///             @"C:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         @"c:\repo"));
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.ReadCakeAliasCategory)]
        public static IEnumerable<IIssue> ReadIssues(
            this ICakeContext context,
            IIssueProvider issueProvider,
            DirectoryPath repositoryRoot)
        {
            context.NotNull(nameof(context));
            issueProvider.NotNull(nameof(issueProvider));
            repositoryRoot.NotNull(nameof(repositoryRoot));

            return
                context.ReadIssues(
                    issueProvider,
                    new ReadIssuesSettings(repositoryRoot));
        }

        /// <summary>
        /// Reads issues from issue providers.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="issueProviders">The list of provider for issues.</param>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        /// <returns>Issues reported by all issue providers.</returns>
        /// <example>
        /// <para>Read issues reported as MsBuild warnings and issues reported by JetBrains inspect code:</para>
        /// <code>
        /// <![CDATA[
        ///     var issues = ReadIssues(
        ///         new List<IIssueProvider>
        ///         {
        ///             MsBuildIssuesFromFilePath(
        ///                 @"C:\build\msbuild.log",
        ///                 MsBuildXmlFileLoggerFormat),
        ///             InspectCodeIssuesFromFilePath(
        ///                 @"C:\build\inspectcode.log",
        ///                 MsBuildXmlFileLoggerFormat)
        ///         },
        ///         @"c:\repo"));
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.ReadCakeAliasCategory)]
        public static IEnumerable<IIssue> ReadIssues(
            this ICakeContext context,
            IEnumerable<IIssueProvider> issueProviders,
            DirectoryPath repositoryRoot)
        {
            context.NotNull(nameof(context));
            repositoryRoot.NotNull(nameof(repositoryRoot));

            // ReSharper disable once PossibleMultipleEnumeration
            issueProviders.NotNullOrEmptyOrEmptyElement(nameof(issueProviders));

            // ReSharper disable once PossibleMultipleEnumeration
            return
                context.ReadIssues(
                    issueProviders,
                    new ReadIssuesSettings(repositoryRoot));
        }

        /// <summary>
        /// Reads issues from a single issue provider using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="issueProvider">The provider for issues.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Issues reported by issue provider.</returns>
        /// <example>
        /// <para>Read issues reported as MsBuild warnings and format comments in Markdown:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         new ReadIssuesSettings(@"c:\repo")
        ///         {
        ///             Format = IssueCommentFormat.Markdown
        ///         };
        ///
        ///     var issues = ReadIssues(
        ///         MsBuildIssuesFromFilePath(
        ///             @"C:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         settings));
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.ReadCakeAliasCategory)]
        public static IEnumerable<IIssue> ReadIssues(
            this ICakeContext context,
            IIssueProvider issueProvider,
            ReadIssuesSettings settings)
        {
            context.NotNull(nameof(context));
            issueProvider.NotNull(nameof(issueProvider));
            settings.NotNull(nameof(settings));

            return
                context.ReadIssues(
                    new List<IIssueProvider> { issueProvider },
                    settings);
        }

        /// <summary>
        /// Reads issues from issue providers using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="issueProviders">The list of provider for issues.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Issues reported by all issue providers.</returns>
        /// <example>
        /// <para>Read issues reported as MsBuild warnings and issues reported by JetBrains inspect code
        /// with comments formatted as Markdown:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         new ReadIssuesSettings(@"c:\repo")
        ///         {
        ///             Format = IssueCommentFormat.Markdown
        ///         };
        ///
        ///     var issues = ReadIssues(
        ///         new List<IIssueProvider>
        ///         {
        ///             MsBuildIssuesFromFilePath(
        ///                 @"C:\build\msbuild.log",
        ///                 MsBuildXmlFileLoggerFormat),
        ///             InspectCodeIssuesFromFilePath(
        ///                 @"C:\build\inspectcode.log",
        ///                 MsBuildXmlFileLoggerFormat)
        ///         },
        ///         settings));
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.ReadCakeAliasCategory)]
        public static IEnumerable<IIssue> ReadIssues(
            this ICakeContext context,
            IEnumerable<IIssueProvider> issueProviders,
            ReadIssuesSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            // ReSharper disable once PossibleMultipleEnumeration
            issueProviders.NotNullOrEmptyOrEmptyElement(nameof(issueProviders));

            // ReSharper disable once PossibleMultipleEnumeration
            var issuesReader =
                new IssuesReader(context.Log, issueProviders, settings);

            return issuesReader.ReadIssues(settings.Format);
        }
    }
}
