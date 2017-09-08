namespace Cake.Issues.Markdownlint
{
    using System.IO;
    using Core.IO;

    /// <summary>
    /// Settings for <see cref="MarkdownlintIssuesProvider"/>.
    /// </summary>
    public class MarkdownlintIssuesSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownlintIssuesSettings"/> class.
        /// </summary>
        /// <param name="logFilePath">Path to the the Markdownlint log file.</param>
        protected MarkdownlintIssuesSettings(FilePath logFilePath)
        {
            logFilePath.NotNull(nameof(logFilePath));

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
        /// <param name="logFileContent">Content of the the Markdownlint log file.</param>
        protected MarkdownlintIssuesSettings(string logFileContent)
        {
            logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));

            this.LogFileContent = logFileContent;
        }

        /// <summary>
        /// Gets the content of the log file.
        /// </summary>
        public string LogFileContent { get; private set; }

        /// <summary>
        /// Returns a new instance of the <see cref="MarkdownlintIssuesSettings"/> class from a log file on disk.
        /// </summary>
        /// <param name="logFilePath">Path to the Markdownlint log file.</param>
        /// <returns>Instance of the <see cref="MarkdownlintIssuesSettings"/> class.</returns>
        public static MarkdownlintIssuesSettings FromFilePath(FilePath logFilePath)
        {
            return new MarkdownlintIssuesSettings(logFilePath);
        }

        /// <summary>
        /// Returns a new instance of the <see cref="MarkdownlintIssuesSettings"/> class from the content
        /// of a Markdownlint log file.
        /// </summary>
        /// <param name="logFileContent">Content of the Markdownlint log file.</param>
        /// <returns>Instance of the <see cref="MarkdownlintIssuesSettings"/> class.</returns>
        public static MarkdownlintIssuesSettings FromContent(string logFileContent)
        {
            return new MarkdownlintIssuesSettings(logFileContent);
        }
    }
}
