namespace Cake.Issues.Tests
{
    using System.Runtime.InteropServices;
    using Cake.Issues.Testing;
    using Shouldly;
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
            [InlineData(@"/foo/")]
            [InlineData(@"/foo/bar")]
            public void Should_Return_True_If_Valid_Path(string path)
            {
                // Given / When
                var result = path.IsValidPath();

                // Then
                result.ShouldBeTrue();
            }

            [Theory]
            [InlineData("foo\0bar")]
            public void Should_Return_False_If_Invalid_Path(string path)
            {
                // Given / When
                var result = path.IsValidPath();

                // Then
                result.ShouldBeFalse();
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
                var result = Record.Exception(() => "c:\\foo\0bar".IsFullPath());

                // Then
                result.IsArgumentException("path");
            }

            [SkippableTheory]
            [InlineData(@"c:\foo")]
            [InlineData(@"c:\foo\")]
            [InlineData(@"c:\foo\bar")]
            public void Should_Return_True_If_Full_Path(string path)
            {
                // Uses Windows specific paths.
                Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

                // Given / When
                var result = path.IsFullPath();

                // Then
                result.ShouldBeTrue();
            }

            [Theory]
            [InlineData(@"foo")]
            [InlineData(@"foo\")]
            [InlineData(@"foo\bar")]
            [InlineData(@"\foo")]
            [InlineData(@"\foo\")]
            [InlineData(@"\foo\bar")]
            [InlineData(@"/foo")]
            [InlineData(@"/foo/")]
            [InlineData(@"/foo/bar")]
            public void Should_Return_False_If_Not_Full_Path(string path)
            {
                // Given / When
                var result = path.IsFullPath();

                // Then
                result.ShouldBeFalse();
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
                var result = Record.Exception(() => "c:\\foo\0bar".IsSubpathOf(@"c:\foo"));

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
                var result = Record.Exception(() => @"c:\foo\bar".IsSubpathOf("c:\\f\0o"));

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
                result.ShouldBeTrue();
            }

            [Theory]
            [InlineData(@"c:\foobar", @"c:\foo")]
            [InlineData(@"c:\foobar\a.txt", @"c:\foo")]
            [InlineData(@"c:\foobar\a.txt", @"c:\foo\")]
            [InlineData(@"c:\foo\a.txt", @"c:\foobar")]
            [InlineData(@"c:\foo\a.txt", @"c:\foobar\")]
            public void Should_Return_False_If_Not_SubPath(string path, string baseDir)
            {
                // Given / When
                var result = path.IsSubpathOf(baseDir);

                // Then
                result.ShouldBeFalse();
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

            [SkippableTheory]
            [InlineData(@"c:\foo\..\bar\baz", @"c:\foo", false)]
            [InlineData(@"c:\foo\..\bar\baz", @"c:\bar", true)]
            [InlineData(@"c:\foo\..\bar\baz", @"c:\barr", false)]
            public void Should_Work_With_DotDot(string path, string baseDir, bool expectedResult)
            {
                // Uses Windows specific paths.
                Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

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
                var result = Record.Exception(() => "c:\\foo\0bar".NormalizePath());

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

        public sealed class TheIsValidRepositoryFilePathExtension
        {
            [Fact]
            public void Should_Throw_If_FilePath_Is_Null()
            {
                // Given / When
                var result =
                    Record.Exception(
                        () =>
                            ((string)null).IsValidRepositoryFilePath(new RepositorySettings(@"C:\repo")));

                // Then
                result.IsArgumentNullException("filePath");
            }

            [Fact]
            public void Should_Throw_If_FilePath_Is_Empty()
            {
                // Given / When
                var result =
                    Record.Exception(
                        () =>
                            string.Empty.IsValidRepositoryFilePath(new RepositorySettings(@"C:\repo")));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_FilePath_Is_WhiteSpace()
            {
                // Given / When
                var result =
                    Record.Exception(
                        () =>
                            " ".IsValidRepositoryFilePath(new RepositorySettings(@"C:\repo")));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_RepositorySettings_Are_Null()
            {
                // Given / When
                var result =
                    Record.Exception(
                        () =>
                            @"C:\repo".IsValidRepositoryFilePath(null));

                // Then
                result.IsArgumentNullException("repositorySettings");
            }

            [Theory]
            [InlineData(@"C:\repo", @"C:\repo\foo")]
            [InlineData(@"C:\repo", @"C:\repo\foo\")]
            [InlineData(@"C:\repo", @"C:\repo\foo\bar")]
            [InlineData("/repo", "/repo/foo")]
            [InlineData("/repo", "/repo/foo/")]
            [InlineData("/repo", "/repo/foo/bar")]
            public void Should_Return_True_If_File_Is_Valid_Repository_FilePath(string repoRoot, string filePath)
            {
                // Given
                var repositorySettings = new RepositorySettings(repoRoot);

                // When
                var (valid, _) = filePath.IsValidRepositoryFilePath(repositorySettings);

                // Then
                valid.ShouldBeTrue();
            }

            [Theory]
            [InlineData(@"C:\repo", @"C:\r\foo")]
            [InlineData(@"C:\repo", @"C:\r\foo\")]
            [InlineData(@"C:\repo", @"C:\r\foo\bar")]
            [InlineData("/repo", "/r/foo")]
            [InlineData("/repo", "/r/foo/")]
            [InlineData("/repo", "/r/foo/bar")]
            public void Should_Return_False_If_File_Is_Outside_Repository(string repoRoot, string filePath)
            {
                // Given
                var repositorySettings = new RepositorySettings(repoRoot);

                // When
                var (valid, _) = filePath.IsValidRepositoryFilePath(repositorySettings);

                // Then
                valid.ShouldBeFalse();
            }

            [Theory]
            [InlineData(@"C:\repo", @"C:\repo\foo", @"foo")]
            [InlineData(@"C:\repo", @"C:\repo\foo\", @"foo\")]
            [InlineData(@"C:\repo", @"C:\repo\foo\bar", @"foo\bar")]
            [InlineData("/repo", "/repo/foo", "foo")]
            [InlineData("/repo", "/repo/foo/", "foo/")]
            [InlineData("/repo", "/repo/foo/bar", "foo/bar")]
            [InlineData(@"C:\repo", @"C:\r\foo", "")]
            public void Should_Return_Correct_FilePath(string repoRoot, string filePath, string expectedResult)
            {
                // Given
                var repositorySettings = new RepositorySettings(repoRoot);

                // When
                var (_, filePathResult) = filePath.IsValidRepositoryFilePath(repositorySettings);

                // Then
                filePathResult.ShouldBe(expectedResult);
            }
        }

        public sealed class TheIsInRepositoryExtension
        {
            [Fact]
            public void Should_Throw_If_FilePath_Is_Null()
            {
                // Given / When
                var result =
                    Record.Exception(
                        () =>
                            ((string)null).IsInRepository(new RepositorySettings(@"C:\repo")));

                // Then
                result.IsArgumentNullException("filePath");
            }

            [Fact]
            public void Should_Throw_If_FilePath_Is_Empty()
            {
                // Given / When
                var result =
                    Record.Exception(
                        () =>
                            string.Empty.IsInRepository(new RepositorySettings(@"C:\repo")));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_FilePath_Is_WhiteSpace()
            {
                // Given / When
                var result =
                    Record.Exception(
                        () =>
                            " ".IsInRepository(new RepositorySettings(@"C:\repo")));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_RepositorySettings_Are_Null()
            {
                // Given / When
                var result =
                    Record.Exception(
                        () =>
                            @"C:\repo".IsInRepository(null));

                // Then
                result.IsArgumentNullException("repositorySettings");
            }

            [Theory]
            [InlineData(@"C:\repo", @"C:\repo\foo")]
            [InlineData(@"C:\repo", @"C:\repo\foo\")]
            [InlineData(@"C:\repo", @"C:\repo\foo\bar")]
            [InlineData("/repo", "/repo/foo")]
            [InlineData("/repo", "/repo/foo/")]
            [InlineData("/repo", "/repo/foo/bar")]
            public void Should_Return_True_If_File_Is_In_Repository(string repoRoot, string filePath)
            {
                // Given
                var repositorySettings = new RepositorySettings(repoRoot);

                // When
                var result = filePath.IsInRepository(repositorySettings);

                // Then
                result.ShouldBeTrue();
            }

            [Theory]
            [InlineData(@"C:\repo", @"C:\r\foo")]
            [InlineData(@"C:\repo", @"C:\r\foo\")]
            [InlineData(@"C:\repo", @"C:\r\foo\bar")]
            [InlineData("/repo", "/r/foo")]
            [InlineData("/repo", "/r/foo/")]
            [InlineData("/repo", "/r/foo/bar")]
            public void Should_Return_False_If_File_Is_Outside_Repository(string repoRoot, string filePath)
            {
                // Given
                var repositorySettings = new RepositorySettings(repoRoot);

                // When
                var result = filePath.IsInRepository(repositorySettings);

                // Then
                result.ShouldBeFalse();
            }
        }

        public sealed class TheMakeFilePathRelativeToRepositoryRootExtension
        {
            [Fact]
            public void Should_Throw_If_FilePath_Is_Null()
            {
                // Given / When
                var result =
                    Record.Exception(
                        () =>
                            ((string)null).MakeFilePathRelativeToRepositoryRoot(new RepositorySettings(@"C:\repo")));

                // Then
                result.IsArgumentNullException("filePath");
            }

            [Fact]
            public void Should_Throw_If_FilePath_Is_Empty()
            {
                // Given / When
                var result =
                    Record.Exception(
                        () =>
                            string.Empty.MakeFilePathRelativeToRepositoryRoot(new RepositorySettings(@"C:\repo")));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_FilePath_Is_WhiteSpace()
            {
                // Given / When
                var result =
                    Record.Exception(
                        () =>
                            " ".MakeFilePathRelativeToRepositoryRoot(new RepositorySettings(@"C:\repo")));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_RepositorySettings_Are_Null()
            {
                // Given / When
                var result =
                    Record.Exception(
                        () =>
                            @"C:\repo".MakeFilePathRelativeToRepositoryRoot(null));

                // Then
                result.IsArgumentNullException("repositorySettings");
            }

            [Theory]
            [InlineData(@"C:\repo", @"C:\repo\foo", @"foo")]
            [InlineData(@"C:\repo", @"C:\repo\foo\", @"foo\")]
            [InlineData(@"C:\repo", @"C:\repo\foo\bar", @"foo\bar")]
            [InlineData("/repo", "/repo/foo", "foo")]
            [InlineData("/repo", "/repo/foo/", "foo/")]
            [InlineData("/repo", "/repo/foo/bar", "foo/bar")]
            public void Should_Make_FilePath_Relative_To_Repository_Root(string repoRoot, string filePath, string expectedResult)
            {
                // Given
                var repositorySettings = new RepositorySettings(repoRoot);

                // When
                var result = filePath.MakeFilePathRelativeToRepositoryRoot(repositorySettings);

                // Then
                result.ShouldBe(expectedResult);
            }
        }

        public sealed class TheRemoveLeadingDirectorySeparatorExtension
        {
            [Fact]
            public void Should_Throw_If_FilePath_Is_Null()
            {
                // Given / When
                var result =
                    Record.Exception(
                        () =>
                            ((string)null).RemoveLeadingDirectorySeparator());

                // Then
                result.IsArgumentNullException("filePath");
            }

            [Fact]
            public void Should_Throw_If_FilePath_Is_Empty()
            {
                // Given / When
                var result =
                    Record.Exception(
                        () =>
                            string.Empty.RemoveLeadingDirectorySeparator());

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_FilePath_Is_WhiteSpace()
            {
                // Given / When
                var result =
                    Record.Exception(
                        () =>
                            " ".RemoveLeadingDirectorySeparator());

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Theory]
            [InlineData("foo", "foo")]
            [InlineData(@"foo\", @"foo\")]
            [InlineData(@"foo\bar", @"foo\bar")]
            [InlineData(@"\foo", @"foo")]
            [InlineData(@"\foo\", @"foo\")]
            [InlineData(@"\foo\bar", @"foo\bar")]
            [InlineData(@"c:\foo", @"c:\foo")]
            [InlineData(@"c:\foo\", @"c:\foo\")]
            [InlineData(@"c:\foo\bar", @"c:\foo\bar")]
            [InlineData("/foo", "foo")]
            [InlineData("/foo/", "foo/")]
            [InlineData("/foo/bar", "foo/bar")]
            public void Should_Remove_Leading_Directory_Separators(string filePath, string expectedResult)
            {
                // Given / When
                var result = filePath.RemoveLeadingDirectorySeparator();

                // Then
                result.ShouldBe(expectedResult);
            }
        }
    }
}
