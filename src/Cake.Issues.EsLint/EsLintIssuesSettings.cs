namespace Cake.Issues.EsLint
{
    using System.IO;
    using Core.IO;

    /// <summary>
    /// Settings for <see cref="EsLintIssuesAliases"/>.
    /// </summary>
    public class EsLintIssuesSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EsLintIssuesSettings"/> class.
        /// </summary>
        /// <param name="logFilePath">Path to the the ESLint log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided ESLint log file.</param>
        protected EsLintIssuesSettings(FilePath logFilePath, ILogFileFormat format)
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
        /// Initializes a new instance of the <see cref="EsLintIssuesSettings"/> class.
        /// </summary>
        /// <param name="logFileContent">Content of the the ESLint log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided ESLint log file.</param>
        protected EsLintIssuesSettings(string logFileContent, ILogFileFormat format)
        {
            logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));
            format.NotNull(nameof(format));

            this.LogFileContent = logFileContent;
            this.Format = format;
        }

        /// <summary>
        /// Gets the format of the MsBuild log file.
        /// </summary>
        public ILogFileFormat Format { get; private set; }

        /// <summary>
        /// Gets the content of the log file.
        /// </summary>
        public string LogFileContent { get; private set; }

        /// <summary>
        /// Returns a new instance of the <see cref="EsLintIssuesSettings"/> class from a log file on disk.
        /// </summary>
        /// <param name="logFilePath">Path to the ESLint log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided ESLint log file.</param>
        /// <returns>Instance of the <see cref="EsLintIssuesSettings"/> class.</returns>
        public static EsLintIssuesSettings FromFilePath(FilePath logFilePath, ILogFileFormat format)
        {
            return new EsLintIssuesSettings(logFilePath, format);
        }

        /// <summary>
        /// Returns a new instance of the <see cref="EsLintIssuesSettings"/> class from the content
        /// of a ESLint log file.
        /// </summary>
        /// <param name="logFileContent">Content of the ESLint log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided ESLint log file.</param>
        /// <returns>Instance of the <see cref="EsLintIssuesSettings"/> class.</returns>
        public static EsLintIssuesSettings FromContent(string logFileContent, ILogFileFormat format)
        {
            return new EsLintIssuesSettings(logFileContent, format);
        }
    }
}
