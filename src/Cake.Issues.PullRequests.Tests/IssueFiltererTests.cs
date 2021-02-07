﻿namespace Cake.Issues.PullRequests.Tests
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
                var fixture = new IssueFiltererFixture
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
                var fixture = new IssueFiltererFixture
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
                var fixture = new IssueFiltererFixture
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
                var fixture = new IssueFiltererFixture();

                // When
                var result = Record.Exception(() => fixture.FilterIssues(null, new Dictionary<IIssue, IssueCommentInfo>()));

                // Then
                result.IsArgumentNullException("issues");
            }

            [Fact]
            public void Should_Not_Throw_If_Issue_Comments_Are_Null()
            {
                // Given
                var fixture = new IssueFiltererFixture();

                // When
                fixture.FilterIssues(new List<IIssue>(), null);

                // Then
            }

            [Fact]
            public void Should_Apply_Custom_Filters()
            {
                // Given
                var fixture =
                    new IssueFiltererFixture();
                fixture.Settings.IssueFilters.Add(x => x.Where(issue => issue.Rule != "Bar"));

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

            public sealed class TheFilterIssuesByPathMethod
            {
                [Fact]
                public void Should_Throw_If_Modified_Files_Contain_Absolute_Path()
                {
                    // Given
                    var fixture =
                        new IssueFiltererFixture(
                            (builder, settings) => builder
                                .WithFilteringByModifiedFilesCapability(
                                    new List<FilePath>
                                    {
                                    new FilePath(@"c:\FakeIssueProvider.cs")
                                    }));

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
                public void Should_Filter_Issues_If_File_Is_Not_Modified()
                {
                    // Given
                    var fixture =
                        new IssueFiltererFixture(
                            (builder, settings) => builder
                                .WithFilteringByModifiedFilesCapability(
                                    new List<FilePath>
                                    {
                                        new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                                    }));

                    var issue1 =
                        IssueBuilder
                            .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                            .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                            .OfRule("Rule Foo")
                            .WithPriority(IssuePriority.Warning)
                            .Create();
                    var issue2 =
                        IssueBuilder
                            .NewIssue("Message Bar", "ProviderType Bar", "ProviderName Bar")
                            .InFile(@"src\Cake.Issues.Tests\NotModified.cs", 10)
                            .OfRule("Rule Bar")
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
                    fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered because they do not belong to files that were changed in this pull request");
                }
            }

            public sealed class TheFilterPreExistingCommentsMethod
            {
                [Fact]
                public void Should_Filter_Issues_With_Existing_Active_Comment()
                {
                    // Given
                    var fixture = new IssueFiltererFixture();

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
                    var fixture = new IssueFiltererFixture();

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
                    var fixture = new IssueFiltererFixture();

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
            }

            public sealed class TheFilterIssuesByNumberMethod
            {
                public sealed class ForPropertyMaxIssuesToPost
                {
                    [Fact]
                    public void Should_Limit_Messages_To_Maximum()
                    {
                        // Given
                        var fixture = new IssueFiltererFixture();
                        fixture.Settings.MaxIssuesToPost = 1;

                        var issue1 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue2 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderType Bar", "ProviderName Bar")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
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
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 1");
                    }

                    [Fact]
                    public void Should_Limit_Messages_To_Maximum_By_Priority()
                    {
                        // Given
                        var fixture = new IssueFiltererFixture();
                        fixture.Settings.MaxIssuesToPost = 1;

                        var issue1 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue2 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderType Bar", "ProviderName Bar")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Error)
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
                        issues.ShouldContain(issue2);
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 1");
                    }

                    [Fact]
                    public void Should_Limit_Messages_To_Maximum_By_FilePath()
                    {
                        // Given
                        var fixture = new IssueFiltererFixture();
                        fixture.Settings.MaxIssuesToPost = 1;

                        var issue1 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue2 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderType Bar", "ProviderName Bar")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
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
                        issues.ShouldContain(issue2);
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 1");
                    }
                }

                public sealed class ForPropertyMaxIssuesToPostAcrossRunsForEachProvider
                {
                    [Fact]
                    public void Should_Limit_Messages_To_Maximum()
                    {
                        // Given
                        var fixture = new IssueFiltererFixture();
                        fixture.Settings.ProviderIssueLimits.Add(
                            "ProviderType Foo",
                            new ProviderIssueIssueLimits(maxIssuesToPostAcrossRuns: 2));

                        var newIssue1 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create();

                        var newIssue2 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderType Bar", "ProviderName Bar")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();

                        // When
                        var issues =
                            fixture.FilterIssues(
                                new List<IIssue>
                                {
                                    newIssue1, newIssue2
                                },
                                new Dictionary<IIssue, IssueCommentInfo>(),
                                new List<IPullRequestDiscussionThread>
                                {
                                    new PullRequestDiscussionThread(
                                        1,
                                        PullRequestDiscussionStatus.Active,
                                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                        new List<IPullRequestDiscussionComment>
                                        {
                                            new PullRequestDiscussionComment
                                            {
                                                Content = "Message FooBar",
                                                IsDeleted = false
                                            }
                                        })
                                    {
                                        ProviderType = "ProviderType Foo"
                                    },
                                    new PullRequestDiscussionThread(
                                        1,
                                        PullRequestDiscussionStatus.Active,
                                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                        new List<IPullRequestDiscussionComment>
                                        {
                                            new PullRequestDiscussionComment
                                            {
                                                Content = "Message FooBar",
                                                IsDeleted = false
                                            }
                                        })
                                    {
                                        ProviderType = "ProviderType Foo"
                                    }
                                });

                        // Then
                        issues.Count().ShouldBe(1);
                        issues.ShouldContain(newIssue2);
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 2 across all runs for provider 'ProviderType Foo' (2 issues already posted in previous runs)");
                    }

                    [Fact]
                    public void Should_Limit_Messages_To_Maximum_By_Priority()
                    {
                        // Given
                        var fixture = new IssueFiltererFixture();
                        fixture.Settings.ProviderIssueLimits.Add(
                            "ProviderType Foo",
                            new ProviderIssueIssueLimits(maxIssuesToPostAcrossRuns: 2));

                        var issue1 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue2 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Error)
                                .Create();

                        var issue3 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderType Bar", "ProviderName Bar")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Error)
                                .Create();

                        // When
                        var issues =
                            fixture.FilterIssues(
                                new List<IIssue>
                                {
                                    issue1, issue2, issue3
                                },
                                new Dictionary<IIssue, IssueCommentInfo>(),
                                new List<IPullRequestDiscussionThread>
                                {
                                new PullRequestDiscussionThread(
                                    1,
                                    PullRequestDiscussionStatus.Active,
                                    @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                    new List<IPullRequestDiscussionComment>
                                    {
                                        new PullRequestDiscussionComment
                                        {
                                            Content = "Message FooBar",
                                            IsDeleted = false
                                        }
                                    })
                                {
                                    ProviderType = "ProviderType Foo"
                                }
                                });

                        // Then
                        issues.Count().ShouldBe(2);
                        issues.ShouldContain(issue2);
                        issues.ShouldContain(issue3);
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 2 across all runs for provider 'ProviderType Foo' (1 issues already posted in previous runs)");
                    }

                    [Fact]
                    public void Should_Limit_Messages_To_Maximum_By_FilePath()
                    {
                        // Given
                        var fixture = new IssueFiltererFixture();
                        fixture.Settings.ProviderIssueLimits.Add(
                            "ProviderType Foo",
                            new ProviderIssueIssueLimits(maxIssuesToPostAcrossRuns: 2));

                        var issue1 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Error)
                                .Create();

                        var issue2 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Error)
                                .Create();

                        var issue3 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Tests\MyIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Error)
                                .Create();

                        var issue4 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderType Bar", "ProviderName Bar")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Error)
                                .Create();

                        // When
                        var issues =
                            fixture.FilterIssues(
                                new List<IIssue>
                                {
                                    issue1, issue2, issue3, issue4
                                },
                                new Dictionary<IIssue, IssueCommentInfo>(),
                                new List<IPullRequestDiscussionThread>
                                {
                                new PullRequestDiscussionThread(
                                    1,
                                    PullRequestDiscussionStatus.Active,
                                    @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                    new List<IPullRequestDiscussionComment>
                                    {
                                        new PullRequestDiscussionComment
                                        {
                                            Content = "Message FooBar",
                                            IsDeleted = false
                                        }
                                    })
                                {
                                    ProviderType = "ProviderType Foo"
                                }
                                });

                        // Then
                        issues.Count().ShouldBe(2);
                        issues.ShouldContain(issue2);
                        issues.ShouldContain(issue4);
                        fixture.Log.Entries.ShouldContain(x => x.Message == "2 issue(s) were filtered to match the global issue limit of 2 across all runs for provider 'ProviderType Foo' (1 issues already posted in previous runs)");
                    }
                }

                public sealed class ForPropertyMaxIssuesToPostForEachProvider
                {
                    [Fact]
                    public void Should_Limit_Messages_To_Maximum()
                    {
                        // Given
                        var fixture = new IssueFiltererFixture();
                        fixture.Settings.ProviderIssueLimits.Add(
                            "ProviderTypeA",
                            new ProviderIssueIssueLimits(maxIssuesToPost: 1));
                        fixture.Settings.ProviderIssueLimits.Add(
                            "ProviderTypeB",
                            new ProviderIssueIssueLimits(maxIssuesToPost: 1));

                        var issue1 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderTypeA", "ProviderNameA")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue2 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderTypeA", "ProviderNameA")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue3 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderTypeB", "ProviderNameB")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue4 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderTypeB", "ProviderNameB")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();

                        // When
                        var issues =
                            fixture.FilterIssues(
                                new List<IIssue>
                                {
                                    issue1, issue2, issue3, issue4
                                },
                                new Dictionary<IIssue, IssueCommentInfo>());

                        // Then
                        issues.Count().ShouldBe(2);
                        issues.ShouldContain(issue1);
                        issues.ShouldContain(issue3);

                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global limit of 1 issues which should be reported for issue provider 'ProviderTypeA'");
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global limit of 1 issues which should be reported for issue provider 'ProviderTypeB'");
                    }

                    [Fact]
                    public void Should_Limit_Messages_To_Maximum_By_Priority()
                    {
                        // Given
                        var fixture = new IssueFiltererFixture();
                        fixture.Settings.ProviderIssueLimits.Add(
                            "ProviderTypeA",
                            new ProviderIssueIssueLimits(maxIssuesToPost: 1));
                        fixture.Settings.ProviderIssueLimits.Add(
                            "ProviderTypeB",
                            new ProviderIssueIssueLimits(maxIssuesToPost: 1));

                        var issue1 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderTypeA", "ProviderNameA")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue2 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderTypeA", "ProviderNameA")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Error)
                                .Create();
                        var issue3 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderTypeB", "ProviderNameB")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Error)
                                .Create();
                        var issue4 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderTypeB", "ProviderNameB")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();

                        // When
                        var issues =
                            fixture.FilterIssues(
                                new List<IIssue>
                                {
                                    issue1, issue2, issue3, issue4
                                },
                                new Dictionary<IIssue, IssueCommentInfo>());

                        // Then
                        issues.Count().ShouldBe(2);
                        issues.ShouldContain(issue2);
                        issues.ShouldContain(issue3);
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global limit of 1 issues which should be reported for issue provider 'ProviderTypeA'");
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global limit of 1 issues which should be reported for issue provider 'ProviderTypeB'");
                    }

                    [Fact]
                    public void Should_Limit_Messages_To_Maximum_By_FilePath()
                    {
                        // Given
                        var fixture = new IssueFiltererFixture();
                        fixture.Settings.ProviderIssueLimits.Add(
                            "ProviderTypeA",
                            new ProviderIssueIssueLimits(maxIssuesToPost: 1));
                        fixture.Settings.ProviderIssueLimits.Add(
                            "ProviderTypeB",
                            new ProviderIssueIssueLimits(maxIssuesToPost: 1));

                        var issue1 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderTypeA", "ProviderNameA")
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue2 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderTypeA", "ProviderNameA")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue3 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderTypeB", "ProviderNameB")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue4 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderTypeB", "ProviderNameB")
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();

                        // When
                        var issues =
                            fixture.FilterIssues(
                                new List<IIssue>
                                {
                                    issue1, issue2, issue3, issue4
                                },
                                new Dictionary<IIssue, IssueCommentInfo>());

                        // Then
                        issues.Count().ShouldBe(2);
                        issues.ShouldContain(issue2);
                        issues.ShouldContain(issue3);
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global limit of 1 issues which should be reported for issue provider 'ProviderTypeA'");
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global limit of 1 issues which should be reported for issue provider 'ProviderTypeB'");
                    }

                    [Fact]
                    public void Should_Limit_Messages_To_Maximum_With_Different_Maximum_Limits()
                    {
                        // Given
                        var fixture = new IssueFiltererFixture();
                        fixture.Settings.ProviderIssueLimits.Add(
                            "ProviderTypeA",
                            new ProviderIssueIssueLimits(maxIssuesToPost: 1));
                        fixture.Settings.ProviderIssueLimits.Add(
                            "ProviderTypeB",
                            new ProviderIssueIssueLimits(maxIssuesToPost: 3));

                        var issue1 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderTypeA", "ProviderNameA")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue2 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderTypeA", "ProviderNameA")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue3 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderTypeB", "ProviderNameB")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 3)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue4 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderTypeB", "ProviderNameB")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 4)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue5 =
                            IssueBuilder
                                .NewIssue("Message Test", "ProviderTypeB", "ProviderNameB")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 5)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue6 =
                            IssueBuilder
                                .NewIssue("Message FooBar", "ProviderTypeB", "ProviderNameB")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 6)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue7 =
                            IssueBuilder
                                .NewIssue("Message TestFoo", "ProviderTypeB", "ProviderNameB")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 7)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue8 =
                            IssueBuilder
                                .NewIssue("Message BarFoo", "ProviderTypeB", "ProviderNameB")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 8)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue9 =
                            IssueBuilder
                                .NewIssue("Message BarFooTest", "ProviderTypeB", "ProviderNameB")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 9)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();

                        // When
                        var issues =
                            fixture.FilterIssues(
                                new List<IIssue>
                                {
                                    issue1, issue2, issue3, issue4, issue5, issue6, issue7, issue8, issue9
                                },
                                new Dictionary<IIssue, IssueCommentInfo>());

                        // Then
                        issues.Count().ShouldBe(4);

                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global limit of 1 issues which should be reported for issue provider 'ProviderTypeA'");
                        fixture.Log.Entries.ShouldContain(x => x.Message == "4 issue(s) were filtered to match the global limit of 3 issues which should be reported for issue provider 'ProviderTypeB'");
                    }
                }

                public sealed class ForPropertyMaxIssuesToPostAcrossRuns
                {
                    [Fact]
                    public void Should_Limit_Messages_To_Maximum()
                    {
                        // Given
                        var fixture = new IssueFiltererFixture();
                        fixture.Settings.MaxIssuesToPostAcrossRuns = 2;

                        var issue1 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue2 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderType Bar", "ProviderName Bar")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();

                        // When
                        var issues =
                            fixture.FilterIssues(
                                new List<IIssue>
                                {
                                issue1, issue2
                                },
                                new Dictionary<IIssue, IssueCommentInfo>(),
                                new List<IPullRequestDiscussionThread>
                                {
                                new PullRequestDiscussionThread(
                                    1,
                                    PullRequestDiscussionStatus.Active,
                                    @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                    new List<IPullRequestDiscussionComment>
                                    {
                                        new PullRequestDiscussionComment
                                        {
                                            Content = "Message FooBar",
                                            IsDeleted = false
                                        }
                                    })
                                });

                        // Then
                        issues.Count().ShouldBe(1);
                        issues.ShouldContain(issue1);
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 2 across all runs (1 issues already posted in previous runs)");
                    }

                    [Fact]
                    public void Should_Limit_Messages_To_Maximum_By_Priority()
                    {
                        // Given
                        var fixture = new IssueFiltererFixture();
                        fixture.Settings.MaxIssuesToPostAcrossRuns = 2;

                        var issue1 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue2 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderType Bar", "ProviderName Bar")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Error)
                                .Create();

                        // When
                        var issues =
                            fixture.FilterIssues(
                                new List<IIssue>
                                {
                                issue1, issue2
                                },
                                new Dictionary<IIssue, IssueCommentInfo>(),
                                new List<IPullRequestDiscussionThread>
                                {
                                new PullRequestDiscussionThread(
                                    1,
                                    PullRequestDiscussionStatus.Active,
                                    @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                    new List<IPullRequestDiscussionComment>
                                    {
                                        new PullRequestDiscussionComment
                                        {
                                            Content = "Message FooBar",
                                            IsDeleted = false
                                        }
                                    })
                                });

                        // Then
                        issues.Count().ShouldBe(1);
                        issues.ShouldContain(issue2);
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 2 across all runs (1 issues already posted in previous runs)");
                    }

                    [Fact]
                    public void Should_Limit_Messages_To_Maximum_By_FilePath()
                    {
                        // Given
                        var fixture = new IssueFiltererFixture();
                        fixture.Settings.MaxIssuesToPostAcrossRuns = 2;

                        var issue1 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue2 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderType Bar", "ProviderName Bar")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();

                        // When
                        var issues =
                            fixture.FilterIssues(
                                new List<IIssue>
                                {
                                issue1, issue2
                                },
                                new Dictionary<IIssue, IssueCommentInfo>(),
                                new List<IPullRequestDiscussionThread>
                                {
                                new PullRequestDiscussionThread(
                                    1,
                                    PullRequestDiscussionStatus.Active,
                                    @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                    new List<IPullRequestDiscussionComment>
                                    {
                                        new PullRequestDiscussionComment
                                        {
                                            Content = "Message FooBar",
                                            IsDeleted = false
                                        }
                                    })
                                });

                        // Then
                        issues.Count().ShouldBe(1);
                        issues.ShouldContain(issue2);
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 2 across all runs (1 issues already posted in previous runs)");
                    }
                }

                public sealed class ForPropertyMaxIssuesToPostForEachIssueProvider
                {
                    [Fact]
                    public void Should_Limit_Messages_To_Maximum()
                    {
                        // Given
                        var fixture = new IssueFiltererFixture();
                        fixture.Settings.MaxIssuesToPostForEachIssueProvider = 1;

                        var issue1 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderTypeA", "ProviderNameA")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue2 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderTypeA", "ProviderNameA")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue3 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderTypeB", "ProviderNameB")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue4 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderTypeB", "ProviderNameB")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();

                        // When
                        var issues =
                            fixture.FilterIssues(
                                new List<IIssue>
                                {
                                    issue1, issue2, issue3, issue4
                                },
                                new Dictionary<IIssue, IssueCommentInfo>());

                        // Then
                        issues.Count().ShouldBe(2);
                        issues.ShouldContain(issue1);
                        issues.ShouldContain(issue3);
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) of type ProviderTypeA were filtered to match the maximum of 1 issues which should be reported for each issue provider");
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) of type ProviderTypeB were filtered to match the maximum of 1 issues which should be reported for each issue provider");
                    }

                    [Fact]
                    public void Should_Limit_Messages_To_Maximum_By_Priority()
                    {
                        // Given
                        var fixture = new IssueFiltererFixture();
                        fixture.Settings.MaxIssuesToPostForEachIssueProvider = 1;

                        var issue1 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderTypeA", "ProviderNameA")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue2 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderTypeA", "ProviderNameA")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Error)
                                .Create();
                        var issue3 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderTypeB", "ProviderNameB")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Error)
                                .Create();
                        var issue4 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderTypeB", "ProviderNameB")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();

                        // When
                        var issues =
                            fixture.FilterIssues(
                                new List<IIssue>
                                {
                                    issue1, issue2, issue3, issue4
                                },
                                new Dictionary<IIssue, IssueCommentInfo>());

                        // Then
                        issues.Count().ShouldBe(2);
                        issues.ShouldContain(issue2);
                        issues.ShouldContain(issue3);
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) of type ProviderTypeA were filtered to match the maximum of 1 issues which should be reported for each issue provider");
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) of type ProviderTypeB were filtered to match the maximum of 1 issues which should be reported for each issue provider");
                    }

                    [Fact]
                    public void Should_Limit_Messages_To_Maximum_By_FilePath()
                    {
                        // Given
                        var fixture = new IssueFiltererFixture();
                        fixture.Settings.MaxIssuesToPostForEachIssueProvider = 1;

                        var issue1 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderTypeA", "ProviderNameA")
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue2 =
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderTypeA", "ProviderNameA")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue3 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderTypeB", "ProviderNameB")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();
                        var issue4 =
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderTypeB", "ProviderNameB")
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create();

                        // When
                        var issues =
                            fixture.FilterIssues(
                                new List<IIssue>
                                {
                                    issue1, issue2, issue3, issue4
                                },
                                new Dictionary<IIssue, IssueCommentInfo>());

                        // Then
                        issues.Count().ShouldBe(2);
                        issues.ShouldContain(issue2);
                        issues.ShouldContain(issue3);
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) of type ProviderTypeA were filtered to match the maximum of 1 issues which should be reported for each issue provider");
                        fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) of type ProviderTypeB were filtered to match the maximum of 1 issues which should be reported for each issue provider");
                    }
                }
            }
        }
    }
}
