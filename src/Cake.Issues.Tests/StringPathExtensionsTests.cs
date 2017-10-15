namespace Cake.Issues.Tests
{
    using Shouldly;
    using Testing;
    using Xunit;

    public sealed class StringPathExtensionsTests
    {
        public sealed class TheIsValidPathExtension
        {
            [Fact]
            public void Should_Throw_If_Path_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => ((string)null).IsValidPath());

                // Then
                result.IsArgumentNullException("path");
            }

            [Fact]
            public void Should_Throw_If_Path_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => string.Empty.IsValidPath());

                // Then
                result.IsArgumentOutOfRangeException("path");
            }

            [Fact]
            public void Should_Throw_If_Path_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => " ".IsValidPath());

                // Then
                result.IsArgumentOutOfRangeException("path");
            }

            [Theory]
            [InlineData(@"foo")]
            [InlineData(@"foo\")]
            [InlineData(@"foo\bar")]
            [InlineData(@"\foo")]
            [InlineData(@"\foo\")]
            [InlineData(@"\foo\bar")]
            [InlineData(@"c:\foo")]
            [InlineData(@"c:\foo\")]
            [InlineData(@"c:\foo\bar")]
            [InlineData(@"/foo")]
            [InlineData(@"/foo")]
            [InlineData(@"/foo/bar")]
            public void Should_Return_True_If_Valid_Path(string path)
            {
                // Given / When
                var result = path.IsValidPath();

                // Then
                result.ShouldBe(true);
            }

            [Theory]
            [InlineData("foo\tbar")]
            public void Should_Return_False_If_Valid_Path(string path)
            {
                // Given / When
                var result = path.IsValidPath();

                // Then
                result.ShouldBe(false);
            }
        }

        public sealed class TheIsFullPathExtension
        {
            [Fact]
            public void Should_Throw_If_Path_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => ((string)null).IsFullPath());

                // Then
                result.IsArgumentNullException("path");
            }

            [Fact]
            public void Should_Throw_If_Path_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => string.Empty.IsFullPath());

                // Then
                result.IsArgumentOutOfRangeException("path");
            }

            [Fact]
            public void Should_Throw_If_Path_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => " ".IsFullPath());

                // Then
                result.IsArgumentOutOfRangeException("path");
            }

            [Fact]
            public void Should_Throw_If_Path_Is_Not_Valid()
            {
                // Given / When
                var result = Record.Exception(() => "c:\\foo\tbar".IsFullPath());

                // Then
                result.IsArgumentException("path");
            }

            [Theory]
            [InlineData(@"c:\foo")]
            [InlineData(@"c:\foo\")]
            [InlineData(@"c:\foo\bar")]
            public void Should_Return_True_If_Full_Path(string path)
            {
                // Given / When
                var result = path.IsFullPath();

                // Then
                result.ShouldBe(true);
            }

            [Theory]
            [InlineData(@"foo")]
            [InlineData(@"foo\")]
            [InlineData(@"foo\bar")]
            [InlineData(@"\foo")]
            [InlineData(@"\foo\")]
            [InlineData(@"\foo\bar")]
            [InlineData(@"/foo")]
            [InlineData(@"/foo")]
            [InlineData(@"/foo/bar")]
            public void Should_Return_False_If_Not_Full_Path(string path)
            {
                // Given / When
                var result = path.IsFullPath();

                // Then
                result.ShouldBe(false);
            }
        }

        public sealed class TheIsSubPathOfExtension
        {
            [Fact]
            public void Should_Throw_If_Path_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => ((string)null).IsSubpathOf(@"c:\"));

                // Then
                result.IsArgumentNullException("path");
            }

            [Fact]
            public void Should_Throw_If_Path_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => string.Empty.IsSubpathOf(@"c:\"));

                // Then
                result.IsArgumentOutOfRangeException("path");
            }

            [Fact]
            public void Should_Throw_If_Path_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => " ".IsSubpathOf(@"c:\"));

                // Then
                result.IsArgumentOutOfRangeException("path");
            }

            [Fact]
            public void Should_Throw_If_Path_Is_Invalid()
            {
                // Given / When
                var result = Record.Exception(() => "c:\\foo\tbar".IsSubpathOf(@"c:\foo"));

                // Then
                result.IsArgumentException("path");
            }

            [Fact]
            public void Should_Throw_If_BaseDirPath_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => @"c:\".IsSubpathOf(null));

                // Then
                result.IsArgumentNullException("baseDirPath");
            }

            [Fact]
            public void Should_Throw_If_BaseDirPath_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => @"c:\".IsSubpathOf(string.Empty));

                // Then
                result.IsArgumentOutOfRangeException("baseDirPath");
            }

            [Fact]
            public void Should_Throw_If_BaseDirPath_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => @"c:\".IsSubpathOf(" "));

                // Then
                result.IsArgumentOutOfRangeException("baseDirPath");
            }

            [Fact]
            public void Should_Throw_If_BaseDirPath_Is_Invalid()
            {
                // Given / When
                var result = Record.Exception(() => @"c:\foo\bar".IsSubpathOf("c:\\f\to"));

                // Then
                result.IsArgumentException("baseDirPath");
            }

            [Theory]
            [InlineData(@"c:\foo", @"c:")]
            [InlineData(@"c:\foo", @"c:\")]
            [InlineData(@"c:\foo", @"c:\foo")]
            [InlineData(@"c:\foo\bar", @"c:\foo")]
            [InlineData(@"c:\foo\bar\", @"c:\foo")]
            [InlineData(@"c:\foo\a.txt", @"c:\foo")]
            public void Should_Return_True_If_SubPath(string path, string baseDir)
            {
                // Given / When
                var result = path.IsSubpathOf(baseDir);

                // Then
                result.ShouldBe(true);
            }

            [Theory]
            [InlineData(@"c:\foobar", @"c:\foo")]
            [InlineData(@"c:\foobar\a.txt", @"c:\foo")]
            [InlineData(@"c:\foobar\a.txt", @"c:\foo\")]
            [InlineData(@"c:\foo\a.txt", @"c:\foobar")]
            [InlineData(@"c:\foo\a.txt", @"c:\foobar")]
            public void Should_Return_False_If_Not_SubPath(string path, string baseDir)
            {
                // Given / When
                var result = path.IsSubpathOf(baseDir);

                // Then
                result.ShouldBe(false);
            }

            [Theory]
            [InlineData(@"c:\foo", @"c:\foo\", true)]
            [InlineData(@"c:\foo\", @"c:\foo", true)]
            public void Should_Ignore_Trailing_Slashes(string path, string baseDir, bool expectedResult)
            {
                // Given / When
                var result = path.IsSubpathOf(baseDir);

                // Then
                result.ShouldBe(expectedResult);
            }

            // TODO Case sensitive if running on Unix?
            [Theory]
            [InlineData(@"c:\FOO\a.txt", @"c:\foo", true)]
            [InlineData(@"c:\foo\a.txt", @"c:\Foo", true)]
            public void Should_Ignore_Case(string path, string baseDir, bool expectedResult)
            {
                // Given / When
                var result = path.IsSubpathOf(baseDir);

                // Then
                result.ShouldBe(expectedResult);
            }

            [Theory]
            [InlineData(@"c:/foo/a.txt", @"c:\foo", true)]
            [InlineData(@"c:\foo\a.txt", @"c:/foo", true)]
            [InlineData(@"c:/foo/a.txt", @"c:/foo", true)]
            public void Should_Handle_Unix_Style_Slashes(string path, string baseDir, bool expectedResult)
            {
                // Given / When
                var result = path.IsSubpathOf(baseDir);

                // Then
                result.ShouldBe(expectedResult);
            }

            [Theory]
            [InlineData(@"c:\foo\..\bar\baz", @"c:\foo", false)]
            [InlineData(@"c:\foo\..\bar\baz", @"c:\bar", true)]
            [InlineData(@"c:\foo\..\bar\baz", @"c:\barr", false)]
            public void Should_Work_With_DotDot(string path, string baseDir, bool expectedResult)
            {
                // Given / When
                var result = path.IsSubpathOf(baseDir);

                // Then
                result.ShouldBe(expectedResult);
            }

            [Theory]
            [InlineData(@"foo\bar", @"foo", true)]
            [InlineData(@"..\foo\bar", @"..\foo", true)]
            [InlineData(@"foo\bar", @"\foo", false)]
            public void Should_Work_With_RelativePaths(string path, string baseDir, bool expectedResult)
            {
                // Given / When
                var result = path.IsSubpathOf(baseDir);

                // Then
                result.ShouldBe(expectedResult);
            }
        }

        public sealed class TheNormalizePathExtension
        {
            [Fact]
            public void Should_Throw_If_Path_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => ((string)null).NormalizePath());

                // Then
                result.IsArgumentNullException("path");
            }

            [Fact]
            public void Should_Throw_If_Path_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => string.Empty.NormalizePath());

                // Then
                result.IsArgumentOutOfRangeException("path");
            }

            [Fact]
            public void Should_Throw_If_Path_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => " ".NormalizePath());

                // Then
                result.IsArgumentOutOfRangeException("path");
            }

            [Fact]
            public void Should_Throw_If_Path_Is_Invalid()
            {
                // Given / When
                var result = Record.Exception(() => "c:\\foo\tbar".NormalizePath());

                // Then
                result.IsArgumentException("path");
            }

            [Theory]
            [InlineData(@"c:/foo", @"c:\foo")]
            [InlineData(@"c:/foo/bar", @"c:\foo\bar")]
            public void Should_Convert_Unix_To_Windows_Style_Slashes(string path, string expectedResult)
            {
                // Given / When
                var result = path.NormalizePath();

                // Then
                result.ShouldBe(expectedResult);
            }

            [Theory]
            [InlineData(@"c:\foo", @"c:\foo")]
            [InlineData(@"c:\foo\bar", @"c:\foo\bar")]
            public void Should_Ignore_Windows_Style_Slashes(string path, string expectedResult)
            {
                // Given / When
                var result = path.NormalizePath();

                // Then
                result.ShouldBe(expectedResult);
            }

            [Theory]
            [InlineData(@"c:\foo\", @"c:\foo\")]
            [InlineData(@"c:\foo\bar\", @"c:\foo\bar\")]
            public void Should_Handle_Trailing_Slashes(string path, string expectedResult)
            {
                // Given / When
                var result = path.NormalizePath();

                // Then
                result.ShouldBe(expectedResult);
            }
        }
    }
}
