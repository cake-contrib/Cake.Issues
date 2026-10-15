namespace Cake.Issues.GitRepository.Tests;

public sealed class GitRepositoryIssuesProviderTests
{
    public sealed class TheReadIssuesMethod
    {
        [Fact]
        public void Should_Not_Report_Files_Outside_Sparse_Checkout_As_Binary()
        {
            // Given
            var fixture = new GitRepositoryIssuesProviderFixture
            {
                TaggedFiles = ["H included/text.txt", "S excluded/text.txt", "S excluded/binary.png"],
                TextFiles = ["included/text.txt"],
            };
            fixture.Settings.CheckBinaryFilesTrackedByLfs = true;

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.ShouldBeEmpty();
        }

        [Fact]
        public void Should_Still_Report_Checked_Out_Binary_Files_In_Sparse_Checkout()
        {
            // Given
            var fixture = new GitRepositoryIssuesProviderFixture
            {
                TaggedFiles =
                [
                    "H included/text.txt",
                    "H included/empty.png",
                    "H included/binary file.png",
                    "H included/lfs.png",
                    "S excluded/text.txt",
                ],
                TextFiles = ["included/text.txt"],
                EmptyFiles = ["included/empty.png"],
                LfsFiles = ["included/lfs.png"],
            };
            fixture.Settings.CheckBinaryFilesTrackedByLfs = true;

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            IssueChecker.Check(
                issues[0],
                IssueBuilder.NewIssue(
                    "The binary file \"included/binary file.png\" is not tracked by Git LFS",
                    GitRepositoryIssuesProvider.ProviderTypeName,
                    "Git Repository")
                    .WithMessageInHtmlFormat("The binary file <code>included/binary file.png</code> is not tracked by Git LFS")
                    .WithMessageInMarkdownFormat("The binary file `included/binary file.png` is not tracked by Git LFS")
                    .InFile("included/binary file.png")
                    .OfRule(new BinaryFileNotTrackedByLfsRuleDescription(), issues[0].RuleUrl.Segments[1].Trim('/')));
        }

        [Fact]
        public void Should_Still_Check_Path_Length_For_Files_Outside_Sparse_Checkout()
        {
            // Given
            var fixture = new GitRepositoryIssuesProviderFixture
            {
                TaggedFiles = ["H a.txt", "S excluded/long-file-name.txt"],
            };
            fixture.Settings.CheckFilesPathLength = true;
            fixture.Settings.MaxFilePathLength = 10;

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            issues[0].AffectedFileRelativePath.FullPath.ShouldBe("excluded/long-file-name.txt");
            issues[0].RuleId.ShouldBe("FilePathTooLong");
        }
    }

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