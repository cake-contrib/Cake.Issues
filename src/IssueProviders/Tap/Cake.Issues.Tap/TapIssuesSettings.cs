namespace Cake.Issues.Tap;

using Cake.Core.IO;

/// <summary>
/// Settings for <see cref="TapIssuesAliases"/>.
/// </summary>
public class TapIssuesSettings : BaseMultiFormatIssueProviderSettings<TapIssuesProvider, TapIssuesSettings>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TapIssuesSettings"/> class.
    /// </summary>
    /// <param name="logFilePath">Path to the TAP file.
    /// The TAP file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
    /// <param name="format">Format of the provided log file.</param>
    public TapIssuesSettings(FilePath logFilePath, BaseTapLogFileFormat format)
        : base(logFilePath, format)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TapIssuesSettings"/> class.
    /// </summary>
    /// <param name="logFileContent">Content of the TAP file.
    /// The TAP file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
    /// <param name="format">Format of the provided log file.</param>
    public TapIssuesSettings(byte[] logFileContent, BaseTapLogFileFormat format)
        : base(logFileContent, format)
    {
    }
}