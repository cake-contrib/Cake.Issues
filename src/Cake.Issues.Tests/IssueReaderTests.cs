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
            fixture.IssueProviders.ShouldAllBe(x => x.Settings == fixture.Settings);
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
            fixture.IssueProviders.ShouldAllBe(x => x.Settings == fixture.Settings);
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
            
            // Since IIssue is now immutable, we verify the returned issues
            // have the same identifiers as the original issues
            issues.Select(x => x.Identifier).ShouldContain(issue1.Identifier);
            issues.Select(x => x.Identifier).ShouldContain(issue2.Identifier);
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
            
            // Since IIssue is now immutable, we verify the returned issues
            // have the same identifiers as the original issues
            issues.Select(x => x.Identifier).ShouldContain(issue1.Identifier);
            issues.Select(x => x.Identifier).ShouldContain(issue2.Identifier);
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
            
            // Since IIssue is now immutable, we verify the returned issues
            // have the same identifiers as the original issues
            issues.Select(x => x.Identifier).ShouldContain(issue1.Identifier);
            issues.Select(x => x.Identifier).ShouldContain(issue2.Identifier);
            issues.Select(x => x.Identifier).ShouldContain(issue3.Identifier);
            issues.Select(x => x.Identifier).ShouldContain(issue4.Identifier);
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
            
            // Since IIssue is now immutable, the returned issues are new objects
            // We check that the returned issues have the same content as originals
            var returnedIssue1 = issues.First(x => x.Identifier == issue1.Identifier);
            var returnedIssue2 = issues.First(x => x.Identifier == issue2.Identifier);
            
            // Verify the Run property is set on the returned issues
            returnedIssue1.Run.ShouldBe(run);
            returnedIssue2.Run.ShouldBe(run);
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
            
            // Since IIssue is now immutable, the returned issues are new objects  
            // We check that the returned issues have the same content as originals
            var returnedIssue1 = issues.First(x => x.Identifier == issue1.Identifier);
            var returnedIssue2 = issues.First(x => x.Identifier == issue2.Identifier);
            
            // Verify the FileLink property is set on the returned issues
            returnedIssue1.FileLink.ToString()
                .ShouldBe($"{repoUrl}/blob/{branch}/{filePath1.Replace(@"\", "/")}#L{line1}-L{endLine1}");
            returnedIssue2.FileLink.ToString()
                .ShouldBe($"{repoUrl}/blob/{branch}/{filePath2.Replace(@"\", "/")}#L{line2}");
        }
    }
}