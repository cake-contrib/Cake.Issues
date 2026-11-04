namespace Cake.Issues;

using Cake.Core.IO;

/// <summary>
/// Settings containing a path to a repository.
/// </summary>
public class RepositorySettings : IRepositorySettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RepositorySettings"/> class.
    /// </summary>
    /// <param name="repositoryRoot">Root path of the repository.</param>
    public RepositorySettings(DirectoryPath repositoryRoot)
    {
        repositoryRoot.NotNull();

        this.RepositoryRoot = repositoryRoot;
    }

    /// <inheritdoc/>
    public DirectoryPath RepositoryRoot { get; }
}
