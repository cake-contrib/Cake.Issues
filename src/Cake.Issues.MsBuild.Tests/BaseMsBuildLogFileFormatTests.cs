namespace Cake.Issues.MsBuild.Tests
{
    using Cake.Issues.Testing;
    using Cake.Testing;
    using Shouldly;
    using Xunit;

    public sealed class BaseMsBuildLogFileFormatTests
    {
        public sealed class TheValidateFilePathMethod
        {
            [Fact]
            public void Should_Throw_If_FilePath_Is_Null()
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                string filePath = null;
                var settings = new RepositorySettings(@"c:\repo");

                // When
                var result = Record.Exception(() => format.ValidateFilePath(filePath, settings));

                // Then
                result.IsArgumentNullException("filePath");
            }

            [Fact]
            public void Should_Throw_If_FilePath_Is_Empty()
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                var filePath = string.Empty;
                var settings = new RepositorySettings(@"c:\repo");

                // When
                var result = Record.Exception(() => format.ValidateFilePath(filePath, settings));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_FilePath_Is_WhiteSpace()
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                var filePath = " ";
                var settings = new RepositorySettings(@"c:\repo");

                // When
                var result = Record.Exception(() => format.ValidateFilePath(filePath, settings));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                var filePath = @"c:\repo\foo.ch";
                RepositorySettings settings = null;

                // When
                var result = Record.Exception(() => format.ValidateFilePath(filePath, settings));

                // Then
                result.IsArgumentNullException("repositorySettings");
            }

            [Theory]
            [InlineData(@"c:\foo\bar.cs", @"c:\foo\", true)]
            [InlineData(@"c:\foo\bar.cs", @"c:\foo", true)]
            [InlineData(@"c:\foo\bar.cs", @"c:\bar", false)]
            public void Should_Return_Correct_Value_For_Valid(
                string filePath,
                string repoRoot,
                bool expectedValue)
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                var settings = new RepositorySettings(repoRoot);

                // When
                var result = format.ValidateFilePath(filePath, settings);

                // Then
                result.Valid.ShouldBe(expectedValue);
            }

            [Theory]
            [InlineData(@"c:\foo\bar.cs", @"c:\foo\", @"bar.cs")]
            [InlineData(@"c:\foo\bar.cs", @"c:\foo", @"bar.cs")]
            [InlineData(@"c:\foo\bar.cs", @"c:\bar", @"")]
            public void Should_Return_Correct_Value_For_FilePath(
                string filePath,
                string repoRoot,
                string expectedValue)
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                var settings = new RepositorySettings(repoRoot);

                // When
                var result = format.ValidateFilePath(filePath, settings);

                // Then
                result.FilePath.ShouldBe(expectedValue);
            }
        }

        public sealed class TheCheckIfFileIsInRepositoryMethod
        {
            [Fact]
            public void Should_Throw_If_FilePath_Is_Null()
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                string filePath = null;
                var settings = new RepositorySettings(@"c:\repo");

                // When
                var result = Record.Exception(() => format.CheckIfFileIsInRepository(filePath, settings));

                // Then
                result.IsArgumentNullException("filePath");
            }

            [Fact]
            public void Should_Throw_If_FilePath_Is_Empty()
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                var filePath = string.Empty;
                var settings = new RepositorySettings(@"c:\repo");

                // When
                var result = Record.Exception(() => format.CheckIfFileIsInRepository(filePath, settings));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_FilePath_Is_WhiteSpace()
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                var filePath = " ";
                var settings = new RepositorySettings(@"c:\repo");

                // When
                var result = Record.Exception(() => format.CheckIfFileIsInRepository(filePath, settings));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                var filePath = @"c:\repo\foo.ch";
                RepositorySettings settings = null;

                // When
                var result = Record.Exception(() => format.CheckIfFileIsInRepository(filePath, settings));

                // Then
                result.IsArgumentNullException("repositorySettings");
            }

            [Theory]
            [InlineData(@"c:\foo\bar.cs", @"c:\foo\", true)]
            [InlineData(@"c:\foo\bar.cs", @"c:\foo", true)]
            [InlineData(@"c:\foo\bar.cs", @"c:\bar", false)]
            public void Should_Check_If_File_Is_In_Repository(
                string filePath,
                string repoRoot,
                bool expectedValue)
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                var settings = new RepositorySettings(repoRoot);

                // When
                var result = format.CheckIfFileIsInRepository(filePath, settings);

                // Then
                result.ShouldBe(expectedValue);
            }
        }

        public sealed class TheMakeFilePathRelativeToRepositoryRootMethod
        {
            [Fact]
            public void Should_Throw_If_FilePath_Is_Null()
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                string filePath = null;
                var settings = new RepositorySettings(@"c:\repo");

                // When
                var result = Record.Exception(() => format.MakeFilePathRelativeToRepositoryRoot(filePath, settings));

                // Then
                result.IsArgumentNullException("filePath");
            }

            [Fact]
            public void Should_Throw_If_FilePath_Is_Empty()
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                var filePath = string.Empty;
                var settings = new RepositorySettings(@"c:\repo");

                // When
                var result = Record.Exception(() => format.MakeFilePathRelativeToRepositoryRoot(filePath, settings));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_FilePath_Is_WhiteSpace()
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                var filePath = " ";
                var settings = new RepositorySettings(@"c:\repo");

                // When
                var result = Record.Exception(() => format.MakeFilePathRelativeToRepositoryRoot(filePath, settings));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                var filePath = @"c:\repo\foo.ch";
                RepositorySettings settings = null;

                // When
                var result = Record.Exception(() => format.MakeFilePathRelativeToRepositoryRoot(filePath, settings));

                // Then
                result.IsArgumentNullException("repositorySettings");
            }

            [Theory]
            [InlineData(@"c:\foo\bar.cs", @"c:\foo\", @"\bar.cs")]
            [InlineData(@"c:\foo\bar.cs", @"c:\foo", @"\bar.cs")]
            public void Should_Make_FilePath_Relative_To_Repository_Root(
                string filePath,
                string repoRoot,
                string expectedValue)
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                var settings = new RepositorySettings(repoRoot);

                // When
                var result = format.MakeFilePathRelativeToRepositoryRoot(filePath, settings);

                // Then
                result.ShouldBe(expectedValue);
            }
        }

        public sealed class TheRemoveLeadingDirectorySeparatorMethod
        {
            [Fact]
            public void Should_Throw_If_FilePath_Is_Null()
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                string filePath = null;

                // When
                var result = Record.Exception(() => format.RemoveLeadingDirectorySeparator(filePath));

                // Then
                result.IsArgumentNullException("filePath");
            }

            [Fact]
            public void Should_Throw_If_FilePath_Is_Empty()
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                var filePath = string.Empty;

                // When
                var result = Record.Exception(() => format.RemoveLeadingDirectorySeparator(filePath));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Fact]
            public void Should_Throw_If_FilePath_Is_WhiteSpace()
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());
                var filePath = " ";

                // When
                var result = Record.Exception(() => format.RemoveLeadingDirectorySeparator(filePath));

                // Then
                result.IsArgumentOutOfRangeException("filePath");
            }

            [Theory]
            [InlineData(@"\foo\bar.cs", @"foo\bar.cs")]
            [InlineData(@"foo\bar.cs", @"foo\bar.cs")]
            public void Should_Remove_Leading_Directory_Separator(string filePath, string expectedValue)
            {
                // Given
                var format = new FakeMsBuildLogFileFormat(new FakeLog());

                // When
                var result = format.RemoveLeadingDirectorySeparator(filePath);

                // Then
                result.ShouldBe(expectedValue);
            }
        }
    }
}
