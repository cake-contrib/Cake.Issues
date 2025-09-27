namespace Cake.Issues;

using System.Collections.Generic;
using Cake.Core.Diagnostics;
using Cake.Core.IO;

/// <summary>
/// Base class for all issue provider implementations.
/// </summary>
/// <param name="log">The Cake log context.</param>
public abstract class BaseIssueProvider(ICakeLog log)
    : BaseIssueComponent<IReadIssuesSettings>(log), IIssueProvider
{
    /// <inheritdoc/>
    public abstract string ProviderName { get; }

    /// <inheritdoc/>
    public virtual string ProviderType => this.GetType().FullName;

    /// <inheritdoc/>
    public IEnumerable<IIssue> ReadIssues()
    {
        this.AssertInitialized();

        return this.InternalReadIssues();
    }

    /// <summary>
    /// Validates a file path.
    /// </summary>
    /// <param name="filePath">Full file path.</param>
    /// <param name="repositorySettings">Repository settings.</param>
    /// <returns>Tuple containing a value if validation was successful, and file path relative to repository root.</returns>
    public static (bool Valid, string FilePath) ValidateFilePath(string filePath, IRepositorySettings repositorySettings)
    {
        filePath.NotNullOrWhiteSpace();
        repositorySettings.NotNull();

        if (!new FilePath(filePath).IsRelative)
        {
            // Ignore files from outside the repository.
            if (!filePath.IsInRepository(repositorySettings))
            {
                return (false, string.Empty);
            }

            // Make path relative to repository root.
            filePath = filePath.NormalizePath().MakeFilePathRelativeToRepositoryRoot(repositorySettings);
        }

        return (true, filePath);
    }

    /// <summary>
    /// Gets all issues.
    /// Compared to <see cref="ReadIssues"/> it is safe to access Settings from this method.
    /// </summary>
    /// <returns>List of issues.</returns>
    protected abstract IEnumerable<IIssue> InternalReadIssues();
}
