namespace Cake.Issues
{
    using System.IO;
    using System.Text;
    using Cake.Core.IO;

    /// <summary>
    /// Base settings for <see cref="IIssueProvider"/>.
    /// </summary>
    /// <typeparam name="TIssueProvider">Type of the issue provider.</typeparam>
    /// <typeparam name="TSettings">Type of the settings.</typeparam>
    public class MultiFormatIssueProviderSettings<TIssueProvider, TSettings>
        where TIssueProvider : IIssueProvider
        where TSettings : MultiFormatIssueProviderSettings<TIssueProvider, TSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiFormatIssueProviderSettings{TIssueProvider, TSettings}"/> class
        /// for reading a log file on disk.
        /// </summary>
        /// <param name="logFilePath">Path to the log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided log file.</param>
        public MultiFormatIssueProviderSettings(FilePath logFilePath, ILogFileFormat<TIssueProvider, TSettings> format)
        {
            logFilePath.NotNull(nameof(logFilePath));
            format.NotNull(nameof(format));

            this.Format = format;
            this.LogFileContent = File.ReadAllBytes(logFilePath.FullPath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiFormatIssueProviderSettings{TIssueProvider, TSettings}"/> class
        /// for a log file content in memoy.
        /// </summary>
        /// <param name="logFileContent">Content of the log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided log file.</param>
        public MultiFormatIssueProviderSettings(byte[] logFileContent, ILogFileFormat<TIssueProvider, TSettings> format)
        {
            logFileContent.NotNullOrEmpty(nameof(logFileContent));
            format.NotNull(nameof(format));

            this.LogFileContent = logFileContent;
            this.Format = format;
        }

        /// <summary>
        /// Gets the content of the log file.
        /// </summary>
        public byte[] LogFileContent { get; private set; }

        /// <summary>
        /// Gets the format of the log file.
        /// </summary>
        public ILogFileFormat<TIssueProvider, TSettings> Format { get; private set; }
    }
}
