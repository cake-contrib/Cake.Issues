﻿namespace Cake.Issues.Testing
{
    using Cake.Core.IO;

    /// <summary>
    /// Implementation of <see cref="FakeIssueProviderSettings"/> for use in test cases.
    /// </summary>
    public class FakeIssueProviderSettings : MultiFormatIssueProviderSettings<FakeIssueProvider, FakeIssueProviderSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeIssueProviderSettings"/> class
        /// for reading a log file on disk.
        /// </summary>
        /// <param name="logFilePath">Path to the log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided log file.</param>
        public FakeIssueProviderSettings(FilePath logFilePath, FakeLogFileFormat format)
            : base(logFilePath, format)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeIssueProviderSettings"/> class
        /// for a log file content in memoy.
        /// </summary>
        /// <param name="logFileContent">Content of the log file.
        /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
        /// <param name="format">Format of the provided log file.</param>
        public FakeIssueProviderSettings(byte[] logFileContent, FakeLogFileFormat format)
            : base(logFileContent, format)
        {
        }
    }
}
