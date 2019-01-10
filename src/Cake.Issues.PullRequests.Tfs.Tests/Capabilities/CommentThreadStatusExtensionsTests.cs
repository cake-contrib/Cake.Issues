namespace Cake.Issues.PullRequests.Tfs.Tests.Capabilities
{
    using Cake.Issues.PullRequests.Tfs.Capabilities;
    using Cake.Tfs.PullRequest.CommentThread;
    using Shouldly;
    using Xunit;

    public sealed class CommentThreadStatusExtensionsTests
    {
        public sealed class TheToPullRequestDiscussionStatusExtension
        {
            [Theory]
            [InlineData(
                TfsCommentThreadStatus.Unknown,
                PullRequestDiscussionStatus.Unknown)]
            [InlineData(
                TfsCommentThreadStatus.Active,
                PullRequestDiscussionStatus.Active)]
            [InlineData(
                TfsCommentThreadStatus.Pending,
                PullRequestDiscussionStatus.Active)]
            [InlineData(
                TfsCommentThreadStatus.Fixed,
                PullRequestDiscussionStatus.Resolved)]
            [InlineData(
                TfsCommentThreadStatus.WontFix,
                PullRequestDiscussionStatus.Resolved)]
            [InlineData(
                TfsCommentThreadStatus.Closed,
                PullRequestDiscussionStatus.Resolved)]
            [InlineData(
                TfsCommentThreadStatus.ByDesign,
                PullRequestDiscussionStatus.Resolved)]
            public void Should_Return_Correct_Value(
                TfsCommentThreadStatus status,
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
                TfsCommentThreadStatus.Unknown,
                PullRequestDiscussionResolution.Unknown)]
            [InlineData(
                TfsCommentThreadStatus.Active,
                PullRequestDiscussionResolution.Unknown)]
            [InlineData(
                TfsCommentThreadStatus.Pending,
                PullRequestDiscussionResolution.Unknown)]
            [InlineData(
                TfsCommentThreadStatus.Fixed,
                PullRequestDiscussionResolution.Resolved)]
            [InlineData(
                TfsCommentThreadStatus.WontFix,
                PullRequestDiscussionResolution.WontFix)]
            [InlineData(
                TfsCommentThreadStatus.Closed,
                PullRequestDiscussionResolution.Resolved)]
            [InlineData(
                TfsCommentThreadStatus.ByDesign,
                PullRequestDiscussionResolution.Resolved)]
            public void Should_Return_Correct_Value(
                TfsCommentThreadStatus status,
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
