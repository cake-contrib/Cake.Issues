namespace Cake.Issues;

using Cake.Core.IO;

/// <summary>
/// Settings for reading issues.
/// </summary>
/// <param name="repositoryRoot">Root path of the repository.</param>
public class ReadIssuesSettings(DirectoryPath repositoryRoot) : RepositorySettings(repositoryRoot), IReadIssuesSettings
{
    /// <inheritdoc/>
    public string Run { get; set; }

    /// <inheritdoc/>
    public FileLinkSettings FileLinkSettings { get; set; }
}
