namespace Cake.Issues.PullRequests.Tests
{
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;

    public sealed class BaseDiscussionThreadsCapabilityTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                const ICakeLog log = null;
                var pullRequestSystem = new FakePullRequestSystem(new FakeLog());
                var discussionThreads = new List<IPullRequestDiscussionThread>();

                // When
                var result =
                    Record.Exception(() =>
                        new FakeDiscussionThreadsCapability(log, pullRequestSystem, discussionThreads));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_PullRequestSystem_Is_Null()
            {
                // Given
                var log = new FakeLog();
                const FakePullRequestSystem pullRequestSystem = null;
                var discussionThreads = new List<IPullRequestDiscussionThread>();

                // When
                var result =
                    Record.Exception(() =>
                        new FakeDiscussionThreadsCapability(log, pullRequestSystem, discussionThreads));

                // Then
                result.IsArgumentNullException("pullRequestSystem");
            }

            [Fact]
            public void Should_Set_Log()
            {
                // Given
                var log = new FakeLog();
                var pullRequestSystem = new FakePullRequestSystem(log);
                var discussionThreads = new List<IPullRequestDiscussionThread>();

                // When
                var capability = new FakeDiscussionThreadsCapability(log, pullRequestSystem, discussionThreads);

                // Then
                capability.Log.ShouldBe(log);
            }

