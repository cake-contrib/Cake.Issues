namespace Cake.Issues.MsBuild
{
    using System.Collections.Generic;
    using Core.Diagnostics;
    using IssueProvider;

    /// <summary>
    /// Base class for all MsBuild log file format implementations.
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
            RepositorySettings repositorySettings,
            MsBuildIssuesSettings msBuildIssuesSettings);
    }
}
