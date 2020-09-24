namespace Cake.Issues.PullRequests.GitHubActions
{
    using System.Collections.Generic;
    using Cake.Core;

    /// <summary>
    /// Class for posting issues to GitHub Actions.
    /// </summary>
    public class GitHubActionsPullRequestSystem : BasePullRequestSystem
    {
        private readonly ICakeContext context;

        private readonly GitHubActionsBuildSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubActionsPullRequestSystem"/> class.
        /// </summary>
        /// <param name="context">The Cake context.</param>
        /// <param name="settings">Settings for writing the issues to GitHub Actions.</param>
        public GitHubActionsPullRequestSystem(ICakeContext context, GitHubActionsBuildSettings settings)
            : base(context?.Log)
        {
            settings.NotNull(nameof(settings));

            this.context = context;
            this.settings = settings;
        }

        /// <inheritdoc />
        protected override void InternalPostDiscussionThreads(IEnumerable<IIssue> issues, string commentSource)
        {
            // TODO post issues to GitHub Actions
        }
    }
}
