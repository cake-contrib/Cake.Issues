namespace Cake.Issues.GitRepository;

using Cake.Core;
using Cake.Core.Annotations;

/// <summary>
/// Contains functionality related to analyze Git repositories.
/// </summary>
[CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
public static class GitRepositoryIssuesAliases
{
    /// <summary>
    /// Gets the name of the Git repository issue provider.
    /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>Name of the Git repository issue provider.</returns>
    [CakePropertyAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static string GitRepositoryIssuesProviderTypeName(
        this ICakeContext context)
    {
        context.NotNull();

        return GitRepositoryIssuesProvider.ProviderTypeName;
    }

    /// <summary>
    /// Gets an instance of a provider for analyzing a Git repository and reporting issues using specified settings.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">Settings for analyzing the Git repository.</param>
    /// <returns>Instance of a provider for analyzing a Git repository and reporting issues.</returns>
    /// <example>
    /// <para>Check for binary files not tracked by Git LFS:</para>
    /// <code>
    /// <![CDATA[
    ///     var settings =
    ///         new GitRepositoryIssuesSettings
    ///         {
    ///             CheckBinaryFilesTrackedWithLfs = true
    ///         };
    ///
    ///     var issues =
    ///         ReadIssues(
    ///             GitRepositoryIssues(settings),
    ///             @"c:\repo");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static IIssueProvider GitRepositoryIssues(
        this ICakeContext context,
        GitRepositoryIssuesSettings settings)
    {
        context.NotNull();
        settings.NotNull();

        return
            new GitRepositoryIssuesProvider(
                context.Log,
                context.FileSystem,
                context.Environment,
                context.ProcessRunner,
                context.Tools,
                settings);
    }
}
