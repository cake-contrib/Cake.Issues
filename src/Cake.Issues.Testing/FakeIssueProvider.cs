namespace Cake.Issues.Testing
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Implementation of a <see cref="BaseIssueProvider"/> for use in test cases.
    /// </summary>
    public class FakeIssueProvider : BaseIssueProvider
    {
        private readonly List<IIssue> issues = new List<IIssue>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeIssueProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        public FakeIssueProvider(ICakeLog log)
            : base(log)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeIssueProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        /// <param name="issues">Issues which should be returned by the issue provider.</param>
        public FakeIssueProvider(ICakeLog log, IEnumerable<IIssue> issues)
            : base(log)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            issues.NotNull(nameof(issues));

            // ReSharper disable once PossibleMultipleEnumeration
            this.issues.AddRange(issues);
        }

        /// <summary>
        /// Gets the Cake log instance.
        /// </summary>
        public new ICakeLog Log => base.Log;

        /// <summary>
        /// Gets the settings.
        /// </summary>
        public new IRepositorySettings Settings => base.Settings;

        /// <inheritdoc/>
        public override string ProviderName => "Fake Issue Provider";

        /// <inheritdoc/>
        protected override IEnumerable<IIssue> InternalReadIssues(FileLinkSettings fileLinkSettings)
        {
            return this.issues;
        }
    }
}
