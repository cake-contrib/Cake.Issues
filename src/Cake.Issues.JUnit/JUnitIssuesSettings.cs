namespace Cake.Issues.JUnit;

using System.Text;
using Cake.Core.IO;

/// <summary>
/// Settings for <see cref="JUnitIssuesProvider"/>.
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

    /// <summary>
    /// Returns a new instance of the <see cref="JUnitIssuesSettings"/> class from a log file on disk.
    /// </summary>
    /// <param name="logFilePath">Path to the JUnit log file.</param>
    /// <returns>Instance of the <see cref="JUnitIssuesSettings"/> class.</returns>
    public static JUnitIssuesSettings FromFilePath(FilePath logFilePath)
    {
        logFilePath.NotNull();

        return new JUnitIssuesSettings(logFilePath);
    }

    /// <summary>
    /// Returns a new instance of the <see cref="JUnitIssuesSettings"/> class from log file content.
    /// </summary>
    /// <param name="logFileContent">Content of the JUnit log file.</param>
    /// <returns>Instance of the <see cref="JUnitIssuesSettings"/> class.</returns>
    public static JUnitIssuesSettings FromContent(string logFileContent)
    {
        logFileContent.NotNullOrWhiteSpace();

        return new JUnitIssuesSettings(Encoding.UTF8.GetBytes(logFileContent));
    }

    /// <summary>
    /// Returns a new instance of the <see cref="JUnitIssuesSettings"/> class from log file content.
    /// </summary>
    /// <param name="logFileContent">Content of the JUnit log file.</param>
    /// <returns>Instance of the <see cref="JUnitIssuesSettings"/> class.</returns>
    public static JUnitIssuesSettings FromContent(byte[] logFileContent)
    {
        logFileContent.NotNull();

        return new JUnitIssuesSettings(logFileContent);
    }
}