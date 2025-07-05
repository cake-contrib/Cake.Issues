namespace Cake.Issues.Tests;

public sealed class IssueReaderTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given
            var fixture = new IssuesFixture
            {
                Log = null,
            };

            // When
            var result = Record.Exception(fixture.ReadIssues);

            // Then
            result.IsArgumentNullException("log");
        }

        [Fact]
        public void Should_Throw_If_Issue_Provider_List_Is_Null()
        {
            // Given
            var fixture = new IssuesFixture
            {
                IssueProviders = null,
            };

            // When
            var result = Record.Exception(fixture.ReadIssues);

            // Then
            result.IsArgumentNullException("issueProviders");
        }

        [Fact]
        public void Should_Throw_If_Issue_Provider_List_Is_Empty()
        {
            // Given
            var fixture = new IssuesFixture();
            fixture.IssueProviders.Clear();

            // When
            var result = Record.Exception(fixture.ReadIssues);

            // Then
            result.IsArgumentException("issueProviders");
        }

        [Fact]
        public void Should_Throw_If_Issue_Provider_Is_Null()
        {
            // Given
            var fixture = new IssuesFixture();
            fixture.IssueProviders.Clear();
            fixture.IssueProviders.Add(null);

            // When
            var result = Record.Exception(fixture.ReadIssues);

            // Then
            result.IsArgumentOutOfRangeException("issueProviders");
        }

        [Fact]
        public void Should_Throw_If_Settings_Are_Null()
        {
            // Given
            var fixture = new IssuesFixture
            {
                Settings = null,
            };

            // When
            var result = Record.Exception(fixture.ReadIssues);

            // Then
            result.IsArgumentNullException("settings");
        }
    }

    public sealed class TheReadIssuesMethod
    {
        [Fact]
        public void Should_Initialize_Issue_Provider()
        {
            // Given
            var fixture = new IssuesFixture();

            // When
            _ = fixture.ReadIssues();

            // Then
            fixture.IssueProviders.OfType<FakeIssueProvider>().ShouldAllBe(x => x.Settings == fixture.Settings);
        }

        [Fact]
        public void Should_Initialize_All_Issue_Provider()
        {
            // Given
            var fixture = new IssuesFixture();
            fixture.IssueProviders.Clear();
            fixture.IssueProviders.Add(
                new FakeIssueProvider(
                    fixture.Log,
                    [
                        IssueBuilder
                            .NewIssue("Foo", "ProviderTypeFoo", "ProviderNameFoo")
                            .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                            .OfRule("Foo")
                            .WithPriority(IssuePriority.Warning)
                            .Create(),
                        IssueBuilder
                            .NewIssue("Bar", "ProviderTypeBar", "ProviderNameBar")
                            .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                            .OfRule("Bar")
                            .WithPriority(IssuePriority.Warning)
                            .Create(),
                    ]));
            fixture.IssueProviders.Add(
                new FakeIssueProvider(
                    fixture.Log,
                    [
                        IssueBuilder
                            .NewIssue("Foo", "ProviderTypeFoo", "ProviderNameFoo")
                            .InFile(@"src\Cake.Issues.Tests\Foo.cs", 5)
                            .OfRule("Foo")
                            .WithPriority(IssuePriority.Warning)
                            .Create(),
                        IssueBuilder
                            .NewIssue("Bar", "ProviderTypeBar", "ProviderNameBar")
                            .InFile(@"src\Cake.Issues.Tests\Bar.cs", 7)
                            .OfRule("Bar")
                            .WithPriority(IssuePriority.Warning)
                            .Create(),
                    ]));

            // When
            _ = fixture.ReadIssues();

            // Then
            fixture.IssueProviders.OfType<FakeIssueProvider>().ShouldAllBe(x => x.Settings == fixture.Settings);
        }

        [Fact]
        public void Should_Read_Correct_Number_Of_Issues()
        {
            // Given
            var issue1 =
                IssueBuilder
                    .NewIssue("Foo", "ProviderTypeFoo", "ProviderNameFoo")
                    .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                    .OfRule("Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create();
            var issue2 =
                IssueBuilder
                    .NewIssue("Bar", "ProviderTypeBar", "ProviderNameBar")
                    .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                    .OfRule("Bar")
                    .WithPriority(IssuePriority.Warning)
                    .Create();
            var fixture = new IssuesFixture();
            fixture.IssueProviders.Clear();
            fixture.IssueProviders.Add(
                new FakeIssueProvider(
                    fixture.Log,
                    [issue1, issue2]));

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(2);
            issues.ShouldContain(issue1);
            issues.ShouldContain(issue2);
        }

        [Fact]
        public void Should_Read_Correct_Number_Of_Issues_Not_Related_To_A_File()
        {
            // Given
            var issue1 =
                IssueBuilder
                    .NewIssue("Foo", "ProviderTypeFoo", "ProviderNameFoo")
                    .OfRule("Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create();
            var issue2 =
                IssueBuilder
                    .NewIssue("Bar", "ProviderTypeBar", "ProviderNameBar")
                    .OfRule("Bar")
                    .WithPriority(IssuePriority.Warning)
                    .Create();
            var fixture = new IssuesFixture();
            fixture.IssueProviders.Clear();
            fixture.IssueProviders.Add(
                new FakeIssueProvider(
                    fixture.Log,
                    [issue1, issue2]));

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(2);
            issues.ShouldContain(issue1);
            issues.ShouldContain(issue2);
        }

        [Fact]
        public void Should_Read_Correct_Number_Of_Issues_From_Multiple_Providers()
        {
            // Given
            var issue1 =
                IssueBuilder
                    .NewIssue("Foo", "ProviderTypeFoo", "ProviderNameFoo")
                    .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                    .OfRule("Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create();
            var issue2 =
                IssueBuilder
                    .NewIssue("Bar", "ProviderTypeBar", "ProviderNameBar")
                    .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                    .OfRule("Bar")
                    .WithPriority(IssuePriority.Warning)
                    .Create();
            var issue3 =
                IssueBuilder
                    .NewIssue("Foo", "ProviderTypeFoo", "ProviderNameFoo")
                    .InFile(@"src\Cake.Issues.Tests\Foo.cs", 5)
                    .OfRule("Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create();
            var issue4 =
                IssueBuilder
                    .NewIssue("Bar", "ProviderTypeBar", "ProviderNameBar")
                    .InFile(@"src\Cake.Issues.Tests\Bar.cs", 5)
                    .OfRule("Bar")
                    .WithPriority(IssuePriority.Warning)
                    .Create();
            var fixture = new IssuesFixture();
            fixture.IssueProviders.Clear();
            fixture.IssueProviders.Add(
                new FakeIssueProvider(
                    fixture.Log,
                    [issue1, issue2]));
            fixture.IssueProviders.Add(
                new FakeIssueProvider(
                    fixture.Log,
                    [issue3, issue4]));

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(4);
            issues.ShouldContain(issue1);
            issues.ShouldContain(issue2);
            issues.ShouldContain(issue3);
            issues.ShouldContain(issue4);
        }

        [Fact]
        public void Should_Set_Run_Property()
        {
            // Given
            var issue1 =
                IssueBuilder
                    .NewIssue("Foo", "ProviderTypeFoo", "ProviderNameFoo")
                    .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                    .OfRule("Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create();
            var issue2 =
                IssueBuilder
                    .NewIssue("Bar", "ProviderTypeBar", "ProviderNameBar")
                    .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                    .OfRule("Bar")
                    .WithPriority(IssuePriority.Warning)
                    .Create();
            var fixture = new IssuesFixture();
            fixture.IssueProviders.Clear();
            fixture.IssueProviders.Add(
                new FakeIssueProvider(
                    fixture.Log,
                    [issue1, issue2]));
            var run = "Run";
            fixture.Settings.Run = run;

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(2);
            issues.ShouldContain(issue1);
            issue1.Run.ShouldBe(run);
            issues.ShouldContain(issue2);
            issue2.Run.ShouldBe(run);
        }

        [Fact]
        public void Should_Set_FileLink_Property()
        {
            // Given
            var filePath1 = @"src\Cake.Issues.Tests\Foo.cs";
            var line1 = 10;
            var endLine1 = 12;
            var issue1 =
                IssueBuilder
                    .NewIssue("Foo", "ProviderTypeFoo", "ProviderNameFoo")
                    .InFile(filePath1, line1, endLine1, 1, 1)
                    .OfRule("Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create();
            var filePath2 = @"src\Cake.Issues.Tests\Bar.cs";
            var line2 = 12;
            var issue2 =
                IssueBuilder
                    .NewIssue("Bar", "ProviderTypeBar", "ProviderNameBar")
                    .InFile(filePath2, line2)
                    .OfRule("Bar")
                    .WithPriority(IssuePriority.Warning)
                    .Create();
            var fixture = new IssuesFixture();
            fixture.IssueProviders.Clear();
            fixture.IssueProviders.Add(
                new FakeIssueProvider(
                    fixture.Log,
                    [issue1, issue2]));
            var repoUrl = "https://github.com/cake-contrib/Cake.Issues.Website";
            var branch = "develop";
            fixture.Settings.FileLinkSettings =
                FileLinkSettings.ForGitHub(new Uri(repoUrl)).Branch(branch);

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(2);
            issues.ShouldContain(issue1);
            issue1.FileLink.ToString()
                .ShouldBe($"{repoUrl}/blob/{branch}/{filePath1.Replace(@"\", "/")}#L{line1}-L{endLine1}");
            issues.ShouldContain(issue2);
            issue2.FileLink.ToString()
                .ShouldBe($"{repoUrl}/blob/{branch}/{filePath2.Replace(@"\", "/")}#L{line2}");
        }

        [Fact]
        public void Should_Read_Issues_From_Multiple_Providers_Concurrently()
        {
            // Given
            const int providerCount = 10;
            const int issuesPerProvider = 100;
            var fixture = new IssuesFixture();
            fixture.IssueProviders.Clear();

            // Create multiple providers with different issues
            for (var i = 0; i < providerCount; i++)
            {
                var providerIssues = new List<IIssue>();
                for (var j = 0; j < issuesPerProvider; j++)
                {
                    var issue = IssueBuilder
                        .NewIssue($"Issue{i}-{j}", $"ProviderType{i}", $"ProviderName{i}")
                        .InFile($@"src\Provider{i}\File{j}.cs", j + 1)
                        .OfRule($"Rule{j}")
                        .WithPriority(IssuePriority.Warning)
                        .Create();
                    providerIssues.Add(issue);
                }
                fixture.IssueProviders.Add(new FakeIssueProvider(fixture.Log, providerIssues));
            }

            // When
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var issues = fixture.ReadIssues().ToList();
            stopwatch.Stop();

            // Then
            issues.Count.ShouldBe(providerCount * issuesPerProvider);

            // Verify all providers contributed issues
            for (var i = 0; i < providerCount; i++)
            {
                var providerIssues = issues.Where(x => x.ProviderType == $"ProviderType{i}").ToList();
                providerIssues.Count.ShouldBe(issuesPerProvider);
            }

            // Verify all Run properties are set
            issues.ShouldAllBe(x => x.Run == fixture.Settings.Run);

            // Log timing for performance verification
            System.Console.WriteLine($"Reading {issues.Count} issues from {providerCount} providers took {stopwatch.ElapsedMilliseconds}ms");
        }

        [Fact]
        public void Should_Handle_Provider_Initialization_Failures_Concurrently()
        {
            // Given
            var fixture = new IssuesFixture();
            fixture.IssueProviders.Clear();

            // Add mix of successful and failing providers
            fixture.IssueProviders.Add(new FakeIssueProvider(fixture.Log, [
                IssueBuilder.NewIssue("Success1", "ProviderType1", "ProviderName1")
                    .InFile(@"src\File1.cs", 1)
                    .OfRule("Rule1")
                    .WithPriority(IssuePriority.Warning)
                    .Create()
            ]));

            // Create a failing provider by passing null settings later
            var failingProvider = new FakeFailingIssueProvider(fixture.Log);
            fixture.IssueProviders.Add(failingProvider);
            fixture.IssueProviders.Add(new FakeIssueProvider(fixture.Log, [
                IssueBuilder.NewIssue("Success2", "ProviderType2", "ProviderName2")
                    .InFile(@"src\File2.cs", 2)
                    .OfRule("Rule2")
                    .WithPriority(IssuePriority.Warning)
                    .Create()
            ]));

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            // Should get issues from successful providers only
            issues.Count.ShouldBe(2);
            issues.ShouldContain(x => x.MessageText == "Success1");
            issues.ShouldContain(x => x.MessageText == "Success2");
        }

        [Fact]
        public void Should_Demonstrate_Parallel_Processing_Benefits_With_Simulated_Delays()
        {
            // Given - Create providers that simulate processing delays
            const int providerCount = 5;
            const int delayPerProviderMs = 50; // Simulate 50ms delay per provider
            var fixture = new IssuesFixture();
            fixture.IssueProviders.Clear();

            for (var i = 0; i < providerCount; i++)
            {
                var issue = IssueBuilder
                    .NewIssue($"SlowIssue{i}", $"SlowProviderType{i}", $"SlowProviderName{i}")
                    .InFile($@"src\SlowFile{i}.cs", i + 1)
                    .OfRule($"SlowRule{i}")
                    .WithPriority(IssuePriority.Warning)
                    .Create();
                fixture.IssueProviders.Add(new FakeSlowIssueProvider(fixture.Log, [issue], delayPerProviderMs));
            }

            // When
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var issues = fixture.ReadIssues().ToList();
            stopwatch.Stop();

            // Then
            issues.Count.ShouldBe(providerCount);

            // With parallel processing, total time should be significantly less than 
            // sum of all delays (providerCount * delayPerProviderMs)
            var expectedSequentialTime = providerCount * delayPerProviderMs;
            var actualTime = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"Sequential time would be ~{expectedSequentialTime}ms, parallel time was {actualTime}ms");
            actualTime.ShouldBeLessThan(expectedSequentialTime, "Parallel reading of issue providers is slower than sequential processing would be");

            // This assertion may be flaky in CI environments, so we'll use a generous threshold
            // Should be much faster than 40% of sequential time
            var maxExpectedParallelTime = expectedSequentialTime * 0.4;
            Convert.ToDouble(actualTime).ShouldBeLessThan(maxExpectedParallelTime, "Parallel reading of issue providers is more than 40% of time it took for sequential processing");
        }
    }
}