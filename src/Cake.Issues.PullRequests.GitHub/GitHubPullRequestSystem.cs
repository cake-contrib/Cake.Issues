namespace Cake.Issues.PullRequests.GitHub
{
    using System.Collections.Generic;
    using Cake.Core;

    /// <summary>
    /// Class for posting issues to GitHub.
    /// </summary>
    public class GitHubPullRequestSystem : BasePullRequestSystem
    {
        private readonly ICakeContext context;
        private readonly GitHubPullRequestSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubPullRequestSystem"/> class.
        /// </summary>
        /// <param name="context">The Cake context.</param>
        /// <param name="settings">Settings for writing the issues to GitHub.</param>
        public GitHubPullRequestSystem(ICakeContext context, GitHubPullRequestSettings settings)
            : base(context?.Log)
        {
            settings.NotNull(nameof(settings));

            this.context = context;
            this.settings = settings;
        }

        /// <inheritdoc />
        protected override void InternalPostDiscussionThreads(IEnumerable<IIssue> issues, string commentSource)
        {
            // TODO post issues to GitHub through an app
        }
    }
}
