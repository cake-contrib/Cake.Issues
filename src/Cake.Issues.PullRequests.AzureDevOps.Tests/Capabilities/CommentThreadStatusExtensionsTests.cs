namespace Cake.Issues.PullRequests.AzureDevOps.Tests.Capabilities
{
    using Cake.AzureDevOps.PullRequest.CommentThread;
    using Cake.Issues.PullRequests.AzureDevOps.Capabilities;
    using Shouldly;
    using Xunit;

    public sealed class CommentThreadStatusExtensionsTests
    {
        public sealed class TheToPullRequestDiscussionStatusExtension
        {
            [Theory]
            [InlineData(
                AzureDevOpsCommentThreadStatus.Unknown,
                PullRequestDiscussionStatus.Unknown)]
            [InlineData(
                AzureDevOpsCommentThreadStatus.Active,
                PullRequestDiscussionStatus.Active)]
            [InlineData(
                AzureDevOpsCommentThreadStatus.Pending,
                PullRequestDiscussionStatus.Active)]
            [InlineData(
                AzureDevOpsCommentThreadStatus.Fixed,
                PullRequestDiscussionStatus.Resolved)]
            [InlineData(
                AzureDevOpsCommentThreadStatus.WontFix,
                PullRequestDiscussionStatus.Resolved)]
            [InlineData(
                AzureDevOpsCommentThreadStatus.Closed,
                PullRequestDiscussionStatus.Resolved)]
            [InlineData(
                AzureDevOpsCommentThreadStatus.ByDesign,
                PullRequestDiscussionStatus.Resolved)]
            public void Should_Return_Correct_Value(
                AzureDevOpsCommentThreadStatus status,
                PullRequestDiscussionStatus expectedResult)
            {
                // Given

                // When
                var result = status.ToPullRequestDiscussionStatus();

                // Then
                result.ShouldBe(expectedResult);
            }
        }

        public sealed class TheToPullRequestDiscussionResolutionExtension
        {
            [Theory]
            [InlineData(
                AzureDevOpsCommentThreadStatus.Unknown,
                PullRequestDiscussionResolution.Unknown)]
            [InlineData(
                AzureDevOpsCommentThreadStatus.Active,
                PullRequestDiscussionResolution.Unknown)]
            [InlineData(
                AzureDevOpsCommentThreadStatus.Pending,
                PullRequestDiscussionResolution.Unknown)]
            [InlineData(
                AzureDevOpsCommentThreadStatus.Fixed,
                PullRequestDiscussionResolution.Resolved)]
            [InlineData(
                AzureDevOpsCommentThreadStatus.WontFix,
                PullRequestDiscussionResolution.WontFix)]
            [InlineData(
                AzureDevOpsCommentThreadStatus.Closed,
                PullRequestDiscussionResolution.Resolved)]
            [InlineData(
                AzureDevOpsCommentThreadStatus.ByDesign,
                PullRequestDiscussionResolution.Resolved)]
            public void Should_Return_Correct_Value(
                AzureDevOpsCommentThreadStatus status,
                PullRequestDiscussionResolution expectedResult)
            {
                // Given

                // When
                var result = status.ToPullRequestDiscussionResolution();

                // Then
                result.ShouldBe(expectedResult);
            }
        }
    }
}
