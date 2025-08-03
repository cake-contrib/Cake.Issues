namespace Cake.Issues.JUnit;

using Cake.Core.IO;

/// <summary>
/// Settings for <see cref="JUnitIssuesAliases"/>.
/// </summary>
public class JUnitIssuesSettings : IssueProviderSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JUnitIssuesSettings"/> class
    /// for reading a JUnit log file on disk.
    /// </summary>
    /// <param name="logFilePath">Path to the JUnit log file.</param>
    public JUnitIssuesSettings(FilePath logFilePath)
        : base(logFilePath)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JUnitIssuesSettings"/> class
    /// for a JUnit log file content in memory.
    /// </summary>
    /// <param name="logFileContent">Content of the JUnit log file.</param>
    public JUnitIssuesSettings(byte[] logFileContent)
        : base(logFileContent)
    {
    }
}