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
                const string filePath = null;
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
                const string filePath = " ";
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
                const string filePath = @"c:\repo\foo.ch";
                const RepositorySettings settings = null;

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
                var (valid, _) = format.ValidateFilePath(filePath, settings);

                // Then
                valid.ShouldBe(expectedValue);
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
                var (_, resultFilePath) = format.ValidateFilePath(filePath, settings);

                // Then
                resultFilePath.ShouldBe(expectedValue);
            }
        }
    }
}
