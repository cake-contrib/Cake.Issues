namespace Cake.Issues.CodeClimate;

using Cake.Core.IO;

/// <summary>
/// Settings for <see cref="CodeClimateIssuesAliases"/>.
/// </summary>
public class CodeClimateIssuesSettings : IssueProviderSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CodeClimateIssuesSettings"/> class.
    /// </summary>
    /// <param name="logFilePath">Path to the CodeClimate file.</param>
    public CodeClimateIssuesSettings(FilePath logFilePath)
        : base(logFilePath)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeClimateIssuesSettings"/> class.
    /// </summary>
    /// <param name="logFileContent">Content of the CodeClimate file.</param>
    public CodeClimateIssuesSettings(byte[] logFileContent)
        : base(logFileContent)
    {
    }
}