namespace Cake.Issues.PullRequests.Tests
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class PullRequestDiscussionThreadTests
    {
        public sealed class TheCtor
        {
            [Theory]
            [InlineData(@"/foo")]
            [InlineData(@"\foo")]
            public void Should_Throw_If_File_Path_Is_Absolute(string filePath)
            {
                // Given / When
                var result = Record.Exception(() =>
                    new PullRequestDiscussionThread(
                        1,
                        PullRequestDiscussionStatus.Active,
                        filePath,
                        new List<IPullRequestDiscussionComment>()));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [SkippableTheory]
            [InlineData(@"c:\src\foo.cs")]
            public void Should_Throw_If_File_Path_Is_Absolute_Windows_Path(string filePath)
            {
                // Uses Windows specific paths.
                Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

                // Given / When
                var result = Record.Exception(() =>
                    new PullRequestDiscussionThread(
                        1,
                        PullRequestDiscussionStatus.Active,
                        filePath,
                        new List<IPullRequestDiscussionComment>()));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_Comments_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new PullRequestDiscussionThread(1, PullRequestDiscussionStatus.Active, @"foo.cs", null));

                // Then
                result.IsArgumentNullException("comments");
            }

            [Fact]
            public void Should_Handle_File_Paths_Which_Are_Null()
            {
                // Given / When
                var thread =
                    new PullRequestDiscussionThread(
                        1,
                        PullRequestDiscussionStatus.Active,
                        null,
                        new List<IPullRequestDiscussionComment>());

                // Then
                thread.AffectedFileRelativePath.ShouldBe(null);
            }

            [Theory]
            [InlineData(int.MinValue)]
            [InlineData(0)]
            [InlineData(int.MaxValue)]
            public void Should_Set_Id(int id)
            {
                // Given / When
                var thread =
                    new PullRequestDiscussionThread(
                        id,
                        PullRequestDiscussionStatus.Active,
                        "foo.cs",
                        new List<IPullRequestDiscussionComment>());

                // Then
                thread.Id.ShouldBe(id);
            }

            [Theory]
            [InlineData(PullRequestDiscussionStatus.Active)]
            [InlineData(PullRequestDiscussionStatus.Resolved)]
            public void Should_Set_Status(PullRequestDiscussionStatus status)
            {
                // Given / When
                var thread =
                    new PullRequestDiscussionThread(
                        1,
                        status,
                        "foo.cs",
                        new List<IPullRequestDiscussionComment>());

                // Then
                thread.Status.ShouldBe(status);
            }

            [Fact]
            public void Should_Set_Comments()
            {
                // Given
                var comments =
                    new List<IPullRequestDiscussionComment>
                    {
                        new PullRequestDiscussionComment
                        {
                            Content = "Foo",
                            IsDeleted = false,
                        },
                        new PullRequestDiscussionComment
                        {
                            Content = "Bar",
                            IsDeleted = true,
                        },
                    };

                // When
                var thread =
                    new PullRequestDiscussionThread(
                        1,
                        PullRequestDiscussionStatus.Active,
                        "foo.cs",
                        comments);

                // Then
                thread.Comments.ShouldContain(comment => comments.Contains(comment), comments.Count);
            }

            [Theory]
            [InlineData(@"foo", @"foo")]
            [InlineData(@"foo\bar", @"foo/bar")]
            [InlineData(@"foo/bar", @"foo/bar")]
            [InlineData(@"foo\bar\", @"foo/bar")]
            [InlineData(@"foo/bar/", @"foo/bar")]
            [InlineData(@".\foo", @"foo")]
            [InlineData(@"./foo", @"foo")]
            [InlineData(@"foo\..\bar", @"foo/../bar")]
            [InlineData(@"foo/../bar", @"foo/../bar")]
            public void Should_Set_File_Path(string filePath, string expectedFilePath)
            {
                // Given / When
                var thread =
                    new PullRequestDiscussionThread(
                        1,
                        PullRequestDiscussionStatus.Active,
                        filePath,
                        new List<IPullRequestDiscussionComment>());

                // Then
                thread.AffectedFileRelativePath.ToString().ShouldBe(expectedFilePath);
                thread.AffectedFileRelativePath.IsRelative.ShouldBe(true, "File path was not set as relative.");
            }
        }
    }
}
