namespace Cake.Issues.DocFx
{
    using System.IO;
    using Core.IO;

    /// <summary>
    /// Settings for <see cref="DocFxIssuesProvider"/>.
    /// </summary>
    public class DocFxIssuesSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocFxIssuesSettings"/> class.
        /// </summary>
        /// <param name="logFilePath">Path to the the DocFx log file.</param>
        /// <param name="docRootPath">Path to the root directory of the DocFx project.
        /// Either the full path or the path relative to the repository root.</param>
        protected DocFxIssuesSettings(FilePath logFilePath, DirectoryPath docRootPath)
        {
            logFilePath.NotNull(nameof(logFilePath));
            docRootPath.NotNull(nameof(docRootPath));

            this.DocRootPath = docRootPath;

            using (var stream = new FileStream(logFilePath.FullPath, FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(stream))
                {
                    this.LogFileContent = sr.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocFxIssuesSettings"/> class.
        /// </summary>
        /// <param name="logFileContent">Content of the the DocFx log file.</param>
        /// <param name="docRootPath">Path to the root directory of the DocFx project.
        /// Either the full path or the path relative to the repository root.</param>
        protected DocFxIssuesSettings(string logFileContent, DirectoryPath docRootPath)
        {
            logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));
            docRootPath.NotNull(nameof(docRootPath));

            this.LogFileContent = logFileContent;
            this.DocRootPath = docRootPath;
        }

        /// <summary>
        /// Gets the content of the log file.
        /// </summary>
        public string LogFileContent { get; private set; }

        /// <summary>
        /// Gets the path to the root directory of the DocFx project.
        /// Either the full path or the path relative to the repository root.
        /// </summary>
        public DirectoryPath DocRootPath { get; private set; }

        /// <summary>
        /// Returns a new instance of the <see cref="DocFxIssuesSettings"/> class from a log file on disk.
        /// </summary>
        /// <param name="logFilePath">Path to the DocFx log file.</param>
        /// <param name="docRootPath">Path to the root directory of the DocFx project.
        /// Either the full path or the path relative to the repository root.</param>
        /// <returns>Instance of the <see cref="DocFxIssuesSettings"/> class.</returns>
        public static DocFxIssuesSettings FromFilePath(FilePath logFilePath, DirectoryPath docRootPath)
        {
            return new DocFxIssuesSettings(logFilePath, docRootPath);
        }

        /// <summary>
        /// Returns a new instance of the <see cref="DocFxIssuesSettings"/> class from the content
        /// of a DocFx log file.
        /// </summary>
        /// <param name="logFileContent">Content of the DocFx log file.</param>
        /// <param name="docRootPath">Path to the root directory of the DocFx project.
        /// Either the full path or the path relative to the repository root.</param>
        /// <returns>Instance of the <see cref="DocFxIssuesSettings"/> class.</returns>
        public static DocFxIssuesSettings FromContent(string logFileContent, DirectoryPath docRootPath)
        {
            return new DocFxIssuesSettings(logFileContent, docRootPath);
        }
    }
}
