namespace Cake.Issues.MsBuild
{
    using System;
    using Core;
    using Core.Annotations;
    using Core.IO;
    using IssueProvider;

    /// <summary>
    /// Contains functionality related to importing code analysis issues from MSBuild logs to write them to
    /// pull requests.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    public static class MsBuildIssuesAliases
    {
        /// <summary>
        /// Registers a new URL resolver with default priority of 0.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="resolver">Resolver which returns an <see cref="Uri"/> linking to a site
        /// containing help for a specific <see cref="BaseRuleDescription"/>.</param>
        /// <example>
        /// <para>Adds a provider with default priority of 0 returning a link for all rules of the category <c>CA</c> to
        /// search <c>msdn.microsoft.com</c> with Google for the rule:</para>
        /// <code>
        /// <![CDATA[
        /// MsBuildAddRuleUrlResolver(x =>
        ///     x.Category.ToUpperInvariant() == "CA" ?
        ///     new Uri("https://www.google.com/search?q=%22" + x.Rule + ":%22+site:msdn.microsoft.com") :
        ///     null)
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static void MsBuildAddRuleUrlResolver(
            this ICakeContext context,
            Func<MsBuildRuleDescription, Uri> resolver)
        {
            context.NotNull(nameof(context));
            resolver.NotNull(nameof(resolver));

            MsBuildRuleUrlResolver.Instance.AddUrlResolver(resolver);
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
        /// <para>Adds a provider of priority 5 returning a link for all rules of the category <c>CA</c> to
        /// search <c>msdn.microsoft.com</c> with Google for the rule:</para>
        /// <code>
        /// <![CDATA[
        /// MsBuildAddRuleUrlResolver(x =>
        ///     x.Category.ToUpperInvariant() == "CA" ?
        ///     new Uri("https://www.google.com/search?q=%22" + x.Rule + ":%22+site:msdn.microsoft.com") :
        ///     null,
        ///     5)
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static void MsBuildAddRuleUrlResolver(
            this ICakeContext context,
            Func<MsBuildRuleDescription, Uri> resolver,
            int priority)
        {
            context.NotNull(nameof(context));
            resolver.NotNull(nameof(resolver));

            MsBuildRuleUrlResolver.Instance.AddUrlResolver(resolver, priority);
        }

        /// <summary>
        /// <para>
        /// Gets an instance for the MsBuild log format as written by the <c>XmlFileLogger</c> class
        /// from MSBuild Extension Pack.
        /// </para>
        /// <para>
        /// You can add the logger to the MSBuildSettings like this:
        /// <code>
        /// var settings = new MsBuildSettings()
        ///     .WithLogger(
        ///         Context.Tools.Resolve("MSBuild.ExtensionPack.Loggers.dll").FullPath,
        ///         "XmlFileLogger",
        ///         string.Format(
        ///             "logfile=\"{0}\";verbosity=Detailed;encoding=UTF-8",
        ///             @"C:\build\msbuild.log")
        ///     )
        /// </code>
        /// </para>
        /// <para>
        /// In order to use the above logger, include the following in your build.cake file to download and
        /// install from NuGet.org:
        /// <code>
        /// #tool "nuget:?package=MSBuild.Extension.Pack"
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Instance for the MsBuild log format.</returns>
        [CakePropertyAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static ILogFileFormat MsBuildXmlFileLoggerFormat(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return new XmlFileLoggerFormat(context.Log);
        }

        /// <summary>
        /// Gets an instance of a provider for code analysis issues reported as MsBuild warnings using a log file from disk.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFilePath">Path to the the MsBuild log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided MsBuild log file.</param>
        /// <returns>Instance of a provider for code analysis issues reported as MsBuild warnings.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var repoRoot = new DirectoryPath("c:\repo");
        ///     ReportIssuesToPullRequest(
        ///         MsBuildIssuesFromFilePath(
        ///             "C:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat),
        ///         TfsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm()),
        ///         repoRoot);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider MsBuildIssuesFromFilePath(
            this ICakeContext context,
            FilePath logFilePath,
            ILogFileFormat format)
        {
            context.NotNull(nameof(context));
            logFilePath.NotNull(nameof(logFilePath));
            format.NotNull(nameof(format));

            return context.MsBuildIssues(MsBuildIssuesSettings.FromFilePath(logFilePath, format));
        }

        /// <summary>
        /// Gets an instance of a provider for code analysis issues reported as MsBuild warnings using log content.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFileContent">Content of the the MsBuild log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided MsBuild log file.</param>
        /// <returns>Instance of a provider for code analysis issues reported as MsBuild warnings.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var repoRoot = new DirectoryPath("c:\repo");
        ///     ReportIssuesToPullRequest(
        ///         MsBuildIssuesFromContent(
        ///             logFileContent,
        ///             MsBuildXmlFileLoggerFormat),
        ///         TfsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm()),
        ///         repoRoot);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider MsBuildIssuesFromContent(
            this ICakeContext context,
            string logFileContent,
            ILogFileFormat format)
        {
            context.NotNull(nameof(context));
            logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));
            format.NotNull(nameof(format));

            return context.MsBuildIssues(MsBuildIssuesSettings.FromContent(logFileContent, format));
        }

        /// <summary>
        /// Gets an instance of a provider for code analysis issues reported as MsBuild warnings using specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for reading the MSBuild log.</param>
        /// <returns>Instance of a provider for code analysis issues reported as MsBuild warnings.</returns>
        /// <example>
        /// <para>Report code analysis issues reported as MsBuild warnings to a TFS pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var repoRoot = new DirectoryPath("c:\repo");
        ///     var settings =
        ///         MsBuildIssuesSettings.FromFilePath(
        ///             "C:\build\msbuild.log",
        ///             MsBuildXmlFileLoggerFormat);
        ///
        ///     ReportIssuesToPullRequest(
        ///         MsBuildIssues(settings),
        ///         TfsPullRequests(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm()),
        ///         repoRoot);
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
