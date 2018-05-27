namespace Cake.Issues.Tests
{
    using System;
    using System.Linq;
    using Shouldly;
    using Testing;
    using Xunit;

    public class IssueOfTTests
    {
        public sealed class TheCtor
        {
            [Theory]
            [InlineData("foo\tbar")]
            public void Should_Throw_If_File_Path_Is_Invalid(string filePath)
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(issueProvider, filePath, 100, "Foo", 1, "Bar"));

                // Then
                result.IsArgumentException("filePath");
            }

            [Theory]
            [InlineData(@"c:\src\foo.cs")]
            [InlineData(@"/foo")]
            [InlineData(@"\foo")]
            public void Should_Throw_If_File_Path_Is_Absolute(string filePath)
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(issueProvider, filePath, 100, "Foo", 1, "Bar"));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Negative()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(issueProvider, @"src\foo.cs", -1, "Foo", 1, "Bar"));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Zero()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(issueProvider, @"src\foo.cs", 0, "Foo", 1, "Bar"));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Set_But_No_File()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(issueProvider, null, 10, "Foo", 1, "Bar"));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_Null()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(issueProvider, @"src\foo.cs", 100, null, 1, "Bar"));

                // Then
                result.IsArgumentNullException("message");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_Empty()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(issueProvider, @"src\foo.cs", 100, string.Empty, 1, "Bar"));

                // Then
                result.IsArgumentOutOfRangeException("message");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_WhiteSpace()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(issueProvider, @"src\foo.cs", 100, " ", 1, "Bar"));

                // Then
                result.IsArgumentOutOfRangeException("message");
            }

            [Fact]
            public void Should_Handle_Projects_Which_Are_Null()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, null, @"src\foo.cs", null, "Foo", 1, "Bar");

                // Then
                issue.Project.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_Projects_Which_Are_WhiteSpace()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, " ", @"src\foo.cs", null, "Foo", 1, "Bar");

                // Then
                issue.Project.ShouldBe(" ");
            }

            [Fact]
            public void Should_Handle_Projects_Which_Are_Empty()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, string.Empty, @"src\foo.cs", null, "Foo", 1, "Bar");

                // Then
                issue.Project.ShouldBe(string.Empty);
            }

            [Theory]
            [InlineData("project")]
            public void Should_Set_Project(string project)
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, project, @"src\foo.cs", null, "Foo", 1, "Bar");

                // Then
                issue.Project.ShouldBe(project);
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_Null()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, null, null, "Foo", 1, "Bar");

                // Then
                issue.AffectedFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_Empty()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, string.Empty, null, "Foo", 1, "Bar");

                // Then
                issue.AffectedFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_WhiteSpace()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, " ", null, "Foo", 1, "Bar");

                // Then
                issue.AffectedFileRelativePath.ShouldBe(null);
            }

            [Theory]
            [InlineData(@"foo", @"foo")]
            [InlineData(@"foo\bar", @"foo/bar")]
            [InlineData(@"foo/bar", @"foo/bar")]
            [InlineData(@"foo\bar\", @"foo/bar")]
            [InlineData(@"foo/bar/", @"foo/bar")]
            [InlineData(@".\foo", @"foo")]
            [InlineData(@"./foo", @"foo")]
            [InlineData(@"foo\..\bar", @"foo/../bar")]
            [InlineData(@"foo/../bar", @"foo/../bar")]
            public void Should_Set_File_Path(string filePath, string expectedFilePath)
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, filePath, 100, "Foo", 1, "Bar");

                // Then
                issue.AffectedFileRelativePath.ToString().ShouldBe(expectedFilePath);
                issue.AffectedFileRelativePath.IsRelative.ShouldBe(true, "File path was not set as relative.");
            }

            [Theory]
            [InlineData(null)]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Set_Line(int? line)
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, @"foo.cs", line, "Foo", 1, "Bar");

                // Then
                issue.Line.ShouldBe(line);
            }

            [Theory]
            [InlineData("message")]
            public void Should_Set_Message(string message)
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, @"foo.cs", 100, message, 1, "Bar");

                // Then
                issue.Message.ShouldBe(message);
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("rule")]
            public void Should_Set_Rule(string rule)
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, @"foo.cs", 100, "foo", 1, rule);

                // Then
                issue.Rule.ShouldBe(rule);
            }

            [Fact]
            public void Should_Set_ProviderType()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // Given
                var issue = new Issue<FakeIssueProvider>(issueProvider, @"foo.cs", 100, "foo", 1, "foo");

                // Then
                issue.ProviderType.ShouldBe("Cake.Issues.Testing.FakeIssueProvider");
            }
        }

        public sealed class TheIssueOfTCtorWithRuleId
        {
            [Theory]
            [InlineData("foo\tbar")]
            public void Should_Throw_If_File_Path_Is_Invalid(string filePath)
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(issueProvider, filePath, 100, "Foo", 1, "Bar", new Uri("https://google.com")));

                // Then
                result.IsArgumentException("filePath");
            }

            [Theory]
            [InlineData(@"c:\src\foo.cs")]
            [InlineData(@"/foo")]
            [InlineData(@"\foo")]
            public void Should_Throw_If_File_Path_Is_Absolute(string filePath)
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(issueProvider, filePath, 100, "Foo", 1, "Bar", new Uri("https://google.com")));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Negative()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(issueProvider, @"src\foo.cs", -1, "Foo", 1, "Bar", new Uri("https://google.com")));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Zero()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(issueProvider, @"src\foo.cs", 0, "Foo", 1, "Bar", new Uri("https://google.com")));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Set_But_No_File()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(issueProvider, null, 10, "Foo", 1, "Bar", new Uri("https://google.com")));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_Null()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(issueProvider, @"src\foo.cs", 100, null, 1, "Bar", new Uri("https://google.com")));

                // Then
                result.IsArgumentNullException("message");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_Empty()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(issueProvider, @"src\foo.cs", 100, string.Empty, 1, "Bar", new Uri("https://google.com")));

                // Then
                result.IsArgumentOutOfRangeException("message");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_WhiteSpace()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(issueProvider, @"src\foo.cs", 100, " ", 1, "Bar", new Uri("https://google.com")));

                // Then
                result.IsArgumentOutOfRangeException("message");
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_Null()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, null, null, "Foo", 1, "Bar", new Uri("https://google.com"));

                // Then
                issue.AffectedFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_Empty()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, string.Empty, null, "Foo", 1, "Bar", new Uri("https://google.com"));

                // Then
                issue.AffectedFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_WhiteSpace()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, " ", null, "Foo", 1, "Bar", new Uri("https://google.com"));

                // Then
                issue.AffectedFileRelativePath.ShouldBe(null);
            }

            [Theory]
            [InlineData(@"foo", @"foo")]
            [InlineData(@"foo\bar", @"foo/bar")]
            [InlineData(@"foo/bar", @"foo/bar")]
            [InlineData(@"foo\bar\", @"foo/bar")]
            [InlineData(@"foo/bar/", @"foo/bar")]
            [InlineData(@".\foo", @"foo")]
            [InlineData(@"./foo", @"foo")]
            [InlineData(@"foo\..\bar", @"foo/../bar")]
            [InlineData(@"foo/../bar", @"foo/../bar")]
            public void Should_Set_File_Path(string filePath, string expectedFilePath)
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, filePath, 100, "Foo", 1, "Bar", new Uri("https://google.com"));

                // Then
                issue.AffectedFileRelativePath.ToString().ShouldBe(expectedFilePath);
                issue.AffectedFileRelativePath.IsRelative.ShouldBe(true, "File path was not set as relative.");
            }

            [Theory]
            [InlineData(null)]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Set_Line(int? line)
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, @"foo.cs", line, "Foo", 1, "Bar", new Uri("https://google.com"));

                // Then
                issue.Line.ShouldBe(line);
            }

            [Theory]
            [InlineData("message")]
            public void Should_Set_Message(string message)
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, @"foo.cs", 100, message, 1, "Bar", new Uri("https://google.com"));

                // Then
                issue.Message.ShouldBe(message);
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("rule")]
            public void Should_Set_Rule(string rule)
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, @"foo.cs", 100, "foo", 1, rule, new Uri("https://google.com"));

                // Then
                issue.Rule.ShouldBe(rule);
            }

            [Fact]
            public void Should_Set_Rule_Url()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();
                var ruleUrl = new Uri("http://google.com");

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, @"foo.cs", 100, "foo", 1, "foo", ruleUrl);

                // Then
                issue.RuleUrl.ShouldBe(ruleUrl);
            }

            [Fact]
            public void Should_Set_Rule_Url_If_Null()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();
                Uri ruleUrl = null;

                // When
                var issue = new Issue<FakeIssueProvider>(issueProvider, @"foo.cs", 100, "foo", 1, "foo", ruleUrl);

                // Then
                issue.RuleUrl.ShouldBe(ruleUrl);
            }

            [Fact]
            public void Should_Set_ProviderType()
            {
                // Given
                var fixture = new IssuesFixture();
                var issueProvider = fixture.IssueProviders.OfType<FakeIssueProvider>().Single();

                // When
                var issue =
                    new Issue<FakeIssueProvider>(issueProvider, @"foo.cs", 100, "foo", 1, "foo", new Uri("https://google.com"));

                // Then
                issue.ProviderType.ShouldBe("Cake.Issues.Testing.FakeIssueProvider");
            }
        }

        public sealed class TheGetProviderTypeNameMethod
        {
            [Fact]
            public void Should_Return_Full_Type_Name()
            {
                // Given / When
                var result = Issue<FakeIssueProvider>.GetProviderTypeName();

                // Then
                result.ShouldBe("Cake.Issues.Testing.FakeIssueProvider");
            }
        }
    }
}