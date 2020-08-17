namespace Cake.Issues.PullRequests.AzureDevOps.Tests.Capabilities
{
    using Cake.AzureDevOps.Repos.PullRequest.CommentThread;
    using Cake.Issues.PullRequests.AzureDevOps.Capabilities;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class CommentExtensionsTests
    {
        public sealed class TheToPullRequestDiscussionCommentExtension
        {
            [Fact]
            public void Should_Throw_If_Comment_Is_Null()
            {
                // Given
                AzureDevOpsComment comment = null;

                // When
                var result = Record.Exception(() => comment.ToPullRequestDiscussionComment());

                // Then
                result.IsArgumentNullException("comment");
            }

            [Fact]
            public void Should_Set_Correct_Content()
            {
                // Given
                var content = "foo";
                var comment =
                    new AzureDevOpsComment
                    {
                        Content = content,
                    };

                // When
                var result = comment.ToPullRequestDiscussionComment();

                // Then
                result.Content.ShouldBe(content);
            }

            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Set_Correct_IsDeleted(bool isDeleted)
            {
                // Given
                var comment =
                    new AzureDevOpsComment
                    {
                        IsDeleted = isDeleted,
                    };

                // When
                var result = comment.ToPullRequestDiscussionComment();

                // Then
                result.IsDeleted.ShouldBe(isDeleted);
            }
        }
    }
}
