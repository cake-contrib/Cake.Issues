namespace Cake.Issues.JUnit;

using Cake.Core.IO;

/// <summary>
/// Settings for <see cref="JUnitIssuesAliases"/>.
/// </summary>
public class JUnitIssuesSettings : BaseMultiFormatIssueProviderSettings<JUnitIssuesProvider, JUnitIssuesSettings>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JUnitIssuesSettings"/> class
    /// for reading a JUnit log file on disk.
    /// </summary>
    /// <param name="logFilePath">Path to the JUnit log file.
    /// The JUnit file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
    /// <param name="format">Format of the provided JUnit file.</param>
    public JUnitIssuesSettings(FilePath logFilePath, BaseJUnitLogFileFormat format)
        : base(logFilePath, format)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JUnitIssuesSettings"/> class
    /// for a JUnit log file content in memory.
    /// </summary>
    /// <param name="logFileContent">Content of the JUnit log file.
    /// The JUnit file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
    /// <param name="format">Format of the provided JUnit file.</param>
    public JUnitIssuesSettings(byte[] logFileContent, BaseJUnitLogFileFormat format)
        : base(logFileContent, format)
    {
    }
}