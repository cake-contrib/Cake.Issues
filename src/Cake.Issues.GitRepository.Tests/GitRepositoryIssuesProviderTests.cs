namespace Cake.Issues.GitRepository.Tests;

using System.Reflection;

public sealed class GitRepositoryIssuesProviderTests
{
    public sealed class TheIsFileExcludedMethod
    {
        [Theory]
        [InlineData("file.tmp", new[] { "*.tmp" }, "BinaryFilesLfs", true)]
        [InlineData("file.txt", new[] { "*.tmp" }, "BinaryFilesLfs", false)]
        [InlineData("temp/file.txt", new[] { "temp/**" }, "FilePathLength", true)]
        [InlineData("other/file.txt", new[] { "temp/**" }, "FilePathLength", false)]
        public void Should_Exclude_Files_Based_On_Global_Patterns(string filePath, string[] patterns, string checkType, bool expectedExcluded)
        {
            // Given
            var settings = new GitRepositoryIssuesSettings();
            foreach (var pattern in patterns)
            {
                settings.FilesToExclude.Add(pattern);
            }

            var provider = this.CreateProvider(settings);

            // Use reflection to access the private IsFileExcluded method
            var isFileExcludedMethod = typeof(GitRepositoryIssuesProvider).GetMethod("IsFileExcluded",
                BindingFlags.NonPublic | BindingFlags.Instance);
            var checkTypeEnum = this.GetCheckTypeEnumValue(checkType);

            // When
            var result = (bool)isFileExcludedMethod.Invoke(provider, [filePath, checkTypeEnum]);

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

            var provider = this.CreateProvider(settings);

            // Use reflection to access the private IsFileExcluded method
            var isFileExcludedMethod = typeof(GitRepositoryIssuesProvider).GetMethod("IsFileExcluded",
                BindingFlags.NonPublic | BindingFlags.Instance);
            var checkTypeEnum = this.GetCheckTypeEnumValue("BinaryFilesLfs");

            // When
            var result = (bool)isFileExcludedMethod.Invoke(provider, [filePath, checkTypeEnum]);

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

            var provider = this.CreateProvider(settings);

            // Use reflection to access the private IsFileExcluded method
            var isFileExcludedMethod = typeof(GitRepositoryIssuesProvider).GetMethod("IsFileExcluded",
                BindingFlags.NonPublic | BindingFlags.Instance);
            var checkTypeEnum = this.GetCheckTypeEnumValue("FilePathLength");

            // When
            var result = (bool)isFileExcludedMethod.Invoke(provider, [filePath, checkTypeEnum]);

            // Then
            result.ShouldBe(expectedExcluded);
        }

        [Fact]
        public void Should_Prioritize_Global_Exclusions_Over_Specific_Exclusions()
        {
            // Given
            var settings = new GitRepositoryIssuesSettings();
            settings.FilesToExclude.Add("*.tmp");
            // Even if it's not in the specific exclusion list, global should take precedence
            var provider = this.CreateProvider(settings);

            // Use reflection to access the private IsFileExcluded method
            var isFileExcludedMethod = typeof(GitRepositoryIssuesProvider).GetMethod("IsFileExcluded",
                BindingFlags.NonPublic | BindingFlags.Instance);
            var checkTypeEnum = this.GetCheckTypeEnumValue("BinaryFilesLfs");

            // When
            var result = (bool)isFileExcludedMethod.Invoke(provider, ["file.tmp", checkTypeEnum]);

            // Then
            result.ShouldBeTrue();
        }

        private GitRepositoryIssuesProvider CreateProvider(GitRepositoryIssuesSettings settings)
        {
            var log = new FakeLog();
            var environment = new FakeEnvironment(Core.PlatformFamily.Linux);
            var fileSystem = new FakeFileSystem(environment);
            //var processRunner = new FakeProcessRunner();
            //var toolLocator = new FakeToolLocator();

            return new GitRepositoryIssuesProvider(log, fileSystem, environment, null, null, settings);
        }

        private object GetCheckTypeEnumValue(string checkTypeName)
        {
            var checkTypeType = typeof(GitRepositoryIssuesProvider).GetNestedType("CheckType", BindingFlags.NonPublic);
            return Enum.Parse(checkTypeType, checkTypeName);
        }
    }
}