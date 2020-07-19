namespace Cake.Issues.Tests.Testing
{
    using System.Linq;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class BaseConfigurableIssueProviderFixtureTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_FileResourceName_Is_Null()
            {
                // Given
                string fileResourceName = null;

                // When
                var result = Record.Exception(() => new FakeConfigurableIssueProviderFixture(fileResourceName));

                // Then
                result.IsArgumentNullException("fileResourceName");
            }

            [Fact]
            public void Should_Throw_If_FileResourceName_Is_Empty()
            {
                // Given
                var fileResourceName = string.Empty;

                // When
                var result = Record.Exception(() => new FakeConfigurableIssueProviderFixture(fileResourceName));

                // Then
                result.IsArgumentOutOfRangeException("fileResourceName");
            }

            [Fact]
            public void Should_Throw_If_FileResourceName_Is_WhiteSpace()
            {
                // Given
                var fileResourceName = " ";

                // When
                var result = Record.Exception(() => new FakeConfigurableIssueProviderFixture(fileResourceName));

                // Then
                result.IsArgumentOutOfRangeException("fileResourceName");
            }

            [Fact]
            public void Should_Throw_If_File_Resource_Does_Not_Exist()
            {
                // Given
                var fileResourceName = "foo";

                // When
                var result = Record.Exception(() => new FakeConfigurableIssueProviderFixture(fileResourceName));

                // Then
                result.IsArgumentException("fileResourceName");
            }

            [Fact]
            public void Should_Set_Log()
            {
                // Given

                // When
                var result = new FakeConfigurableIssueProviderFixture("Build.log");

                // Then
                result.Log.ShouldNotBeNull();
            }

            [Fact]
            public void Should_Set_RepositorySettings()
            {
                // Given

                // When
                var result = new FakeConfigurableIssueProviderFixture("Build.log");

                // Then
                result.ReadIssuesSettings.ShouldNotBeNull();
            }

            [Fact]
            public void Should_Set_LogFileContent()
            {
                // Given

                // When
                var result = new FakeConfigurableIssueProviderFixture("Build.log");

                // Then
                result.LogFileContent.ShouldNotBeNull();
                result.LogFileContent.ShouldNotBeEmpty();
            }

            [Fact]
            public void Should_Set_LogFileContent_For_Empty_File()
            {
                // Given

                // When
                var result = new FakeConfigurableIssueProviderFixture("Empty.log");

                // Then
                result.LogFileContent.ShouldNotBeNull();
                result.LogFileContent.ShouldBeEmpty();
            }
        }

        public sealed class TheReadIssuesMethod
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                var fixture = new FakeConfigurableIssueProviderFixture("Build.log")
                {
                    Log = null,
                };

                // When
                var result = Record.Exception(() => fixture.ReadIssues());

                // Then
                result.IsInvalidOperationException("No log instance set.");
            }

            [Fact]
            public void Should_Throw_If_RepositorySettings_Are_Null()
            {
                // Given
                var fixture = new FakeConfigurableIssueProviderFixture("Build.log")
                {
                    ReadIssuesSettings = null,
                };

                // When
                var result = Record.Exception(() => fixture.ReadIssues());

                // Then
                result.IsInvalidOperationException("No settings for reading issues set.");
            }

            [Fact]
            public void Should_Return_Empty_List_For_Empty_File()
            {
                // Given
                var fixture =
                    new FakeConfigurableIssueProviderFixture("Empty.log");

                // When
                var result = fixture.ReadIssues();

                // Then
                result.ShouldBeEmpty();
            }
        }
    }
}
