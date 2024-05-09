namespace Cake.Issues.Reporting.Tests
{
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

    /// <summary>
    /// Implementation of a <see cref="IssueReportFormat"/> for use in test cases.
    /// </summary>
    /// <param name="log">The Cake log instance.</param>
    public class FakeIssueReportFormat(ICakeLog log) : IssueReportFormat(log)
    {
        public new ICakeLog Log => base.Log;

        public new ICreateIssueReportSettings Settings => base.Settings;

        /// <summary>
        /// Gets or sets a value indicating whether the report format should return false during <see cref="Initialize"/>.
        /// </summary>
        public bool ShouldFailOnInitialization { get; set; }

        /// <inheritdoc />
        public override bool Initialize(ICreateIssueReportSettings settings)
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
