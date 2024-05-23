namespace Cake.Issues.MsBuild;

using Cake.Core;
using Cake.Core.Annotations;
using Cake.Issues.MsBuild.LogFileFormat;

/// <content>
/// Contains functionality related to <see cref="XmlFileLoggerLogFileFormat"/>.
/// </content>
public static partial class MsBuildIssuesAliases
{
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
    /// In order to use the above logger, include the following in your <c>build.cake</c> file to download and
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
    public static BaseMsBuildLogFileFormat MsBuildXmlFileLoggerFormat(
        this ICakeContext context)
    {
        context.NotNull();

        return new XmlFileLoggerLogFileFormat(context.Log);
    }
}
