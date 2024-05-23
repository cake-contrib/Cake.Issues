namespace Cake.Issues.PullRequests.Tests;

public sealed class PullRequestIssueResultTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_ReportedIssues_Is_Null()
        {
            // Given
            const IEnumerable<IIssue> reportedIssues = null;
            IEnumerable<IIssue> postedIssues = [];

            // When
            var result = Record.Exception(() => new PullRequestIssueResult(reportedIssues, postedIssues));

            // Then
            result.IsArgumentNullException("reportedIssues");
        }

        [Fact]
        public void Should_Throw_If_PostedIssues_Is_Null()
        {
            // Given
            IEnumerable<IIssue> reportedIssues = [];
            const IEnumerable<IIssue> postedIssues = null;

            // When
            var result = Record.Exception(() => new PullRequestIssueResult(reportedIssues, postedIssues));

            // Then
            result.IsArgumentNullException("postedIssues");
        }

        [Fact]
        public void Should_Set_ReportedIssues()
        {
            // Given
            IEnumerable<IIssue> reportedIssues = [];
            IEnumerable<IIssue> postedIssues = [];

            // When
            var result = new PullRequestIssueResult(reportedIssues, postedIssues);

            // Then
            result.ReportedIssues.ShouldBe(reportedIssues);
        }

        [Fact]
        public void Should_Set_PostedIssues()
        {
            // Given
            IEnumerable<IIssue> reportedIssues = [];
            IEnumerable<IIssue> postedIssues = [];

            // When
            var result = new PullRequestIssueResult(reportedIssues, postedIssues);

            // Then
            result.PostedIssues.ShouldBe(postedIssues);
        }
    }
}
