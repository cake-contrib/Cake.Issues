namespace Cake.Issues.PullRequests.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Core.IO;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class IssueFiltererTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                var fixture = new PullRequestsFixture
                {
                    Log = null
                };

                // When
                var result = Record.Exception(() => fixture.FilterIssues(null, null));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Pull_Request_System_Is_Null()
            {
                // Given
                var fixture = new PullRequestsFixture
                {
                    PullRequestSystem = null
                };

                // When
                var result = Record.Exception(() => fixture.FilterIssues(null, null));

                // Then
                result.IsArgumentNullException("pullRequestSystem");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var fixture = new PullRequestsFixture
                {
                    Settings = null
                };

                // When
                var result = Record.Exception(() => fixture.FilterIssues(null, null));

                // Then
                result.IsArgumentNullException("settings");
            }
        }

        public sealed class TheFilterIssuesMethod
        {
            [Fact]
            public void Should_Throw_If_Issues_Are_Null()
            {
                // Given
                var fixture = new PullRequestsFixture();

                // When
                var result = Record.Exception(() => fixture.FilterIssues(null, new Dictionary<IIssue, IssueCommentInfo>()));

                // Then
                result.IsArgumentNullException("issues");
            }

            [Fact]
            public void Should_Throw_If_Issue_Comments_Are_Null()
            {
                // Given
                var fixture = new PullRequestsFixture();

                // When
                var result = Record.Exception(() => fixture.FilterIssues(new List<IIssue>(), null));

                // Then
                result.IsArgumentNullException("issueComments");
            }

            [Fact]
            public void Should_Throw_If_Modified_Files_Contain_Absolute_Path()
            {
                // Given
                var fixture = new PullRequestsFixture();
                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>
                        {
                            new FilePath(@"c:\FakeIssueProvider.cs")
                        });

                // When
                var result = Record.Exception(() =>
                    fixture.FilterIssues(
                        new List<IIssue>
                        {
                            IssueBuilder
                                .NewIssue("Message", "ProviderType", "ProviderName")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule")
                                .WithPriority(IssuePriority.Warning)
                                .Create()
                        },
                        new Dictionary<IIssue, IssueCommentInfo>()));

                // Then
                result.IsPullRequestIssuesException(@"Absolute file paths are not suported for modified files. Path: c:/FakeIssueProvider.cs");
            }

            [Fact]
            public void Should_Filter_Issues_With_Existing_Active_Comment()
            {
                // Given
                var fixture = new PullRequestsFixture();
                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });

                var issue1 =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType", "ProviderName")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                        .OfRule("Foo")
                        .WithPriority(IssuePriority.Warning)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("Message Bar", "ProviderType", "ProviderName")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                        .OfRule("Bar")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                // When
                var issues =
                    fixture.FilterIssues(
                        new List<IIssue>
                        {
                            issue1, issue2
                        },
                        new Dictionary<IIssue, IssueCommentInfo>
                        {
                            {
                                issue1,
                                new IssueCommentInfo(
                                    new List<IPullRequestDiscussionComment>
                                    {
                                        new PullRequestDiscussionComment
                                        {
                                            Content = "Message Foo",
                                            IsDeleted = false
                                        }
                                    },
                                    new List<IPullRequestDiscussionComment>(),
                                    new List<IPullRequestDiscussionComment>())
                            }
                        });

                // Then
                issues.Count().ShouldBe(1);
                issues.ShouldContain(issue2);
            }

            [Fact]
            public void Should_Filter_Issues_With_Existing_WontFix_Comment()
            {
                // Given
                var fixture = new PullRequestsFixture();
                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });

                var issue1 =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType", "ProviderName")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                        .OfRule("Foo")
                        .WithPriority(IssuePriority.Warning)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("Message Bar", "ProviderType", "ProviderName")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                        .OfRule("Bar")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                // When
                var issues =
                    fixture.FilterIssues(
                        new List<IIssue>
                        {
                            issue1, issue2
                        },
                        new Dictionary<IIssue, IssueCommentInfo>
                        {
                            {
                                issue1,
                                new IssueCommentInfo(
                                    new List<IPullRequestDiscussionComment>(),
                                    new List<IPullRequestDiscussionComment>
                                    {
                                        new PullRequestDiscussionComment
                                        {
                                            Content = "Message Foo",
                                            IsDeleted = false
                                        }
                                    },
                                    new List<IPullRequestDiscussionComment>())
                            }
                        });

                // Then
                issues.Count().ShouldBe(1);
                issues.ShouldContain(issue2);
            }

            [Fact]
            public void Should_Filter_Issues_With_Existing_Resolved_Comment()
            {
                // Given
                var fixture = new PullRequestsFixture();
                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });

                var issue1 =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType", "ProviderName")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                        .OfRule("Foo")
                        .WithPriority(IssuePriority.Warning)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("Message Bar", "ProviderType", "ProviderName")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                        .OfRule("Bar")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                // When
                var issues =
                    fixture.FilterIssues(
                        new List<IIssue>
                        {
                            issue1, issue2
                        },
                        new Dictionary<IIssue, IssueCommentInfo>
                        {
                            {
                                issue1,
                                new IssueCommentInfo(
                                    new List<IPullRequestDiscussionComment>(),
                                    new List<IPullRequestDiscussionComment>(),
                                    new List<IPullRequestDiscussionComment>
                                    {
                                        new PullRequestDiscussionComment
                                        {
                                            Content = "Message Foo",
                                            IsDeleted = false
                                        }
                                    })
                            }
                        });

                // Then
                issues.Count().ShouldBe(1);
                issues.ShouldContain(issue2);
            }

            [Fact]
            public void Should_Apply_Custom_Filters()
            {
                // Given
                var fixture = new PullRequestsFixture();
                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });
                fixture.ReportIssuesToPullRequestSettings.IssueFilters.Add(x => x.Where(issue => issue.Rule != "Bar"));

                var issue1 =
                    IssueBuilder
                        .NewIssue("Message", "ProviderType", "ProviderName")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                        .OfRule("Foo")
                        .WithPriority(IssuePriority.Warning)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("Message", "ProviderType", "ProviderName")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                        .OfRule("Bar")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                // When
                var issues =
                    fixture.FilterIssues(
                        new List<IIssue>
                        {
                            issue1, issue2
                        },
                        new Dictionary<IIssue, IssueCommentInfo>());

                // Then
                issues.Count().ShouldBe(1);
                issues.ShouldContain(issue1);
            }
        }
    }
}
