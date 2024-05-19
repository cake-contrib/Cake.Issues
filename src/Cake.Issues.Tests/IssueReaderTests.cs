namespace Cake.Issues.Tests
{
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
                        new List<IIssue>
                        {
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
                        }));
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
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
                        }));

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
                        new List<IIssue>
                        {
                            issue1,
                            issue2,
                        }));

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
                        new List<IIssue>
                        {
                            issue1,
                            issue2,
                        }));

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
                        new List<IIssue>
                        {
                            issue1,
                            issue2,
                        }));
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            issue3,
                            issue4,
                        }));

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
                        new List<IIssue>
                        {
                            issue1,
                            issue2,
                        }));
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
                        new List<IIssue>
                        {
                            issue1,
                            issue2,
                        }));
                var repoUrl = "https://github.com/cake-contrib/Cake.Issues.Website";
                var branch = "develop";
                fixture.Settings.FileLinkSettings =
                    FileLinkSettings.ForGitHub(new System.Uri(repoUrl)).Branch(branch);

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
        }
    }
}