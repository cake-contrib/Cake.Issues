namespace Cake.Issues.Tests
{
    using System;
    using System.Collections.Generic;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class FileLinkSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Builder_Is_Null()
            {
                // Given
                Func<IIssue, IDictionary<string, string>, Uri> builder = null;

                // When
                var result = Record.Exception(() => new FileLinkSettings(builder));

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
                Uri repositoryUrl = null;

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
                Uri repositoryUrl = null;

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
                IIssue issue = null;

                // When
                var result =
                    Record.Exception(() =>
                        FileLinkSettings
                            .ForGitHub(new Uri("https://github.com"))
                            .Branch("master")
                            .GetFileLink(issue));

                // Then
                result.IsArgumentNullException("issue");
            }
        }
    }
}
