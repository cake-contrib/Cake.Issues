namespace Cake.Issues
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Base class for all log file format implementations.
    /// </summary>
    /// <typeparam name="TIssueProvider">Type of the issue provider.</typeparam>
    /// <typeparam name="TSettings">Type of the settings.</typeparam>
    public abstract class BaseLogFileFormat<TIssueProvider, TSettings> : ILogFileFormat<TIssueProvider, TSettings>
        where TIssueProvider : BaseMultiFormatIssueProvider<TSettings, TIssueProvider>
        where TSettings : BaseMultiFormatIssueProviderSettings<TIssueProvider, TSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLogFileFormat{TIssueProvider, TSettings}"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        protected BaseLogFileFormat(ICakeLog log)
        {
            log.NotNull();

            this.Log = log;
        }

        /// <summary>
        /// Gets the Cake log instance.
        /// </summary>
        protected ICakeLog Log { get; }

        /// <inheritdoc/>
        public abstract IEnumerable<IIssue> ReadIssues(
            TIssueProvider issueProvider,
            IRepositorySettings repositorySettings,
            TSettings issueProviderSettings);
    }
}
