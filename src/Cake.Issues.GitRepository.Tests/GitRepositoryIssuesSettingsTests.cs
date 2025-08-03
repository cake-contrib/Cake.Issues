namespace Cake.Issues.GitRepository.Tests;

public sealed class GitRepositoryIssuesSettingsTests
{
    public sealed class TheConstructor
    {
        [Fact]
        public void Should_Initialize_Exclusion_Lists()
        {
            // When
            var settings = new GitRepositoryIssuesSettings();

            // Then
            settings.FilesToExclude
                .ShouldNotBeNull()
                .ShouldBeEmpty();
            settings.FilesToExcludeFromBinaryFilesLfsCheck
                .ShouldNotBeNull()
                .ShouldBeEmpty();
            settings.FilesToExcludeFromFilePathLengthCheck
                .ShouldNotBeNull()
                .ShouldBeEmpty();
        }

        [Fact]
        public void Should_Allow_Adding_Exclusion_Patterns()
        {
            // Given
            var settings = new GitRepositoryIssuesSettings();

            // When
            settings.FilesToExclude.Add("*.tmp");
            settings.FilesToExclude.Add("temp/**");
            settings.FilesToExcludeFromBinaryFilesLfsCheck.Add("*.dll");
            settings.FilesToExcludeFromFilePathLengthCheck.Add("very/long/path/**");

            // Then
            settings.FilesToExclude.Count.ShouldBe(2);
            settings.FilesToExclude.ShouldContain("*.tmp");
            settings.FilesToExclude.ShouldContain("temp/**");

            settings.FilesToExcludeFromBinaryFilesLfsCheck.Count.ShouldBe(1);
            settings.FilesToExcludeFromBinaryFilesLfsCheck.ShouldContain("*.dll");

            settings.FilesToExcludeFromFilePathLengthCheck.Count.ShouldBe(1);
            settings.FilesToExcludeFromFilePathLengthCheck.ShouldContain("very/long/path/**");
        }
    }
}