namespace Cake.Issues.Markdownlint
{
    using System.IO;
    using Core.IO;

    /// <summary>
    /// Settings for <see cref="MarkdownlintIssuesAliases"/>.
    /// </summary>
    public class MarkdownlintIssuesSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownlintIssuesSettings"/> class.
        /// </summary>
        /// <param name="logFilePath">Path to the the Markdownlint log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided MsBuild log file.</param>
        protected MarkdownlintIssuesSettings(FilePath logFilePath, ILogFileFormat format)
        {
            logFilePath.NotNull(nameof(logFilePath));
            format.NotNull(nameof(format));

            this.Format = format;

            using (var stream = new FileStream(logFilePath.FullPath, FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(stream))
                {
                    this.LogFileContent = sr.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownlintIssuesSettings"/> class.
        /// </summary>
        /// <param name="logFileContent">Content of the the Markdownlint log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided MsBuild log file.</param>
        protected MarkdownlintIssuesSettings(string logFileContent, ILogFileFormat format)
        {
            logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));
            format.NotNull(nameof(format));

            this.LogFileContent = logFileContent;
            this.Format = format;
        }

        /// <summary>
        /// Gets the format of the Markdownlint log file.
        /// </summary>
        public ILogFileFormat Format { get; private set; }

        /// <summary>
        /// Gets the content of the log file.
        /// </summary>
        public string LogFileContent { get; private set; }

        /// <summary>
        /// Returns a new instance of the <see cref="MarkdownlintIssuesSettings"/> class from a log file on disk.
        /// </summary>
        /// <param name="logFilePath">Path to the Markdownlint log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided MsBuild log file.</param>
        /// <returns>Instance of the <see cref="MarkdownlintIssuesSettings"/> class.</returns>
        public static MarkdownlintIssuesSettings FromFilePath(FilePath logFilePath, ILogFileFormat format)
        {
            return new MarkdownlintIssuesSettings(logFilePath, format);
        }

        /// <summary>
        /// Returns a new instance of the <see cref="MarkdownlintIssuesSettings"/> class from the content
        /// of a Markdownlint log file.
        /// </summary>
        /// <param name="logFileContent">Content of the Markdownlint log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided MsBuild log file.</param>
        /// <returns>Instance of the <see cref="MarkdownlintIssuesSettings"/> class.</returns>
        public static MarkdownlintIssuesSettings FromContent(string logFileContent, ILogFileFormat format)
        {
            return new MarkdownlintIssuesSettings(logFileContent, format);
        }
    }
}
