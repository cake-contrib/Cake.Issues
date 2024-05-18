namespace Cake.Issues.Tests
{
    using Cake.Core.IO;

    public sealed class RepositorySettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_RepositoryRoot_Is_Null()
            {
                // Given
                const DirectoryPath repoRoot = null;

                // When
                var result = Record.Exception(() => new RepositorySettings(repoRoot));

                // Then
                result.IsArgumentNullException("repositoryRoot");
            }

            [Fact]
            public void Should_Set_RepositoryRoot_If_Not_Null()
            {
                // Given
                DirectoryPath repoRoot = @"c:\repo";

                // When
                var settings = new RepositorySettings(repoRoot);

                // Then
                settings.RepositoryRoot.ShouldBe(repoRoot);
            }
        }
    }
}
