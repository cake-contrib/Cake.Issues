namespace Cake.Issues.EsLint
{
    using Cake.Core.IO;

    /// <summary>
    /// Settings for <see cref="EsLintIssuesAliases"/>.
    /// </summary>
    public class EsLintIssuesSettings : BaseMultiFormatIssueProviderSettings<EsLintIssuesProvider, EsLintIssuesSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EsLintIssuesSettings"/> class
        /// for reading a log file on disk.
        /// </summary>
        /// <param name="logFilePath">Path to the ESLint log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided ESLint log file.</param>
        public EsLintIssuesSettings(FilePath logFilePath, BaseEsLintLogFileFormat format)
            : base(logFilePath, format)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EsLintIssuesSettings"/> class
        /// for a log file content in memory.
        /// </summary>
        /// <param name="logFileContent">Content of the ESLint log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided ESLint log file.</param>
        public EsLintIssuesSettings(byte[] logFileContent, BaseEsLintLogFileFormat format)
            : base(logFileContent, format)
        {
        }
    }
}
