namespace Cake.Issues;

using System;
using System.Collections.Generic;
using Cake.Issues.FileLinking;

/// <summary>
/// Settings how issues should be linked to files.
/// </summary>
public class FileLinkSettings
{
    private readonly Func<IIssue, IDictionary<string, string>, Uri> builder;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileLinkSettings"/> class.
    /// </summary>
    /// <param name="builder">Callback called for building the file link.</param>
    internal FileLinkSettings(Func<IIssue, IDictionary<string, string>, Uri> builder)
    {
        builder.NotNull();

        this.builder = builder;
    }

    /// <summary>
    /// Returns settings to link files based on a custom pattern.
    /// </summary>
    /// <param name="pattern">Pattern of the file link.
    /// See <see cref="IIssueExtensions.ReplaceIssuePattern(string, IIssue)"/>
    /// for a list of tokens supported in the pattern.</param>
    /// <returns>File link settings.</returns>
    public static FileLinkSettings ForPattern(string pattern)
    {
        pattern.NotNullOrWhiteSpace();

        return
            new FileLinkSettings(
                (issue, _) => new Uri(pattern.ReplaceIssuePattern(issue)));
    }

    /// <summary>
    /// Returns settings to link files based on a custom pattern.
    /// </summary>
    /// <param name="builder">Callback called for building the file link.</param>
    /// <returns>File link settings.</returns>
    public static FileLinkSettings ForAction(Func<IIssue, Uri> builder)
    {
        builder.NotNull();

        return new FileLinkSettings((issue, _) => builder(issue));
    }

    /// <summary>
    /// Returns builder class for settings for linking to files hosted in GitHub.
    /// </summary>
    /// <param name="repositoryUrl">Full URL of the Git repository,
    /// e.g. <code>https://github.com/cake-contrib/Cake.Issues</code>.</param>
    /// <returns>Builder class for the settings.</returns>
    public static GitHubFileLinkSettingsBuilder ForGitHub(Uri repositoryUrl)
    {
        repositoryUrl.NotNull();

        return new GitHubFileLinkSettingsBuilder(repositoryUrl);
    }

    /// <summary>
    /// Returns builder class for settings for linking to files hosted in Azure DevOps.
    /// </summary>
    /// <param name="repositoryUrl">Full URL of the Git repository,
    /// e.g. <code>https://dev.azure.com/myorganization/_git/myrepo</code>.</param>
    /// <returns>Builder class for the settings.</returns>
    public static AzureDevOpsFileLinkSettingsBuilder ForAzureDevOps(Uri repositoryUrl)
    {
        repositoryUrl.NotNull();

        return new AzureDevOpsFileLinkSettingsBuilder(repositoryUrl);
    }

    /// <summary>
    /// Returns the URL to the file on the source code hosting system
    /// for the issue <paramref name="issue"/>.
    /// </summary>
    /// <param name="issue">Issue for which the link should be returned.</param>
    /// <returns>URL to the file on the source code hosting system.</returns>
    public Uri GetFileLink(IIssue issue)
    {
        issue.NotNull();

        return this.builder(issue, new Dictionary<string, string>());
    }
}
