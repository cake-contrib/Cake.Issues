namespace Cake.Issues.PullRequests.Tfs.Tests.Capabilities
{
    using Cake.Issues.PullRequests.Tfs.Capabilities;
    using Cake.Issues.Testing;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
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
                Comment comment = null;

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
                    new Comment
                    {
                        Content = content
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
                    new Comment
                    {
                        IsDeleted = isDeleted
                    };

                // When
                var result = comment.ToPullRequestDiscussionComment();

                // Then
                result.IsDeleted.ShouldBe(isDeleted);
            }
        }
    }
}
