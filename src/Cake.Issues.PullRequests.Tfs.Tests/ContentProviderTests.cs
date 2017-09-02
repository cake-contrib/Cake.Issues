namespace Cake.Issues.PullRequests.Tfs.Tests
{
    using System;
    using IssueProvider;
    using Shouldly;
    using Xunit;

    public class ContentProviderTests
    {
        public sealed class TheGetContentClass
        {
            [Theory]
            [InlineData(
                @"foo.cs",
                123,
                "Some message",
                1,
                "foo",
                null,
                "foo: Some message")]
            [InlineData(
                @"foo.cs",
                123,
                "Some message",
                1,
                "",
                null,
                "Some message")]
            [InlineData(
                @"foo.cs",
                123,
                "Some message",
                1,
                " ",
                null,
                "Some message")]
            [InlineData(
                @"foo.cs",
                123,
                "Some message",
                1,
                "foo",
                "http://google.com",
                "[foo](http://google.com/): Some message")]
            public void Should_Return_Correct_Value(
                string filePath,
                int? line,
                string message,
                int priority,
                string rule,
                string ruleUrl,
                string expectedResult)
            {
                // Given
                Uri ruleUri = null;
                if (!string.IsNullOrWhiteSpace(ruleUrl))
                {
                    ruleUri = new Uri(ruleUrl);
                }

                var issue = new Issue(filePath, line, message, priority, rule, ruleUri, "Foo");

                // When
                var result = ContentProvider.GetContent(issue);

                // Then
                result.ShouldBe(expectedResult);
            }
        }
    }
}
