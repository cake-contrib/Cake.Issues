namespace Cake.Issues.Sarif;

using Cake.Core.IO;

/// <summary>
/// Settings for <see cref="SarifIssuesAliases"/>.
/// </summary>
public class SarifIssuesSettings : IssueProviderSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SarifIssuesSettings"/> class.
    /// </summary>
    /// <param name="logFilePath">Path to the Sarif file.</param>
    public SarifIssuesSettings(FilePath logFilePath)
        : base(logFilePath)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SarifIssuesSettings"/> class.
    /// </summary>
    /// <param name="logFileContent">Content of the SARIF file.</param>
    public SarifIssuesSettings(byte[] logFileContent)
        : base(logFileContent)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether the tool name reported in the SARIF log or a fixed value should be
    /// used as issue provider name.
    /// Default value is <c>true</c>.
    /// </summary>
    public bool UseToolNameAsIssueProviderName { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether issues containing supressions should be ignored.
    /// Default value is <c>true</c>.
    /// </summary>
    public bool IgnoreSuppressedIssues { get; set; } = true;
}