namespace Cake.Issues.Markdownlint
{
    using Core.IO;

    /// <summary>
    /// Settings for <see cref="MarkdownlintIssuesAliases"/>.
    /// </summary>
    public class MarkdownlintIssuesSettings : BaseMultiFormatIssueProviderSettings<MarkdownlintIssuesProvider, MarkdownlintIssuesSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownlintIssuesSettings"/> class
        /// for reading a log file on disk.
        /// </summary>
        /// <param name="logFilePath">Path to the log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided log file.</param>
        public MarkdownlintIssuesSettings(FilePath logFilePath, BaseMarkdownlintLogFileFormat format)
            : base(logFilePath, format)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownlintIssuesSettings"/> class
        /// for a log file content in memory.
        /// </summary>
        /// <param name="logFileContent">Content of the log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided log file.</param>
        public MarkdownlintIssuesSettings(byte[] logFileContent, BaseMarkdownlintLogFileFormat format)
            : base(logFileContent, format)
        {
        }
    }
}
