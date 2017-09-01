namespace Cake.Issues.Tests.IssueProvider
{
    using System;
    using Issues.IssueProvider;
    using Shouldly;
    using Testing;
    using Xunit;

    public class IssueOfTTests
    {
        public sealed class TheCtor
        {
            [Theory]
            [InlineData(@"foo<bar")]
            public void Should_Throw_If_File_Path_Is_Invalid(string filePath)
            {
                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(filePath, 100, "Foo", 1, "Bar"));

                // Then
                result.IsArgumentException("filePath");
            }

            [Theory]
            [InlineData(@"c:\src\foo.cs")]
            [InlineData(@"/foo")]
            [InlineData(@"\foo")]
            public void Should_Throw_If_File_Path_Is_Absolute(string filePath)
            {
                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(filePath, 100, "Foo", 1, "Bar"));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Negative()
            {
                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(@"src\foo.cs", -1, "Foo", 1, "Bar"));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Zero()
            {
                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(@"src\foo.cs", 0, "Foo", 1, "Bar"));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Set_But_No_File()
            {
                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(null, 10, "Foo", 1, "Bar"));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_Null()
            {
                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(@"src\foo.cs", 100, null, 1, "Bar"));

                // Then
                result.IsArgumentNullException("message");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_Empty()
            {
                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(@"src\foo.cs", 100, string.Empty, 1, "Bar"));

                // Then
                result.IsArgumentOutOfRangeException("message");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_WhiteSpace()
            {
                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(@"src\foo.cs", 100, " ", 1, "Bar"));

                // Then
                result.IsArgumentOutOfRangeException("message");
            }

            [Fact]
            public void Should_Throw_If_Rule_Is_Null()
            {
                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(@"src\foo.cs", 100, "foo", 1, null));

                // Then
                result.IsArgumentNullException("rule");
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_Null()
            {
                // Given / When
                var issue = new Issue<FakeIssueProvider>(null, null, "Foo", 1, "Bar");

                // Then
                issue.AffectedFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_Empty()
            {
                // Given / When
                var issue = new Issue<FakeIssueProvider>(string.Empty, null, "Foo", 1, "Bar");

                // Then
                issue.AffectedFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_WhiteSpace()
            {
                // Given / When
                var issue = new Issue<FakeIssueProvider>(" ", null, "Foo", 1, "Bar");

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
                // Given / When
                var issue = new Issue<FakeIssueProvider>(filePath, 100, "Foo", 1, "Bar");

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
                // Given / When
                var issue = new Issue<FakeIssueProvider>(@"foo.cs", line, "Foo", 1, "Bar");

                // Then
                issue.Line.ShouldBe(line);
            }

            [Theory]
            [InlineData("message")]
            public void Should_Set_Message(string message)
            {
                // Given / When
                var issue = new Issue<FakeIssueProvider>(@"foo.cs", 100, message, 1, "Bar");

                // Then
                issue.Message.ShouldBe(message);
            }

            [Theory]
            [InlineData("rule")]
            public void Should_Set_Rule(string rule)
            {
                // Given / When
                var issue = new Issue<FakeIssueProvider>(@"foo.cs", 100, "foo", 1, rule);

                // Then
                issue.Rule.ShouldBe(rule);
            }

            [Fact]
            public void Should_Set_ProviderType()
            {
                // Given / When
                var issue = new Issue<FakeIssueProvider>(@"foo.cs", 100, "foo", 1, "foo");

                // Then
                issue.ProviderType.ShouldBe("Cake.Issues.Testing.FakeIssueProvider");
            }
        }

        public sealed class TheIssueOfTCtorWithRuleId
        {
            [Theory]
            [InlineData(@"foo<bar")]
            public void Should_Throw_If_File_Path_Is_Invalid(string filePath)
            {
                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(filePath, 100, "Foo", 1, "Bar", new Uri("https://google.com")));

                // Then
                result.IsArgumentException("filePath");
            }

            [Theory]
            [InlineData(@"c:\src\foo.cs")]
            [InlineData(@"/foo")]
            [InlineData(@"\foo")]
            public void Should_Throw_If_File_Path_Is_Absolute(string filePath)
            {
                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(filePath, 100, "Foo", 1, "Bar", new Uri("https://google.com")));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Negative()
            {
                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(@"src\foo.cs", -1, "Foo", 1, "Bar", new Uri("https://google.com")));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Zero()
            {
                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(@"src\foo.cs", 0, "Foo", 1, "Bar", new Uri("https://google.com")));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Set_But_No_File()
            {
                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(null, 10, "Foo", 1, "Bar", new Uri("https://google.com")));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_Null()
            {
                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(@"src\foo.cs", 100, null, 1, "Bar", new Uri("https://google.com")));

                // Then
                result.IsArgumentNullException("message");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_Empty()
            {
                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(@"src\foo.cs", 100, string.Empty, 1, "Bar", new Uri("https://google.com")));

                // Then
                result.IsArgumentOutOfRangeException("message");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_WhiteSpace()
            {
                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(@"src\foo.cs", 100, " ", 1, "Bar", new Uri("https://google.com")));

                // Then
                result.IsArgumentOutOfRangeException("message");
            }

            [Fact]
            public void Should_Throw_If_Rule_Is_Null()
            {
                // Given / When
                var result =
                    Record.Exception(() =>
                        new Issue<FakeIssueProvider>(@"src\foo.cs", 100, "foo", 1, null, new Uri("https://google.com")));

                // Then
                result.IsArgumentNullException("rule");
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_Null()
            {
                // Given / When
                var issue = new Issue<FakeIssueProvider>(null, null, "Foo", 1, "Bar", new Uri("https://google.com"));

                // Then
                issue.AffectedFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_Empty()
            {
                // Given / When
                var issue = new Issue<FakeIssueProvider>(string.Empty, null, "Foo", 1, "Bar", new Uri("https://google.com"));

                // Then
                issue.AffectedFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_WhiteSpace()
            {
                // Given / When
                var issue = new Issue<FakeIssueProvider>(" ", null, "Foo", 1, "Bar", new Uri("https://google.com"));

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
                // Given / When
                var issue = new Issue<FakeIssueProvider>(filePath, 100, "Foo", 1, "Bar", new Uri("https://google.com"));

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
                // Given / When
                var issue = new Issue<FakeIssueProvider>(@"foo.cs", line, "Foo", 1, "Bar", new Uri("https://google.com"));

                // Then
                issue.Line.ShouldBe(line);
            }

            [Theory]
            [InlineData("message")]
            public void Should_Set_Message(string message)
            {
                // Given / When
                var issue = new Issue<FakeIssueProvider>(@"foo.cs", 100, message, 1, "Bar", new Uri("https://google.com"));

                // Then
                issue.Message.ShouldBe(message);
            }

            [Theory]
            [InlineData("rule")]
            public void Should_Set_Rule(string rule)
            {
                // Given / When
                var issue = new Issue<FakeIssueProvider>(@"foo.cs", 100, "foo", 1, rule, new Uri("https://google.com"));

                // Then
                issue.Rule.ShouldBe(rule);
            }

            [Fact]
            public void Should_Set_Rule_Url()
            {
                // Given
                var ruleUrl = new Uri("http://google.com");

                // When
                var issue = new Issue<FakeIssueProvider>(@"foo.cs", 100, "foo", 1, "foo", ruleUrl);

                // Then
                issue.RuleUrl.ShouldBe(ruleUrl);
            }

            [Fact]
            public void Should_Set_Rule_Url_If_Null()
            {
                // Given
                Uri ruleUrl = null;

                // When
                var issue = new Issue<FakeIssueProvider>(@"foo.cs", 100, "foo", 1, "foo", ruleUrl);

                // Then
                issue.RuleUrl.ShouldBe(ruleUrl);
            }

            [Fact]
            public void Should_Set_ProviderType()
            {
                // Given / When
                var issue = new Issue<FakeIssueProvider>(@"foo.cs", 100, "foo", 1, "foo", new Uri("https://google.com"));

                // Then
                issue.ProviderType.ShouldBe("Cake.Issues.Testing.FakeIssueProvider");
            }
        }
    }
}
