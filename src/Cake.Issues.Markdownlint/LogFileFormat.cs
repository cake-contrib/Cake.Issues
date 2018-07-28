namespace Cake.Issues.Markdownlint
{
    using System.Collections.Generic;
    using Core.Diagnostics;

    /// <summary>
    /// Base class for all Markdownlint log file format implementations.
    /// </summary>
    public abstract class LogFileFormat : ILogFileFormat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogFileFormat"/> class.
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
            MarkdownlintIssuesProvider issueProvider,
            RepositorySettings repositorySettings,
            MarkdownlintIssuesSettings markdownlintIssuesSettings);
    }
}
