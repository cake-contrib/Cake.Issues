namespace Cake.Issues.Sarif.Tests
{
    using System;
    using Cake.Core.IO;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class SarifIssuesSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_LogFilePath_Is_Null()
            {
                // Given
                FilePath logFilePath = null;

                // When
                var result = Record.Exception(() => new SarifIssuesSettings(logFilePath));

                // Then
                result.IsArgumentNullException("logFilePath");
            }

            [Fact]
            public void Should_Throw_If_LogFileContent_Is_Null()
            {
                // Given
                byte[] logFileContent = null;

                // When
                var result = Record.Exception(() => new SarifIssuesSettings(logFileContent));

                // Then
                result.IsArgumentNullException("logFileContent");
            }

            [Fact]
            public void Should_Set_LogFileContent()
            {
                // Given
                var logFileContent = "Foo".ToByteArray();

                // When
                var settings = new SarifIssuesSettings(logFileContent);

                // Then
                settings.LogFileContent.ShouldBe(logFileContent);
            }

            [Fact]
            public void Should_Set_LogFileContent_If_Empty()
            {
                // Given
                byte[] logFileContent = Array.Empty<byte>();

                // When
                var settings = new SarifIssuesSettings(logFileContent);

                // Then
                settings.LogFileContent.ShouldBe(logFileContent);
            }

            [Fact]
            public void Should_Set_LogFileContent_From_LogFilePath()
            {
                // Given
                using (var tempFile = new ResourceTempFile("Cake.Issues.Sarif.Tests.Testfiles.minimal.sarif"))
                {
                    // When
                    var settings = new SarifIssuesSettings(tempFile.FileName);

                    // Then
                    settings.LogFileContent.ShouldBe(tempFile.Content);
                }
            }
        }
    }
}
