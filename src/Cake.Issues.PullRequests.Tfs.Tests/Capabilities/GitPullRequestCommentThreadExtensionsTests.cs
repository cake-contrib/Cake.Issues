namespace Cake.Issues.PullRequests.Tfs.Tests.Capabilities
{
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Issues.PullRequests.Tfs.Capabilities;
    using Cake.Issues.Testing;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Microsoft.VisualStudio.Services.WebApi;
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
                GitPullRequestCommentThread thread = null;

                // When
                var result = Record.Exception(() => thread.ToPullRequestDiscussionThread());

                // Then
                result.IsArgumentNullException("thread");
            }

            [Fact]
            public void Should_Throw_If_ThreadContext_Is_Null()
            {
                // Given
                var thread =
                    new GitPullRequestCommentThread
                    {
                        Id = 123,
                        Status = CommentThreadStatus.Active,
                        ThreadContext = null,
                        Comments = new List<Comment>(),
                        Properties = new PropertiesCollection()
                    };

                // When
                var result = Record.Exception(() => thread.ToPullRequestDiscussionThread());

                // Then
                result.IsInvalidOperationException("ThreadContext is not created.");
            }

            [Fact]
            public void Should_Throw_If_Comments_Are_Null()
            {
                // Given
                var thread =
                    new GitPullRequestCommentThread
                    {
                        Id = 123,
                        Status = CommentThreadStatus.Active,
                        ThreadContext = new CommentThreadContext { FilePath = "/foo.cs" },
                        Comments = null,
                        Properties = new PropertiesCollection()
                    };

                // When
                var result = Record.Exception(() => thread.ToPullRequestDiscussionThread());

                // Then
                result.IsInvalidOperationException("Comments list is not created.");
            }

            [Fact]
            public void Should_Throw_If_Properties_Are_Null()
            {
                // Given
                var thread =
                    new GitPullRequestCommentThread
                    {
                        Id = 123,
                        Status = CommentThreadStatus.Active,
                        ThreadContext = new CommentThreadContext { FilePath = "/foo.cs" },
                        Comments = new List<Comment>(),
                        Properties = null
                    };

                // When
                var result = Record.Exception(() => thread.ToPullRequestDiscussionThread());

                // Then
                result.IsInvalidOperationException("Properties collection is not created.");
            }

            [Fact]
            public void Should_Set_Correct_Id()
            {
                // Given
                var id = 123;
                var status = CommentThreadStatus.Active;
                var filePath = "/foo.cs";
                var thread =
                    new GitPullRequestCommentThread
                    {
                        Id = id,
                        Status = status,
                        ThreadContext = new CommentThreadContext() { FilePath = filePath },
                        Comments = new List<Comment>(),
                        Properties = new PropertiesCollection()
                    };

                // When
                var result = thread.ToPullRequestDiscussionThread();

                // Then
                result.Id.ShouldBe(id);
            }

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
            public void Should_Set_Correct_Status(
                CommentThreadStatus status,
                PullRequestDiscussionStatus expectedResult)
            {
                // Given
                var id = 123;
                var filePath = "/foo.cs";
                var thread =
                    new GitPullRequestCommentThread
                    {
                        Id = id,
                        Status = status,
                        ThreadContext = new CommentThreadContext() { FilePath = filePath },
                        Comments = new List<Comment>(),
                        Properties = new PropertiesCollection()
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
                var status = CommentThreadStatus.Active;
                var thread =
                    new GitPullRequestCommentThread
                    {
                        Id = id,
                        Status = status,
                        ThreadContext = new CommentThreadContext() { FilePath = filePath },
                        Comments = new List<Comment>(),
                        Properties = new PropertiesCollection()
                    };

                // When
                var result = thread.ToPullRequestDiscussionThread();

                // Then
                result.AffectedFileRelativePath.ToString().ShouldBe(expectedResult);
            }

            [Fact]
            public void Should_Set_Correct_Comments()
            {
                // Given
                var id = 123;
                var status = CommentThreadStatus.Active;
                var filePath = "/foo.cs";
                var commentContent = "foo";
                var commentIsDeleted = false;
                var thread =
                    new GitPullRequestCommentThread
                    {
                        Id = id,
                        Status = status,
                        ThreadContext = new CommentThreadContext() { FilePath = filePath },
                        Comments = new List<Comment>
                        {
                            new Comment()
                            {
                                Content = commentContent,
                                IsDeleted = commentIsDeleted
                            }
                        },
                        Properties = new PropertiesCollection()
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
                var status = CommentThreadStatus.Active;
                var filePath = "/foo.cs";
                var commentSource = "foo";
                var thread =
                    new GitPullRequestCommentThread
                    {
                        Id = id,
                        Status = status,
                        ThreadContext = new CommentThreadContext() { FilePath = filePath },
                        Comments = new List<Comment>(),
                        Properties = new PropertiesCollection()
                    };
                thread.SetCommentSource(commentSource);

                // When
                var result = thread.ToPullRequestDiscussionThread();

                // Then
                result.CommentSource.ShouldBe(commentSource);
            }

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
            public void Should_Set_Correct_Resolution(
                CommentThreadStatus status,
                PullRequestDiscussionResolution expectedResult)
            {
                // Given
                var id = 123;
                var filePath = "/foo.cs";
                var thread =
                    new GitPullRequestCommentThread
                    {
                        Id = id,
                        Status = status,
                        ThreadContext = new CommentThreadContext() { FilePath = filePath },
                        Comments = new List<Comment>(),
                        Properties = new PropertiesCollection()
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
                GitPullRequestCommentThread thread = null;

                // When
                var result = Record.Exception(() => thread.GetCommentSource());

                // Then
                result.IsArgumentNullException("thread");
            }

            [Fact]
            public void Should_Throw_If_Properties_Are_Null()
            {
                // Given
                var thread =
                    new GitPullRequestCommentThread
                    {
                        Id = 123,
                        Status = CommentThreadStatus.Active,
                        ThreadContext = new CommentThreadContext { FilePath = "/foo.cs" },
                        Comments = new List<Comment>(),
                        Properties = null
                    };

                // When
                var result = Record.Exception(() => thread.GetCommentSource());

                // Then
                result.IsInvalidOperationException("Properties collection is not created.");
            }

            [Fact]
            public void Should_Return_Comment_Source()
            {
                // Given
                var commentSource = "foo";
                var thread =
                    new GitPullRequestCommentThread
                    {
                        Id = 123,
                        Status = CommentThreadStatus.Active,
                        ThreadContext = new CommentThreadContext() { FilePath = "/foo.cs" },
                        Comments = new List<Comment>(),
                        Properties = new PropertiesCollection()
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
                GitPullRequestCommentThread thread = null;
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
                    new GitPullRequestCommentThread
                    {
                        Id = 123,
                        Status = CommentThreadStatus.Active,
                        ThreadContext = new CommentThreadContext { FilePath = "/foo.cs" },
                        Comments = new List<Comment>(),
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
                    new GitPullRequestCommentThread
                    {
                        Id = 123,
                        Status = CommentThreadStatus.Active,
                        ThreadContext = new CommentThreadContext() { FilePath = "/foo.cs" },
                        Comments = new List<Comment>(),
                        Properties = new PropertiesCollection()
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
                GitPullRequestCommentThread thread = null;
                var value = "foo";

                // When
                var result = Record.Exception(() => thread.IsCommentSource(value));

                // Then
                result.IsArgumentNullException("thread");
            }

            [Fact]
            public void Should_Throw_If_Properties_Are_Null()
            {
                // Given
                var thread =
                    new GitPullRequestCommentThread
                    {
                        Id = 123,
                        Status = CommentThreadStatus.Active,
                        ThreadContext = new CommentThreadContext { FilePath = "/foo.cs" },
                        Comments = new List<Comment>(),
                        Properties = null
                    };
                var value = "foo";

                // When
                var result = Record.Exception(() => thread.IsCommentSource(value));

                // Then
                result.IsInvalidOperationException("Properties collection is not created.");
            }

            [Fact]
            public void Should_Return_True_For_Existing_Comment_Source()
            {
                // Given
                var commentSource = "foo";
                var thread =
                    new GitPullRequestCommentThread
                    {
                        Id = 123,
                        Status = CommentThreadStatus.Active,
                        ThreadContext = new CommentThreadContext() { FilePath = "/foo.cs" },
                        Comments = new List<Comment>(),
                        Properties = new PropertiesCollection()
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
                    new GitPullRequestCommentThread
                    {
                        Id = 123,
                        Status = CommentThreadStatus.Active,
                        ThreadContext = new CommentThreadContext() { FilePath = "/foo.cs" },
                        Comments = new List<Comment>(),
                        Properties = new PropertiesCollection()
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
                GitPullRequestCommentThread thread = null;

                // When
                var result = Record.Exception(() => thread.GetIssueMessage());

                // Then
                result.IsArgumentNullException("thread");
            }

            [Fact]
            public void Should_Throw_If_Properties_Are_Null()
            {
                // Given
                var thread =
                    new GitPullRequestCommentThread
                    {
                        Id = 123,
                        Status = CommentThreadStatus.Active,
                        ThreadContext = new CommentThreadContext { FilePath = "/foo.cs" },
                        Comments = new List<Comment>(),
                        Properties = null
                    };

                // When
                var result = Record.Exception(() => thread.GetIssueMessage());

                // Then
                result.IsInvalidOperationException("Properties collection is not created.");
            }

            [Fact]
            public void Should_Return_Message()
            {
                // Given
                var message = "foo";
                var thread =
                    new GitPullRequestCommentThread
                    {
                        Id = 123,
                        Status = CommentThreadStatus.Active,
                        ThreadContext = new CommentThreadContext() { FilePath = "/foo.cs" },
                        Comments = new List<Comment>(),
                        Properties = new PropertiesCollection()
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
                GitPullRequestCommentThread thread = null;
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
                    new GitPullRequestCommentThread
                    {
                        Id = 123,
                        Status = CommentThreadStatus.Active,
                        ThreadContext = new CommentThreadContext { FilePath = "/foo.cs" },
                        Comments = new List<Comment>(),
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
                    new GitPullRequestCommentThread
                    {
                        Id = 123,
                        Status = CommentThreadStatus.Active,
                        ThreadContext = new CommentThreadContext() { FilePath = "/foo.cs" },
                        Comments = new List<Comment>(),
                        Properties = new PropertiesCollection()
                    };

                // When
                thread.SetIssueMessage(message);

                // Then
                thread.GetIssueMessage().ShouldBe(message);
            }
        }
    }
}
