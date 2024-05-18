namespace Cake.Issues.PullRequests.Tests
{
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

    /// <summary>
    /// Class to create instances of <see cref="FakePullRequestSystem"/> with a fluent API.
    /// </summary>
    public class FakePullRequestSystemBuilder
    {
        private readonly ICakeLog log;
        private readonly List<IPullRequestDiscussionThread> discussionThreads = [];
        private readonly List<FilePath> modifiedFiles = [];
        private bool withCheckingCommitIdCapability;
        private bool withDiscussionThreadsCapability;
        private bool withFilteringByModifiedFilesCapability;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakePullRequestSystemBuilder"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        private FakePullRequestSystemBuilder(ICakeLog log)
        {
            log.NotNull();

            this.log = log;
        }

        /// <summary>
        /// Initiates the creation of a new <see cref="FakePullRequestSystem"/>.
        /// </summary>
        /// <returns>Builder class for creating a new pull request system.</returns>
        public static FakePullRequestSystemBuilder NewPullRequestSystem() => NewPullRequestSystem(new FakeLog());

        /// <summary>
        /// Initiates the creation of a new <see cref="FakePullRequestSystem"/>.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <returns>Builder class for creating a new pull request system.</returns>
        public static FakePullRequestSystemBuilder NewPullRequestSystem(ICakeLog log)
        {
            log.NotNull();

            return new FakePullRequestSystemBuilder(log);
        }

        /// <summary>
        /// Defines that the pull request should have the <see cref="FakeCheckingCommitIdCapability"/>.
        /// </summary>
        /// <returns>Pull request builder instance.</returns>
        public FakePullRequestSystemBuilder WithCheckingCommitIdCapability()
        {
            this.withCheckingCommitIdCapability = true;

            return this;
        }

        /// <summary>
        /// Defines that the pull request should have the <see cref="FakeDiscussionThreadsCapability"/>.
        /// </summary>
        /// <returns>Pull request builder instance.</returns>
        public FakePullRequestSystemBuilder WithDiscussionThreadsCapability()
        {
            this.withDiscussionThreadsCapability = true;

            return this;
        }

        /// <summary>
        /// Defines that the pull request should have the <see cref="FakeDiscussionThreadsCapability"/>.
        /// </summary>
        /// <param name="discussionThreads">Discussion threads which the pull request system should return.</param>
        /// <returns>Pull request builder instance.</returns>
        public FakePullRequestSystemBuilder WithDiscussionThreadsCapability(
            IEnumerable<IPullRequestDiscussionThread> discussionThreads)
        {
            discussionThreads.NotNull();

            this.withDiscussionThreadsCapability = true;
            this.discussionThreads.AddRange(discussionThreads);

            return this;
        }

        /// <summary>
        /// Defines that the pull request should have the <see cref="FakeFilteringByModifiedFilesCapability"/>.
        /// </summary>
        /// <returns>Pull request builder instance.</returns>
        public FakePullRequestSystemBuilder WithFilteringByModifiedFilesCapability()
        {
            this.withFilteringByModifiedFilesCapability = true;

            return this;
        }

        /// <summary>
        /// Defines that the pull request should have the <see cref="FakeFilteringByModifiedFilesCapability"/>.
        /// </summary>
        /// <param name="modifiedFiles">List of modified files which the pull request system should return.</param>
        /// <returns>Pull request builder instance.</returns>
        public FakePullRequestSystemBuilder WithFilteringByModifiedFilesCapability(IEnumerable<FilePath> modifiedFiles)
        {
            modifiedFiles.NotNull();

            this.withFilteringByModifiedFilesCapability = true;
            this.modifiedFiles.AddRange(modifiedFiles);

            return this;
        }

        /// <summary>
        /// Creates a new <see cref="FakePullRequestSystem"/>.
        /// </summary>
        /// <returns>New pull request system.</returns>
        public FakePullRequestSystem Create()
        {
            var pullRequestSystem = new FakePullRequestSystem(this.log);

            if (this.withCheckingCommitIdCapability)
            {
                pullRequestSystem.AddCapability(
                    new FakeCheckingCommitIdCapability(this.log, pullRequestSystem));
            }

            if (this.withDiscussionThreadsCapability)
            {
                pullRequestSystem.AddCapability(
                    new FakeDiscussionThreadsCapability(this.log, pullRequestSystem, this.discussionThreads));
            }

            if (this.withFilteringByModifiedFilesCapability)
            {
                pullRequestSystem.AddCapability(
                    new FakeFilteringByModifiedFilesCapability(this.log, pullRequestSystem, this.modifiedFiles));
            }

            return pullRequestSystem;
        }
    }
}
