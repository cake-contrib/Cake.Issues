namespace Cake.Issues.GitRepository
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Provider for issues in Git repositories.
    /// </summary>
    internal class GitRepositoryIssuesProvider : BaseIssueProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GitRepositoryIssuesProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="issueProviderSettings">Settings for the issue provider.</param>
        public GitRepositoryIssuesProvider(ICakeLog log, GitRepositoryIssuesSettings issueProviderSettings)
            : base(log)
        {
            issueProviderSettings.NotNull(nameof(issueProviderSettings));

            this.IssueProviderSettings = issueProviderSettings;
        }

        /// <inheritdoc />
        public override string ProviderName => "Git Repository";

        /// <summary>
        /// Gets the settings for the issue provider.
        /// </summary>
        protected GitRepositoryIssuesSettings IssueProviderSettings { get; private set; }

        /// <inheritdoc />
        protected override IEnumerable<IIssue> InternalReadIssues(IssueCommentFormat format)
        {
            return new List<IIssue>();
        }
    }
}