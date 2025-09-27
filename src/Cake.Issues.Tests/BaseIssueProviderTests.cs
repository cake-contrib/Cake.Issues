namespace Cake.Issues.Tests;

public sealed class BaseIssueProviderTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() => new FakeIssueProvider(null));

            // Then
            result.IsArgumentNullException("log");
        }

        [Fact]
        public void Should_Set_Log()
        {
            // Given
            var log = new FakeLog();

            // When
            var provider = new FakeIssueProvider(log);

            // Then
            provider.Log.ShouldBe(log);
        }
    }

    public sealed class TheProviderTypeProperty
    {
        [Fact]
        public void Should_Return_Full_Type_Name_Of_Concrete_IssueProvider()
        {
            // Given
            var provider = new FakeIssueProvider(new FakeLog());

            // When
            var result = provider.ProviderType;

            // Then
            result.ShouldBe("Cake.Issues.Testing.FakeIssueProvider");
        }
    }

    public sealed class TheReadIssuesMethod
    {
        [Fact]
        public void Should_Throw_If_Settings_Is_Null()
        {
            // Given
            var provider = new FakeIssueProvider(new FakeLog());

            // When
            var result = Record.Exception(provider.ReadIssues);

            // Then
            result.IsInvalidOperationException("Initialize needs to be called first.");
        }
    }

    public sealed class TheValidateFilePathMethod
    {
        [Fact]
        public void Should_Throw_If_FilePath_Is_Null()
        {
            // Given
            const string filePath = null;
            var repositorySettings = new RepositorySettings(@"c:\repo");

            // When
            var result = Record.Exception(() => FakeIssueProviderForTesting.CallValidateFilePath(filePath, repositorySettings));

            // Then
            result.IsArgumentNullException("filePath");
        }

        [Fact]
        public void Should_Throw_If_FilePath_Is_Empty()
        {
            // Given
            var filePath = string.Empty;
            var repositorySettings = new RepositorySettings(@"c:\repo");

            // When
            var result = Record.Exception(() => FakeIssueProviderForTesting.CallValidateFilePath(filePath, repositorySettings));

            // Then
            result.IsArgumentOutOfRangeException("filePath");
        }

        [Fact]
        public void Should_Throw_If_FilePath_Is_WhiteSpace()
        {
            // Given
            const string filePath = " ";
            var repositorySettings = new RepositorySettings(@"c:\repo");

            // When
            var result = Record.Exception(() => FakeIssueProviderForTesting.CallValidateFilePath(filePath, repositorySettings));

            // Then
            result.IsArgumentOutOfRangeException("filePath");
        }

        [Fact]
        public void Should_Throw_If_RepositorySettings_Are_Null()
        {
            // Given
            const string filePath = @"c:\repo\foo.cs";
            const IRepositorySettings repositorySettings = null;

            // When
            var result = Record.Exception(() => FakeIssueProviderForTesting.CallValidateFilePath(filePath, repositorySettings));

            // Then
            result.IsArgumentNullException("repositorySettings");
        }

        [Theory]
        [InlineData(@"c:\foo\bar.cs", @"c:\foo\", true)]
        [InlineData(@"c:\foo\bar.cs", @"c:\foo", true)]
        [InlineData(@"c:\foo\bar.cs", @"c:\bar", false)]
        public void Should_Return_Correct_Value_For_Valid_Absolute_Path(
            string filePath,
            string repoRoot,
            bool expectedValue)
        {
            // Given
            var repositorySettings = new RepositorySettings(repoRoot);

            // When
            var (valid, _) = FakeIssueProviderForTesting.CallValidateFilePath(filePath, repositorySettings);

            // Then
            valid.ShouldBe(expectedValue);
        }

        [Theory]
        [InlineData(@"c:\foo\bar.cs", @"c:\foo\", "bar.cs")]
        [InlineData(@"c:\foo\bar.cs", @"c:\foo", "bar.cs")]
        [InlineData(@"c:\foo\bar.cs", @"c:\bar", "")]
        public void Should_Return_Correct_FilePath_For_Absolute_Path(
            string filePath,
            string repoRoot,
            string expectedValue)
        {
            // Given
            var repositorySettings = new RepositorySettings(repoRoot);

            // When
            var (_, resultFilePath) = FakeIssueProviderForTesting.CallValidateFilePath(filePath, repositorySettings);

            // Then
            resultFilePath.ShouldBe(expectedValue);
        }

        [Theory]
        [InlineData("foo.cs", true)]
        [InlineData(@"foo\bar.cs", true)]
        [InlineData("bar.cs", true)]
        public void Should_Return_True_For_Relative_Path(
            string filePath,
            bool expectedValue)
        {
            // Given
            var repositorySettings = new RepositorySettings(@"c:\repo");

            // When
            var (valid, _) = FakeIssueProviderForTesting.CallValidateFilePath(filePath, repositorySettings);

            // Then
            valid.ShouldBe(expectedValue);
        }

        [Theory]
        [InlineData("foo.cs", "foo.cs")]
        [InlineData(@"foo\bar.cs", @"foo\bar.cs")]
        [InlineData("bar.cs", "bar.cs")]
        public void Should_Return_Unchanged_FilePath_For_Relative_Path(
            string filePath,
            string expectedValue)
        {
            // Given
            var repositorySettings = new RepositorySettings(@"c:\repo");

            // When
            var (_, resultFilePath) = FakeIssueProviderForTesting.CallValidateFilePath(filePath, repositorySettings);

            // Then
            resultFilePath.ShouldBe(expectedValue);
        }
    }

    /// <summary>
    /// Test helper class that exposes protected static methods for testing.
    /// </summary>
    private class FakeIssueProviderForTesting : BaseIssueProvider
    {
        public FakeIssueProviderForTesting()
            : base(new FakeLog())
        {
        }

        public override string ProviderName => "Fake Provider";

        public static (bool Valid, string FilePath) CallValidateFilePath(string filePath, IRepositorySettings repositorySettings) =>
            BaseIssueProvider.ValidateFilePath(filePath, repositorySettings);

        protected override IEnumerable<IIssue> InternalReadIssues() => [];
    }
}
