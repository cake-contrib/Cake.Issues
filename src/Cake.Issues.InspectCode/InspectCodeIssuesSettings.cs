namespace Cake.Issues.InspectCode
{
    using System.IO;
    using Core.IO;

    /// <summary>
    /// Settings for <see cref="InspectCodeIssuesProvider"/>.
    /// </summary>
    public class InspectCodeIssuesSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InspectCodeIssuesSettings"/> class.
        /// </summary>
        /// <param name="logFilePath">Path to the the Inspect Code log file.</param>
        protected InspectCodeIssuesSettings(FilePath logFilePath)
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
        /// Initializes a new instance of the <see cref="InspectCodeIssuesSettings"/> class.
        /// </summary>
        /// <param name="logFileContent">Content of the the Inspect Code log file.</param>
        protected InspectCodeIssuesSettings(string logFileContent)
        {
            logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));

            this.LogFileContent = logFileContent;
        }

        /// <summary>
        /// Gets the content of the log file.
        /// </summary>
        public string LogFileContent { get; private set; }

        /// <summary>
        /// Returns a new instance of the <see cref="InspectCodeIssuesSettings"/> class from a log file on disk.
        /// </summary>
        /// <param name="logFilePath">Path to the JetBrains Inspect Code log file.</param>
        /// <returns>Instance of the <see cref="InspectCodeIssuesSettings"/> class.</returns>
        public static InspectCodeIssuesSettings FromFilePath(FilePath logFilePath)
        {
            return new InspectCodeIssuesSettings(logFilePath);
        }

        /// <summary>
        /// Returns a new instance of the <see cref="InspectCodeIssuesSettings"/> class from the content
        /// of a JetBrains Inspect Code log file.
        /// </summary>
        /// <param name="logFileContent">Content of the JetBrains Inspect Code log file.</param>
        /// <returns>Instance of the <see cref="InspectCodeIssuesSettings"/> class.</returns>
        public static InspectCodeIssuesSettings FromContent(string logFileContent)
        {
            return new InspectCodeIssuesSettings(logFileContent);
        }
    }
}
