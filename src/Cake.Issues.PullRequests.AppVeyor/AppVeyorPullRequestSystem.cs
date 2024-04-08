namespace Cake.Issues.PullRequests.AppVeyor
{
    using System.Collections.Generic;
    using Cake.Common.Build;
    using Cake.Core;

    /// <summary>
    /// Class for posting issues to AppVeyor.
    /// </summary>
    public class AppVeyorPullRequestSystem : BasePullRequestSystem
    {
        private readonly ICakeContext context;
        private readonly AppVeyorBuildSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppVeyorPullRequestSystem"/> class.
        /// </summary>
        /// <param name="context">The Cake context.</param>
        /// <param name="settings">Settings for writting issues to AppVeyor.</param>
        public AppVeyorPullRequestSystem(ICakeContext context, AppVeyorBuildSettings settings)
            : base(context?.Log)
        {
            settings.NotNull(nameof(settings));

            this.context = context;
            this.settings = settings;
        }

        /// <inheritdoc />
        protected override void InternalPostDiscussionThreads(IEnumerable<IIssue> issues, string commentSource)
        {
            foreach (var issue in issues)
            {
                this.context.AppVeyor().AddMessage(
                    this.settings.MessagePattern.ReplaceIssuePattern(issue),
                    issue.Priority.ToAppVeyorMessageCategoryType(),
                    this.settings.DetailsPattern.ReplaceIssuePattern(issue));
            }
        }
    }
}
