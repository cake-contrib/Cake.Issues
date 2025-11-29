namespace Cake.Issues.Reporting;

using Cake.Core.IO;

/// <summary>
/// Interface for setting affecting how reports are created.
/// </summary>
public interface ICreateIssueReportSettings : IRepositorySettings
{
    /// <summary>
    /// Gets the path of the generated report file.
    /// </summary>
    FilePath OutputFilePath { get; }
}
