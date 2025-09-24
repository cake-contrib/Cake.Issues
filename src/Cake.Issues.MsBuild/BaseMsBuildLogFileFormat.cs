namespace Cake.Issues.MsBuild;

using Cake.Core.Diagnostics;

/// <summary>
/// Base class for all MSBuild log file format.
/// </summary>
/// <param name="log">The Cake log instance.</param>
public abstract class BaseMsBuildLogFileFormat(ICakeLog log)
    : BaseLogFileFormat<MsBuildIssuesProvider, MsBuildIssuesSettings>(log)
{
    /// <summary>
    /// Validates a file path.
    /// </summary>
    /// <param name="filePath">Full file path.</param>
    /// <param name="repositorySettings">Repository settings.</param>
    /// <returns>Tuple containing a value if validation was successful, and file path relative to repository root.</returns>
    protected (bool Valid, string FilePath) ValidateFilePath(string filePath, IRepositorySettings repositorySettings) =>
        BaseIssueProvider.ValidateFilePath(filePath, repositorySettings);
}
