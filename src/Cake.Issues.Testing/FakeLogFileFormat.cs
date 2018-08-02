namespace Cake.Issues.Testing
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Implementation of <see cref="LogFileFormat{TIssueProvider, TSettings}"/> for use in test cases.
    /// </summary>
    public class FakeLogFileFormat : LogFileFormat<FakeIssueProvider, FakeIssueProviderSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeLogFileFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        public FakeLogFileFormat(ICakeLog log)
            : base(log)
        {
        }

        /// <summary>
        /// Gets the Cake log instance.
        /// </summary>
        public new ICakeLog Log => base.Log;

        /// <inheritdoc/>
        public override IEnumerable<IIssue> ReadIssues(
            FakeIssueProvider issueProvider,
            RepositorySettings repositorySettings,
            FakeIssueProviderSettings issueProviderSettings)
        {
            return new List<IIssue>();
        }
    }
}
