namespace Cake.Issues
{
    using Cake.Core.IO;

    /// <summary>
    /// Base settings for <see cref="BaseMultiFormatIssueProvider{TSettings, TIssueProvider}"/>.
    /// </summary>
    /// <typeparam name="TIssueProvider">Type of the issue provider.</typeparam>
    /// <typeparam name="TSettings">Type of the settings.</typeparam>
    public abstract class BaseMultiFormatIssueProviderSettings<TIssueProvider, TSettings> : IssueProviderSettings
        where TIssueProvider : BaseMultiFormatIssueProvider<TSettings, TIssueProvider>
        where TSettings : BaseMultiFormatIssueProviderSettings<TIssueProvider, TSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMultiFormatIssueProviderSettings{TIssueProvider, TSettings}"/> class
        /// for reading a log file on disk.
        /// </summary>
        /// <param name="logFilePath">Path to the log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided log file.</param>
        protected BaseMultiFormatIssueProviderSettings(FilePath logFilePath, ILogFileFormat<TIssueProvider, TSettings> format)
            : base(logFilePath)
        {
            format.NotNull();

            this.Format = format;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMultiFormatIssueProviderSettings{TIssueProvider, TSettings}"/> class
        /// for a log file content in memory.
        /// </summary>
        /// <param name="logFileContent">Content of the log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided log file.</param>
        protected BaseMultiFormatIssueProviderSettings(byte[] logFileContent, ILogFileFormat<TIssueProvider, TSettings> format)
            : base(logFileContent)
        {
            format.NotNull();

            this.Format = format;
        }

        /// <summary>
        /// Gets the format of the log file.
        /// </summary>
        public ILogFileFormat<TIssueProvider, TSettings> Format { get; }
    }
}
