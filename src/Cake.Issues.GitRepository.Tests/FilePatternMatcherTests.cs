namespace Cake.Issues.GitRepository.Tests;

public sealed class FilePatternMatcherTests
{
    public sealed class TheIsMatchMethod
    {
        [Theory]
        [InlineData("file.txt", "file.txt", true)]
        [InlineData("file.txt", "File.txt", true)] // Case insensitive
        [InlineData("path/file.txt", "path/file.txt", true)]
        [InlineData("path\\file.txt", "path/file.txt", true)] // Different separators
        [InlineData("file.txt", "other.txt", false)]
        [InlineData("path/file.txt", "file.txt", false)]
        public void Should_Match_Exact_Paths_Correctly(string filePath, string pattern, bool expected)
        {
            // When
            var result = FilePatternMatcher.IsMatch(filePath, pattern);

            // Then
            result.ShouldBe(expected);
        }

        [Theory]
        [InlineData("file.txt", "*.txt", true)]
        [InlineData("file.doc", "*.txt", false)]
        [InlineData("test.file.txt", "*.txt", true)]
        [InlineData("path/file.txt", "*.txt", false)] // * doesn't match /
        [InlineData("file.txt", "file.*", true)]
        [InlineData("file", "file.*", false)]
        public void Should_Match_Single_Wildcard_Correctly(string filePath, string pattern, bool expected)
        {
            // When
            var result = FilePatternMatcher.IsMatch(filePath, pattern);

            // Then
            result.ShouldBe(expected);
        }

        [Theory]
        [InlineData("path/to/file.txt", "**/file.txt", true)]
        [InlineData("file.txt", "**/file.txt", true)]
        [InlineData("deep/nested/path/file.txt", "**/file.txt", true)]
        [InlineData("path/to/other.txt", "**/file.txt", false)]
        [InlineData("path/to/file.txt", "path/**/file.txt", true)]
        [InlineData("other/to/file.txt", "path/**/file.txt", false)]
        public void Should_Match_Double_Wildcard_Correctly(string filePath, string pattern, bool expected)
        {
            // When
            var result = FilePatternMatcher.IsMatch(filePath, pattern);

            // Then
            result.ShouldBe(expected);
        }

        [Theory]
        [InlineData("file1.txt", "file?.txt", true)]
        [InlineData("fileA.txt", "file?.txt", true)]
        [InlineData("file12.txt", "file?.txt", false)] // ? matches only one character
        [InlineData("file.txt", "file?.txt", false)]
        [InlineData("path/file1.txt", "path/file?.txt", true)]
        public void Should_Match_Question_Mark_Correctly(string filePath, string pattern, bool expected)
        {
            // When
            var result = FilePatternMatcher.IsMatch(filePath, pattern);

            // Then
            result.ShouldBe(expected);
        }

        [Theory]
        [InlineData("temp/file.txt", "temp/**", true)]
        [InlineData("temp/nested/file.txt", "temp/**", true)]
        [InlineData("other/file.txt", "temp/**", false)]
        [InlineData("file.tmp", "*.tmp", true)]
        [InlineData("docs/generated/file.txt", "docs/generated/*", true)]
        [InlineData("docs/generated/nested/file.txt", "docs/generated/*", false)]
        [InlineData("docs/generated/nested/file.txt", "docs/generated/**", true)]
        public void Should_Match_Common_Patterns_Correctly(string filePath, string pattern, bool expected)
        {
            // When
            var result = FilePatternMatcher.IsMatch(filePath, pattern);

            // Then
            result.ShouldBe(expected);
        }

        [Fact]
        public void Should_Return_False_For_Null_Or_Empty_Input()
        {
            // When & Then
            FilePatternMatcher.IsMatch(null, "*.txt").ShouldBeFalse();
            FilePatternMatcher.IsMatch("", "*.txt").ShouldBeFalse();
            FilePatternMatcher.IsMatch("file.txt", null).ShouldBeFalse();
            FilePatternMatcher.IsMatch("file.txt", "").ShouldBeFalse();
        }

        [Fact]
        public void Should_Match_Against_Multiple_Patterns()
        {
            // Given
            var patterns = new[] { "*.txt", "*.md", "temp/**" };

            // When & Then
            FilePatternMatcher.IsMatch("file.txt", patterns).ShouldBeTrue();
            FilePatternMatcher.IsMatch("readme.md", patterns).ShouldBeTrue();
            FilePatternMatcher.IsMatch("temp/file.log", patterns).ShouldBeTrue();
            FilePatternMatcher.IsMatch("other.doc", patterns).ShouldBeFalse();
        }

        [Theory]
        [InlineData("file[1].txt", "file[1].txt", true)] // Special regex chars should be escaped
        [InlineData("file(1).txt", "file(1).txt", true)]
        [InlineData("file.txt", "file\\.txt", true)]
        [InlineData("file^test.txt", "file^test.txt", true)]
        public void Should_Escape_Regex_Special_Characters(string filePath, string pattern, bool expected)
        {
            // When
            var result = FilePatternMatcher.IsMatch(filePath, pattern);

            // Then
            result.ShouldBe(expected);
        }
    }
}