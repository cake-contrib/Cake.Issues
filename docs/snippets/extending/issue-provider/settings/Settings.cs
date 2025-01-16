/// <summary>
/// Settings for my issue provider.
/// </summary>
public class MyIssuesSettings : IssueProviderSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MyIssuesSettings"/> class
    /// for reading a log file on disk.
    /// </summary>
    /// <param name="logFilePath">Path to the log file.</param>
    public MyIssuesSettings(FilePath logFilePath)
        : base(logFilePath)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MyIssuesSettings"/> class
    /// for a log file content in memory.
    /// </summary>
    /// <param name="logFileContent">Content of the log file.</param>
    public MyIssuesSettings(byte[] logFileContent)
        : base(logFileContent)
    {
    }

    // Add additional settings for the issue provider here.
}
