namespace Cake.Issues.DocFx
{
    using Cake.Core.IO;

    /// <summary>
    /// Settings for <see cref="DocFxIssuesAliases"/>.
    /// </summary>
    public class DocFxIssuesSettings : IssueProviderSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocFxIssuesSettings"/> class.
        /// </summary>
        /// <param name="logFilePath">Path to the DocFx log file.</param>
        /// <param name="docRootPath">Path to the root directory of the DocFx project.
        /// Either the full path or the path relative to the repository root.</param>
        public DocFxIssuesSettings(FilePath logFilePath, DirectoryPath docRootPath)
            : base(logFilePath)
        {
            docRootPath.NotNull(nameof(docRootPath));

            this.DocRootPath = docRootPath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocFxIssuesSettings"/> class.
        /// </summary>
        /// <param name="logFileContent">Content of the DocFx log file.</param>
        /// <param name="docRootPath">Path to the root directory of the DocFx project.
        /// Either the full path or the path relative to the repository root.</param>
        public DocFxIssuesSettings(byte[] logFileContent, DirectoryPath docRootPath)
            : base(logFileContent)
        {
            docRootPath.NotNull(nameof(docRootPath));

            this.DocRootPath = docRootPath;
        }

        /// <summary>
        /// Gets the path to the root directory of the DocFx project.
        /// Either the full path or the path relative to the repository root.
        /// </summary>
        public DirectoryPath DocRootPath { get; private set; }
    }
}
