namespace Cake.Issues.PullRequests.Tests
{
    using System.Runtime.InteropServices;
    using Cake.Core.IO;

    public sealed class OrchestratorTests
    {
        public sealed class TheCtorWithIssues
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                var fixture = new OrchestratorForIssuesFixture()
                {
                    Log = null,
                };
                var issues = new List<IIssue>();

                // When
                var result = Record.Exception(() => fixture.RunOrchestrator(issues));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Issues_Are_Null()
            {
                // Given
                var fixture = new OrchestratorForIssuesFixture();
                const List<IIssue> issues = null;

                // When
                var result = Record.Exception(() => fixture.RunOrchestrator(issues));

                // Then
                result.IsArgumentNullException("issues");
            }

            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                var fixture = new OrchestratorForIssuesFixture();
                var issues = new List<IIssue> { null };

                // When
                var result = Record.Exception(() => fixture.RunOrchestrator(issues));

                // Then
                result.IsArgumentOutOfRangeException("issues");
            }

            [Fact]
            public void Should_Throw_If_Pull_Request_System_Is_Null()
            {
                // Given
                var fixture = new OrchestratorForIssuesFixture
                {
                    PullRequestSystem = null,
                };
                var issues = new List<IIssue>();

                // When
                var result = Record.Exception(() => fixture.RunOrchestrator(issues));

                // Then
                result.IsArgumentNullException("pullRequestSystem");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var fixture = new OrchestratorForIssuesFixture
                {
                    Settings = null,
                };
                var issues = new List<IIssue>();

                // When
                var result = Record.Exception(() => fixture.RunOrchestrator(issues));

                // Then
                result.IsArgumentNullException("settings");
            }

            [Fact]
            public void Should_Initialize_Pull_Request_System()
            {
                // Given
                var fixture = new OrchestratorForIssuesFixture();
                var issues = new List<IIssue>();

                // When
                _ = fixture.RunOrchestrator(issues);

                // Then
                fixture.PullRequestSystem.Settings.ShouldBe(fixture.Settings);
            }

