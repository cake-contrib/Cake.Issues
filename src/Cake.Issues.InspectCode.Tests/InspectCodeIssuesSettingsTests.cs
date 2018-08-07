namespace Cake.Issues.InspectCode.Tests
{
    using System;
    using Cake.Core.IO;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class InspectCodeIssuesSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_LogFilePath_Is_Null()
            {
                // Given
                FilePath logFilePath = null;

                // When
                var result = Record.Exception(() => new InspectCodeIssuesSettings(logFilePath));

                // Then
                result.IsArgumentNullException("logFilePath");
            }

            [Fact]
            public void Should_Throw_If_LogFileContent_Is_Null()
            {
                // Given
                byte[] logFileContent = null;

                // When
                var result = Record.Exception(() => new InspectCodeIssuesSettings(logFileContent));

                // Then
                result.IsArgumentNullException("logFileContent");
            }

            [Fact]
            public void Should_Throw_If_LogFileContent_Is_Empty()
            {
                // Given
                byte[] logFileContent = Array.Empty<byte>();

                // When
                var result = Record.Exception(() => new InspectCodeIssuesSettings(logFileContent));

                // Then
                result.IsArgumentException("logFileContent");
            }

            [Fact]
            public void Should_Set_LogContent()
            {
                // Given
                var logFileContent = "Foo".ToByteArray();

                // When
                var settings = new InspectCodeIssuesSettings(logFileContent);

                // Then
                settings.LogFileContent.ShouldBe(logFileContent);
            }

            [Fact]
            public void Should_Set_LogContent_From_LogFilePath()
            {
                // Given
                using (var tempFile = new ResourceTempFile("Cake.Issues.InspectCode.Tests.Testfiles.inspectcode.xml"))
                {
                    // When
                    var settings = new InspectCodeIssuesSettings(tempFile.FileName);

                    // Then
                    settings.LogFileContent.ShouldBe(tempFile.Content);
                }
            }
        }
    }
}
