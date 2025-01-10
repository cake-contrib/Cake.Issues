namespace Cake.Issues.Tap;

using Cake.Core.Diagnostics;
using Cake.Core.IO;

/// <summary>
/// Base class for all log file formats supported by the TAP issue provider.
/// </summary>
/// <param name="log">The Cake log instance.</param>
public abstract class BaseTapLogFileFormat(ICakeLog log)
    : BaseLogFileFormat<TapIssuesProvider, TapIssuesSettings>(log)
{
    /// <summary>
    /// Validates a file path.
    /// </summary>
    /// <param name="filePath">Full file path.</param>
    /// <param name="repositorySettings">Repository settings.</param>
    /// <returns>Tuple containing a value if validation was successful, and file path relative to repository root.</returns>
    protected static (bool Valid, string FilePath) ValidateFilePath(string filePath, IRepositorySettings repositorySettings)
    {
        filePath.NotNullOrWhiteSpace();
        repositorySettings.NotNull();

        // Make path relative to repository root.
        if (!new FilePath(filePath).IsRelative)
        {
            filePath = filePath.NormalizePath().MakeFilePathRelativeToRepositoryRoot(repositorySettings);
        }

        return (true, filePath);
    }
}
