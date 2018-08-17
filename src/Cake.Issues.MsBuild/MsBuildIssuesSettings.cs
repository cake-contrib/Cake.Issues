namespace Cake.Issues.MsBuild
{
    using Cake.Core.IO;

    /// <summary>
    /// Settings for <see cref="MsBuildIssuesAliases"/>.
    /// </summary>
    public class MsBuildIssuesSettings : BaseMultiFormatIssueProviderSettings<MsBuildIssuesProvider, MsBuildIssuesSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MsBuildIssuesSettings"/> class
        /// for reading a log file on disk.
        /// </summary>
        /// <param name="logFilePath">Path to the MSBuild log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided MSBuild log file.</param>
        public MsBuildIssuesSettings(FilePath logFilePath, BaseMsBuildLogFileFormat format)
            : base(logFilePath, format)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsBuildIssuesSettings"/> class
        /// for a log file content in memory.
        /// </summary>
        /// <param name="logFileContent">Content of the MSBuild log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided MSBuild log file.</param>
        public MsBuildIssuesSettings(byte[] logFileContent, BaseMsBuildLogFileFormat format)
            : base(logFileContent, format)
        {
        }
    }
}
