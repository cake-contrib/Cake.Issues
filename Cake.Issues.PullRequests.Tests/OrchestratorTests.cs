namespace Cake.Issues.PullRequests.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.IO;
    using Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class OrchestratorTests
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
                var result = Record.Exception(() => fixture.RunOrchestrator());

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Issue_Provider_List_Is_Null()
            {
                // Given
                var fixture = new PullRequestsFixture
                {
                    IssueProviders = null
                };

                // When
                var result = Record.Exception(() => fixture.RunOrchestrator());

                // Then
                result.IsArgumentNullException("issueProviders");
            }

            [Fact]
            public void Should_Throw_If_Issue_Provider_List_Is_Empty()
            {
                // Given
                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();

                // When
                var result = Record.Exception(() => fixture.RunOrchestrator());

                // Then
                result.IsArgumentException("issueProviders");
            }

            [Fact]
            public void Should_Throw_If_Issue_Provider_Is_Null()
            {
                // Given
                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(null);

                // When
                var result = Record.Exception(() => fixture.RunOrchestrator());

                // Then
                result.IsArgumentOutOfRangeException("issueProviders");
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
                var result = Record.Exception(() => fixture.RunOrchestrator());

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
                var result = Record.Exception(() => fixture.RunOrchestrator());

                // Then
                result.IsArgumentNullException("settings");
            }
        }

        public sealed class TheRunMethod
        {
            [Theory]
            [InlineData(IssueCommentFormat.Undefined)]
            [InlineData(IssueCommentFormat.Html)]
            [InlineData(IssueCommentFormat.Markdown)]
            [InlineData(IssueCommentFormat.PlainText)]
            public void Should_Use_The_Correct_Comment_Format(IssueCommentFormat format)
            {
                // Given
                var fixture = new PullRequestsFixture();
                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>())
                    {
                        CommentFormat = format
                    };

                // When
                fixture.RunOrchestrator();

                // Then
                fixture.IssueProviders.ShouldAllBe(x => x.Format == format);
            }

            [Fact]
            public void Should_Initialize_Pull_Request_System()
            {
                // Given
                var fixture = new PullRequestsFixture();

                // When
                fixture.RunOrchestrator();

                // Then
                fixture.PullRequestSystem.Settings.ShouldBe(fixture.Settings);
            }

            [Fact]
            public void Should_Ignore_Issues_If_File_Is_Not_Modified()
            {
                // Given
                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            new Issue(
                                @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                10,
                                "Message Foo",
                                0,
                                "Warning",
                                "Rule Foo",
                                "ProviderType Foo",
                                "ProviderName Foo"),
                            new Issue(
                                @"src\Cake.Issues.Tests\NotModified.cs",
                                12,
                                "Message Bar",
                                0,
                                "Warning",
                                "Rule Bar",
                                "ProviderType Bar",
                                "ProviderName Bar")
                        }));

                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });

                // When
                fixture.RunOrchestrator();

                // Then
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered because they do not belong to files that were changed in this pull request");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Ignore_Issues_Already_Present()
            {
                // Given
                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            new Issue(
                                @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                10,
                                "Message Foo",
                                0,
                                "Warning",
                                "Rule Foo",
                                "ProviderType Foo",
                                "ProviderName Foo"),
                            new Issue(
                                @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                12,
                                "Message Bar",
                                0,
                                "Warning",
                                "Rule Bar",
                                "ProviderType Bar",
                                "ProviderName Bar")
                        }));

                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
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
                                        IsDeleted = false
                                    }
                                })
                            {
                                CommentSource = fixture.ReportIssuesToPullRequestSettings.CommentSource,
                            }
                        },
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });

                // When
                fixture.RunOrchestrator();

                // Then
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Ignore_Issues_Already_Present_Not_Related_To_A_File()
            {
                // Given
                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            new Issue(
                                null,
                                null,
                                "Message Foo",
                                0,
                                "Warning",
                                "Rule Foo",
                                "ProviderType Foo",
                                "ProviderName Foo"),
                            new Issue(
                                null,
                                null,
                                "Message Bar",
                                0,
                                "Warning",
                                "Rule Bar",
                                "ProviderType Bar",
                                "ProviderType Foo")
                        }));

                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>
                        {
                            new PullRequestDiscussionThread(
                                1,
                                PullRequestDiscussionStatus.Active,
                                null,
                                new List<IPullRequestDiscussionComment>
                                {
                                    new PullRequestDiscussionComment()
                                    {
                                        Content = "Message Foo",
                                        IsDeleted = false
                                    }
                                })
                            {
                                CommentSource = fixture.ReportIssuesToPullRequestSettings.CommentSource,
                            }
                        },
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });

                // When
                fixture.RunOrchestrator();

                // Then
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Only_Ignore_Issues_With_Same_Comment_Source()
            {
                // Given
                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            new Issue(
                                @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                10,
                                "Message Foo",
                                0,
                                "Warning",
                                "Rule Foo",
                                "ProviderType Foo",
                                "ProviderName Foo")
                        }));

                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
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
                                        Content = "Foo",
                                        IsDeleted = false
                                    }
                                })
                            {
                                CommentSource = "DifferentCommentSource",
                            }
                        },
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });

                // When
                fixture.RunOrchestrator();

                // Then
                fixture.Log.Entries.ShouldContain(x => x.Message == "0 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Limit_Messages_To_Global_Maximum()
            {
                // Given
                var issue1 =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        10,
                        "Message Foo",
                        0,
                        "Warning",
                        "Rule Foo",
                        "ProviderType Foo",
                        "ProviderName Foo");
                var issue2 =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        12,
                        "Message Bar",
                        0,
                        "Warning",
                        "Rule Bar",
                        "ProviderType Bar",
                        "ProviderName Bar");

                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            issue1, issue2
                        }));

                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });

                fixture.ReportIssuesToPullRequestSettings.MaxIssuesToPost = 1;

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(2);
                result.PostedIssues.Count().ShouldBe(1);
                result.PostedIssues.ShouldContain(issue1);
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 1");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Limit_Messages_To_Global_Maximum_By_Priority()
            {
                // Given
                var issue1 =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        10,
                        "Message Foo",
                        0,
                        "Warning",
                        "Rule Foo",
                        "ProviderType Foo",
                        "ProviderName Foo");
                var issue2 =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        12,
                        "Message Bar",
                        1,
                        "Error",
                        "Rule Bar",
                        "ProviderType Bar",
                        "ProviderName Foo");

                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            issue1, issue2
                        }));

                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });

                fixture.ReportIssuesToPullRequestSettings.MaxIssuesToPost = 1;

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(2);
                result.PostedIssues.Count().ShouldBe(1);
                result.PostedIssues.ShouldContain(issue2);
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 1");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Limit_Messages_To_Global_Maximum_By_FilePath()
            {
                // Given
                var issue1 =
                    new Issue(
                        null,
                        null,
                        "Message Foo",
                        0,
                        "Warning",
                        "Rule Foo",
                        "ProviderType Foo",
                        "ProviderName Foo");
                var issue2 =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        12,
                        "Message Bar",
                        0,
                        "Warning",
                        "Rule Bar",
                        "ProviderType Bar",
                        "ProviderName Bar");

                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            issue1, issue2
                        }));

                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });

                fixture.ReportIssuesToPullRequestSettings.MaxIssuesToPost = 1;

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(2);
                result.PostedIssues.Count().ShouldBe(1);
                result.PostedIssues.ShouldContain(issue2);
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 1");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Limit_Messages_To_Maximum_Per_Issue_Provider()
            {
                // Given
                var issue1 =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        10,
                        "Message Foo",
                        0,
                        "Warning",
                        "Rule Foo",
                        "ProviderTypeA",
                        "ProviderNameA");
                var issue2 =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        12,
                        "Message Foo",
                        0,
                        "Warning",
                        "Rule Foo",
                        "ProviderTypeA",
                        "ProviderNameA");
                var issue3 =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        10,
                        "Message Bar",
                        0,
                        "Warning",
                        "Rule Bar",
                        "ProviderTypeB",
                        "ProviderNameB");
                var issue4 =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        12,
                        "Message Bar",
                        0,
                        "Warning",
                        "Rule Bar",
                        "ProviderTypeB",
                        "ProviderNameB");
                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            issue1, issue2, issue3, issue4
                        }));

                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });

                fixture.ReportIssuesToPullRequestSettings.MaxIssuesToPostForEachIssueProvider = 1;

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(4);
                result.PostedIssues.Count().ShouldBe(2);
                result.PostedIssues.ShouldContain(issue1);
                result.PostedIssues.ShouldContain(issue3);
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) of type ProviderTypeA were filtered to match the maximum of 1 issues which should be reported for each issue provider");
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) of type ProviderTypeB were filtered to match the maximum of 1 issues which should be reported for each issue provider");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 2 issue(s):"));
            }

            [Fact]
            public void Should_Limit_Messages_To_Maximum_Per_Issue_Provider_By_Priority()
            {
                // Given
                var issue1 =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        10,
                        "Message Foo",
                        0,
                        "Warning",
                        "Rule Foo",
                        "ProviderTypeA",
                        "ProviderNameA");
                var issue2 =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        12,
                        "Message Foo",
                        1,
                        "Error",
                        "Rule Foo",
                        "ProviderTypeA",
                        "ProviderNameA");
                var issue3 =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        10,
                        "Message Bar",
                        1,
                        "Error",
                        "Rule Bar",
                        "ProviderTypeB",
                        "ProviderNameB");
                var issue4 =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        12,
                        "Message Bar",
                        0,
                        "Warning",
                        "Rule Bar",
                        "ProviderTypeB",
                        "ProviderNameB");
                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            issue1, issue2, issue3, issue4
                        }));

                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });

                fixture.ReportIssuesToPullRequestSettings.MaxIssuesToPostForEachIssueProvider = 1;

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(4);
                result.PostedIssues.Count().ShouldBe(2);
                result.PostedIssues.ShouldContain(issue2);
                result.PostedIssues.ShouldContain(issue3);
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) of type ProviderTypeA were filtered to match the maximum of 1 issues which should be reported for each issue provider");
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) of type ProviderTypeB were filtered to match the maximum of 1 issues which should be reported for each issue provider");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 2 issue(s):"));
            }

            [Fact]
            public void Should_Limit_Messages_To_Maximum_Per_Issue_Provider_By_FilePath()
            {
                // Given
                var issue1 =
                    new Issue(
                        null,
                        null,
                        "Message Foo",
                        0,
                        "Warning",
                        "Rule Foo",
                        "ProviderTypeA",
                        "ProviderNameA");
                var issue2 =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        12,
                        "Message Foo",
                        0,
                        "Warning",
                        "Rule Foo",
                        "ProviderTypeA",
                        "ProviderNameA");
                var issue3 =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        10,
                        "Message Bar",
                        0,
                        "Warning",
                        "Rule Bar",
                        "ProviderTypeB",
                        "ProviderNameB");
                var issue4 =
                    new Issue(
                        null,
                        null,
                        "Message Bar",
                        0,
                        "Warning",
                        "Rule Bar",
                        "ProviderTypeB",
                        "ProviderNameB");
                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            issue1, issue2, issue3, issue4
                        }));

                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });

                fixture.ReportIssuesToPullRequestSettings.MaxIssuesToPostForEachIssueProvider = 1;

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(4);
                result.PostedIssues.Count().ShouldBe(2);
                result.PostedIssues.ShouldContain(issue2);
                result.PostedIssues.ShouldContain(issue3);
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) of type ProviderTypeA were filtered to match the maximum of 1 issues which should be reported for each issue provider");
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) of type ProviderTypeB were filtered to match the maximum of 1 issues which should be reported for each issue provider");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 2 issue(s):"));
            }

            [Fact]
            public void Should_Log_Message_If_All_Issues_Are_Filtered()
            {
                // Given
                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            new Issue(
                                @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                10,
                                "Message Foo",
                                0,
                                "Warning",
                                "Rule Foo",
                                "ProviderType Foo",
                                "ProviderName Foo"),
                            new Issue(
                                @"src\Cake.Issues.Tests\NotModified.cs",
                                12,
                                "Message Bar",
                                0,
                                "Warning",
                                "Rule Bar",
                                "ProviderType Bar",
                                "ProviderName Bar")
                        }));

                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>());

                // When
                fixture.RunOrchestrator();

                // Then
                fixture.Log.Entries.ShouldContain(x => x.Message == "All issues were filtered. Nothing new to post.");
            }

            [Fact]
            public void Should_Resolve_Closed_Issues()
            {
                // Given
                var threadToResolve =
                    new PullRequestDiscussionThread(
                        1,
                        PullRequestDiscussionStatus.Active,
                        new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs"),
                        new List<IPullRequestDiscussionComment>
                        {
                            new PullRequestDiscussionComment()
                            {
                                Content = "Bar",
                                IsDeleted = false
                            }
                        })
                    {
                        CommentSource = null
                    };

                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            new Issue(
                                @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                10,
                                "Message Foo",
                                0,
                                "Warning",
                                "Rule Foo",
                                "ProviderType Foo",
                                "ProviderName Foo")
                        }));

                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>
                        {
                            threadToResolve
                        },
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });

                // When
                fixture.RunOrchestrator();

                // Then
                fixture.PullRequestSystem.ThreadsMarkedAsFixed.ShouldContain(threadToResolve);
                fixture.Log.Entries.ShouldContain(x => x.Message == "Found 1 existing thread(s) that do not match any new issue and can be resolved.");
            }

            [Fact]
            public void Should_Post_Issue()
            {
                // Given
                var issueToPost =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        10,
                        "Message Foo",
                        0,
                        "Warning",
                        "Rule Foo",
                        "ProviderType Foo",
                        "ProviderName Foo");

                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            issueToPost
                        }));

                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });

                // When
                fixture.RunOrchestrator();

                // Then
                fixture.PullRequestSystem.PostedIssues.ShouldContain(issueToPost);
                fixture.Log.Entries.ShouldContain(
                    x =>
                        x.Message ==
                            $"Posting 1 issue(s):\n  Rule: {issueToPost.Rule} Line: {issueToPost.Line} File: {issueToPost.AffectedFileRelativePath}");
            }

            [Fact]
            public void Should_Post_Issue_Not_Related_To_A_File()
            {
                // Given
                var issueToPost =
                    new Issue(
                        null,
                        null,
                        "Message Foo",
                        0,
                        "Warning",
                        "Rule Foo",
                        "ProviderType Foo",
                        "ProviderName Foo");

                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            issueToPost
                        }));

                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });

                // When
                fixture.RunOrchestrator();

                // Then
                fixture.PullRequestSystem.PostedIssues.ShouldContain(issueToPost);
                fixture.Log.Entries.ShouldContain(
                    x =>
                        x.Message ==
                            $"Posting 1 issue(s):\n  Rule: {issueToPost.Rule} Line: {issueToPost.Line} File: {issueToPost.AffectedFileRelativePath}");
            }

            [Fact]
            public void Should_Skip_Posting_If_Commit_Is_Outdate()
            {
                // Given
                var issueToPost =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        10,
                        "Message Foo",
                        0,
                        "Warning",
                        "Rule Foo",
                        "ProviderType Foo",
                        "ProviderName Foo");

                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            issueToPost
                        }));

                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });

                fixture.PullRequestSystem.LastSourceCommitId = "9ebcec39e16c39b5ffcb10f253d0c2bcf8438cf6";
                fixture.ReportIssuesToPullRequestSettings.CommitId = "15c54be6435cfb6b6973896d7be79f1d9b7497a9";

                // When
                fixture.RunOrchestrator();

                // Then
                fixture.PullRequestSystem.PostedIssues.ShouldBeEmpty();
            }

            [Fact]
            public void Should_Return_Correct_Values()
            {
                // Given
                var reportedIssue =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        10,
                        "Message Foo",
                        0,
                        "Warning",
                        "Rule Foo",
                        "ProviderType Foo",
                        "ProviderName Foo");
                var postedIssue =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        10,
                        "Message Foo",
                        0,
                        "Warning",
                        "Rule Foo",
                        "ProviderType Foo",
                        "ProviderName Foo");
                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            postedIssue, reportedIssue
                        }));

                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        });

                fixture.ReportIssuesToPullRequestSettings.MaxIssuesToPost = 1;

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(2);
                result.ReportedIssues.ShouldContain(reportedIssue);
                result.ReportedIssues.ShouldContain(postedIssue);
                result.PostedIssues.Count().ShouldBe(1);
                result.PostedIssues.ShouldContain(postedIssue);
            }

            [Fact]
            public void Should_Return_Reported_Issues_If_PullRequestSystem_Could_Not_Be_Initialized()
            {
                // Given
                var firstIssue =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        10,
                        "Message Foo",
                        0,
                        "Warning",
                        "Rule Foo",
                        "ProviderType Foo",
                        "ProviderName Foo");
                var secondIssue =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        10,
                        "Message Foo",
                        0,
                        "Warning",
                        "Rule Foo",
                        "ProviderType Foo",
                        "ProviderName Foo");
                var fixture = new PullRequestsFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            firstIssue, secondIssue
                        }));

                fixture.PullRequestSystem =
                    new FakePullRequestSystem(
                        fixture.Log,
                        new List<IPullRequestDiscussionThread>(),
                        new List<FilePath>
                        {
                            new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")
                        })
                    {
                        ShouldFailOnInitialization = true
                    };

                fixture.ReportIssuesToPullRequestSettings.MaxIssuesToPost = 1;

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(2);
                result.ReportedIssues.ShouldContain(firstIssue);
                result.ReportedIssues.ShouldContain(secondIssue);
                result.PostedIssues.Count().ShouldBe(0);
            }
        }
    }
}