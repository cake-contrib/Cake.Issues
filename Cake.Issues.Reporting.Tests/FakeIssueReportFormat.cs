namespace Cake.Issues.Reporting.Tests
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

    /// <summary>
    /// Implementation of a <see cref="IssueReportFormat"/> for use in test cases.
    /// </summary>
    public class FakeIssueReportFormat : IssueReportFormat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeIssueReportFormat"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        public FakeIssueReportFormat(ICakeLog log)
            : base(log)
        {
        }

        public new ICakeLog Log => base.Log;

        public new RepositorySettings Settings => base.Settings;

        /// <summary>
        /// Gets or sets a value indicating whether the report format should return false during <see cref="Initialize"/>.
        /// </summary>
        public bool ShouldFailOnInitialization { get; set; } = false;

        /// <inheritdoc />
        public override bool Initialize(CreateIssueReportSettings settings)
        {
            var result = base.Initialize(settings);

            return result && !this.ShouldFailOnInitialization;
        }

        protected override FilePath InternalCreateReport(IEnumerable<IIssue> issues)
        {
            return new FilePath(@"c:\report.html");
        }
    }
}
