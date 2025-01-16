/// <summary>
/// Settings for my issue provider.
/// </summary>
public class MyIssuesSettings
    : BaseMultiFormatIssueProviderSettings<MyIssuesProvider, MyIssuesSettings>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MyIssuesSettings"/> class
    /// for reading a log file on disk.
    /// </summary>
    /// <param name="logFilePath">Path to the log file.
    /// The log file needs to be in the format as defined by the
    /// <paramref name="format"/> parameter.</param>
    /// <param name="format">Format of the provided log file.</param>
    public MyIssuesSettings(FilePath logFilePath, MyLogFileFormat format)
        : base(logFilePath, format)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MyIssuesSettings"/> class
    /// for a log file content in memory.
    /// </summary>
    /// <param name="logFileContent">Content of the log file.
    /// The log file needs to be in the format as defined by the
    /// <paramref name="format"/> parameter.</param>
    /// <param name="format">Format of the provided log file.</param>
    public MyIssuesSettings(byte[] logFileContent, MyLogFileFormat format)
        : base(logFileContent, format)
    {
    }
}
