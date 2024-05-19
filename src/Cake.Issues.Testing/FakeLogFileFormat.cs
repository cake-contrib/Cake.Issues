namespace Cake.Issues.Testing
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Implementation of <see cref="BaseLogFileFormat{TIssueProvider, TSettings}"/> for use in test cases.
    /// </summary>
    public class FakeLogFileFormat : BaseLogFileFormat<FakeMultiFormatIssueProvider, FakeMultiFormatIssueProviderSettings>
    {
        private readonly List<IIssue> issues = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeLogFileFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        public FakeLogFileFormat(ICakeLog log)
            : base(log)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeLogFileFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        /// <param name="issues">Issues which should be returned by the log file format.</param>
        public FakeLogFileFormat(ICakeLog log, IEnumerable<IIssue> issues)
            : base(log)
        {
            issues.NotNull();

            this.issues.AddRange(issues);
        }

        /// <summary>
        /// Gets the Cake log instance.
        /// </summary>
        public new ICakeLog Log => base.Log;

        /// <inheritdoc/>
        public override IEnumerable<IIssue> ReadIssues(
            FakeMultiFormatIssueProvider issueProvider,
            IRepositorySettings repositorySettings,
            FakeMultiFormatIssueProviderSettings issueProviderSettings) => this.issues;
    }
}
