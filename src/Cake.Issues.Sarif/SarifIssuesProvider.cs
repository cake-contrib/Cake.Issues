namespace Cake.Issues.Sarif
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Provider for issues in SARIF compatible formt.
    /// </summary>
    internal class SarifIssuesProvider : BaseConfigurableIssueProvider<SarifIssuesSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SarifIssuesProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="issueProviderSettings">Settings for the issue provider.</param>
        public SarifIssuesProvider(ICakeLog log, SarifIssuesSettings issueProviderSettings)
            : base(log, issueProviderSettings)
        {
        }

        /// <inheritdoc />
        public override string ProviderName => "SARIF";

        /// <inheritdoc />
        protected override IEnumerable<IIssue> InternalReadIssues()
        {
            // TODO Implement
            return new List<IIssue>();
        }
    }
}
