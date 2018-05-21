namespace Cake.Issues.Tests
{
    using System;
    using Shouldly;
    using Testing;
    using Xunit;

    public sealed class IssueTests
    {
        public sealed class TheCtor
        {
            [Theory]
            [InlineData("foo\tbar")]
            public void Should_Throw_If_File_Path_Is_Invalid(string filePath)
            {
                // Given / When
                var result = Record.Exception(() => new Issue(filePath, 100, "Foo", 1, "Bar", "foo"));

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
                var result = Record.Exception(() => new Issue(filePath, 100, "Foo", 1, "Bar", "foo"));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Negative()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", -1, "Foo", 1, "Bar", "foo"));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Zero()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 0, "Foo", 1, "Bar", "foo"));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Set_But_No_File()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(null, 10, "Foo", 1, "Bar", "foo"));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, null, 1, "Bar", "foo"));

                // Then
                result.IsArgumentNullException("message");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, string.Empty, 1, "Bar", "foo"));

                // Then
                result.IsArgumentOutOfRangeException("message");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, " ", 1, "Bar", "foo"));

                // Then
                result.IsArgumentOutOfRangeException("message");
            }

            [Fact]
            public void Should_Throw_If_Rule_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, "foo", 1, null, "foo"));

                // Then
                result.IsArgumentNullException("rule");
            }

            [Fact]
            public void Should_Throw_If_Provider_Type_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, "foo", 1, "foo", null));

                // Then
                result.IsArgumentNullException("providerType");
            }

            [Fact]
            public void Should_Throw_If_Provider_Type_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, "foo", 1, "foo", string.Empty));

                // Then
                result.IsArgumentOutOfRangeException("providerType");
            }

            [Fact]
            public void Should_Throw_If_Provider_Type_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, "foo", 1, "foo", " "));

                // Then
                result.IsArgumentOutOfRangeException("providerType");
            }

            [Fact]
            public void Should_Handle_Projects_Which_Are_Null()
            {
                // Given / When
                var issue = new Issue(null, @"src\foo.cs", null, "Foo", 1, "Bar", "foo");

                // Then
                issue.Project.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_Projects_Which_Are_WhiteSpace()
            {
                // Given / When
                var issue = new Issue(" ", @"src\foo.cs", null, "Foo", 1, "Bar", "foo");

                // Then
                issue.Project.ShouldBe(" ");
            }

            [Fact]
            public void Should_Handle_Projects_Which_Are_Empty()
            {
                // Given / When
                var issue = new Issue(string.Empty, @"src\foo.cs", null, "Foo", 1, "Bar", "foo");

                // Then
                issue.Project.ShouldBe(string.Empty);
            }

            [Theory]
            [InlineData("project")]
            public void Should_Set_Project(string project)
            {
                // Given / When
                var issue = new Issue(project, @"src\foo.cs", null, "Foo", 1, "Bar", "foo");

                // Then
                issue.Project.ShouldBe(project);
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_Null()
            {
                // Given / When
                var issue = new Issue(null, null, "Foo", 1, "Bar", "foo");

                // Then
                issue.AffectedFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_Empty()
            {
                // Given / When
                var issue = new Issue(string.Empty, null, "Foo", 1, "Bar", "foo");

                // Then
                issue.AffectedFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_WhiteSpace()
            {
                // Given / When
                var issue = new Issue(" ", null, "Foo", 1, "Bar", "foo");

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
                var issue = new Issue(filePath, 100, "Foo", 1, "Bar", "foo");

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
                var issue = new Issue(@"foo.cs", line, "Foo", 1, "Bar", "foo");

                // Then
                issue.Line.ShouldBe(line);
            }

            [Theory]
            [InlineData("message")]
            public void Should_Set_Message(string message)
            {
                // Given / When
                var issue = new Issue(@"foo.cs", 100, message, 1, "Bar", "foo");

                // Then
                issue.Message.ShouldBe(message);
            }

            [Theory]
            [InlineData("")]
            [InlineData("rule")]
            public void Should_Set_Rule(string rule)
            {
                // Given / When
                var issue = new Issue(@"foo.cs", 100, "foo", 1, rule, "foo");

                // Then
                issue.Rule.ShouldBe(rule);
            }

            [Fact]
            public void Should_Set_Rule_Url()
            {
                // Given
                var ruleUrl = new Uri("http://google.com");

                // When
                var issue = new Issue(@"foo.cs", 100, "foo", 1, "foo", ruleUrl, "foo");

                // Then
                issue.RuleUrl.ShouldBe(ruleUrl);
            }

            [Fact]
            public void Should_Set_Rule_Url_If_Null()
            {
                // Given
                Uri ruleUrl = null;

                // When
                var issue = new Issue(@"foo.cs", 100, "foo", 1, "foo", ruleUrl, "foo");

                // Then
                issue.RuleUrl.ShouldBe(ruleUrl);
            }

            [Theory]
            [InlineData("foo")]
            public void Should_Set_ProviderType(string providerType)
            {
                // Given / When
                var issue = new Issue(@"foo.cs", 100, "foo", 1, "foo", providerType);

                // Then
                issue.ProviderType.ShouldBe(providerType);
            }
        }
    }
}
