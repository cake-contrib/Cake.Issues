namespace Cake.Issues.Build.Tests;

using Cake.Core.IO;
using Shouldly;
using Xunit;

public sealed class ReportIssuesToBuildServerSettingsTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_RepositoryRoot_Is_Null()
        {
            // Given
            DirectoryPath repositoryRoot = null;

            // When
            var result = Record.Exception(() => new ReportIssuesToBuildServerSettings(repositoryRoot));

            // Then
            result.IsArgumentNullException("repositoryRoot");
        }

        [Fact]
        public void Should_Set_Repository_Root()
        {
            // Given
            DirectoryPath repositoryRoot = @"c:\Source\Cake.Issues";

            // When
            var result = new ReportIssuesToBuildServerSettings(repositoryRoot);

            // Then
            result.RepositoryRoot.ShouldBe(repositoryRoot);
        }

        [Fact]
        public void Should_Set_Default_MaxIssuesToPostForEachIssueProvider()
        {
            // Given
            var repositoryRoot = @"c:\Source\Cake.Issues";

            // When
            var result = new ReportIssuesToBuildServerSettings(repositoryRoot);

            // Then
            result.MaxIssuesToPostForEachIssueProvider.ShouldBe(100);
        }

        [Fact]
        public void Should_Initialize_Empty_Collections()
        {
            // Given
            var repositoryRoot = @"c:\Source\Cake.Issues";

            // When
            var result = new ReportIssuesToBuildServerSettings(repositoryRoot);

            // Then
            result.ProviderIssueLimits.ShouldBeEmpty();
            result.IssueFilters.ShouldBeEmpty();
        }
    }
}