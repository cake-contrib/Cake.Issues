namespace Cake.Issues.IssueProvider
{
    using System.Collections.Generic;
    using Core.Diagnostics;

    /// <summary>
    /// Base class for all issue provider implementations.
    /// </summary>
    public abstract class IssueProvider : BaseIssueComponent<RepositorySettings>, IIssueProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IssueProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        protected IssueProvider(ICakeLog log)
            : base(log)
        {
        }

        /// <inheritdoc/>
        public IEnumerable<IIssue> ReadIssues(IssueCommentFormat format)
        {
            this.AssertSettings();

            return this.InternalReadIssues(format);
        }

        /// <summary>
        /// Gets all issues.
        /// Compared to <see cref="ReadIssues"/> it is safe to access Settings from this method.
        /// </summary>
        /// <param name="format">Preferred format of the comments.</param>
        /// <returns>List of issues</returns>
        protected abstract IEnumerable<IIssue> InternalReadIssues(IssueCommentFormat format);
    }
}
