namespace Cake.Issues.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Shouldly;
    using Testing;
    using Xunit;

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
                    Log = null
                };

                // When
                var result = Record.Exception(() => fixture.ReadIssues(IssueCommentFormat.Undefined));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Issue_Provider_List_Is_Null()
            {
                // Given
                var fixture = new IssuesFixture
                {
                    IssueProviders = null
                };

                // When
                var result = Record.Exception(() => fixture.ReadIssues(IssueCommentFormat.Undefined));

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
                var result = Record.Exception(() => fixture.ReadIssues(IssueCommentFormat.Undefined));

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
                var result = Record.Exception(() => fixture.ReadIssues(IssueCommentFormat.Undefined));

                // Then
                result.IsArgumentOutOfRangeException("issueProviders");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var fixture = new IssuesFixture
                {
                    Settings = null
                };

                // When
                var result = Record.Exception(() => fixture.ReadIssues(IssueCommentFormat.Undefined));

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
                fixture.ReadIssues(IssueCommentFormat.Undefined);

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
                            new Issue(
                                @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                10,
                                "Foo",
                                0,
                                "Foo",
                                "Foo"),
                            new Issue(
                                @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                                12,
                                "Bar",
                                0,
                                "Bar",
                                "Bar")
                        }));
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            new Issue(
                                @"src\Cake.Issues.Tests\Foo.cs",
                                5,
                                "Foo",
                                0,
                                "Foo",
                                "Foo"),
                            new Issue(
                                @"src\Cake.Issues.Tests\Bar.cs",
                                7,
                                "Bar",
                                0,
                                "Bar",
                                "Bar")
                        }));

                // When
                fixture.ReadIssues(IssueCommentFormat.Undefined);

                // Then
                fixture.IssueProviders.ShouldAllBe(x => x.Settings == fixture.Settings);
            }

            [Fact]
            public void Should_Read_Correct_Number_Of_Issues()
            {
                // Given
                var issue1 =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        10,
                        "Foo",
                        0,
                        "Foo",
                        "Foo");
                var issue2 =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        12,
                        "Bar",
                        0,
                        "Bar",
                        "Bar");
                var fixture = new IssuesFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            issue1,
                            issue2
                        }));

                // When
                var issues = fixture.ReadIssues(IssueCommentFormat.Undefined).ToList();

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
                    new Issue(
                        null,
                        null,
                        "Foo",
                        0,
                        "Foo",
                        "Foo");
                var issue2 =
                    new Issue(
                        null,
                        null,
                        "Bar",
                        0,
                        "Bar",
                        "Bar");
                var fixture = new IssuesFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            issue1,
                            issue2
                        }));

                // When
                var issues = fixture.ReadIssues(IssueCommentFormat.Undefined).ToList();

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
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        10,
                        "Foo",
                        0,
                        "Foo",
                        "Foo");
                var issue2 =
                    new Issue(
                        @"src\Cake.Issues.Tests\FakeIssueProvider.cs",
                        12,
                        "Bar",
                        0,
                        "Bar",
                        "Bar");
                var issue3 =
                    new Issue(
                        @"src\Cake.Issues.Tests\Foo.cs",
                        5,
                        "Foo",
                        0,
                        "Foo",
                        "Foo");
                var issue4 =
                    new Issue(
                        @"src\Cake.Issues.Tests\Bar.cs",
                        7,
                        "Bar",
                        0,
                        "Bar",
                        "Bar");
                var fixture = new IssuesFixture();
                fixture.IssueProviders.Clear();
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            issue1,
                            issue2
                        }));
                fixture.IssueProviders.Add(
                    new FakeIssueProvider(
                        fixture.Log,
                        new List<IIssue>
                        {
                            issue3,
                            issue4
                        }));

                // When
                var issues = fixture.ReadIssues(IssueCommentFormat.Undefined).ToList();

                // Then
                issues.Count.ShouldBe(4);
                issues.ShouldContain(issue1);
                issues.ShouldContain(issue2);
                issues.ShouldContain(issue3);
                issues.ShouldContain(issue4);
            }
        }
    }
}