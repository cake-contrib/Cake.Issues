namespace Cake.Issues.JUnit;

using Cake.Core.Diagnostics;
using Cake.Core.IO;

/// <summary>
/// Base class for all log file formats supported by the JUnit issue provider.
/// </summary>
/// <param name="log">The Cake log instance.</param>
public abstract class BaseJUnitLogFileFormat(ICakeLog log)
    : BaseLogFileFormat<JUnitIssuesProvider, JUnitIssuesSettings>(log)
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
}