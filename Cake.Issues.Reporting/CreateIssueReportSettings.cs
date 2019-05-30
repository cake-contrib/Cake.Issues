namespace Cake.Issues.Reporting
{
    using Cake.Core.IO;

    /// <summary>
    /// Setting affecting how reports are created.
    /// </summary>
    public class CreateIssueReportSettings : RepositorySettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateIssueReportSettings"/> class.
        /// </summary>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        /// <param name="outputFilePath">Path of the generated report file.</param>
        public CreateIssueReportSettings(DirectoryPath repositoryRoot, FilePath outputFilePath)
            : base(repositoryRoot)
        {
            outputFilePath.NotNull(nameof(outputFilePath));

            this.OutputFilePath = outputFilePath;
        }

        /// <summary>
        /// Gets the path of the generated report file.
        /// </summary>
        public FilePath OutputFilePath { get; private set; }
    }
}
