namespace Cake.Issues.PullRequests.Tests;

using Cake.Issues.Shared;

public sealed class IIssueExtensionsTests
{
    public sealed class TheSortWithDefaultPrioritizationMethod
    {
        [Fact]
        public void Should_Throw_If_Issues_Are_Null()
        {
            // Given
            const IEnumerable<IIssue> issues = null;

            // When
            var result =
                Record.Exception(issues.SortWithDefaultPrioritization);

            // Then
            result.IsArgumentNullException("issues");
        }

        [Fact]
        public void Should_Order_By_Priority()
        {
            // Given
            var issue1 =
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 10)
                    .OfRule("Rule Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create();
            var issue2 =
                IssueBuilder
                    .NewIssue("Message Bar", "ProviderType Bar", "ProviderName Bar")
                    .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                    .OfRule("Rule Bar")
                    .WithPriority(IssuePriority.Error)
                    .Create();
            var issues =
                new List<IIssue>
                {
                    issue1,
                    issue2,
                };

            // When
            var result = issues.SortWithDefaultPrioritization().ToList();

            // Then
            result.First().ShouldBe(issue2);
            result.Last().ShouldBe(issue1);
        }

        [Fact]
        public void Should_Order_By_Existence_Of_FilePath()
        {
            // Given
            var issue1 =
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .OfRule("Rule Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create();
            var issue2 =
                IssueBuilder
                    .NewIssue("Message Bar", "ProviderType Bar", "ProviderName Bar")
                    .InFile(@"src\Cake.Issues.Tests\FakeIssueProvider.cs", 12)
                    .OfRule("Rule Bar")
                    .WithPriority(IssuePriority.Warning)
                    .Create();
            var issues =
                new List<IIssue>
                {
                    issue1,
                    issue2,
                };

            // When
            var result = issues.SortWithDefaultPrioritization().ToList();

            // Then
            result.First().ShouldBe(issue2);
            result.Last().ShouldBe(issue1);
        }

        [Fact]
        public void Should_Order_By_FilePath()
        {
            // Given
            var issue1 =
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .OfRule("Rule Foo")
                    .InFile(@"src\Cake.Issues.Tests\B.cs", 12)
                    .WithPriority(IssuePriority.Warning)
                    .Create();
            var issue2 =
                IssueBuilder
                    .NewIssue("Message Bar", "ProviderType Bar", "ProviderName Bar")
                    .InFile(@"src\Cake.Issues.Tests\A.cs", 12)
                    .OfRule("Rule Bar")
                    .WithPriority(IssuePriority.Warning)
                    .Create();
            var issues =
                new List<IIssue>
                {
                    issue1,
                    issue2,
                };

            // When
            var result = issues.SortWithDefaultPrioritization().ToList();

            // Then
            result.First().ShouldBe(issue2);
            result.Last().ShouldBe(issue1);
        }
    }
}