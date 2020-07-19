namespace Cake.Issues.Testing
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Implementation of a <see cref="BaseConfigurableIssueProvider{T}"/> for use in test cases.
    /// </summary>
    public class FakeConfigurableIssueProvider
        : BaseConfigurableIssueProvider<IssueProviderSettings>
    {
        private readonly List<IIssue> issues = new List<IIssue>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeConfigurableIssueProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        /// <param name="settings">The issue provider settings.</param>
        public FakeConfigurableIssueProvider(ICakeLog log, IssueProviderSettings settings)
            : base(log, settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeConfigurableIssueProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log instance.</param>
        /// <param name="settings">The issue provider settings.</param>
        /// <param name="issues">Issues which should be returned by the issue provider.</param>
        public FakeConfigurableIssueProvider(
            ICakeLog log,
            IssueProviderSettings settings,
            IEnumerable<IIssue> issues)
            : base(log, settings)
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
        /// Gets the repository settings.
        /// </summary>
        public IRepositorySettings RepositorySettings => this.Settings;

        /// <summary>
        /// Gets the issue provider settings.
        /// </summary>
        public new IssueProviderSettings IssueProviderSettings => base.IssueProviderSettings;

        /// <inheritdoc/>
        public override string ProviderName => "Fake Issue Provider";

        /// <inheritdoc/>
        protected override IEnumerable<IIssue> InternalReadIssues(FileLinkSettings fileLinkSettings)
        {
            return this.issues;
        }
    }
}
