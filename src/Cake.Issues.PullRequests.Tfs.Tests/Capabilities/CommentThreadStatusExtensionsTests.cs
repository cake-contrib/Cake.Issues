namespace Cake.Issues.PullRequests.Tfs.Tests.Capabilities
{
    using Cake.Issues.PullRequests.Tfs.Capabilities;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Shouldly;
    using Xunit;

    public sealed class CommentThreadStatusExtensionsTests
    {
        public sealed class TheToPullRequestDiscussionStatusExtension
        {
            [Theory]
            [InlineData(
                CommentThreadStatus.Unknown,
                PullRequestDiscussionStatus.Unknown)]
            [InlineData(
                CommentThreadStatus.Active,
                PullRequestDiscussionStatus.Active)]
            [InlineData(
                CommentThreadStatus.Pending,
                PullRequestDiscussionStatus.Active)]
            [InlineData(
                CommentThreadStatus.Fixed,
                PullRequestDiscussionStatus.Resolved)]
            [InlineData(
                CommentThreadStatus.WontFix,
                PullRequestDiscussionStatus.Resolved)]
            [InlineData(
                CommentThreadStatus.Closed,
                PullRequestDiscussionStatus.Resolved)]
            [InlineData(
                CommentThreadStatus.ByDesign,
                PullRequestDiscussionStatus.Resolved)]
            public void Should_Return_Correct_Value(
                CommentThreadStatus status,
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
                CommentThreadStatus.Unknown,
                PullRequestDiscussionResolution.Unknown)]
            [InlineData(
                CommentThreadStatus.Active,
                PullRequestDiscussionResolution.Unknown)]
            [InlineData(
                CommentThreadStatus.Pending,
                PullRequestDiscussionResolution.Unknown)]
            [InlineData(
                CommentThreadStatus.Fixed,
                PullRequestDiscussionResolution.Resolved)]
            [InlineData(
                CommentThreadStatus.WontFix,
                PullRequestDiscussionResolution.WontFix)]
            [InlineData(
                CommentThreadStatus.Closed,
                PullRequestDiscussionResolution.Resolved)]
            [InlineData(
                CommentThreadStatus.ByDesign,
                PullRequestDiscussionResolution.Resolved)]
            public void Should_Return_Correct_Value(
                CommentThreadStatus status,
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
