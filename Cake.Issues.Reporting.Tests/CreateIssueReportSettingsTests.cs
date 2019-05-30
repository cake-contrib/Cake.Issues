namespace Cake.Issues.Reporting.Tests
{
    using Cake.Core.IO;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class CreateIssueReportSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_RepositoryRoot_Is_Null()
            {
                // Given
                DirectoryPath repoRoot = null;
                FilePath outputFile = @"C:\report.html";

                // When
                var result = Record.Exception(() => new CreateIssueReportSettings(repoRoot, outputFile));

                // Then
                result.IsArgumentNullException("repositoryRoot");
            }

            [Fact]
            public void Should_Throw_If_OutputFilePath_Is_Null()
            {
                // Given
                DirectoryPath repoRoot = @"c:\repo";
                FilePath outputFile = null;

                // When
                var result = Record.Exception(() => new CreateIssueReportSettings(repoRoot, outputFile));

                // Then
                result.IsArgumentNullException("outputFilePath");
            }

            [Fact]
            public void Should_Set_RepositoryRoot_If_Not_Null()
            {
                // Given
                DirectoryPath repoRoot = @"c:\repo";
                FilePath outputFile = @"C:\report.html";

                // When
                var settings = new CreateIssueReportSettings(repoRoot, outputFile);

                // Then
                settings.RepositoryRoot.ShouldBe(repoRoot);
            }

            [Fact]
            public void Should_Set_OutputFilePath_If_Not_Null()
            {
                // Given
                DirectoryPath repoRoot = @"c:\repo";
                FilePath outputFile = @"C:\report.html";

                // When
                var settings = new CreateIssueReportSettings(repoRoot, outputFile);

                // Then
                settings.OutputFilePath.ShouldBe(outputFile);
            }
        }
    }
}
