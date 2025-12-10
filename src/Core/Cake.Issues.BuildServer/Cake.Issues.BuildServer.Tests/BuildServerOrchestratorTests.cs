namespace Cake.Issues.BuildServer.Tests;

public sealed class BuildServerOrchestratorTests
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
        public void Should_Throw_If_Build_Server_System_Is_Null()
        {
            // Given
            var fixture = new OrchestratorForIssuesFixture
            {
                BuildServerSystem = null,
            };
            var issues = new List<IIssue>();

            // When
            var result = Record.Exception(() => fixture.RunOrchestrator(issues));

            // Then
            result.IsArgumentNullException("buildServerSystem");
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
        public void Should_Initialize_Build_Server_System()
        {
            // Given
            var fixture = new OrchestratorForIssuesFixture();
            var issues = new List<IIssue>();

            // When
            _ = fixture.RunOrchestrator(issues);

            // Then
            fixture.BuildServerSystem.Settings.ShouldBe(fixture.Settings);
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
        public void Should_Throw_If_Build_Server_System_Is_Null()
        {
            // Given
            var fixture = new OrchestratorForIssueProvidersFixture
            {
                BuildServerSystem = null,
            };

            // When
            var result = Record.Exception(fixture.RunOrchestrator);

            // Then
            result.IsArgumentNullException("buildServerSystem");
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
        public void Should_Initialize_Build_Server_System()
        {
            // Given
            var fixture = new OrchestratorForIssueProvidersFixture();

            // When
            _ = fixture.RunOrchestrator();

            // Then
            fixture.BuildServerSystem.Settings.ShouldBe(fixture.Settings);
        }
    }

    public sealed class TheRunMethod
    {
        [Fact]
        public Task Should_Output_Diagnostic_Information()
        {
            // Given
            var fixture = new OrchestratorForIssueProvidersFixture();
            fixture.Log.Verbosity = Core.Diagnostics.Verbosity.Diagnostic;

            // When
            _ = fixture.RunOrchestrator();

            // Then
            return Verify(fixture.Console.Output);
        }

        [Fact]
        public Task Should_Post_Issue()
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
            fixture.Log.Verbosity = Core.Diagnostics.Verbosity.Diagnostic;
            fixture.IssueProviders.Clear();
            fixture.IssueProviders.Add(
                new FakeIssueProvider(
                    fixture.Log,
                    [issueToPost]));

            // When
            _ = fixture.RunOrchestrator();

            // Then
            fixture.BuildServerSystem.PostedIssues.ShouldContain(issueToPost);
            fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            return Verify(fixture.Console.Output);
        }

        [Fact]
        public Task Should_Post_Issue_Not_Related_To_A_File()
        {
            // Given
            var issueToPost =
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .OfRule("Rule Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create();

            var fixture = new OrchestratorForIssueProvidersFixture();
            fixture.Log.Verbosity = Core.Diagnostics.Verbosity.Diagnostic;
            fixture.IssueProviders.Clear();
            fixture.IssueProviders.Add(
                new FakeIssueProvider(
                    fixture.Log,
                    [issueToPost]));

            // When
            _ = fixture.RunOrchestrator();

            // Then
            fixture.BuildServerSystem.PostedIssues.ShouldContain(issueToPost);
            fixture.Log.Entries.ShouldContain(x => x.Message.StartsWith("Posting 1 issue(s):"));
            return Verify(fixture.Console.Output);
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
        public void Should_Return_Reported_Issues_If_BuildServerSystem_Could_Not_Be_Initialized()
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
                    BuildServerSystem =
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
    }
}
