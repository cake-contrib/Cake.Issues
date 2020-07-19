namespace Cake.Issues.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Core.Diagnostics;
    using Cake.Testing;

    /// <summary>
    /// Base class for test fixtures for testing issue providers inherited from <see cref="BaseIssueProvider"/>.
    /// </summary>
    /// <typeparam name="T">Type of issue provider.</typeparam>
    public abstract class BaseIssueProviderFixture<T>
        where T : BaseIssueProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseIssueProviderFixture{T}"/> class.
        /// </summary>
        protected BaseIssueProviderFixture()
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };
            this.ReadIssuesSettings = new ReadIssuesSettings(@"c:\repo");
        }

        /// <summary>
        /// Gets or sets the Cake logging instance.
        /// </summary>
        public FakeLog Log { get; set; }

        /// <summary>
        /// Gets or sets the repository settings.
        /// </summary>
        public ReadIssuesSettings ReadIssuesSettings { get; set; }

        /// <summary>
        /// Calls <see cref="BaseIssueProvider.ReadIssues()"/>.
        /// </summary>
        /// <returns>Issues returned from issue provider.</returns>
        public IEnumerable<IIssue> ReadIssues()
        {
            var issueProvider = this.CreateIssueProvider();
            return issueProvider.ReadIssues();
        }

        /// <summary>
        /// Returns arguments for creating a new instance of an issue provider.
        /// </summary>
        /// <returns>Arguments for creating a new instance of an issue provider.</returns>
        protected virtual IList<object> GetCreateIssueProviderArguments()
        {
            if (this.Log == null)
            {
                throw new InvalidOperationException("No log instance set.");
            }

            return new List<object> { this.Log };
        }

        /// <summary>
        /// Creates and initializes a new instance of the issue provider.
        /// </summary>
        /// <returns>Instance of the issue provider.</returns>
        private T CreateIssueProvider()
        {
            var provider =
                (T)Activator.CreateInstance(
                    typeof(T),
                    this.GetCreateIssueProviderArguments().ToArray());

            if (this.ReadIssuesSettings == null)
            {
                throw new InvalidOperationException("No settings for reading issues set.");
            }

            provider.Initialize(this.ReadIssuesSettings);
            return provider;
        }
    }
}
