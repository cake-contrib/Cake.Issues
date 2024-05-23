namespace Cake.Issues.Tests;

public sealed class FileLinkSettingsTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Builder_Is_Null()
        {
            // Given
            const Func<IIssue, IDictionary<string, string>, Uri> builder = null;

            // When
            var result = Record.Exception(() => new FileLinkSettings(builder));

            // Then
            result.IsArgumentNullException("builder");
        }
    }

    public sealed class TheForPatternMethod
    {
        [Fact]
        public void Should_Throw_If_Pattern_Is_Null()
        {
            // Given
            const string pattern = null;

            // When
            var result = Record.Exception(() => FileLinkSettings.ForPattern(pattern));

            // Then
            result.IsArgumentNullException("pattern");
        }

        [Fact]
        public void Should_Throw_If_Pattern_Is_Empty()
        {
            // Given
            var pattern = string.Empty;

            // When
            var result = Record.Exception(() => FileLinkSettings.ForPattern(pattern));

            // Then
            result.IsArgumentOutOfRangeException("pattern");
        }

        [Fact]
        public void Should_Throw_If_Pattern_Is_WhiteSpace()
        {
            // Given
            const string pattern = " ";

            // When
            var result = Record.Exception(() => FileLinkSettings.ForPattern(pattern));

            // Then
            result.IsArgumentOutOfRangeException("pattern");
        }
    }

    public sealed class TheForActionMethod
    {
        [Fact]
        public void Should_Throw_If_Builder_Is_Null()
        {
            // Given
            const Func<IIssue, Uri> builder = null;

            // When
            var result = Record.Exception(() => FileLinkSettings.ForAction(builder));

            // Then
            result.IsArgumentNullException("builder");
        }
    }

    public sealed class TheForGitHubMethod
    {
        [Fact]
        public void Should_Throw_If_RepositoryUrl_Is_Null()
        {
            // Given
            const Uri repositoryUrl = null;

            // When
            var result = Record.Exception(() => FileLinkSettings.ForGitHub(repositoryUrl));

            // Then
            result.IsArgumentNullException("repositoryUrl");
        }
    }

    public sealed class TheForAzureDevOpsMethod
    {
        [Fact]
        public void Should_Throw_If_RepositoryUrl_Is_Null()
        {
            // Given
            const Uri repositoryUrl = null;

            // When
            var result = Record.Exception(() => FileLinkSettings.ForAzureDevOps(repositoryUrl));

            // Then
            result.IsArgumentNullException("repositoryUrl");
        }
    }

    public sealed class TheGetFileLinkMethod
    {
        [Fact]
        public void Should_Throw_If_Issue_Is_Null()
        {
            // Given
            const IIssue issue = null;

            // When
            var result = Record.Exception(() => FileLinkSettings.ForPattern("foo").GetFileLink(issue));

            // Then
            result.IsArgumentNullException("issue");
        }
    }
}