            [Fact]
            public void Should_Set_PullRequestSystem()
            {
                // Given
                var log = new FakeLog();
                var pullRequestSystem = new FakePullRequestSystem(log);
                var discussionThreads = new List<IPullRequestDiscussionThread>();

                // When
                var capability = new FakeDiscussionThreadsCapability(log, pullRequestSystem, discussionThreads);

                // Then
                capability.PullRequestSystem.ShouldBe(pullRequestSystem);
            }
        }

        public sealed class TheFetchDiscussionThreadsMethod
        {
            [Fact]
            public void Should_Throw_If_Not_Initialized()
            {
                // Given
                var log = new FakeLog();
                var pullRequestSystem = new FakePullRequestSystem(log);
                var discussionThreads = new List<IPullRequestDiscussionThread>();
                var capability = new FakeDiscussionThreadsCapability(log, pullRequestSystem, discussionThreads);

                // When
                var result =
                    Record.Exception(() =>
                        capability.FetchDiscussionThreads("foo"));

                // Then
                result.IsInvalidOperationException("Initialize needs to be called first.");
            }

            [Fact]
            public void Should_Call_InternalFetchDiscussionThreads()
            {
                // Given
                var log = new FakeLog();
                var pullRequestSystem = new FakePullRequestSystem(log);
                var settings = new ReportIssuesToPullRequestSettings(@"c:\repo");
                var discussionThreads =
                    new List<IPullRequestDiscussionThread>
                    {
                        new PullRequestDiscussionThread(
                            1,
                            PullRequestDiscussionStatus.Active,
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs"),
                            new List<IPullRequestDiscussionComment>
                            {
                                new PullRequestDiscussionComment()
                                {
                                    Content = "Message Foo",
                                    IsDeleted = false,
                                },
                            })
                        {
                            CommentIdentifier = "Message Foo",
                            CommentSource = settings.CommentSource,
                        },
                    };
                var capability = new FakeDiscussionThreadsCapability(log, pullRequestSystem, discussionThreads);

                // When
                pullRequestSystem.Initialize(settings);
                var result = capability.FetchDiscussionThreads(settings.CommentSource);

                // Then
                result.ShouldBe(discussionThreads);
            }
        }

        public sealed class TheResolveDiscussionThreadsMethod
        {
            [Fact]
            public void Should_Throw_If_Not_Initialized()
            {
                // Given
                var log = new FakeLog();
                var pullRequestSystem = new FakePullRequestSystem(log);
                var discussionThreads = new List<IPullRequestDiscussionThread>();
                var capability = new FakeDiscussionThreadsCapability(log, pullRequestSystem, discussionThreads);

                // When
                var result =
                    Record.Exception(() =>
                        capability.ResolveDiscussionThreads(new List<IPullRequestDiscussionThread>()));

                // Then
                result.IsInvalidOperationException("Initialize needs to be called first.");
            }

            [Fact]
            public void Should_Call_InternalResolveDiscussionThreads()
            {
                // Given
                var log = new FakeLog();
                var pullRequestSystem = new FakePullRequestSystem(log);
                var settings = new ReportIssuesToPullRequestSettings(@"c:\repo");
                var discussionThread1 =
                    new PullRequestDiscussionThread(
                        1,
                        PullRequestDiscussionStatus.Active,
                        new FilePath(@"src\Cake.Issues.Tests\Foo.cs"),
                        new List<IPullRequestDiscussionComment>
                        {
                            new PullRequestDiscussionComment()
                            {
                                Content = "Message Foo",
                                IsDeleted = false,
                            },
                        })
                    {
                        CommentIdentifier = "Message Foo",
                        CommentSource = settings.CommentSource,
                    };
                var discussionThread2 =
                    new PullRequestDiscussionThread(
                        1,
                        PullRequestDiscussionStatus.Active,
                        new FilePath(@"src\Cake.Issues.Tests\Bar.cs"),
                        new List<IPullRequestDiscussionComment>
                        {
                            new PullRequestDiscussionComment()
                            {
                                Content = "Message Bar",
                                IsDeleted = false,
                            },
                        })
                    {
                        CommentIdentifier = "Message Bar",
                        CommentSource = settings.CommentSource,
                    };
                var discussionThreads =
                    new List<IPullRequestDiscussionThread>
                    {
                        discussionThread1,
                        discussionThread2,
                    };
                var capability = new FakeDiscussionThreadsCapability(log, pullRequestSystem, discussionThreads);

                // When
                pullRequestSystem.Initialize(settings);
                capability.ResolveDiscussionThreads(
                    new List<IPullRequestDiscussionThread>
                    {
                        discussionThread1,
                    });

                // Then
                capability.ResolvedThreads.ShouldContain(discussionThread1);
            }
        }

        public sealed class TheReopenDiscussionThreadsMethod
        {
            [Fact]
            public void Should_Throw_If_Not_Initialized()
            {
                // Given
                var log = new FakeLog();
                var pullRequestSystem = new FakePullRequestSystem(log);
                var discussionThreads = new List<IPullRequestDiscussionThread>();
                var capability = new FakeDiscussionThreadsCapability(log, pullRequestSystem, discussionThreads);

                // When
                var result =
                    Record.Exception(() =>
                        capability.ReopenDiscussionThreads(new List<IPullRequestDiscussionThread>()));

                // Then
                result.IsInvalidOperationException("Initialize needs to be called first.");
            }

            [Fact]
            public void Should_Call_InternalReopenDiscussionThreads()
            {
                // Given
                var log = new FakeLog();
                var pullRequestSystem = new FakePullRequestSystem(log);
                var settings = new ReportIssuesToPullRequestSettings(@"c:\repo");
                var discussionThread1 =
                    new PullRequestDiscussionThread(
                        1,
                        PullRequestDiscussionStatus.Active,
                        new FilePath(@"src\Cake.Issues.Tests\Foo.cs"),
                        new List<IPullRequestDiscussionComment>
                        {
                            new PullRequestDiscussionComment()
                            {
                                Content = "Message Foo",
                                IsDeleted = false,
                            },
                        })
                    {
                        CommentIdentifier = "Message Foo",
                        CommentSource = settings.CommentSource,
                    };
                var discussionThread2 =
                    new PullRequestDiscussionThread(
                            1,
                            PullRequestDiscussionStatus.Active,
                            new FilePath(@"src\Cake.Issues.Tests\Bar.cs"),
                            new List<IPullRequestDiscussionComment>
                            {
                                new PullRequestDiscussionComment()
                                {
                                    Content = "Message Bar",
                                    IsDeleted = false,
                                },
                            })
                    {
                        CommentSource = settings.CommentSource,
                    };
                var discussionThreads =
                    new List<IPullRequestDiscussionThread>
                    {
                        discussionThread1,
                        discussionThread2,
                    };
                var capability = new FakeDiscussionThreadsCapability(log, pullRequestSystem, discussionThreads);

                // When
                pullRequestSystem.Initialize(settings);
                capability.ReopenDiscussionThreads(
                    new List<IPullRequestDiscussionThread>
                    {
                        discussionThread1,
                    });

                // Then
                capability.ReopenedThreads.ShouldContain(discussionThread1);
            }
        }
    }
}
