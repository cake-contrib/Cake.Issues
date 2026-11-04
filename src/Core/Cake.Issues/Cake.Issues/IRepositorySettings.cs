namespace Cake.Issues;

using Cake.Core.IO;

/// <summary>
/// Interface for settings containing a path to a repository.
/// </summary>
public interface IRepositorySettings
{
    /// <summary>
    /// Gets the Root path of the repository.
    /// </summary>
    DirectoryPath RepositoryRoot { get; }
}
