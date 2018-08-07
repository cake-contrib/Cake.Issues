namespace Cake.Issues.InspectCode
{
    using Core.IO;

    /// <summary>
    /// Settings for <see cref="InspectCodeIssuesAliases"/>.
    /// </summary>
    public class InspectCodeIssuesSettings : IssueProviderSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InspectCodeIssuesSettings"/> class.
        /// </summary>
        /// <param name="logFilePath">Path to the the Inspect Code log file.</param>
        public InspectCodeIssuesSettings(FilePath logFilePath)
            : base(logFilePath)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InspectCodeIssuesSettings"/> class.
        /// </summary>
        /// <param name="logFileContent">Content of the the Inspect Code log file.</param>
        public InspectCodeIssuesSettings(byte[] logFileContent)
            : base(logFileContent)
        {
        }
    }
}
