namespace Cake.Issues.MsBuild
{
    using System;
    using System.Text;
    using Core;
    using Core.Annotations;
    using Core.IO;

    /// <summary>
    /// Contains functionality for reading warnings from MSBuild log files.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    public static class MsBuildIssuesAliases
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

            return typeof(MsBuildIssuesProvider).FullName;
        }

        /// <summary>
        /// Registers a new URL resolver with default priority of 0.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="resolver">Resolver which returns an <see cref="Uri"/> linking to a site
        /// containing help for a specific <see cref="MsBuildRuleDescription"/>.</param>
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
        /// containing help for a specific <see cref="MsBuildRuleDescription"/>.</param>
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
        /// var settings = new MSBuildSettings()
        ///     .WithLogger(
        ///         Context.Tools.Resolve("MSBuild.ExtensionPack.Loggers.dll").FullPath,
        ///         "XmlFileLogger",
        ///         string.Format(
        ///             "logfile=\"{0}\";verbosity=Detailed;encoding=UTF-8",
        ///             @"c:\build\msbuild.log")
        ///     );
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
        public static MsBuildLogFileFormat MsBuildXmlFileLoggerFormat(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return new XmlFileLoggerFormat(context.Log);
        }

        /// <summary>
        /// Gets an instance of a provider for issues reported as MsBuild warnings using a log file from disk.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logFilePath">Path to the the MsBuild log file.
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
            MsBuildLogFileFormat format)
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
        /// <param name="logFileContent">Content of the the MsBuild log file.
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
            MsBuildLogFileFormat format)
        {
            context.NotNull(nameof(context));
            logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));
            format.NotNull(nameof(format));

            return context.MsBuildIssues(new MsBuildIssuesSettings(Encoding.UTF8.GetBytes(logFileContent), format));
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
        ///         MsBuildIssuesSettings.FromFilePath(
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
