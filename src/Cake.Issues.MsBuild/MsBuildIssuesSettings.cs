namespace Cake.Issues.MsBuild
{
    using System.IO;
    using Core.IO;

    /// <summary>
    /// Settings for <see cref="MsBuildIssuesProvider"/>.
    /// </summary>
    public class MsBuildIssuesSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MsBuildIssuesSettings"/> class.
        /// </summary>
        /// <param name="logFilePath">Path to the the MsBuild log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided MsBuild log file.</param>
        protected MsBuildIssuesSettings(FilePath logFilePath, ILogFileFormat format)
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
        /// Initializes a new instance of the <see cref="MsBuildIssuesSettings"/> class.
        /// </summary>
        /// <param name="logFileContent">Content of the the MsBuild log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided MsBuild log file.</param>
        protected MsBuildIssuesSettings(string logFileContent, ILogFileFormat format)
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
        /// Returns a new instance of the <see cref="MsBuildIssuesSettings"/> class from a log file on disk.
        /// </summary>
        /// <param name="logFilePath">Path to the MsBuild log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided MsBuild log file.</param>
        /// <returns>Instance of the <see cref="MsBuildIssuesSettings"/> class.</returns>
        public static MsBuildIssuesSettings FromFilePath(FilePath logFilePath, ILogFileFormat format)
        {
            return new MsBuildIssuesSettings(logFilePath, format);
        }

        /// <summary>
        /// Returns a new instance of the <see cref="MsBuildIssuesSettings"/> class from the content
        /// of a MsBuild log file.
        /// </summary>
        /// <param name="logFileContent">Content of the MsBuild log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided MsBuild log file.</param>
        /// <returns>Instance of the <see cref="MsBuildIssuesSettings"/> class.</returns>
        public static MsBuildIssuesSettings FromContent(string logFileContent, ILogFileFormat format)
        {
            return new MsBuildIssuesSettings(logFileContent, format);
        }
    }
}
