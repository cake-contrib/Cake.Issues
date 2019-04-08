namespace Cake.Issues.PullRequests.Tfs.Tests.Capabilities
{
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Issues.PullRequests.Tfs.Capabilities;
    using Cake.Issues.Testing;
    using Cake.Tfs.PullRequest.CommentThread;
    using Shouldly;
    using Xunit;

    public sealed class GitPullRequestCommentThreadExtensionsTests
    {
        public sealed class TheToPullRequestDiscussionThreadExtension
        {
            [Fact]
            public void Should_Throw_If_Thread_Is_Null()
            {
                // Given
                TfsPullRequestCommentThread thread = null;

                // When
                var result = Record.Exception(() => thread.ToPullRequestDiscussionThread());

                // Then
                result.IsArgumentNullException("thread");
            }

            [Fact]
            public void Should_Throw_If_Comments_Are_Null()
            {
                // Given
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = 123,
                        Status = TfsCommentThreadStatus.Active,
                        FilePath = "/foo.cs",
                        Comments = null,
                        Properties = new Dictionary<string, object>()
                    };

                // When
                var result = Record.Exception(() => thread.ToPullRequestDiscussionThread());

                // Then
                result.IsInvalidOperationException("Comments list is not created.");
            }

            [Fact]
            public void Should_Not_Throw_If_Properties_Are_Null()
            {
                // Given
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = 123,
                        Status = TfsCommentThreadStatus.Active,
                        FilePath = "/foo.cs",
                        Comments = new List<TfsComment>(),
                        Properties = null
                    };

                // When
                var result = thread.ToPullRequestDiscussionThread();

                // Then
                result.CommentSource.ShouldBe(default(string));
            }

            [Fact]
            public void Should_Set_Correct_Id()
            {
                // Given
                var id = 123;
                var status = TfsCommentThreadStatus.Active;
                var filePath = "/foo.cs";
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = id,
                        Status = status,
                        FilePath = filePath,
                        Comments = new List<TfsComment>(),
                        Properties = new Dictionary<string, object>()
                    };

                // When
                var result = thread.ToPullRequestDiscussionThread();

                // Then
                result.Id.ShouldBe(id);
            }

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
            public void Should_Set_Correct_Status(
                TfsCommentThreadStatus status,
                PullRequestDiscussionStatus expectedResult)
            {
                // Given
                var id = 123;
                var filePath = "/foo.cs";
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = id,
                        Status = status,
                        FilePath = filePath,
                        Comments = new List<TfsComment>(),
                        Properties = new Dictionary<string, object>()
                    };

                // When
                var result = thread.ToPullRequestDiscussionThread();

                // Then
                result.Status.ShouldBe(expectedResult);
            }

            [Theory]
            [InlineData("/foo.cs", "foo.cs")]
            public void Should_Set_Correct_FilePath(string filePath, string expectedResult)
            {
                // Given
                var id = 123;
                var status = TfsCommentThreadStatus.Active;
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = id,
                        Status = status,
                        FilePath = filePath,
                        Comments = new List<TfsComment>(),
                        Properties = new Dictionary<string, object>()
                    };

                // When
                var result = thread.ToPullRequestDiscussionThread();

                // Then
                result.AffectedFileRelativePath.ToString().ShouldBe(expectedResult);
            }

            [Fact]
            public void Should_Set_Correct_FilePath_If_ThreadContext_Is_Null()
            {
                // Given
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = 123,
                        Status = TfsCommentThreadStatus.Active,
                        Comments = new List<TfsComment>(),
                        Properties = new Dictionary<string, object>()
                    };

                // When
                var result = thread.ToPullRequestDiscussionThread();

                // Then
                result.AffectedFileRelativePath.ShouldBeNull();
            }

            [Fact]
            public void Should_Set_Correct_Comments()
            {
                // Given
                var id = 123;
                var status = TfsCommentThreadStatus.Active;
                var filePath = "/foo.cs";
                var commentContent = "foo";
                var commentIsDeleted = false;
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = id,
                        Status = status,
                        FilePath = filePath,
                        Comments = new List<TfsComment>
                        {
                            new TfsComment()
                            {
                                Content = commentContent,
                                IsDeleted = commentIsDeleted
                            }
                        },
                        Properties = new Dictionary<string, object>()
                    };

                // When
                var result = thread.ToPullRequestDiscussionThread();

                // Then
                result.Comments.Count.ShouldBe(1);
                result.Comments.Single().Content.ShouldBe(commentContent);
                result.Comments.Single().IsDeleted.ShouldBe(commentIsDeleted);
            }

            [Fact]
            public void Should_Set_Correct_CommentSource()
            {
                // Given
                var id = 123;
                var status = TfsCommentThreadStatus.Active;
                var filePath = "/foo.cs";
                var commentSource = "foo";
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = id,
                        Status = status,
                        FilePath = filePath,
                        Comments = new List<TfsComment>(),
                        Properties = new Dictionary<string, object>()
                    };
                thread.SetCommentSource(commentSource);

                // When
                var result = thread.ToPullRequestDiscussionThread();

                // Then
                result.CommentSource.ShouldBe(commentSource);
            }

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
            public void Should_Set_Correct_Resolution(
                TfsCommentThreadStatus status,
                PullRequestDiscussionResolution expectedResult)
            {
                // Given
                var id = 123;
                var filePath = "/foo.cs";
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = id,
                        Status = status,
                        FilePath = filePath,
                        Comments = new List<TfsComment>(),
                        Properties = new Dictionary<string, object>()
                    };

                // When
                var result = thread.ToPullRequestDiscussionThread();

                // Then
                result.Resolution.ShouldBe(expectedResult);
            }
        }

        public sealed class TheGetCommentSourceExtension
        {
            [Fact]
            public void Should_Throw_If_Thread_Is_Null()
            {
                // Given
                TfsPullRequestCommentThread thread = null;

                // When
                var result = Record.Exception(() => thread.GetCommentSource());

                // Then
                result.IsArgumentNullException("thread");
            }

            [Fact]
            public void Should_Not_Throw_If_Properties_Are_Null()
            {
                // Given
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = 123,
                        Status = TfsCommentThreadStatus.Active,
                        FilePath = "/foo.cs",
                        Comments = new List<TfsComment>(),
                        Properties = null
                    };

                // When
                var result = thread.GetCommentSource();

                // Then
                result.ShouldBe(default(string));
            }

            [Fact]
            public void Should_Return_Comment_Source()
            {
                // Given
                var commentSource = "foo";
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = 123,
                        Status = TfsCommentThreadStatus.Active,
                        FilePath = "/foo.cs",
                        Comments = new List<TfsComment>(),
                        Properties = new Dictionary<string, object>()
                    };
                thread.SetCommentSource(commentSource);

                // When
                var result = thread.GetCommentSource();

                // Then
                result.ShouldBe(commentSource);
            }
        }

        public sealed class TheSetCommentSourceExtension
        {
            [Fact]
            public void Should_Throw_If_Thread_Is_Null()
            {
                // Given
                TfsPullRequestCommentThread thread = null;
                var value = "foo";

                // When
                var result = Record.Exception(() => thread.SetCommentSource(value));

                // Then
                result.IsArgumentNullException("thread");
            }

            [Fact]
            public void Should_Throw_If_Properties_Are_Null()
            {
                // Given
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = 123,
                        Status = TfsCommentThreadStatus.Active,
                        FilePath = "/foo.cs",
                        Comments = new List<TfsComment>(),
                        Properties = null
                    };
                var value = "foo";

                // When
                var result = Record.Exception(() => thread.SetCommentSource(value));

                // Then
                result.IsInvalidOperationException("Properties collection is not created.");
            }

            [Fact]
            public void Should_Set_Comment_Source()
            {
                // Given
                var commentSource = "foo";
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = 123,
                        Status = TfsCommentThreadStatus.Active,
                        FilePath = "/foo.cs",
                        Comments = new List<TfsComment>(),
                        Properties = new Dictionary<string, object>()
                    };

                // When
                thread.SetCommentSource(commentSource);

                // Then
                thread.GetCommentSource().ShouldBe(commentSource);
            }
        }

        public sealed class TheIsCommentSourceExtension
        {
            [Fact]
            public void Should_Throw_If_Thread_Is_Null()
            {
                // Given
                TfsPullRequestCommentThread thread = null;
                var value = "foo";

                // When
                var result = Record.Exception(() => thread.IsCommentSource(value));

                // Then
                result.IsArgumentNullException("thread");
            }

            [Fact]
            public void Should_Not_Throw_If_Properties_Are_Null()
            {
                // Given
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = 123,
                        Status = TfsCommentThreadStatus.Active,
                        FilePath = "/foo.cs",
                        Comments = new List<TfsComment>(),
                        Properties = null
                    };
                var value = "foo";

                // When
                var result = thread.IsCommentSource(value);

                // Then
                result.ShouldBeFalse();
            }

            [Fact]
            public void Should_Return_True_For_Existing_Comment_Source()
            {
                // Given
                var commentSource = "foo";
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = 123,
                        Status = TfsCommentThreadStatus.Active,
                        FilePath = "/foo.cs",
                        Comments = new List<TfsComment>(),
                        Properties = new Dictionary<string, object>()
                    };
                thread.SetCommentSource(commentSource);

                // When
                var result = thread.IsCommentSource(commentSource);

                // Then
                result.ShouldBeTrue();
            }

            [Fact]
            public void Should_Return_False_For_Non_Existing_Comment_Source()
            {
                // Given
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = 123,
                        Status = TfsCommentThreadStatus.Active,
                        FilePath = "/foo.cs",
                        Comments = new List<TfsComment>(),
                        Properties = new Dictionary<string, object>()
                    };
                thread.SetCommentSource("foo");

                // When
                var result = thread.IsCommentSource("bar");

                // Then
                result.ShouldBeFalse();
            }
        }

        public sealed class TheGetIssueMessageExtension
        {
            [Fact]
            public void Should_Throw_If_Thread_Is_Null()
            {
                // Given
                TfsPullRequestCommentThread thread = null;

                // When
                var result = Record.Exception(() => thread.GetIssueMessage());

                // Then
                result.IsArgumentNullException("thread");
            }

            [Fact]
            public void Should_Not_Throw_If_Properties_Are_Null()
            {
                // Given
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = 123,
                        Status = TfsCommentThreadStatus.Active,
                        FilePath = "/foo.cs",
                        Comments = new List<TfsComment>(),
                        Properties = null
                    };

                // When
                var result = thread.GetIssueMessage();

                // Then
                result.ShouldBe(default(string));
            }

            [Fact]
            public void Should_Return_Message()
            {
                // Given
                var message = "foo";
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = 123,
                        Status = TfsCommentThreadStatus.Active,
                        FilePath = "/foo.cs",
                        Comments = new List<TfsComment>(),
                        Properties = new Dictionary<string, object>()
                    };
                thread.SetIssueMessage(message);

                // When
                var result = thread.GetIssueMessage();

                // Then
                result.ShouldBe(message);
            }
        }

        public sealed class TheSetIssueMessageExtension
        {
            [Fact]
            public void Should_Throw_If_Thread_Is_Null()
            {
                // Given
                TfsPullRequestCommentThread thread = null;
                var value = "foo";

                // When
                var result = Record.Exception(() => thread.SetIssueMessage(value));

                // Then
                result.IsArgumentNullException("thread");
            }

            [Fact]
            public void Should_Throw_If_Properties_Are_Null()
            {
                // Given
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = 123,
                        Status = TfsCommentThreadStatus.Active,
                        FilePath = "/foo.cs",
                        Comments = new List<TfsComment>(),
                        Properties = null
                    };
                var value = "foo";

                // When
                var result = Record.Exception(() => thread.SetIssueMessage(value));

                // Then
                result.IsInvalidOperationException("Properties collection is not created.");
            }

            [Fact]
            public void Should_Return_Message()
            {
                // Given
                var message = "foo";
                var thread =
                    new TfsPullRequestCommentThread
                    {
                        Id = 123,
                        Status = TfsCommentThreadStatus.Active,
                        FilePath = "/foo.cs",
                        Comments = new List<TfsComment>(),
                        Properties = new Dictionary<string, object>()
                    };

                // When
                thread.SetIssueMessage(message);

                // Then
                thread.GetIssueMessage().ShouldBe(message);
            }
        }
    }
}
