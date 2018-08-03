namespace Cake.Issues
{
    using System.Collections.Generic;
    using Core.Diagnostics;

    /// <summary>
    /// Base class for all log file format implementations.
    /// </summary>
    /// <typeparam name="TIssueProvider">Type of the issue provider.</typeparam>
    /// <typeparam name="TSettings">Type of the settings.</typeparam>
    public abstract class LogFileFormat<TIssueProvider, TSettings> : ILogFileFormat<TIssueProvider, TSettings>
        where TIssueProvider : IIssueProvider
        where TSettings : MultiFormatIssueProviderSettings<TIssueProvider, TSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogFileFormat{TIssueProvider, TSettings}"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        protected LogFileFormat(ICakeLog log)
        {
            log.NotNull(nameof(log));

            this.Log = log;
        }

        /// <summary>
        /// Gets the Cake log instance.
        /// </summary>
        protected ICakeLog Log { get; private set; }

        /// <inheritdoc/>
        public abstract IEnumerable<IIssue> ReadIssues(
            TIssueProvider issueProvider,
            RepositorySettings repositorySettings,
            TSettings issueProviderSettings);
    }
}