            [Fact]
            public void Should_Not_Throw_If_Issues_Are_Empty()
            {
                // Given
                var fixture = new OrchestratorForIssuesFixture();
                var issues = new List<IIssue>();

                // When
                var result = fixture.RunOrchestrator(issues);

                // Then
                result.ReportedIssues.ShouldBeEmpty();
                result.PostedIssues.ShouldBeEmpty();
            }
        }

        public sealed class TheCtorWithIssueProviders
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                var fixture = new OrchestratorForIssueProvidersFixture()
                {
                    Log = null,
                };

                // When
                var result = Record.Exception(fixture.RunOrchestrator);

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Issue_Provider_List_Is_Null()
            {
                // Given
                var fixture = new OrchestratorForIssueProvidersFixture
                {
                    IssueProviders = null,
                };

                // When
                var result = Record.Exception(fixture.RunOrchestrator);

                // Then
                result.IsArgumentNullException("issueProviders");
            }

            [Fact]
            public void Should_Throw_If_Issue_Provider_List_Is_Empty()
            {
                // Given
                var fixture = new OrchestratorForIssueProvidersFixture();
                fixture.IssueProviders.Clear();

                // When
                var result = Record.Exception(fixture.RunOrchestrator);

                // Then
                result.IsArgumentException("issueProviders");
            }

            [Fact]
            public void Should_Throw_If_Issue_Provider_Is_Null()
            {
                // Given
                var fixture = new OrchestratorForIssueProvidersFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(null);

                // When
                var result = Record.Exception(fixture.RunOrchestrator);

                // Then
                result.IsArgumentOutOfRangeException("issueProviders");
            }

            [Fact]
            public void Should_Throw_If_Pull_Request_System_Is_Null()
            {
                // Given
                var fixture = new OrchestratorForIssueProvidersFixture
                {
                    PullRequestSystem = null,
                };

                // When
                var result = Record.Exception(fixture.RunOrchestrator);

                // Then
                result.IsArgumentNullException("pullRequestSystem");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var fixture = new OrchestratorForIssueProvidersFixture
                {
                    Settings = null,
                };

                // When
                var result = Record.Exception(fixture.RunOrchestrator);

                // Then
                result.IsArgumentNullException("settings");
            }

            [Fact]
            public void Should_Initialize_Pull_Request_System()
            {
                // Given
                var fixture = new OrchestratorForIssueProvidersFixture();

                // When
                _ = fixture.RunOrchestrator();

                // Then
                fixture.PullRequestSystem.Settings.ShouldBe(fixture.Settings);
            }
        }

        public sealed class TheRunMethod
        {
            [Fact]
            public void Should_Post_Issue()
            {
                // Given
                var issueToPost =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                        .OfRule("Rule Foo")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                var fixture = new OrchestratorForIssueProvidersFixture();

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issueToPost]));

                // When
                _ = fixture.RunOrchestrator();

                // Then
                fixture.PullRequestSystem.PostedIssues.ShouldContain(issueToPost);
                fixture.Log.Entries.ShouldContain(
                    x =>
                        x.Message ==
                            $"Posting 1 issue(s):\n  Rule: {issueToPost.RuleId} Line: {issueToPost.Line} File: {issueToPost.AffectedFileRelativePath}");
            }

            [Fact]
            public void Should_Post_Issue_Not_Related_To_A_File()
            {
                // Given
                var issueToPost =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .OfRule("Rule Foo")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                var fixture = new OrchestratorForIssueProvidersFixture();

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issueToPost]));

                // When
                _ = fixture.RunOrchestrator();

                // Then
                fixture.PullRequestSystem.PostedIssues.ShouldContain(issueToPost);
                fixture.Log.Entries.ShouldContain(
                    x =>
                        x.Message ==
                            $"Posting 1 issue(s):\n  Rule: {issueToPost.RuleId} Line: {issueToPost.Line} File: {issueToPost.AffectedFileRelativePath}");
            }

            [Fact]
            public void Should_Return_Correct_Values()
            {
                // Given
                var reportedIssue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                        .OfRule("Rule Foo")
                        .WithPriority(IssuePriority.Warning)
                        .Create();
                var postedIssue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                        .OfRule("Rule Foo")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                var fixture = new OrchestratorForIssueProvidersFixture();

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [postedIssue, reportedIssue]));

                fixture.Settings.MaxIssuesToPost = 1;

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
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                        .OfRule("Rule Foo")
                        .WithPriority(IssuePriority.Warning)
                        .Create();
                var secondIssue =
                     IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                        .OfRule("Rule Foo")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                var fixture =
                    new OrchestratorForIssueProvidersFixture
                    {
                        PullRequestSystem =
                        {
                            ShouldFailOnInitialization = true,
                        },
                    };

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [firstIssue, secondIssue]));

                fixture.Settings.MaxIssuesToPost = 1;

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(2);
                result.ReportedIssues.ShouldContain(firstIssue);
                result.ReportedIssues.ShouldContain(secondIssue);
                result.PostedIssues.ShouldBeEmpty();
            }

            [Fact]
            public void Should_Limit_Messages_To_Global_Maximum()
            {
                // Given
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

                var fixture = new OrchestratorForIssueProvidersFixture();

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2]));

                fixture.Settings.MaxIssuesToPost = 1;

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

                var fixture = new OrchestratorForIssueProvidersFixture();

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2]));

                fixture.Settings.MaxIssuesToPost = 1;

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

                var fixture = new OrchestratorForIssueProvidersFixture();

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2]));

                fixture.Settings.MaxIssuesToPost = 1;

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

                var fixture = new OrchestratorForIssueProvidersFixture();

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2, issue3, issue4]));

                fixture.Settings.MaxIssuesToPostForEachIssueProvider = 1;

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

                var fixture = new OrchestratorForIssueProvidersFixture();

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2, issue3, issue4]));

                fixture.Settings.MaxIssuesToPostForEachIssueProvider = 1;

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

                var fixture = new OrchestratorForIssueProvidersFixture();

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2, issue3, issue4]));

                fixture.Settings.MaxIssuesToPostForEachIssueProvider = 1;

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
            public void Should_Limit_Messages_To_Maximum_Per_Issue_Provider_ProviderIssueLimits()
            {
                // Given
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

                var fixture = new OrchestratorForIssueProvidersFixture();

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2, issue3, issue4]));

                fixture.Settings.ProviderIssueLimits.Add(
                    "ProviderTypeA",
                    new ProviderIssueLimits(maxIssuesToPost: 1));
                fixture.Settings.ProviderIssueLimits.Add(
                    "ProviderTypeB",
                    new ProviderIssueLimits(maxIssuesToPost: 1));

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(4);
                result.PostedIssues.Count().ShouldBe(2);
                result.PostedIssues.ShouldContain(issue1);
                result.PostedIssues.ShouldContain(issue3);
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global limit of 1 issues which should be reported for issue provider 'ProviderTypeA'");
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global limit of 1 issues which should be reported for issue provider 'ProviderTypeB'");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 2 issue(s):"));
            }

            [Fact]
            public void Should_Limit_Messages_To_Maximum_Per_Issue_Provider_By_Priority_ProviderIssueLimits()
            {
                // Given
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

                var fixture = new OrchestratorForIssueProvidersFixture();

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2, issue3, issue4]));

                fixture.Settings.ProviderIssueLimits.Add(
                    "ProviderTypeA",
                    new ProviderIssueLimits(maxIssuesToPost: 1));
                fixture.Settings.ProviderIssueLimits.Add(
                    "ProviderTypeB",
                    new ProviderIssueLimits(maxIssuesToPost: 1));

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(4);
                result.PostedIssues.Count().ShouldBe(2);
                result.PostedIssues.ShouldContain(issue2);
                result.PostedIssues.ShouldContain(issue3);
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global limit of 1 issues which should be reported for issue provider 'ProviderTypeA'");
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global limit of 1 issues which should be reported for issue provider 'ProviderTypeB'");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 2 issue(s):"));
            }

            [Fact]
            public void Should_Limit_Messages_To_Maximum_Per_Issue_Provider_By_FilePath_ProviderIssueLimits()
            {
                // Given
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

                var fixture = new OrchestratorForIssueProvidersFixture();

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2, issue3, issue4]));

                fixture.Settings.ProviderIssueLimits.Add(
                    "ProviderTypeA",
                    new ProviderIssueLimits(maxIssuesToPost: 1));
                fixture.Settings.ProviderIssueLimits.Add(
                    "ProviderTypeB",
                    new ProviderIssueLimits(maxIssuesToPost: 1));

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(4);
                result.PostedIssues.Count().ShouldBe(2);
                result.PostedIssues.ShouldContain(issue2);
                result.PostedIssues.ShouldContain(issue3);
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global limit of 1 issues which should be reported for issue provider 'ProviderTypeA'");
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global limit of 1 issues which should be reported for issue provider 'ProviderTypeB'");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 2 issue(s):"));
            }

            [Fact]
            public void Should_Limit_Messages_To_Maximum_Per_Issue_Provider_With_Different_Limits_For_Each_ProviderIssueLimits()
            {
                // Given
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

                var fixture = new OrchestratorForIssueProvidersFixture();

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2, issue3, issue4, issue5, issue6, issue7, issue8, issue9]));

                fixture.Settings.ProviderIssueLimits.Add(
                    "ProviderTypeA",
                    new ProviderIssueLimits(maxIssuesToPost: 1));
                fixture.Settings.ProviderIssueLimits.Add(
                    "ProviderTypeB",
                    new ProviderIssueLimits(maxIssuesToPost: 3));

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(9);
                result.PostedIssues.Count().ShouldBe(4);

                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global limit of 1 issues which should be reported for issue provider 'ProviderTypeA'");
                fixture.Log.Entries.ShouldContain(x => x.Message == "4 issue(s) were filtered to match the global limit of 3 issues which should be reported for issue provider 'ProviderTypeB'");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 4 issue(s):"));
            }
        }

        public sealed class TheRunMethodWithCheckingCommitIdCapability
        {
            [Fact]
            public void Should_Skip_Posting_If_Commit_Is_Outdated()
            {
                // Given
                var issueToPost =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                        .OfRule("Rule Foo")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithCheckingCommitIdCapability());

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issueToPost]));

                fixture.PullRequestSystem.CheckingCommitIdCapability.LastSourceCommitId =
                    "9ebcec39e16c39b5ffcb10f253d0c2bcf8438cf6";
                fixture.Settings.CommitId =
                    "15c54be6435cfb6b6973896d7be79f1d9b7497a9";

                // When
                var result = fixture.RunOrchestrator();

                // Then
                fixture.PullRequestSystem.PostedIssues.ShouldBeEmpty();
                result.PostedIssues.ShouldBeEmpty();
            }
        }

        public sealed class TheRunMethodWithDiscussionThreadsCapability
        {
            [Fact]
            public void Should_Limit_Messages_To_Maximum_Across_Runs()
            {
                // Given
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

                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithDiscussionThreadsCapability([]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2]));

                fixture.Settings.MaxIssuesToPostAcrossRuns = 1;

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(2);
                result.PostedIssues.Count().ShouldBe(1);
                fixture.Log.Entries.ShouldContain(x => x.Message == "0 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 1 across all runs (0 issues already posted in previous runs)");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Limit_Messages_To_Maximum_Across_Runs_By_Priority()
            {
                // Given
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

                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithDiscussionThreadsCapability([]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2]));

                fixture.Settings.MaxIssuesToPostAcrossRuns = 1;

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(2);
                result.PostedIssues.Count().ShouldBe(1);
                result.PostedIssues.ShouldContain(issue2);
                fixture.Log.Entries.ShouldContain(x => x.Message == "0 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 1 across all runs (0 issues already posted in previous runs)");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Limit_Messages_To_Maximum_Across_Runs_By_FilePath()
            {
                // Given
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

                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithDiscussionThreadsCapability([]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2]));

                fixture.Settings.MaxIssuesToPostAcrossRuns = 1;

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(2);
                result.PostedIssues.Count().ShouldBe(1);
                result.PostedIssues.ShouldContain(issue2);
                fixture.Log.Entries.ShouldContain(x => x.Message == "0 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 1 across all runs (0 issues already posted in previous runs)");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Limit_Messages_To_Maximum_Across_Runs_With_Unresolved_Issues_From_Previous_Run()
            {
                // Given
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
                var issue3 =
                    IssueBuilder
                        .NewIssue("Message Foo Bar", "ProviderType Bar", "ProviderName Bar")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 13)
                        .OfRule("Rule Bar")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithDiscussionThreadsCapability(
                                [
                                    new PullRequestDiscussionThread(
                                        1,
                                        PullRequestDiscussionStatus.Active,
                                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                        [
                                            new PullRequestDiscussionComment
                                            {
                                                Content = "Message Foo",
                                                IsDeleted = false,
                                            },
                                        ])
                                    {
                                        CommentIdentifier = "Message Foo",
                                    },
                                    new PullRequestDiscussionThread(
                                        2,
                                        PullRequestDiscussionStatus.Active,
                                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                        [
                                            new PullRequestDiscussionComment
                                            {
                                                Content = "Message Bar",
                                                IsDeleted = false,
                                            },
                                        ])
                                    {
                                        CommentIdentifier = "Message Bar",
                                    },
                                ]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2, issue3]));

                fixture.Settings.MaxIssuesToPostAcrossRuns = 2;

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(3);
                result.PostedIssues.Count().ShouldBe(0);
                fixture.Log.Entries.ShouldContain(x => x.Message == "2 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 2 across all runs (2 issues already posted in previous runs)");
                fixture.Log.Entries.ShouldContain(x => x.Message == "All issues were filtered. Nothing new to post.");
            }

            [Fact]
            public void Should_Limit_Messages_To_Maximum_Across_Runs_With_Unresolved_Issues_From_Previous_Run_With_Multiple_Comments()
            {
                // Given
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

                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithDiscussionThreadsCapability(
                                [
                                    new PullRequestDiscussionThread(
                                        1,
                                        PullRequestDiscussionStatus.Active,
                                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                        [
                                            new PullRequestDiscussionComment
                                            {
                                                Content = "Message FooBar",
                                                IsDeleted = false,
                                            },
                                            new PullRequestDiscussionComment
                                            {
                                                Content = "Additional comment",
                                                IsDeleted = false,
                                            },
                                        ]),
                                ]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2]));

                fixture.Settings.MaxIssuesToPostAcrossRuns = 2;

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(2);
                result.PostedIssues.Count().ShouldBe(1);
                result.PostedIssues.ShouldContain(issue1);
                fixture.Log.Entries.ShouldContain(x => x.Message == "0 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 2 across all runs (1 issues already posted in previous runs)");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Limit_Messages_To_Maximum_Across_Runs_With_Resolved_Issues_From_Previous_Run()
            {
                // Given
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

                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithDiscussionThreadsCapability(
                                [
                                    new PullRequestDiscussionThread(
                                        1,
                                        PullRequestDiscussionStatus.Active,
                                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                        [
                                            new PullRequestDiscussionComment
                                            {
                                                Content = "Message FooBar",
                                                IsDeleted = false,
                                            },
                                        ]),
                                ]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2]));

                fixture.Settings.MaxIssuesToPostAcrossRuns = 2;

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(2);
                result.PostedIssues.Count().ShouldBe(1);
                result.PostedIssues.ShouldContain(issue1);
                fixture.Log.Entries.ShouldContain(x => x.Message == "0 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 2 across all runs (1 issues already posted in previous runs)");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Limit_Messages_To_Maximum_Across_Runs_For_A_Specific_Provider()
            {
                // Given
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
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                var issue3 =
                    IssueBuilder
                        .NewIssue("Message Bar", "ProviderType Bar", "ProviderName Bar")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                        .OfRule("Rule Bar")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithDiscussionThreadsCapability([]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2, issue3]));

                fixture.Settings.ProviderIssueLimits.Add(
                    "ProviderType Foo",
                    new ProviderIssueLimits(maxIssuesToPostAcrossRuns: 1));

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(3);
                result.PostedIssues.Count().ShouldBe(2);
                fixture.Log.Entries.ShouldContain(x => x.Message == "0 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 1 across all runs for provider 'ProviderType Foo' (0 issues already posted in previous runs)");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 2 issue(s):"));
            }

            [Fact]
            public void Should_Limit_Messages_To_Maximum_Across_Runs_For_A_Specific_Provider_By_Priority()
            {
                // Given
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

                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithDiscussionThreadsCapability([]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2]));

                fixture.Settings.ProviderIssueLimits.Add(
                    "ProviderType Foo",
                    new ProviderIssueLimits(maxIssuesToPostAcrossRuns: 1));

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(2);
                result.PostedIssues.Count().ShouldBe(1);
                result.PostedIssues.ShouldContain(issue2);
                fixture.Log.Entries.ShouldContain(x => x.Message == "0 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 1 across all runs for provider 'ProviderType Foo' (0 issues already posted in previous runs)");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Limit_Messages_To_Maximum_Across_Runs_For_A_Specific_Provider_By_FilePath()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .OfRule("Rule Foo")
                        .WithPriority(IssuePriority.Warning)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("Message Bar", "ProviderType Foo", "ProviderName Foo")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                        .OfRule("Rule Bar")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                var issue3 =
                    IssueBuilder
                        .NewIssue("Message Bar", "ProviderType Foo", "ProviderName Foo")
                        .InFile(@"src\Cake.Issues.Tests\MyIssueProvider.cs", 12)
                        .OfRule("Rule Bar")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithDiscussionThreadsCapability([]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2, issue3]));

                fixture.Settings.ProviderIssueLimits.Add(
                    "ProviderType Foo",
                    new ProviderIssueLimits(maxIssuesToPostAcrossRuns: 1));

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(3);
                result.PostedIssues.Count().ShouldBe(1);
                result.PostedIssues.ShouldContain(issue2);
                fixture.Log.Entries.ShouldContain(x => x.Message == "0 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message == "2 issue(s) were filtered to match the global issue limit of 1 across all runs for provider 'ProviderType Foo' (0 issues already posted in previous runs)");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Limit_Messages_To_Maximum_Across_Runs_For_A_Specific_Provider_With_Unresolved_Issues_From_Previous_Run()
            {
                // Given
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
                        .WithPriority(IssuePriority.Warning)
                        .Create();
                var issue3 =
                    IssueBuilder
                        .NewIssue("Message Foo Bar", "ProviderType Foo", "ProviderName Foo")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 13)
                        .OfRule("Rule Bar")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithDiscussionThreadsCapability(
                                [
                                    new PullRequestDiscussionThread(
                                        1,
                                        PullRequestDiscussionStatus.Active,
                                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                        [
                                            new PullRequestDiscussionComment
                                            {
                                                Content = "Message Foo",
                                                IsDeleted = false,
                                            },
                                        ])
                                    {
                                        CommentIdentifier = "Message Foo",
                                        ProviderType = "ProviderType Foo",
                                    },
                                    new PullRequestDiscussionThread(
                                        2,
                                        PullRequestDiscussionStatus.Active,
                                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                        [
                                            new PullRequestDiscussionComment
                                            {
                                                Content = "Message Bar",
                                                IsDeleted = false,
                                            },
                                        ])
                                    {
                                        CommentIdentifier = "Message Bar",
                                        ProviderType = "ProviderType Foo",
                                    },
                                ]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2, issue3]));

                fixture.Settings.ProviderIssueLimits.Add(
                    "ProviderType Foo",
                    new ProviderIssueLimits(maxIssuesToPostAcrossRuns: 2));

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(3);
                result.PostedIssues.Count().ShouldBe(0);
                fixture.Log.Entries.ShouldContain(x => x.Message == "2 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 2 across all runs for provider 'ProviderType Foo' (2 issues already posted in previous runs)");
                fixture.Log.Entries.ShouldContain(x => x.Message == "All issues were filtered. Nothing new to post.");
            }

            [Fact]
            public void Should_Limit_Messages_To_Maximum_Across_Runs_For_A_Specific_Provider_With_Unresolved_Issues_From_Previous_Run_With_Multiple_Comments()
            {
                // Given
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
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithDiscussionThreadsCapability(
                                [
                                    new PullRequestDiscussionThread(
                                        1,
                                        PullRequestDiscussionStatus.Active,
                                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                        [
                                            new PullRequestDiscussionComment
                                            {
                                                Content = "Message FooBar",
                                                IsDeleted = false,
                                            },
                                            new PullRequestDiscussionComment
                                            {
                                                Content = "Additional comment",
                                                IsDeleted = false,
                                            },
                                        ])
                                    {
                                        ProviderType = "ProviderType Foo",
                                    },
                                ]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2]));

                fixture.Settings.ProviderIssueLimits.Add(
                    "ProviderType Foo",
                    new ProviderIssueLimits(maxIssuesToPostAcrossRuns: 2));

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(2);
                result.PostedIssues.Count().ShouldBe(1);
                result.PostedIssues.ShouldContain(issue1);
                fixture.Log.Entries.ShouldContain(x => x.Message == "0 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 2 across all runs for provider 'ProviderType Foo' (1 issues already posted in previous runs)");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Limit_Messages_To_Maximum_Across_Runs_For_A_Specific_Provider_With_Resolved_Issues_From_Previous_Run()
            {
                // Given
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
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithDiscussionThreadsCapability(
                                [
                                    new PullRequestDiscussionThread(
                                        1,
                                        PullRequestDiscussionStatus.Active,
                                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                        [
                                            new PullRequestDiscussionComment
                                            {
                                                Content = "Message FooBar",
                                                IsDeleted = false,
                                            },
                                        ])
                                    {
                                        ProviderType = "ProviderType Foo",
                                    },
                                ]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2]));

                fixture.Settings.ProviderIssueLimits.Add(
                    "ProviderType Foo",
                    new ProviderIssueLimits(maxIssuesToPostAcrossRuns: 2));

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(2);
                result.PostedIssues.Count().ShouldBe(1);
                result.PostedIssues.ShouldContain(issue1);
                fixture.Log.Entries.ShouldContain(x => x.Message == "0 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered to match the global issue limit of 2 across all runs for provider 'ProviderType Foo' (1 issues already posted in previous runs)");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Ignore_Issues_Already_Present_In_Active_Comment()
            {
                // Given
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
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                        .OfRule("Rule Bar")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, settings) => builder
                            .WithDiscussionThreadsCapability(
                                [
                                    new PullRequestDiscussionThread(
                                        1,
                                        PullRequestDiscussionStatus.Active,
                                        new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs"),
                                        [
                                            new PullRequestDiscussionComment()
                                            {
                                                Content = "Message Foo",
                                                IsDeleted = false,
                                            },
                                        ])
                                    {
                                        CommentIdentifier = "Message Foo",
                                        CommentSource = settings.CommentSource,
                                    },
                                ]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2]));

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(2);
                result.ReportedIssues.ShouldContain(issue1);
                result.ReportedIssues.ShouldContain(issue2);
                result.PostedIssues.Count().ShouldBe(1);
                result.PostedIssues.ShouldContain(issue2);

                fixture.PullRequestSystem.PostedIssues.Count().ShouldBe(1);
                fixture.PullRequestSystem.PostedIssues.ShouldContain(issue2);
                fixture.PullRequestSystem.DiscussionThreadsCapability.ResolvedThreads.ShouldBeEmpty();
                fixture.PullRequestSystem.DiscussionThreadsCapability.ReopenedThreads.ShouldBeEmpty();

                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Ignore_Issues_Already_Present_In_WontFix_Comment()
            {
                // Given
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
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                        .OfRule("Rule Bar")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, settings) => builder
                            .WithDiscussionThreadsCapability(
                                [
                                    new PullRequestDiscussionThread(
                                        1,
                                        PullRequestDiscussionStatus.Resolved,
                                        new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs"),
                                        [
                                            new PullRequestDiscussionComment()
                                            {
                                                Content = "Message Foo",
                                                IsDeleted = false,
                                            },
                                        ])
                                    {
                                        CommentIdentifier = "Message Foo",
                                        CommentSource = settings.CommentSource,
                                        Resolution = PullRequestDiscussionResolution.WontFix,
                                    },
                                ]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2]));

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(2);
                result.ReportedIssues.ShouldContain(issue1);
                result.ReportedIssues.ShouldContain(issue2);
                result.PostedIssues.Count().ShouldBe(1);
                result.PostedIssues.ShouldContain(issue2);

                fixture.PullRequestSystem.PostedIssues.Count().ShouldBe(1);
                fixture.PullRequestSystem.PostedIssues.ShouldContain(issue2);
                fixture.PullRequestSystem.DiscussionThreadsCapability.ResolvedThreads.ShouldBeEmpty();
                fixture.PullRequestSystem.DiscussionThreadsCapability.ReopenedThreads.ShouldBeEmpty();

                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Ignore_Issues_Already_Present_Not_Related_To_A_File()
            {
                // Given
                var issue1 =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .OfRule("Rule Foo")
                        .WithPriority(IssuePriority.Warning)
                        .Create();
                var issue2 =
                    IssueBuilder
                        .NewIssue("Message Bar", "ProviderType Bar", "ProviderName Bar")
                        .OfRule("Rule Bar")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, settings) => builder
                            .WithDiscussionThreadsCapability(
                                [
                                    new PullRequestDiscussionThread(
                                        1,
                                        PullRequestDiscussionStatus.Active,
                                        null,
                                        [
                                            new PullRequestDiscussionComment()
                                            {
                                                Content = "Message Foo",
                                                IsDeleted = false,
                                            },
                                        ])
                                    {
                                        CommentIdentifier = "Message Foo",
                                        CommentSource = settings.CommentSource,
                                    },
                                ]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2]));

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(2);
                result.ReportedIssues.ShouldContain(issue1);
                result.ReportedIssues.ShouldContain(issue2);
                result.PostedIssues.Count().ShouldBe(1);
                result.PostedIssues.ShouldContain(issue2);

                fixture.PullRequestSystem.PostedIssues.Count().ShouldBe(1);
                fixture.PullRequestSystem.PostedIssues.ShouldContain(issue2);
                fixture.PullRequestSystem.DiscussionThreadsCapability.ResolvedThreads.ShouldBeEmpty();
                fixture.PullRequestSystem.DiscussionThreadsCapability.ReopenedThreads.ShouldBeEmpty();

                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Only_Ignore_Issues_With_Same_Comment_Source()
            {
                // Given
                var issue =
                    IssueBuilder
                        .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                        .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                        .OfRule("Rule Foo")
                        .WithPriority(IssuePriority.Warning)
                        .Create();

                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithDiscussionThreadsCapability(
                                [
                                    new PullRequestDiscussionThread(
                                        1,
                                        PullRequestDiscussionStatus.Active,
                                        new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs"),
                                        [
                                            new PullRequestDiscussionComment()
                                            {
                                                Content = "Foo",
                                                IsDeleted = false,
                                            },
                                        ])
                                    {
                                        CommentIdentifier = "Foo",
                                        CommentSource = "DifferentCommentSource",
                                    },
                                ]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue]));

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(1);
                result.ReportedIssues.ShouldContain(issue);
                result.PostedIssues.Count().ShouldBe(1);
                result.PostedIssues.ShouldContain(issue);

                fixture.PullRequestSystem.PostedIssues.Count().ShouldBe(1);
                fixture.PullRequestSystem.PostedIssues.ShouldContain(issue);
                fixture.PullRequestSystem.DiscussionThreadsCapability.ResolvedThreads.ShouldBeEmpty();
                fixture.PullRequestSystem.DiscussionThreadsCapability.ReopenedThreads.ShouldBeEmpty();

                fixture.Log.Entries.ShouldContain(x => x.Message == "0 issue(s) were filtered because they were already present");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [Fact]
            public void Should_Resolve_Closed_Issues()
            {
                // Given
                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, settings) => builder
                            .WithDiscussionThreadsCapability(
                                [
                                    new PullRequestDiscussionThread(
                                        1,
                                        PullRequestDiscussionStatus.Active,
                                        new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs"),
                                        [
                                            new PullRequestDiscussionComment()
                                            {
                                                Content = "Bar",
                                                IsDeleted = false,
                                            },
                                        ])
                                    {
                                        CommentIdentifier = "Bar",
                                        CommentSource = settings.CommentSource,
                                    },
                                ]));

                var threadToResolve =
                    fixture.PullRequestSystem.DiscussionThreadsCapability.DiscussionThreads.Single();

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create(),
                        ]));

                // When
                _ = fixture.RunOrchestrator();

                // Then
                fixture.PullRequestSystem.DiscussionThreadsCapability.ResolvedThreads.ShouldContain(threadToResolve);
                fixture.Log.Entries.ShouldContain(x => x.Message == "Found 1 existing thread(s) that do not match any new issue and can be resolved.");
            }

            [Fact]
            public void Should_Only_Resolve_Issues_From_Same_Comment_Source()
            {
                // Given
                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithDiscussionThreadsCapability(
                                [
                                    new PullRequestDiscussionThread(
                                        1,
                                        PullRequestDiscussionStatus.Active,
                                        new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs"),
                                        [
                                            new PullRequestDiscussionComment()
                                            {
                                                Content = "Bar",
                                                IsDeleted = false,
                                            },
                                        ])
                                    {
                                        CommentIdentifier = "Bar",
                                        CommentSource = "DifferentCommentSource",
                                    },
                                ]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create(),
                        ]));

                // When
                _ = fixture.RunOrchestrator();

                // Then
                fixture.PullRequestSystem.DiscussionThreadsCapability.ResolvedThreads.ShouldBeEmpty();
            }

            [Fact]
            public void Should_Reopen_Still_Active_Issues()
            {
                // Given
                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, settings) => builder
                            .WithDiscussionThreadsCapability(
                                [
                                    new PullRequestDiscussionThread(
                                        1,
                                        PullRequestDiscussionStatus.Resolved,
                                        new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs"),
                                        [
                                            new PullRequestDiscussionComment()
                                            {
                                                Content = "Message Foo",
                                                IsDeleted = false,
                                            },
                                        ])
                                    {
                                        CommentIdentifier = "Message Foo",
                                        CommentSource = settings.CommentSource,
                                        Resolution = PullRequestDiscussionResolution.Resolved,
                                    },
                                ]));

                var threadToReopen =
                    fixture.PullRequestSystem.DiscussionThreadsCapability.DiscussionThreads.Single();

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create(),
                        ]));

                // When
                _ = fixture.RunOrchestrator();

                // Then
                fixture.PullRequestSystem.DiscussionThreadsCapability.ReopenedThreads.ShouldContain(threadToReopen);
                fixture.Log.Entries.ShouldContain(x => x.Message == "Found 1 existing thread(s) that are resolved but still have an open issue.");
            }

            [Fact]
            public void Should_Only_Reopen_Still_Active_Issues_From_Same_Comment_Source()
            {
                // Given
                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithDiscussionThreadsCapability(
                                [
                                    new PullRequestDiscussionThread(
                                        1,
                                        PullRequestDiscussionStatus.Resolved,
                                        new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs"),
                                        [
                                            new PullRequestDiscussionComment()
                                            {
                                                Content = "Message Foo",
                                                IsDeleted = false,
                                            },
                                        ])
                                    {
                                        CommentIdentifier = "Message Foo",
                                        CommentSource = "DifferentCommentSource",
                                        Resolution = PullRequestDiscussionResolution.Resolved,
                                    },
                                ]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create(),
                        ]));

                // When
                _ = fixture.RunOrchestrator();

                // Then
                fixture.PullRequestSystem.DiscussionThreadsCapability.ReopenedThreads.ShouldBeEmpty();
            }

            [Fact]
            public void Should_Not_Throw_If_No_Existing_Threads()
            {
                // Given
                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithDiscussionThreadsCapability([]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create(),
                        ]));

                // When
                _ = fixture.RunOrchestrator();

                // Then
                fixture.Log.Entries.ShouldContain(x => x.Message == "No existing threads to resolve.");
                fixture.Log.Entries.ShouldContain(x => x.Message == "No existing threads to reopen.");
            }

            [Fact]
            public void Should_Output_A_Warning_If_Unknown_Thread_Status()
            {
                // Given
                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithDiscussionThreadsCapability(
                                [
                                    new PullRequestDiscussionThread(
                                        1,
                                        PullRequestDiscussionStatus.Unknown,
                                        new FilePath(@"src\Cake.Issues.Tests\FakeIssueProvider.cs"),
                                        [
                                            new PullRequestDiscussionComment()
                                            {
                                                Content = "Message Foo",
                                                IsDeleted = false,
                                            },
                                        ])
                                    {
                                        CommentIdentifier = "Message Foo",
                                    },
                                ]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create(),
                        ]));

                // When
                _ = fixture.RunOrchestrator();

                // Then
                fixture.Log.Entries.ShouldContain(x => x.Message == "Thread has unknown status und matching comment(s) are ignored.");
            }
        }

        public sealed class TheRunMethodWithFilteringByModifiedFilesCapability
        {
            [SkippableFact]
            public void Should_Ignore_Issues_If_File_Is_Not_Modified()
            {
                // Uses Windows specific path separator.
                Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

                // Given
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

                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithFilteringByModifiedFilesCapability(
                                [new(@"src\Cake.Issues.Tests\FakeIssueProvider.cs")]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [issue1, issue2]));

                // When
                var result = fixture.RunOrchestrator();

                // Then
                result.ReportedIssues.Count().ShouldBe(2);
                result.ReportedIssues.ShouldContain(issue1);
                result.ReportedIssues.ShouldContain(issue2);
                result.PostedIssues.Count().ShouldBe(1);
                result.PostedIssues.ShouldContain(issue1);

                fixture.PullRequestSystem.PostedIssues.Count().ShouldBe(1);
                fixture.PullRequestSystem.PostedIssues.ShouldContain(issue1);

                fixture.Log.Entries.ShouldContain(x => x.Message == "1 issue(s) were filtered because they do not belong to files that were changed in this pull request");
                fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            }

            [SkippableFact]
            public void Should_Log_Message_If_All_Issues_Are_Filtered()
            {
                // Uses Windows specific path separator.
                Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

                // Given
                var fixture =
                    new OrchestratorForIssueProvidersFixture(
                        (builder, _) => builder
                            .WithFilteringByModifiedFilesCapability([]));

                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        [
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create(),
                            IssueBuilder
                                .NewIssue("Message Bar", "ProviderType Bar", "ProviderName Bar")
                                .InFile(@"src\Cake.Issues.Tests\NotModified.cs", 10)
                                .OfRule("Rule Bar")
                                .WithPriority(IssuePriority.Warning)
                                .Create(),
                        ]));

                // When
                _ = fixture.RunOrchestrator();

                // Then
                fixture.Log.Entries.ShouldContain(x => x.Message == "All issues were filtered. Nothing new to post.");
            }
        }
    }
}
