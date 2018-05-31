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
                var result = Record.Exception(() => new Issue(filePath, 100, "Foo", 1, "Warning", "Bar", "foo", "providerName"));

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
                var result = Record.Exception(() => new Issue(filePath, 100, "Foo", 1, "Warning", "Bar", "foo", "providerName"));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Negative()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", -1, "Foo", 1, "Warning", "Bar", "foo", "providerName"));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Zero()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 0, "Foo", 1, "Warning", "Bar", "foo", "providerName"));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Line_Is_Set_But_No_File()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(null, 10, "Foo", 1, "Warning", "Bar", "foo", "providerName"));

                // Then
                result.IsArgumentOutOfRangeException("line");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, null, 1, "Warning", "Bar", "foo", "providerName"));

                // Then
                result.IsArgumentNullException("message");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, string.Empty, 1, "Warning", "Bar", "foo", "providerName"));

                // Then
                result.IsArgumentOutOfRangeException("message");
            }

            [Fact]
            public void Should_Throw_If_Message_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, " ", 1, "Warning", "Bar", "foo", "providerName"));

                // Then
                result.IsArgumentOutOfRangeException("message");
            }

            [Fact]
            public void Should_Throw_If_Priority_Name_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, "foo", 1, null, "Bar", "foo", "providerName"));

                // Then
                result.IsArgumentNullException("priorityName");
            }

            [Fact]
            public void Should_Throw_If_Priority_Name_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, "foo", 1, string.Empty, "Bar", "foo", "providerName"));

                // Then
                result.IsArgumentOutOfRangeException("priorityName");
            }

            [Fact]
            public void Should_Throw_If_Priority_Name_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, "foo", 1, " ", "Bar", "foo", "providerName"));

                // Then
                result.IsArgumentOutOfRangeException("priorityName");
            }

            [Fact]
            public void Should_Throw_If_Provider_Type_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, "foo", 1, "Warning", "foo", null, "providerName"));

                // Then
                result.IsArgumentNullException("providerType");
            }

            [Fact]
            public void Should_Throw_If_Provider_Type_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, "foo", 1, "Warning", "foo", string.Empty, "providerName"));

                // Then
                result.IsArgumentOutOfRangeException("providerType");
            }

            [Fact]
            public void Should_Throw_If_Provider_Type_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, "foo", 1, "Warning", "foo", " ", "providerName"));

                // Then
                result.IsArgumentOutOfRangeException("providerType");
            }

            [Fact]
            public void Should_Throw_If_Provider_Name_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, "foo", 1, "Warning", "foo", "providerType", null));

                // Then
                result.IsArgumentNullException("providerName");
            }

            [Fact]
            public void Should_Throw_If_Provider_Name_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, "foo", 1, "Warning", "foo", "providerType", string.Empty));

                // Then
                result.IsArgumentOutOfRangeException("providerName");
            }

            [Fact]
            public void Should_Throw_If_Provider_Name_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => new Issue(@"src\foo.cs", 100, "foo", 1, "Warning", "foo", "providerType", " "));

                // Then
                result.IsArgumentOutOfRangeException("providerName");
            }

            [Fact]
            public void Should_Handle_Projects_Which_Are_Null()
            {
                // Given / When
                var issue = new Issue(null, @"src\foo.cs", null, "Foo", 1, "Warning", "Bar", "foo", "providerName");

                // Then
                issue.Project.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_Projects_Which_Are_WhiteSpace()
            {
                // Given / When
                var issue = new Issue(" ", @"src\foo.cs", null, "Foo", 1, "Warning", "Bar", "foo", "providerName");

                // Then
                issue.Project.ShouldBe(" ");
            }

            [Fact]
            public void Should_Handle_Projects_Which_Are_Empty()
            {
                // Given / When
                var issue = new Issue(string.Empty, @"src\foo.cs", null, "Foo", 1, "Warning", "Bar", "foo", "providerName");

                // Then
                issue.Project.ShouldBe(string.Empty);
            }

            [Theory]
            [InlineData("project")]
            public void Should_Set_Project(string project)
            {
                // Given / When
                var issue = new Issue(project, @"src\foo.cs", null, "Foo", 1, "Warning", "Bar", "foo", "providerName");

                // Then
                issue.Project.ShouldBe(project);
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_Null()
            {
                // Given / When
                var issue = new Issue(null, null, "Foo", 1, "Warning", "Bar", "foo", "providerName");

                // Then
                issue.AffectedFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_Empty()
            {
                // Given / When
                var issue = new Issue(string.Empty, null, "Foo", 1, "Warning", "Bar", "foo", "providerName");

                // Then
                issue.AffectedFileRelativePath.ShouldBe(null);
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_WhiteSpace()
            {
                // Given / When
                var issue = new Issue(" ", null, "Foo", 1, "Warning", "Bar", "foo", "providerName");

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
                var issue = new Issue(filePath, 100, "Foo", 1, "Warning", "Bar", "foo", "providerName");

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
                var issue = new Issue(@"foo.cs", line, "Foo", 1, "Warning", "Bar", "foo", "providerName");

                // Then
                issue.Line.ShouldBe(line);
            }

            [Theory]
            [InlineData("message")]
            public void Should_Set_Message(string message)
            {
                // Given / When
                var issue = new Issue(@"foo.cs", 100, message, 1, "Warning", "Bar", "foo", "providerName");

                // Then
                issue.Message.ShouldBe(message);
            }

            [Theory]
            [InlineData(int.MinValue)]
            [InlineData(-1)]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Set_Priority(int priority)
            {
                // Given / When
                var issue = new Issue(@"foo.cs", 100, "Message", priority, "Warning", "Bar", "foo", "providerName");

                // Then
                issue.Priority.ShouldBe(priority);
            }

            [Theory]
            [InlineData("Warning")]
            public void Should_Set_Priority_Name(string priorityName)
            {
                // Given / When
                var issue = new Issue(@"foo.cs", 100, "Message", 1, priorityName, "Bar", "foo", "providerName");

                // Then
                issue.PriorityName.ShouldBe(priorityName);
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("rule")]
            public void Should_Set_Rule(string rule)
            {
                // Given / When
                var issue = new Issue(@"foo.cs", 100, "foo", 1, "Warning", rule, "foo", "providerName");

                // Then
                issue.Rule.ShouldBe(rule);
            }

            [Fact]
            public void Should_Set_Rule_Url()
            {
                // Given
                var ruleUrl = new Uri("http://google.com");

                // When
                var issue = new Issue(@"foo.cs", 100, "foo", 1, "Warning", "foo", ruleUrl, "foo", "providerName");

                // Then
                issue.RuleUrl.ShouldBe(ruleUrl);
            }

            [Fact]
            public void Should_Set_Rule_Url_If_Null()
            {
                // Given
                Uri ruleUrl = null;

                // When
                var issue = new Issue(@"foo.cs", 100, "foo", 1, "Warning", "foo", ruleUrl, "foo", "providerName");

                // Then
                issue.RuleUrl.ShouldBe(ruleUrl);
            }

            [Theory]
            [InlineData("foo")]
            public void Should_Set_ProviderType(string providerType)
            {
                // Given / When
                var issue = new Issue(@"foo.cs", 100, "foo", 1, "Warning", "foo", providerType, "providerName");

                // Then
                issue.ProviderType.ShouldBe(providerType);
            }

            [Theory]
            [InlineData("providerName")]
            public void Should_Set_ProviderName(string providerName)
            {
                // Given / When
                var issue = new Issue(@"foo.cs", 100, "foo", 1, "Warning", "foo", "foo", providerName);

                // Then
                issue.ProviderName.ShouldBe(providerName);
            }
        }
    }
}
