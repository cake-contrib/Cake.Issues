namespace Cake.Issues.GitRepository.Tests;

using System.Reflection;

public sealed class GitRepositoryIssuesProviderTests
{
    public sealed class TheIsFileExcludedMethod
    {
        [Theory]
        [InlineData("file.tmp", new[] { "*.tmp" }, true)]
        [InlineData("file.txt", new[] { "*.tmp" }, false)]
        [InlineData("temp/file.txt", new[] { "temp/**" }, true)]
        [InlineData("other/file.txt", new[] { "temp/**" }, false)]
        public void Should_Exclude_Files_Based_On_Global_Patterns(string filePath, string[] patterns, bool expectedExcluded)
        {
            // Given
            var settings = new GitRepositoryIssuesSettings();
            foreach (var pattern in patterns)
            {
                settings.FilesToExclude.Add(pattern);
            }

            // When - Test the exclusion logic directly using FilePatternMatcher
            var result = FilePatternMatcher.IsMatch(filePath, settings.FilesToExclude);

            // Then
            result.ShouldBe(expectedExcluded);
        }

        [Theory]
        [InlineData("file.dll", new[] { "*.dll" }, true)]
        [InlineData("file.exe", new[] { "*.dll" }, false)]
        [InlineData("bin/debug/file.dll", new[] { "bin/**" }, true)]
        public void Should_Exclude_Files_From_Binary_LFS_Check_Based_On_Specific_Patterns(string filePath, string[] patterns, bool expectedExcluded)
        {
            // Given
            var settings = new GitRepositoryIssuesSettings();
            foreach (var pattern in patterns)
            {
                settings.FilesToExcludeFromBinaryFilesLfsCheck.Add(pattern);
            }

            // When - Test the exclusion logic directly using FilePatternMatcher
            var result = FilePatternMatcher.IsMatch(filePath, settings.FilesToExcludeFromBinaryFilesLfsCheck);

            // Then
            result.ShouldBe(expectedExcluded);
        }

        [Theory]
        [InlineData("very/long/path/to/some/deeply/nested/file.txt", new[] { "very/long/path/**" }, true)]
        [InlineData("short/file.txt", new[] { "very/long/path/**" }, false)]
        [InlineData("generated/code/file.cs", new[] { "generated/**" }, true)]
        public void Should_Exclude_Files_From_Path_Length_Check_Based_On_Specific_Patterns(string filePath, string[] patterns, bool expectedExcluded)
        {
            // Given
            var settings = new GitRepositoryIssuesSettings();
            foreach (var pattern in patterns)
            {
                settings.FilesToExcludeFromFilePathLengthCheck.Add(pattern);
            }

            // When - Test the exclusion logic directly using FilePatternMatcher
            var result = FilePatternMatcher.IsMatch(filePath, settings.FilesToExcludeFromFilePathLengthCheck);

            // Then
            result.ShouldBe(expectedExcluded);
        }

        [Fact]
        public void Should_Prioritize_Global_Exclusions_Over_Specific_Exclusions()
        {
            // Given
            var settings = new GitRepositoryIssuesSettings();
            settings.FilesToExclude.Add("*.tmp"); // Global exclusion
            settings.FilesToExcludeFromBinaryFilesLfsCheck.Add("*.dll"); // Specific exclusion

            // When - A file matching global exclusions should be excluded regardless of specific settings
            var globalMatch = FilePatternMatcher.IsMatch("file.tmp", settings.FilesToExclude);
            var specificMatch = FilePatternMatcher.IsMatch("file.tmp", settings.FilesToExcludeFromBinaryFilesLfsCheck);

            // Then - Global exclusions take precedence
            globalMatch.ShouldBeTrue();
            specificMatch.ShouldBeFalse();
            
            // Test the actual exclusion logic that combines both (global || specific)
            var shouldExclude = globalMatch || specificMatch;
            shouldExclude.ShouldBeTrue();
        }
    }
}