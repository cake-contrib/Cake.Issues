namespace Cake.Issues.PullRequests.AzureDevOps.Tests
{
    using System;
    using Shouldly;
    using Xunit;

    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class ContentProviderTests
    {
        public sealed class TheGetContentMethod
        {
            [Theory]
            [InlineData(
                "foo.cs",
                123,
                "Some message",
                IssuePriority.Warning,
                "foo",
                null,
                "foo: Some message")]
            [InlineData(
                "foo.cs",
                123,
                "Some message",
                IssuePriority.Warning,
                "",
                null,
                "Some message")]
            [InlineData(
                "foo.cs",
                123,
                "Some message",
                IssuePriority.Warning,
                " ",
                null,
                "Some message")]
            [InlineData(
                "foo.cs",
                123,
                "Some message",
                IssuePriority.Warning,
                "foo",
                "http://google.com",
                "[foo](http://google.com/): Some message")]
            public void Should_Return_Correct_Value(
                string filePath,
                int? line,
                string message,
                IssuePriority priority,
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

                var issue =
                    IssueBuilder
                        .NewIssue(message, "ProviderType", "ProviderName")
                        .InFile(filePath, line)
                        .OfRule(rule, ruleUri)
                        .WithPriority(priority)
                        .Create();

                // When
                var result = ContentProvider.GetContent(issue);

                // Then
                result.ShouldBe(expectedResult);
            }
        }
    }
}
