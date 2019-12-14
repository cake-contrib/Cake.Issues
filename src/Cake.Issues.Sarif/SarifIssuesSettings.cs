namespace Cake.Issues.Sarif
{
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
    }
}
