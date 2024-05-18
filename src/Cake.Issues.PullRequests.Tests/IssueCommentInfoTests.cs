namespace Cake.Issues.PullRequests.Tests
{
    public sealed class IssueCommentInfoTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Active_Comments_Are_Null()
            {
                // Given
                IEnumerable<IPullRequestDiscussionComment> activeComments = null;
                var wontFixComments = new List<IPullRequestDiscussionComment>();
                var resolvedComments = new List<IPullRequestDiscussionComment>();

                // When
                var result =
                    Record.Exception(() =>
                        new IssueCommentInfo(activeComments, wontFixComments, resolvedComments));

                // Then
                result.IsArgumentNullException("activeComments");
            }

            [Fact]
            public void Should_Throw_If_Wont_Fix_Comments_Are_Null()
            {
                // Given
                var activeComments = new List<IPullRequestDiscussionComment>();
                const IEnumerable<IPullRequestDiscussionComment> wontFixComments = null;
                var resolvedComments = new List<IPullRequestDiscussionComment>();

                // When
                var result =
                    Record.Exception(() =>
                        new IssueCommentInfo(activeComments, wontFixComments, resolvedComments));

                // Then
                result.IsArgumentNullException("wontFixComments");
            }

            [Fact]
            public void Should_Throw_If_Resolved_Comments_Are_Null()
            {
                // Given
                var activeComments = new List<IPullRequestDiscussionComment>();
                var wontFixComments = new List<IPullRequestDiscussionComment>();
                const IEnumerable<IPullRequestDiscussionComment> resolvedComments = null;

                // When
                var result =
                    Record.Exception(() =>
                        new IssueCommentInfo(activeComments, wontFixComments, resolvedComments));

                // Then
                result.IsArgumentNullException("resolvedComments");
            }

            [Fact]
            public void Should_Set_Active_Comments()
            {
                // Given
                var comment1 = new PullRequestDiscussionComment();
                var comment2 = new PullRequestDiscussionComment();

                var activeComments = new List<IPullRequestDiscussionComment> { comment1, comment2 };
                var wontFixComments = new List<IPullRequestDiscussionComment>();
                var resolvedComments = new List<IPullRequestDiscussionComment>();

                // When
                var result = new IssueCommentInfo(activeComments, wontFixComments, resolvedComments);

                // Then
                result.ActiveComments.Count().ShouldBe(2);
                result.ActiveComments.ShouldContain(comment1);
                result.ActiveComments.ShouldContain(comment2);
            }

            [Fact]
            public void Should_Set_Wont_Fix_Comments()
            {
                // Given
                var comment1 = new PullRequestDiscussionComment();
                var comment2 = new PullRequestDiscussionComment();

                var activeComments = new List<IPullRequestDiscussionComment>();
                var wontFixComments = new List<IPullRequestDiscussionComment> { comment1, comment2 };
                var resolvedComments = new List<IPullRequestDiscussionComment>();

                // When
                var result = new IssueCommentInfo(activeComments, wontFixComments, resolvedComments);

                // Then
                result.WontFixComments.Count().ShouldBe(2);
                result.WontFixComments.ShouldContain(comment1);
                result.WontFixComments.ShouldContain(comment2);
            }

            [Fact]
            public void Should_Set_Resolved_Comments()
            {
                // Given
                var comment1 = new PullRequestDiscussionComment();
                var comment2 = new PullRequestDiscussionComment();

                var activeComments = new List<IPullRequestDiscussionComment>();
                var wontFixComments = new List<IPullRequestDiscussionComment>();
                var resolvedComments = new List<IPullRequestDiscussionComment> { comment1, comment2 };

                // When
                var result = new IssueCommentInfo(activeComments, wontFixComments, resolvedComments);

                // Then
                result.ResolvedComments.Count().ShouldBe(2);
                result.ResolvedComments.ShouldContain(comment1);
                result.ResolvedComments.ShouldContain(comment2);
            }
        }
    }
}