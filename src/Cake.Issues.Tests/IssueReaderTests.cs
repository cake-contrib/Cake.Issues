﻿namespace Cake.Issues.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Issues.Testing;
    using Shouldly;
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
                    Log = null,
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
                    IssueProviders = null,
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
                    Settings = null,
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
                fixture.ReadIssues(IssueCommentFormat.Undefined);

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