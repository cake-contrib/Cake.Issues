namespace Cake.Issues.Reporting
{
    using Cake.Core.IO;

    /// <summary>
    /// Setting affecting how reports are created which are built passing issue providers.
    /// </summary>
    public class CreateIssueReportFromIssueProviderSettings : ReadIssuesSettings, ICreateIssueReportFromIssueProviderSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateIssueReportFromIssueProviderSettings"/> class.
        /// </summary>
        /// <param name="repositoryRoot">Root path of the repository.</param>
        /// <param name="outputFilePath">Path of the generated report file.</param>
        public CreateIssueReportFromIssueProviderSettings(DirectoryPath repositoryRoot, FilePath outputFilePath)
            : base(repositoryRoot)
        {
            outputFilePath.NotNull();

            this.OutputFilePath = outputFilePath;
        }

        /// <inheritdoc/>
        public FilePath OutputFilePath { get; }
    }
}
