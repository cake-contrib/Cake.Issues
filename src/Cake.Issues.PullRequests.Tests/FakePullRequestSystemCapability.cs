namespace Cake.Issues.PullRequests.Tests
{
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Implementation of a <see cref="BasePullRequestSystemCapability{T}"/> for use in test cases.
    /// </summary>
    public class FakePullRequestSystemCapability : BasePullRequestSystemCapability<FakePullRequestSystem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakePullRequestSystemCapability"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="pullRequestSystem">Pull request system to which this capability belongs.</param>
        public FakePullRequestSystemCapability(ICakeLog log, FakePullRequestSystem pullRequestSystem)
            : base(log, pullRequestSystem)
        {
        }

        /// <summary>
        /// Gets the Cake log context.
        /// </summary>
        public new ICakeLog Log => base.Log;

        /// <summary>
        /// Gets the pull request system to which the capability belongs.
        /// </summary>
        public new FakePullRequestSystem PullRequestSystem => base.PullRequestSystem;
    }
}
