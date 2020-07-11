namespace Cake.Issues
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Base class for all issue provider implementations.
    /// </summary>
    public abstract class BaseIssueProvider : BaseIssueComponent<IRepositorySettings>, IIssueProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseIssueProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        protected BaseIssueProvider(ICakeLog log)
            : base(log)
        {
        }

        /// <inheritdoc/>
        public abstract string ProviderName { get; }

        /// <inheritdoc/>
        public IEnumerable<IIssue> ReadIssues()
        {
            this.AssertInitialized();

            return this.InternalReadIssues();
        }

        /// <summary>
        /// Gets all issues.
        /// Compared to <see cref="ReadIssues"/> it is safe to access Settings from this method.
        /// </summary>
        /// <returns>List of issues.</returns>
        protected abstract IEnumerable<IIssue> InternalReadIssues();
    }
}
